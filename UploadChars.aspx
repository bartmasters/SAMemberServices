<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadChars.aspx.cs" Inherits="UploadChars" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Update the Roster</title>
    <link href="SAMember.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="CommonTitle">Upload to the Roster</h1>
        <center>
            <asp:HyperLink ID="HyperLinkSearch" runat="server" NavigateUrl="~/Search.aspx" CssClass="CommonBreadCrumbArea">Search</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="~/Roster.aspx">Return to the roster</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="FAQ" runat="server" NavigateUrl="~/faq.html" CssClass="CommonBreadCrumbArea">FAQ</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="http://www.southernarmada.org">Exit</asp:HyperLink>
            <p>&nbsp;</p>
        </center>
        <p>Click browse and upload your CharacterProfiler.lua file <br />
        It can normally be found in {WoW Directory}\WTF\Account\{Your Account Name}\SavedVariables\ <br />
        To find out more about character profiles, check our <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="faq.htm">FAQ</asp:HyperLink></p>
        <br />
        <asp:FileUpload ID="CharUpload" runat="server" Width="400px" />
&nbsp;<br />
        <br />
        <asp:Label ID="LabelStatus" runat="server" Width="300px" Font-Bold="True" Font-Names="Arial" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnSelectChars" runat="server" Text="OK" Width="84px" OnClick="btnSelectChars_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" Width="84px" Height="24px" />
        <br />
    </form>
</body>
</html>
