<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="page-top-fixed-panel.ascx.cs" Inherits="Twitch_core.UserControls.page_top_fixed_panel" %>

<div class="tfp usual-block">
    <a href="/" class="logo">
        <asp:Image ID="_logoImage" runat="server" ImageUrl="~/images/logos/streamerspace/text.PNG" />
    </a>
    <asp:Panel ID="_userPanel" runat="server" Visible="false">
        <div class="user-hood">
            <asp:HyperLink ID="_userLink" runat="server" CssClass="useroption option">
                <asp:Image ID="_userImage" runat="server" />
                <asp:Label ID="_userName" runat="server" CssClass="enlight"></asp:Label>
            </asp:HyperLink>
            <asp:HyperLink ID="_userBalance" runat="server" CssClass="option">
                <div class="balance">
                    <asp:Label ID="_userBalanceCurrent" runat="server" CssClass="current"></asp:Label>
                    <asp:Label ID="_userBalanceInProcess" runat="server" CssClass="in-process enlight"></asp:Label>
                </div>
            </asp:HyperLink>
            <asp:HyperLink ID="_userSpace" runat="server" CssClass="option">
                <span class="enlight">Мое пространство</span>
            </asp:HyperLink>
            <asp:HyperLink ID="_userSettings" runat="server" CssClass="option">
                <span class="enlight">Мои настройки</span>
            </asp:HyperLink>
            <asp:HyperLink ID="_userDonations" runat="server" CssClass="option">
                <span class="enlight">Мои пожертвования</span>
            </asp:HyperLink>
            <a class="option" href="javascript:logOut(window.location.href)"><span class="enlight">Выйти</span></a>
        </div>
    </asp:Panel>
    <asp:Panel ID="_accountActions" runat="server" CssClass="actions" Visible="false">
        <a href="/login">Войти</a>
        <a href="/signup">Регистрация</a>
    </asp:Panel>
</div>
