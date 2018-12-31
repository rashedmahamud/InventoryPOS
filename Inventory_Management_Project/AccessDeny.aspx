<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccessDeny" Codebehind="AccessDeny.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body  style="background-color:Silver">
    <form id="form1" runat="server">
    <div > 
        <center>  <br /> <br />            
            <asp:Label ID="Label1" runat="server" Font-Size="19px" ForeColor="Red" Text="Access Deny | Please Contact to the system Administrator"></asp:Label><br />  
             <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Dashboard/Default.aspx" runat="server">X</asp:HyperLink> 
             <hr />  
             <asp:HyperLink ID="HyperLink2" NavigateUrl="~/Login.aspx" runat="server">Login</asp:HyperLink>  <hr /> 
        </center>
    </div>

        
    
    </form>
</body>
</html>
