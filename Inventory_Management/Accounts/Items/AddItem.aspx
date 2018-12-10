<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master"  AutoEventWireup="true" CodeFile="AddItem.aspx.cs" Inherits="AddItem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Textboxcss.css" rel="stylesheet" />
    <link href="../2column.css" rel="stylesheet" />

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
    <script type="text/javascript">
        $(window).load(function () {
            $(".col-3 input").val("");

            $(".input-effect input").focusout(function () {
                if ($(this).val() != "") {
                    $(this).addClass("has-content");
                } else {
                    $(this).removeClass("has-content");
                }
            })
        });
    </script>
    <script type="text/javascript">

        function printing() {
            window.print();


        }
    </script>
       <style type="text/css">

    .roundedcorners
{
-webkit-border-radius: 5px;
-khtml-border-radius: 5px;	
-moz-border-radius: 5px;
border-radius: 5px;
background-color:white;
color:black;
font-size:large;
font-weight:bolder;
text-decoration:none;
text-align:center;
height:35px;
}


        .roundedcorners:hover
{

-webkit-border-radius: 10px;
-khtml-border-radius: 10px;	
-moz-border-radius: 10px;
border-radius: 10px;

background-color:skyblue; 
color:black;
text-align:center;
}

 
    
  
  
   
     
    </style>
    <link type="text/css" href="css/ui-lightness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("#TextBox6").datepicker();
    });
</script>
<style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style>
     <script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script> 
    <style type="text/css">
