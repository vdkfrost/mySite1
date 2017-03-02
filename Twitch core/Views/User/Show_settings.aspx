<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show_settings.aspx.cs" Inherits="Twitch_core.Views.User.Show_edit" %>

<%@ Register Src="~/UserControls/page-footer.ascx" TagName="footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/page-top-fixed-panel.ascx" TagName="fixedPanel" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="~/Content/styles.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/edit.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="~/Scripts/actions.js" />
                <asp:ScriptReference Path="~/Scripts/jquery-1.10.2.min.js" />
            </Scripts>
        </asp:ScriptManager>

        <uc:fixedPanel runat="server"></uc:fixedPanel>

        <div id="_resultBox" runat="server" class="usual-block page-content">
            <asp:Panel ID="_userNotFoundPanel" CssClass="error404" runat="server" Visible="false">
                <asp:Label ID="_errorNum" CssClass="header" runat="server"></asp:Label>
                <asp:Label ID="_notFoundText" runat="server" CssClass="text"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="_pageContent" runat="server" Visible="false">

                <div class="canvas">
                    <div class="options">
                        <asp:HyperLink ID="_profile" NavigateUrl="/profile" runat="server">Профиль</asp:HyperLink>
                        <asp:HyperLink ID="_widgets" NavigateUrl="/widgets" runat="server">Виджеты</asp:HyperLink>
                        <asp:HyperLink ID="_security" NavigateUrl="/security" runat="server">Безопасность</asp:HyperLink>
                        <asp:HyperLink ID="_connections" NavigateUrl="/connections" runat="server">Подключения</asp:HyperLink>
                    </div>
                    <asp:Table ID="_profile_content" CssClass="option-content" runat="server" Visible="false">
                        <asp:TableRow>
                            <asp:TableCell>
                                    <span>Имя пользователя</span>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="_username" runat="server" ReadOnly="true"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <span>Отображаемое имя</span>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="_display_name" runat="server" placeholder="Отображаемое имя"></asp:TextBox>
                                <span class="option-hint">Отображаемое имя может отличаться от имени пользователя лишь регистром каждой из букв. Изменять текущие символы на другие невозможно.</span>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <span>Изображение</span>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Image ID="_avatar" CssClass="user-image" runat="server" />
                                <asp:FileUpload ID="_avatarUpload" CssClass="avatar-upload" runat="server" AllowMultiple="false" Style="display: none" />
                                <input type="button" class="upload-btn button" value="Загрузить" onmouseup="document.getElementById('_avatarUpload').click()" />
                                <asp:CheckBox ID="_delete_image" runat="server" Text="Удалить изображение" />
                                <span class="option-hint">Формат: JPG, GIF или PNG. Максимальный размер: 1Мб.</span>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <span>Электронная почта</span>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="_email" runat="server" Style="display: inherit" placeholder="Электронная почта"></asp:TextBox>
                                <asp:Button ID="_change_email" CssClass="upload-btn button" Style="display: inherit; margin-left: 10px" runat="server" Text="Изменить" />
                                <span class="option-hint">Введите действительный адрес электронной почты, после чего нажмите "Изменить". Новый адрес будет применен сразу после того, как Вы подтвердите его на своей электронной почте.</span>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <span>Пароль</span>
                            </asp:TableCell>
                            <asp:TableCell>
                                <span class="option-info">Старый пароль</span>
                                <asp:TextBox ID="_old_pass" runat="server" TextMode="Password" MaxLength="32" placeholder="Старый пароль"></asp:TextBox>
                                <span class="option-info">Новый пароль</span>
                                <asp:TextBox ID="_new_pass" runat="server" TextMode="Password" MaxLength="32" placeholder="Новый пароль"></asp:TextBox>
                                <span class="option-info">Повторите пароль</span>
                                <asp:TextBox ID="_pass_confirm" runat="server" TextMode="Password" MaxLength="32" placeholder="Подтверждение"></asp:TextBox>
                                <span class="option-hint">Длина пароля должна быть не менее 6 и не более 32 символов. Он должен содержать как минимум 1 большую и маленькую буквы и 1 цифру."</span>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                    <span>О себе</span>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="_about" TextMode="MultiLine" runat="server" placeholder="Немного о себе" MaxLength="300"></asp:TextBox>
                                <span class="option-hint">Тут Вы можете рассказать немного о себе. Максимальная длина текста - 300 символов.</span>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="_widgets_content" CssClass="option-content" runat="server" Visible="false">
                        <asp:TableRow>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="_security_content" CssClass="option-content" runat="server" Visible="false"></asp:Table>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Table ID="_connections_content" CssClass="connections" runat="server" Visible="false">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <div class="connection twitch">
                                            <span class="name">Twitch.tv</span>
                                            <div class="image-hood">
                                                <img src="/images/logos/twitch/logo/icon-white.svg" />
                                            </div>
                                            <div class="info">
                                                <asp:Panel ID="_connectedTwitch" runat="server" Visible="false">
                                                    <span class="option-info">Подключен:</span>
                                                    <asp:HyperLink ID="_userTwitchUsername" Target="_blank" runat="server" CssClass="user-name"></asp:HyperLink>
                                                </asp:Panel>
                                                <asp:Button ID="_actionTwitch" runat="server" Style="margin-top: 15px" CssClass="button" Text="Подключить" />
                                            </div>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="connection vkontakte">
                                            <span class="name">ВКонтакте</span>
                                            <div class="image-hood">
                                                <img src="/images/logos/vk/logo.svg" />
                                            </div>
                                            <div class="info">
                                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                                    <span class="option-info">Подключен:</span>
                                                    <asp:HyperLink ID="_userVkontakteName" Target="_blank" runat="server" CssClass="user-name"></asp:HyperLink>
                                                </asp:Panel>
                                                <asp:Button ID="_actionVkontakte" runat="server" Style="margin-top: 15px" CssClass="button" Text="Подключить" />
                                            </div>
                                        </div>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <div class="connection youtube">
                                            <span class="name">YouTube</span>
                                            <div class="image-hood">
                                                <img src="/images/logos/youtube/logo.svg" />
                                            </div>
                                            <div class="info">
                                                <asp:Panel ID="Panel2" runat="server" Visible="false">
                                                    <span class="option-info">Подключен:</span>
                                                    <asp:HyperLink ID="HyperLink2" Target="_blank" runat="server" CssClass="user-name"></asp:HyperLink>
                                                </asp:Panel>
                                                <asp:Button ID="Button2" runat="server" Style="margin-top: 15px" CssClass="button" Text="Подключить" />
                                            </div>
                                        </div>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="_save_changes" CssClass="button" runat="server" Style="margin-top: 30px" Text="Сохранить изменения" />
                </div>

            </asp:Panel>
        </div>

        <uc:footer runat="server"></uc:footer>
    </form>
</body>
</html>
