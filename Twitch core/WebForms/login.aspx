<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Twitch_core.WebForms.login" Async="true" %>

<%@ Register Src="~/UserControls/page-footer.ascx" TagName="footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/page-header.ascx" TagName="header" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Вход в личный кабинет. Streamerspace</title>
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
        <uc:header ID="_topPage" runat="server" />

        <div class="usual-block" style="background-color: white; min-height: calc(100vh - 440px);">
            <div id="login-panel">
                <span>Имя пользователя / электронная почта</span>
                <asp:TextBox ID="_login" runat="server"></asp:TextBox>
                <span>Пароль</span>
                <asp:TextBox ID="_password" TextMode="Password" runat="server"></asp:TextBox>
                <div id="hint" style="height: 0px">
                    <span>Пользователь с такими данными не найден</span>
                </div>
                <input type="button" onclick="login(this)" class="login-button" value="Войти" />
                <a href="/remind-password" style="float: right">Забыли пароль?</a>
                <a href="/" style="float: left">Регистрация</a>
            </div>
        </div>

        <uc:footer runat="server" />
    </form>
</body>
</html>
