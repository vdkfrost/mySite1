<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="engine.aspx.cs" Inherits="Twitch_core.WebForms.engine" %>

<%@ Register Src="~/UserControls/page-footer.ascx" TagName="footer" TagPrefix="uc" %>
<%@ Register Src="~/UserControls/page-header.ascx" TagName="header" TagPrefix="uc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>STREAMER SPACE - личный помощник каждого стримера</title>
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
        <uc:header runat="server" />
        <div class="usual-block" style="background-color: white">
            <asp:Label ID="text" runat="server" CssClass="engine"></asp:Label>
        </div>
        <uc:footer runat="server"></uc:footer>
    </form>
</body>
</html>
