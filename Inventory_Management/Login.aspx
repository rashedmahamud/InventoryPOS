<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="False" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 
<style type="text/css">
    .auto-style1 {
        width: 316px;
        height: 86px;
    }
</style>
 
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
 
<!--<![endif]-->
<!-- BEGIN HEAD -->
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head id="Head1" runat="server" style ="background-color:Background">

<title>Login | Inventory Management system</title>


<link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css"/>
<link href="assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
<link href="assets/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css">
<link href="assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"> 
<link href="assets/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css"/>
<!-- END GLOBAL MANDATORY STYLES -->
<!-- BEGIN PAGE LEVEL STYLES -->
<link href="assets/css/login.css" rel="stylesheet" type="text/css"/>
<!-- END PAGE LEVEL SCRIPTS -->
<!-- BEGIN THEME STYLES -->
<link href="assets/css/components-md.css" rel="stylesheet" type="text/css"   id="style_components" />
<link href="assets/css/plugins-md.css" rel="stylesheet" type="text/css"/>
<link href="assets/css/layout.css" rel="stylesheet" type="text/css"/>
<link href="assets/css/themes/light.css" rel="stylesheet" type="text/css" id="style_color" />
<link href="assets/css/custom.css" rel="stylesheet" type="text/css"/>
<!-- END THEME STYLES -->

</head>
<!-- END HEAD -->
<!-- BEGIN BODY -->
<body class="page-md login"  > 
<form id="form1" runat="server"  >
	<!-- BEGIN LOGO -->
	<div class="logo"    >
		<a href="Login.aspx">
		<img alt="" class="auto-style1" src="img/logo.png" /> 
		</a>
	</div>
	<!-- END LOGO -->
<!-- BEGIN LOGIN -->
<div class="content" style="background-color:white" >
	<!-- BEGIN LOGIN FORM -->
	<form class="login-form" action="index.html" method="post">
		<h3 class="form-title">Sign In</h3>
		<div class="alert alert-danger display-hide">
			<button class="close" data-close="alert"></button>
			<span>
			Enter any username and password. 
            
            </span>
		</div>
		<div class="form-group" >
			<!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
            <asp:Label ID="lblLogMsg" runat="server"   ForeColor="Red" Text="---------"></asp:Label>
			<label class="control-label visible-ie8 visible-ie9">Username</label>			 
            <asp:TextBox ID="txtuser" runat="server" type="text" class="form-control form-control-solid placeholder-no-fix" placeholder="User ID"  ></asp:TextBox>

		</div>
		<div class="form-group" style ="background-color:Background">
			<label class="control-label visible-ie8 visible-ie9">Password</label>			 
            <asp:TextBox ID="txtpass" runat="server"  class="form-control form-control-solid placeholder-no-fix"  placeholder="Password"   TextMode="Password"></asp:TextBox>
		</div>
        <div class="form-group" style ="background-color:Background">
			<label class="control-label visible-ie8 visible-ie9">Store Number </label>			 
            <asp:TextBox ID="TextBox1" runat="server"  class="form-control form-control-solid placeholder-no-fix"  placeholder="Store Number"   ></asp:TextBox>
		</div>
		<div class="form-actions" >			 
            <asp:Button ID="btnSubmit" runat="server" Text="Sign in" class="btn btn-success uppercase" onclick="btnSubmit_Click" />			 		 
		</div>
	 
	</form>
	<!-- END LOGIN FORM --> 
</div>
<div class="copyright" >
	  Developed by <a href="http://www.cmsnpos.com.au" style="color:White" target="_blank"> Cmsnnetwork </a>  <br/>
                        Copyright © 2013 - <%= DateTime.Now.Year.ToString() %>  All Rights Reserved.
</div>

<script src="assets/scripts/jquery.min.js" type="text/javascript"></script>
<script src="assets/scripts/jquery-migrate.min.js" type="text/javascript"></script>
 
<script src="assets/scripts/jquery.blockui.min.js" type="text/javascript"></script>
<script src="assets/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
 <script src="assets/scripts/jquery.uniform.min.js" type="text/javascript"></script>
<script src="assets/scripts/jquery.cokie.min.js" type="text/javascript"></script>
<!-- END CORE PLUGINS -->
<!-- BEGIN PAGE LEVEL PLUGINS -->
<script src="assets/scripts/jquery.validate.min.js" type="text/javascript"></script>
<!-- END PAGE LEVEL PLUGINS -->
<!-- BEGIN PAGE LEVEL SCRIPTS -->
<script src="assets/scripts/metronic.js" type="text/javascript"></script>
<script src="assets/scripts/layout.js" type="text/javascript"></script>
<script src="assets/scripts/login.js" type="text/javascript"></script>
<!-- END PAGE LEVEL SCRIPTS -->
	<script>
	    jQuery(document).ready(function () {
	        Metronic.init(); // init metronic core components
	        Layout.init(); // init current layout
	        Login.init();
	        Demo.init();
	    });
	</script>
<!-- END JAVASCRIPTS -->
</form>
</body>
<!-- END BODY -->
</html>