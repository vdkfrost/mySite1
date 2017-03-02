<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show_bio.aspx.cs" Inherits="Twitch_core.Views.User.Show_info" %>

<%@ Register Src="~/UserControls/page-footer.ascx" TagName="footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/page-top-fixed-panel.ascx" TagName="fixedPanel" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="~/Content/styles.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/bio.css" rel="stylesheet" type="text/css" />
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

        <asp:Panel ID="_resultBox" runat="server" CssClass="usual-block page-content">
            <asp:Panel ID="_userNotFoundPanel" CssClass="error404" runat="server" Visible="false">
                <span class="header">ОШИБКА 404</span>
                <span>Страницы не существует или была удалена</span>
            </asp:Panel>

            <asp:Panel ID="_infoBlock" runat="server" CssClass="usual-block" Style="background-color: white;" Visible="false">
                <div class="left-part">
                    <div class="user-level-hood">
                        <asp:Label ID="_userLevel" runat="server" CssClass="value" />
                        <div class="progress-hood">
                            <asp:Panel ID="_userLevelProgressBack" runat="server" CssClass="back">
                                <div class="line"></div>
                            </asp:Panel>
                            <asp:Label ID="_userLevelProgress" runat="server" CssClass="progress"></asp:Label>
                        </div>
                    </div>
                    <asp:Image ID="_userImage" CssClass="user-image" runat="server" />
                    <asp:Panel ID="_userConnections" CssClass="user-connections" runat="server" Visible="false">
                        <asp:HyperLink ID="_userTwitchConnection" runat="server" Visible="false" style="background-color: #6b3e83; padding: 6px">
                            <img src="/images/logos/twitch/logo/icon-white.svg" />
                        </asp:HyperLink>
                        <asp:HyperLink ID="_userHitboxConnection" runat="server" Visible="false">
                            <img src=""/>
                        </asp:HyperLink>
                        <asp:HyperLink ID="_userVKConnection" runat="server" Visible="false">
                            <img src="/images/logos/vk/logo.svg" />
                        </asp:HyperLink>
                        <asp:HyperLink ID="_userYTConnection" runat="server" Visible="false">
                            <img src="/images/logos/youtube/logo.svg" />
                        </asp:HyperLink>
                        <asp:HyperLink ID="_userFBConnection" runat="server" Visible="false">
                            <img src="/images/logos/facebook/logo.svg" />
                        </asp:HyperLink>
                    </asp:Panel>
                    <asp:Label ID="_userName" CssClass="user-name" runat="server"></asp:Label>
                    <asp:Label ID="_userCreationDate" CssClass="user-creation-date" runat="server"></asp:Label>
                    <asp:Panel ID="_userProfileLines" CssClass="user-profile-lines" runat="server" Visible="false"></asp:Panel>
                </div>
                <div class="right-part">
                    <div class="bio-header-hood">
                        <asp:Label ID="_bioHeader" CssClass="bio-header" runat="server"></asp:Label>
                        <asp:HyperLink ID="_changeInfo" CssClass="change-info" runat="server" Visible="false">Изменить</asp:HyperLink>
                    </div>
                    <asp:Panel ID="_userBioLines" CssClass="lines" runat="server"></asp:Panel>
                    <asp:Panel ID="_userSocial" runat="server" CssClass="social-hood"></asp:Panel>
                </div>
            </asp:Panel>
        </asp:Panel>

        <uc:footer runat="server"></uc:footer>
    </form>
</body>
</html>
