<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="page-header.ascx.cs" Inherits="Twitch_core.UserControls.page_header" ClassName="PageHeader" %>

<style>
    .m-t-b {
        height: 270px;
        padding-top: 130px;
        background-size: 100% !important;
        background-position: top center !important;
        background-repeat: no-repeat !important;
    }

        .m-t-b .header {
            margin: 0px auto 10px;
            display: table;
            position: relative;
            height: 120px;
            background-color: darkorange;
            padding: 30px 40px;
            box-sizing: border-box;
            border-radius: 5px;
        }

        .m-t-b .desc {
            text-transform: uppercase;
            color: white;
            display: block;
            text-align: center;
            font: 16px 'PT Sans Bold';
            position: relative;
        }
</style>

<div class="full-width m-t-b" <% Response.Write("style=\" background: fixed url(/images/backgrounds/back9.jpg)\""); %>>
    <div style="width: 100%; position: absolute; background: linear-gradient(to top, rgba(0, 0, 0, 0.8), transparent, transparent); top: 0px; opacity: 0.9; height: 400px;"></div>
    <img src="/images/logos/streamerspace/text.PNG" class="header" />
    <h1 class="desc">Личный помощник каждого стримера</h1>
    <asp:PlaceHolder ID="Content" runat="server"></asp:PlaceHolder>
</div>
