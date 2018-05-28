<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectChars.aspx.cs" Inherits="SelectChars" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head2" runat="server">
    <title>Select Characters</title>
    <link href="SAMember.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1 class="CommonTitle">Select characters to upload</h1>
        <p>Select the character(s) you want to upload to the website.</p>
        <asp:CheckBoxList ID="CbxCharacters" runat="server" Height="203px" Width="418px">
        </asp:CheckBoxList>
        <br />
        <asp:Label ID="LabelResult" runat="server" ForeColor="Red"></asp:Label>
        <p>&nbsp;</p>
        <asp:Button ID="ButtonUpload" runat="server" Text="Upload to website" OnClick="ButtonUpload_Click" />
        <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel" />
        <asp:Button ID="ViewChar" runat="server" OnClick="Button1_Click" Text="View Characters" />
    </form>
</body>
</html>