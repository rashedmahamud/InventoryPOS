<%@ Page Language="C#" AutoEventWireup="true" Inherits="LoginCustomer" Codebehind="LoginCustomer.aspx.cs" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login | Inventory Management system </title>  
    <link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/signin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

 <div class="col-lg-6 col-lg-offset-1" >
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title"  >                        
                 Instruction [Customer Panel]                
                </h3>
            </div>
            <div class="panel-body" > 
                <ul>
                    <li>Inventory Management system   </li>
                     <li>With Point of Sale  </li>
                      <li>Customer Module </li>
                      <li>Orders Module </li>
                      <li>Sales Module </li>
                      <li>Purchases Module </li>
                </ul>
            </div>
        </div>
 </div>

<div class="col-lg-3">
      <div class="panel panel-danger">
                    <div class="panel-heading">
                    <h3 class="panel-title"  style="text-align:center">                        
                     Inventory Management system | [Customer Panel]                
                    </h3>
                  </div>
    <div class="panel-body" > 

      <form class="form-signin" role="form">     
        <h5 class="form-signin-heading">Please sign in bill/bill </h5> 
        
     <asp:Label ID="lblLogMsg" runat="server"   ForeColor="Red" Text="---------"></asp:Label>

        <asp:TextBox ID="txtuser" runat="server" type="text" class="form-control" placeholder="User ID" required autofocus></asp:TextBox>
        <asp:TextBox ID="txtpass" runat="server" class="form-control" 
          placeholder="Password" required TextMode="Password"></asp:TextBox>
         
          <input type="checkbox" value="remember-me"> Remember me
        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
          ErrorMessage="*" ControlToValidate="txtuser"></asp:RequiredFieldValidator>
        <asp:Button ID="btnSubmit" runat="server" Text="Sign in" 
            class="btn btn-lg btn-warning btn-block btn-sm" onclick="btnSubmit_Click" />   
      </form>    
    </div>
      
  </div>
      

          <%--Footer--%>
      
            <div class="panel panel-primary" >   
                <div class="panel-footer"  style="text-align:center">
                         
					    Developed by <a href="http://codecanyon.net/user/dynamicsoft" target="_blank"> DynamicSoft </a>  <br/>
                        Copyright © 2014 - <%= DateTime.Now.Year.ToString() %>  All Rights Reserved.
                     
                </div>
            </div>
  </div>       
 

 
    </form>

</body>
</html>
