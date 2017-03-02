<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Show_space.aspx.cs" Inherits="Twitch_core.Views.User.Show" EnableViewState="true" %>

<%@ Register Src="~/UserControls/page-footer.ascx" TagName="footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/page-top-fixed-panel.ascx" TagName="fixedPanel" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../Content/styles.css" rel="stylesheet" type="text/css" />
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
                <span class="header">ОШИБКА 404</span>
                <asp:Label ID="_notFoundText" runat="server" CssClass="text"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="_userContent" runat="server" Visible="false">
                <asp:Panel ID="_userTwitch" runat="server" Visible="false">
                    <asp:Label ID="_twitchHeader" runat="server" Visible="false"></asp:Label>
                    <asp:Table ID="_twitchTable" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <div class="social">
                                    <asp:HyperLink ID="_twicthUserLink" runat="server" Target="_blank">
                                        <img src="/images/logos/twitch/logo/icon-white.svg" />
                                    </asp:HyperLink>
                                    <asp:HyperLink ID="_twitchUserSSLink" runat="server" Target="_blank">
                                        <img src="/images/logos/streamerspace/logo.PNG" />
                                    </asp:HyperLink>
                                </div>
                                <asp:Image ID="_twitchUserImage" runat="server" CssClass="user-image" />
                                <div style="float: left; margin-left: 10px; padding-top: 8px;">
                                    <asp:HyperLink ID="_twitchUserName" runat="server" CssClass="user-name" Target="_blank"></asp:HyperLink>
                                    <asp:Label ID="_twitchUserStreamStatus" runat="server" CssClass="stream-status">
                                        <span>Транслирует 
                                            <asp:HyperLink ID="_twitchUserStreamStatusGame" runat="server" Target="_blank"></asp:HyperLink>
                                        </span>
                                    </asp:Label>
                                </div>
                            </asp:TableCell>
                            <asp:TableCell CssClass="donation">
                                <asp:HyperLink ID="_userDonationLink" runat="server">
                                    <span>Поддержать</span>
                                </asp:HyperLink>
                            </asp:TableCell>
                            <asp:TableCell CssClass="viewers">
                                <asp:Label ID="_twitchCurrentViewers" runat="server"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="views">
                                <asp:Label ID="_twitchViews" runat="server"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4" CssClass="stream">
                                <iframe runat="server" id="_twitchStreamWindow" frameborder="0" allowfullscreen="true" scrolling="no" style="width: 100%; height: 100%"></iframe>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4" CssClass="chat">
                                <iframe runat="server" id="_twitchChat" frameborder="0" scrolling="no" style="width: 100%; height: 100%; margin-top: 3px"></iframe>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
            </asp:Panel>
        </div>

        <uc:footer runat="server"></uc:footer>
    </form>
</body>
</html>
