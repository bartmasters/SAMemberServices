<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search the Armada</title>
    <link href="SAMember.css" rel="stylesheet" type="text/css" />
  
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="CommonTitle">
                Search the Armada</h1>
            <center>
                <asp:HyperLink ID="HyperLinkSearch" runat="server" CssClass="CommonBreadCrumbArea"
                    NavigateUrl="~/Roster.aspx">View the full roster</asp:HyperLink>
                &nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="~/UploadChars.aspx">Upload your chars</asp:HyperLink>
                &nbsp;&nbsp;
                <asp:HyperLink ID="FAQ" runat="server" NavigateUrl="~/faq.html" CssClass="CommonBreadCrumbArea">FAQ</asp:HyperLink>&nbsp;&nbsp;
                &nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="http://www.southernarmada.org">Exit</asp:HyperLink>
                <br />&nbsp;<br />
                <asp:Label ID="Label1" runat="server" Text="What do you want to search for?"></asp:Label>
                <asp:TextBox ID="TxtSearch" runat="server" ></asp:TextBox>
                <asp:Button ID="BtnSearch" runat="server" Text="Search" />
                <br />&nbsp;<br />
            <asp:GridView ID="MainListView" runat="server" AutoGenerateColumns="False" CssClass="BlogPostContent"
                DataSourceID="RecipeDataSource" Width="572px" AllowPaging="True" Caption="Recipes" CaptionAlign="Top">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="Name" DataNavigateUrlFormatString="Character.aspx?char={0}"
                        DataTextField="Name" HeaderText="Name" NavigateUrl="~/Character.aspx" />
                    <asp:BoundField DataField="Item" HeaderText="Recipe" SortExpression="Item" />
                </Columns>
            </asp:GridView>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:ObjectDataSource ID="RecipeDataSource" runat="server" SelectMethod="searchRecipes"
                TypeName="WoWDetails.WoWCharacterSummaryList">
            <SelectParameters>
                <asp:ControlParameter ControlID="TxtSearch" Name="searchString" PropertyName="Text"
                    Type="String" DefaultValue="jjffkk" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="QuestDataSource" runat="server" SelectMethod="searchQuests"
                TypeName="WoWDetails.WoWCharacterSummaryList">
            <SelectParameters>
                <asp:ControlParameter ControlID="TxtSearch" Name="searchString" PropertyName="Text"
                    Type="String" DefaultValue="fdsfd" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="BlogPostContent"
                DataSourceID="QuestDataSource" Width="572px" AllowPaging="True" Caption="Quests" CaptionAlign="Top">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="Name" DataNavigateUrlFormatString="Character.aspx?char={0}"
                    DataTextField="Name" HeaderText="Name" NavigateUrl="~/Character.aspx" />
                <asp:BoundField DataField="Item" HeaderText="Quest" SortExpression="Item" />
            </Columns>
        </asp:GridView>
                    </center>

        </form>
</body>
</html>
