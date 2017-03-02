<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="signup.aspx.cs" Inherits="Twitch_core.WebForms.signup_beta" %>

<%@ Register Src="~/UserControls/page-footer.ascx" TagName="footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/page-header.ascx" TagName="header" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Регистрация на бета-тестирование. Streamerspace</title>
    <link href="../Content/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="../Scripts/actions.js" />
                <asp:ScriptReference Path="../Scripts/jquery-1.10.2.min.js" />
            </Scripts>
        </asp:ScriptManager>

        <uc:header ID="page_top" runat="server" />

        <div class="usual-block" style="background-color: white">
            <asp:UpdatePanel ID="_regUpd" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="registration" runat="server" Visible="true">
                        <div class="reg-info">
                            <span class="info-header">Имя пользователя</span>
                            <asp:TextBox ID="login_txt" runat="server" placeholder="Имя пользователя" CssClass="info-input" Style="background-image: url(/images/icons/user/user.svg)" oninput="checkLogin(this);"></asp:TextBox>
                            <asp:Label ID="_loginHint" runat="server" CssClass="hint" Text="- Длина логина должна быть не менее 3 и не более 15 символов<br>- Логин содержит только латинские буквы и цифры"></asp:Label>

                            <span class="info-header">Электронная почта</span>
                            <asp:TextBox ID="email_txt" TextMode="Email" runat="server" placeholder="Электронная почта" CssClass="info-input" Style="background-image: url(/images/icons/email/email.svg)" oninput="checkEmail(this);"></asp:TextBox>
                            <asp:Label ID="_emailHint" runat="server" CssClass="hint" Text="Введите действительную электронную почту"></asp:Label>

                            <span class="info-header">Пароль</span>
                            <asp:TextBox ID="_password" TextMode="Password" runat="server" placeholder="Пароль" CssClass="info-input" Style="background-image: url(/images/icons/password/password.svg)" oninput="checkPass();"></asp:TextBox>
                            <span class="info-header" style="margin-top: 10px">Повторите пароль</span>
                            <asp:TextBox ID="_passwordRepeat" TextMode="Password" runat="server" placeholder="Подтверждение пароля" CssClass="info-input" Style="background-image: url(/images/icons/password/password.svg)" oninput="checkPass();"></asp:TextBox>
                            <asp:Label ID="_passwordHint" runat="server" CssClass="hint" Text="- Длина пароля должна быть не менее 6 и не более 32 символов<br/>- Пароль должен содержать как минимум 1 большую и маленькую буквы и 1 цифру"></asp:Label>

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="sign_beta_btn" runat="server" OnClick="sign_beta_btn_Click" Text="Зарегистрироваться" Style="margin-top: 10px" CssClass="button signup" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="success" runat="server" Visible="false">
                        <asp:Label ID="success_text" runat="server" CssClass="engine success"></asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <uc:footer runat="server"></uc:footer>
    </form>
</body>
</html>
