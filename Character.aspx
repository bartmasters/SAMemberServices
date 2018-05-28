<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Character.aspx.cs" Inherits="Character" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Viewing a character</title>
    <link href="SAMember.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="CommonTitle" id="CharTitle">
            <asp:Label ID="CharHeader" runat="server" Text="Label"></asp:Label></h1>
            <center>
            <asp:HyperLink ID="Roster" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="~/Roster.aspx">View the full roster</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLinkSearch" runat="server" NavigateUrl="~/Search.aspx" CssClass="CommonBreadCrumbArea">Search</asp:HyperLink>
            &nbsp;&nbsp;
            <asp:HyperLink ID="FAQ" runat="server" NavigateUrl="~/faq.html" CssClass="CommonBreadCrumbArea">FAQ</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="Quests" runat="server" CssClass="CommonBreadCrumbArea">Quests</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="Recipes" runat="server" CssClass="CommonBreadCrumbArea">Recipes</asp:HyperLink>&nbsp;&nbsp;
            <asp:HyperLink ID="Exit" runat="server" CssClass="CommonBreadCrumbArea" NavigateUrl="http://www.southernarmada.org">Exit</asp:HyperLink>
<p>
    <asp:ObjectDataSource ID="QuestDataSource" runat="server" SelectMethod="getQuests" TypeName="WoWDetails.WoWCharacter" OnObjectCreating="CreateQuestList"></asp:ObjectDataSource>
    <asp:GridView ID="QuestGrid" runat="server" AutoGenerateColumns="False" DataSourceID="QuestDataSource" Width="60%">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Zone" HeaderText="Zone" SortExpression="Zone" />
            <asp:BoundField DataField="QuestLevel" HeaderText="Level" SortExpression="QuestLevel" />
            <asp:BoundField DataField="Tag" HeaderText="Type" SortExpression="Tag" />
        </Columns>
    </asp:GridView>
    &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;<asp:ObjectDataSource ID="RecipeDataSource" runat="server" SelectMethod="getRecipes" TypeName="WoWDetails.WoWCharacter" OnObjectCreating="CreateRecipeList">
    </asp:ObjectDataSource>
</p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="RecipeDataSource" Width="60%">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                <asp:BoundField DataField="ProfessionName" HeaderText="Profession" SortExpression="ProfessionName" />
                <asp:BoundField DataField="Tooltip" HeaderText="Tooltip" SortExpression="Tooltip" />
                <asp:BoundField DataField="Texture" HeaderText="Texture" SortExpression="Texture" />
            </Columns>
        </asp:GridView>
        </center>
    </form>
</body>
</html>
