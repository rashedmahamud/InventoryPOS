<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>Delete Add new row to gridview on button click in asp.net </title>
<style type="text/css">
.GridviewDiv {font-size: 100%; font-family: 'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helevetica, sans-serif; color: #303933;}
.headerstyle
{
color:#FFFFFF;border-right-color:#abb079;border-bottom-color:#abb079;background-color: #df5015;padding:0.5em 0.5em 0.5em 0.5em;text-align:center;
}
</style>
</head>
<body>
<form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
<div class="GridviewDiv">
<asp:GridView runat="server" ID="gvDetails" BorderStyle="None" GridLines="None" ShowFooter="true" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDeleting="gvDetails_RowDeleting">
<HeaderStyle CssClass="headerstyle" />
<Columns>
<asp:BoundField DataField="rowid" HeaderText="Row Id" ReadOnly="true" />
<asp:TemplateField HeaderText="Product Name">
<ItemTemplate>
<asp:TextBox ID="txtName" runat="server" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText = "Price">
<ItemTemplate>
<asp:TextBox ID="txtPrice" runat="server" AutoPostBack="true" OnTextChanged="txtPrice_TextChanged" />
</ItemTemplate>
  </asp:TemplateField>
    <asp:TemplateField HeaderText = "QTY" >
    <ItemTemplate>
<asp:TextBox ID="Qty" runat="server" AutoPostBack="true" OnTextChanged="Qty_TextChanged" />
</ItemTemplate>
        </asp:TemplateField>
     <asp:TemplateField HeaderText = "Dis">
       <ItemTemplate>
<asp:TextBox ID="Dis" runat="server" AutoPostBack="true" OnTextChanged="Dis_TextChanged" />
</ItemTemplate>
           </asp:TemplateField>
      <asp:TemplateField HeaderText = "Total">
    <ItemTemplate>       
      
<asp:TextBox ID="Total" runat="server" AutoPostBack="true" OnTextChanged="Total_TextChanged"  />
</ItemTemplate>
    </asp:TemplateField>
 <%--   <asp:TemplateField >
<FooterTemplate>


</FooterTemplate>
</asp:TemplateField>--%>
<asp:CommandField ShowDeleteButton="true" />
</Columns>
</asp:GridView>
    <div><asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" /></div>
</div>
   
    <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
   
</form>
</body>
</html>