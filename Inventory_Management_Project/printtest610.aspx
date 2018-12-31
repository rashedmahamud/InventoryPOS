<%@ Page Language="C#" AutoEventWireup="true" Inherits="printtest610" Codebehind="printtest610.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<script type="text/javascript">

    function Print() {
        window.print();
    }

</script>
</head>
<body>
    <form id="form1" runat="server">
   <div>
        Hello World! Welcome to Sample Print Page.
    </div>
        <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="Print()" OnClick="btnPrint_Click" />
    </form>
</body>
</html>
