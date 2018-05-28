<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Roster.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SA Member Utilities</title>
    <link href="SAMember.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="CommonTitle">
            Members of the Armada</h1>
            <center>
            <asp:HyperLink ID="HyperLinkSearch" runat="server" NavigateUrl="~/Search.aspx" CssClass="CommonBreadCrumbArea">Search</asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="~/UploadChars.aspx">Upload your chars</asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink ID="FAQ" runat="server" NavigateUrl="~/faq.html" CssClass="CommonBreadCrumbArea">FAQ</asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink2" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="http://www.southernarmada.org">Exit</asp:HyperLink>
<p>&nbsp;</p>
        <asp:GridView ID="MainListView" runat="server" AutoGenerateColumns="False" DataSourceID="WoWCharacterDataSource"
            Width="572px" CssClass="BlogPostContent">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Name" DataNavigateUrlFormatString="Character.aspx?char={0}"
                    DataTextField="Name" HeaderText="Name" NavigateUrl="~/Character.aspx" />
                <asp:BoundField DataField="Sex" HeaderText="Sex" SortExpression="Sex" />
                <asp:BoundField DataField="CharClass" HeaderText="Class" SortExpression="CharClass" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Level" HeaderText="Level" SortExpression="Level" />
                <asp:BoundField DataField="Race" HeaderText="Race" SortExpression="Race" />
            </Columns>
        </asp:GridView>
        </center>
            <asp:ObjectDataSource ID="WoWCharacterDataSource" runat="server" SelectMethod="getAll"
                TypeName="WoWDetails.WoWCharacterSummaryList"></asp:ObjectDataSource>
    </form>
</body>
</html>
