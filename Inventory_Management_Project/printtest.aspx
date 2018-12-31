<%@ Page Language="C#" AutoEventWireup="true" Inherits="printtest" Codebehind="printtest.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script type="text/javascript">
        document.body.onload = PrintME();

        function PrintME() {
            self.print();
        }
    </script>--%>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" Text="Print" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Button" />
    </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