.GridviewDiv {font-size: 100%; font-family: 'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helevetica, sans-serif; color: #303933;}
.headerstyle
{
color:black;border-right-color:#abb079; font-size: 12px;  border-bottom-color:#abb079;background-color: #f0f5f5;padding:0.5em 0.5em 0.5em 0.5em;text-align:center;


}
</style>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<%--<div class="col-lg-8 ">--%>
          <asp:UpdatePanel ID="UpdatePanelImageUpload" runat="server"  UpdateMode="Conditional">
                                <ContentTemplate>
    <div class="well well-sm"  >
            <div class="col-lg-4" style="text-align:left">
                <asp:HyperLink ID="hlnkManageItems" Font-Size="20px" ForeColor="Black" ToolTip="Manage Items"  class="fa fa-th-list" NavigateUrl="~/Accounts/Items/ManageItems.aspx"  ValidationGroup="vlpg11" runat="server"></asp:HyperLink>
                Manage Items 
              </div>
                <div class="col-lg-4" > 
                <asp:HyperLink ID="HyperLink2" Font-Size="20px" ForeColor="Black" ToolTip="Manage Items"  class="fa fa-cloud-upload" NavigateUrl="~/Accounts/Items/UploadItems.aspx"  ValidationGroup="vlpg11" runat="server"></asp:HyperLink>
                Bulk Upload
            </div>
            <div class="col-lg-4" style="text-align:Right">
                <asp:HyperLink ID="hlnkAddCategory" Font-Size="20px" ForeColor="Black"  ToolTip="Add Category"  class="fa fa-plus" NavigateUrl="~/Accounts/Items/Category.aspx"  ValidationGroup="vlpg11" runat="server"></asp:HyperLink>
                Add Category
            </div><br />
<%--        <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>--%>
    </div>
 
       <div class="panel panel-primary" style="text-align:left">      
          <div class="panel-body">
                <asp:Label ID="Label1" class="label label-warning" Font-Size="12px" runat="server" Text="Add Item"></asp:Label>
                <asp:Label ID="Label8" runat="server" Font-Size="11px" Text="Please enter Item/product details below"></asp:Label> 
               
              <hr />
                        <div class="col-lg-6">
                           <table width="100%" style="text-align:left " >
                               <tr>
                                   <td>
                            <asp:Label ID="Label2" runat="server" Text="Barcode" Font-Bold="True"></asp:Label>
                                       
                                       <asp:TextBox ID="TextBox1" Width="60%" class="roundedcorners" runat="server" ></asp:TextBox><asp:Button ID="Button1" class="roundedcorners" runat="server" Text="SEARCH" OnClick="Button1_Click" />
                          </td>
                                   
                                       </tr>
                                <tr>
                                   <td>
                                   <asp:Label ID="Label3" runat="server" Text="Item Name "  ></asp:Label><asp:TextBox ID="TextBox2" class="roundedcorners" Width="100%" runat="server" MaxLength="14"></asp:TextBox>
                              </td>
                                       </tr>
                                 <tr>
                                   <td>
                                        <asp:Label ID="Label4" runat="server" Text="Purchase Price"></asp:Label><asp:TextBox ID="TextBox3" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox>  
                            </td>
                                       </tr>
                               <tr>
                                   <td>
                                        <asp:Label ID="Label6" runat="server" Text="RetailPrice"></asp:Label><asp:TextBox ID="TextBox4" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox> 
                              </td>
                                       </tr>
                                <tr>
                                   <td>
                                       <asp:Label ID="Label10" runat="server" Text="Item Qty"></asp:Label><asp:TextBox ID="TextBox5" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox> 
                            </td>
                                       </tr>
                                        <tr>
                                   <td>
                                       <asp:Label ID="Label11" runat="server" Text="Min Stock Qty"></asp:Label><asp:TextBox ID="TextBox6" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox> 
                           </td>
                                       </tr>
                                                <tr>
                                   <td> 
                                        <asp:Label ID="Label12" runat="server" Text="Max Stock Qty"></asp:Label><asp:TextBox ID="TextBox7" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox> 
                             </td>
                                       </tr>
                                                         <tr>
                                   <td> 
                                       <asp:Label ID="Label13" runat="server" Text="Manufacture Date"></asp:Label><asp:TextBox ID="TextBox8" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox>
                           </td>
                                       </tr>
                                                        <tr>
                                   <td> 
                                        <asp:Label ID="Label14" runat="server" Text="Exp Date"></asp:Label><asp:TextBox ID="TextBox9" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox> 
                            </td>
                                       </tr>
                                                         <tr>
                                   <td> 
                                       <asp:Label ID="Label15" runat="server" Text="Supplier ID"></asp:Label> <asp:DropDownList ID="DropDownList1" class="form-control roundedcorners"  ValidationGroup="vlpg43" runat="server"></asp:DropDownList><br /><br />

 </td>
                                       </tr>
                             
                            </table>
                              
                            
                          
                         
                       
                            
                             </div>
                        <div class="col-lg-6">

                      
                            

                            <asp:Label ID="Label7" runat="server"  Font-Size="12px" Text="Item Category"></asp:Label>  |  
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Items/Category.aspx" class="roundedcorners" ToolTip="Please add Category" Font-Size="Larger">+</asp:HyperLink>                            
                            <asp:DropDownList ID="DDLCategory" class="form-control roundedcorners"  ValidationGroup="vlpg43" runat="server"></asp:DropDownList><br /><br />
                            <asp:Label ID="Label5" runat="server"  Text="Tax"></asp:Label><asp:TextBox ID="TextBox11" class="roundedcorners" Width="100%"   runat="server" ForeColor="Black" ></asp:TextBox>  <br /><br />
                            <asp:Label ID="Label16" runat="server" Text="Discount"></asp:Label><asp:TextBox ID="TextBox12" class="roundedcorners" Width="100%" runat="server" ForeColor="Black" ></asp:TextBox> <br /><br /> 


                              <asp:Label ID="Label9" runat="server" Text="Description"></asp:Label><asp:TextBox ID="TextBox13" class="roundedcorners" Width="100%" runat="server" TextMode="MultiLine"></asp:TextBox>  <br /><br />                    
                                                        <asp:Label ID="Label17" runat="server" Text="Shop ID"></asp:Label><asp:TextBox ID="TextBox10" Enabled="false" class="roundedcorners" Width="100%" runat="server" ></asp:TextBox>  <br /><br />                    

                            
                            <p></p>                            
                            
                                                        <asp:FileUpload ID="FUpimg"     runat="server"    /> 
                                
                                 
                                    <br />
                                    <asp:Label ID="lblmessage" ForeColor="Red" runat="server" Font-Size="11px" Text=""></asp:Label> <p></p> 
                                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="vlpg43" class="btn btn-primary btn-sm" Text="Submit" onclick="btnSubmit_Click" />

                                                          
                             <br />

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="Please add decimal value e.g: 20.11 or 20" ForeColor="Red"  ControlToValidate="TextBox3"  ValidationGroup="vlpg43" 
                            ValidationExpression="^[0-9][\.\d]*(,\d+)?$"></asp:RegularExpressionValidator> <br />

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ErrorMessage="Please add decimal value" ForeColor="Red"  ControlToValidate="TextBox4" ValidationGroup="vlpg43" 
                            ValidationExpression="^[0-9][\.\d]*(,\d+)?$"></asp:RegularExpressionValidator> <br />

                    <%--         <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                            ErrorMessage="Please add decimal value" ForeColor="Red"  ControlToValidate="txtItemQty" ValidationGroup="vlpg43" 
                            ValidationExpression="\d{0,9}"></asp:RegularExpressionValidator> <br />--%>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ErrorMessage="Please  add  Item Discount Rate" ForeColor="Red"  ControlToValidate="TextBox12" ValidationGroup="vlpg43" 
                            Display="Dynamic" ValidationExpression="^[0-9][\.\d]*(,\d+)?$"></asp:RegularExpressionValidator>
                        </div>
             </div>
        </div>
<%--</div>--%>
                    </ContentTemplate>   
                                                                                  <Triggers> <asp:PostBackTrigger   ControlID="btnSubmit"/></Triggers>      

                            </asp:UpdatePanel>

    <div>
          
    </div>
</asp:Content>

