<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<html>
    <head runat="server" >
        <title>Compnay Setup</title>

         <link href="../assets/css/style.css" rel="stylesheet" type="text/css" /> 
    <link href="../assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/> 
<link href="../assets/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css"/>
      <link href="3columntable.css" rel="stylesheet" />
<link href="../assets/css/components-md.css" rel="stylesheet" type="text/css"   id="style_components" />
<link href="../assets/css/plugins-md.css" rel="stylesheet" type="text/css"/>
<link href="../assets/css/layout.css" rel="stylesheet" type="text/css"/>
<link href="../assets/css/themes/light.css" rel="stylesheet" type="text/css" id="style_color" />
<link href="../assets/css/custom.css" rel="stylesheet" type="text/css"/>
<!-- END THEME STYLES -->
        <link href="2column.css" rel="stylesheet" />

         <style type="text/css">

             .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 800px;
        height :800px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0
      
    }
    .roundedcorners
{
-webkit-border-radius: 10px;
-khtml-border-radius: 10px;	
-moz-border-radius: 10px;
border-radius: 10px;
background-color:black;
color:white;
text-decoration:none;
border:outset;
}


        .roundedcorners:hover
{

-webkit-border-radius: 10px;
-khtml-border-radius: 10px;	
-moz-border-radius: 10px;
border-radius: 10px;
border:outset;
background-color:ButtonShadow; 


}

    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
         width: 800px;
        height :800px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
        .auto-style1 {
            height: 27px;
        }
        .auto-style2 {
            height: 4px;
        }
         .auto-style4 {
             width: 212px;
         }
         .auto-style6 {
             width: 71px;
         }

         </style>

    </head>
    <body>
        <form runat="server">
            <br /><br /><br /><br /><br />
<asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdateSettings">
<div class="col-lg-8 col-lg-offset-2"> 
    <div class="panel panel-primary" style="text-align:left" >       
          <div class="panel-body">
           <asp:Label ID="Label8" class="label label-warning" Font-Size="12px" runat="server" Text="System Settings"></asp:Label>  
           <asp:Label ID="Label7" runat="server" Font-Size="11px" Text="Please Update the information below"></asp:Label>        
              <p></p>
                <asp:TextBox ID="TextBox2"  Enabled="false" runat="server" ></asp:TextBox>
                <br />  
            <div class="col-lg-6">           
                <asp:Label ID="Label2" runat="server" Font-Size="12px"  Text="Company Name"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="txtCompanyName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                <asp:TextBox ID="txtCompanyName"  BackColor="#D7FDF9" ToolTip="Please Enter Company Name" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
                           
                <asp:Label ID="Label3" runat="server" Font-Size="12px"  Text="Company Address"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtCompanyAddress" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtCompanyAddress"  TextMode="MultiLine"  Height="120px" class="form-control" ValidationGroup="vlpg43"  runat="server"></asp:TextBox>
                <asp:Label ID="Label11" runat="server" Font-Size="12px"  Text="Branch Location"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="TextBox1" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <asp:TextBox ID="TextBox1"    class="form-control" ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label4" runat="server" Font-Size="12px"  Text="Phone"></asp:Label>  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtPhone" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>  
                <asp:TextBox ID="txtPhone" class="form-control"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                  <asp:Label ID="Label5" runat="server" Font-Size="12px"  Text="Email Address"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtEmailAddress" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtEmailAddress" class="form-control"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

            </div>
            <div class="col-lg-6"> 
              
                <asp:Label ID="Label9" runat="server" Font-Size="12px"  Text="Web Address"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtWebAddress" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtWebAddress" class="form-control"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label10" runat="server" Font-Size="12px"  Text="VAT %"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtVatRate" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtVatRate" class="form-control" ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label1" runat="server" Font-Size="12px"  Text="VAT Registration No"></asp:Label> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtvatRegiNo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                             
                <asp:TextBox ID="txtvatRegiNo" class="form-control" ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                <asp:Label ID="Label6" runat="server" Font-Size="12px"  Text="Footer Message"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ValidationGroup="vlpg43"  ControlToValidate="txtFooterMessage" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                              
                <asp:TextBox ID="txtFooterMessage"  BackColor="#D7FDF9"  TextMode="MultiLine" Font-Size="11px" ToolTip="Please Enter Receipt Footer message" class="form-control" ValidationGroup="vlpg43" runat="server"></asp:TextBox>
              <br />
              <asp:Button ID="btnUpdateSettings"    runat="server"  ValidationGroup="vlpg43"  
                  class="btn btn-primary btn-sm" Text="Update Settings"     onclick="btnUpdateSettings_Click" /> 
                                                <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Return to Company Outlet" PostBackUrl="~\Accounts\Outlate.aspx" />

       
                  </div>
                 <div class="footer"></div> 
          </div>
    </div>
</div>

</asp:Panel>
            </form>
        </body>
</html>

