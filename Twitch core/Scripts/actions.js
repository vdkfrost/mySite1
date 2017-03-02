function logOut(redirUrl)
{
    $.ajax({
        type: "POST",
        url: "/WebForms/engine.aspx/LogOut",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (redirUrl != null)
                window.location.href = redirUrl;
        },
        error: function (result) {
            alert('Произошла неизвестная ошибка. Попробуйте позже.');
        }
    });
}

function delayedRedir(target, delay) {
    setTimeout(function redir(target) { window.location.href = target }, delay);
}

function checkEmail(textbox) {
    var hint = document.getElementById('_emailHint');
    var regex = /^([a-z0-9]+(?:[._-][a-z0-9]+)*)@([a-z0-9]+(?:[.-][a-z0-9]+)*\.[a-z]{2,})$/gi;

    if (textbox.value.match(regex) !== null) {
        textbox.style.backgroundImage = 'url(/images/svg/loading.svg)';
        $.ajax({
            type: "POST",
            url: "../WebForms/signup.aspx/checkParam",
            data: "{ param: \"" + textbox.value + "\", paramName: \"email\", regex: \"^([a-z0-9]+(?:[._-][a-z0-9]+)*)@([a-z0-9]+(?:[.-][a-z0-9]+)*\.[a-z]{2,})$\"}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                switch (result.d) {
                    case 'update needed':
                    case 'ok':
                        textbox.style.backgroundImage = 'url(/images/icons/email/email-ok.svg)';
                        hint.innerText = 'Эта почта свободна, ее можно использовать';
                        hint.style.backgroundColor = 'rgba(0, 255, 0, 0.4)';
                        hint.className = 'hint success';
                        break;
                    case 'incorrect':
                        textbox.style.backgroundImage = 'url(/images/icons/email/email-error.svg)';
                        hint.innerText = 'Введите действительную электронную почту';
                        hint.style.backgroundColor = 'rgba(255, 0, 0, 0.4)';
                        hint.className = 'hint';
                        break;
                    case 'taken':
                        textbox.style.backgroundImage = 'url(/images/icons/email/email-error.svg)';
                        hint.innerText = 'Эта почта уже занята, введите другую';
                        hint.style.backgroundColor = 'rgba(255, 0, 0, 0.4)';
                        hint.className = 'hint';
                        break;
                }
            }
        });
    }
    else {
        textbox.style.backgroundImage = 'url(/images/icons/email/email.svg)';
        hint.innerText = 'Введите действительную электронную почту';
        hint.style.backgroundColor = '';
    }
}

function checkLogin(textbox) {
    var hint = document.getElementById('_loginHint');
    var regex = /^(?=.*[a-zA-Z]{1,})(?=.*[\d]{0,})[a-zA-Z0-9]{3,15}$/gi;

    if (textbox.value.match(regex) !== null) {
        textbox.style.backgroundImage = 'url(/images/svg/loading.svg)';
        $.ajax({
            type: "POST",
            url: "../WebForms/signup.aspx/checkParam",
            data: "{ param: \"" + textbox.value + "\", paramName: \"username\", regex: \"^(?=.*[a-zA-Z]{1,})(?=.*[\d]{0,})[a-zA-Z0-9]{3,15}$\"}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                switch (result.d) {
                    case 'ok':
                        textbox.style.backgroundImage = 'url(/images/icons/user/user-ok.svg)';
                        hint.className = 'hint success';
                        hint.innerText = 'Этот логин свободен, его можно занять';
                        break;
                    case 'taken':
                        textbox.style.backgroundImage = 'url(/images/icons/user/user-error.svg)';
                        hint.innerText = 'Этот логин уже занят, попробуйте другой';
                        hint.style.backgroundColor = 'rgba(255, 0, 0, 0.4)';
                        hint.className = 'hint';
                        break;
                }
            }
        });
    }
    else {
        textbox.style.backgroundImage = 'url(/images/icons/user/user.svg)';
        hint.innerHTML = '- Длина логина должна быть не менее 3 и не более 15 символов<br/>- Логин содержит только латинские буквы и цифры';
        hint.style.backgroundColor = '';
        hint.className = 'hint';
    }
}

function login(btn) {
    btn.style.backgroundColor = "transparent";
    btn.style.backgroundImage = "url(../images/svg/loading.svg)";
    btn.value = "";
    btn.setAttribute("disabled", "disabled");

    var hint = document.getElementById('hint');

    $.ajax({
        type: "POST",
        url: "../WebForms/login.aspx/logIn",
        data: "{ username: \"" + document.getElementById("_login").value + "\", password : \"" + document.getElementById("_password").value + "\"}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            switch (result.d[0]) {
                case 'success':
                    window.location.href = "/users/" + result.d[1] + "/bio";
                    hint.style.height = '0px';
                    break;
                case 'error':
                    btn.style.backgroundColor = "darkorange";
                    btn.style.backgroundImage = "";
                    btn.value = "Войти";
                    hint.style.height = '37px';
                    btn.removeAttribute("disabled");
                    break;
            }
        },
        error: function (result) {
            alert('Произошла неизвестная ошибка. Повторите позже!');
        }
    });
}

function checkPass() {
    var pass = document.getElementById('_password');
    var passRepeat = document.getElementById('_passwordRepeat');
    var message = '';
    if (pass.value != passRepeat.value) {
        message = '- Пароли не совпадают<br/>';
        document.getElementById('_passwordHint').className = 'hint';
    }

    var regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,32}$/gi;
    if (pass.value.match(regex) !== null) {
        if (message == '') {
            document.getElementById('_passwordHint').className = 'hint success';
            message = 'Этот пароль подходит';
        }
    }
    else {
        document.getElementById('_passwordHint').className = 'hint';
        message = message + '- Длина пароля должна быть не менее 6 и не более 32 символов<br/>- Пароль должен содержать как минимум 1 большую и маленькую буквы и 1 цифру';
    }
    document.getElementById('_passwordHint').innerHTML = message;
}

function spaceRefresh(target) {
    var refresh = setInterval(function () {
        $.getJSON("/users/" + target + "/RefreshDashboard/", function (json) {
            var status = document.getElementById('_twitchUserStreamStatusGame');
            var viewers = document.getElementById('_twitchCurrentViewers');
            var views = document.getElementById('_twitchCurrentViews');

            if (json[0] !== "")
                status.href = "https://twitch.tv/directory/game/" + json[0];
            if (json[1] !== "") {
                viewers.innerHTML = json[1];
                viewers.style.visibility = "visible";
            }
            else
                viewers.style.visibility = "hidden";
            if (json[2] !== "")
                views.innerHTML = json[2];
        })
    }, 120000);
}