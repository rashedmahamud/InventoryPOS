<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" Inherits="Accounts_DueInvoice" Codebehind="DueInvoice.aspx.cs" %>
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
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
     <div style="width:100%"> 
             <div class="note note-primary note-shadow">
                         <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"
                        OnClientClick="JavaScript: window.history.back(1); return false;">
                        </asp:button> |
               Sales | Take Due Payment                 
            </div>
     
     <div class="panel panel-success" style="text-align:left" > 
        <div class="panel-heading"> Invoice No: 
            <asp:Label ID="lblInvoiceNo" runat="server" Text="Label"></asp:Label> 
            <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" ForeColor="Black" Text="Search" OnClick="Button1_Click" />
         </div>
                 <div class="note note-info note-shadow">
                 <div class="col-lg-7"  style="text-align:left" >     Receiveable   </div>  
                  <div class="col-lg-3"   style="text-align:right" >    
                   | <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript: printDiv('wrapper')" />    </div><br />
                          <asp:Label ID="Label22" runat="server" Text="123456789"></asp:Label>
                          
            </div>
         </div>
        
            <div class="panel-body">                      
                            <asp:GridView ID="grdItemList" runat="server"   
                            class="table table-striped table-hover" Font-Size="11px"  
                            ShowHeaderWhenEmpty="True" onrowdatabound="grdItemList_RowDataBound"></asp:GridView>
                    
            </div>
  </div>
                  
 <div id="wrapper">
     <div>
<table>
    <tr>
        <td>
            <asp:Label ID="Label2" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Customer Info:"></asp:Label><br />
                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Font-Size="Large" Text=""></asp:Label><br />
                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Font-Size="Large" Text=""></asp:Label><br />


        </td>
        <td>

        </td>
    </tr>
</table>

     </div>
     <div class="panel-body">                      
                            <asp:GridView ID="GridView1" runat="server"   
                            class="table table-striped table-hover"  Font-Size="11px"  
                            ShowHeaderWhenEmpty="True" BorderColor="#006666" GridLines="None">
                                <HeaderStyle BackColor="#006666" ForeColor="Black" />
                            </asp:GridView>
                    <div style="text-align:left ;width:100%" >  
     <div class="panel panel-info" style="text-align:left;width:100%" > 
        <div class="panel-heading" style="width:100%"> Amounts  </div>
            <div class="panel-body" style="margin-left:70%">                      
                Discount: <span  style="padding-left:10px"> <asp:Label ID="lbldis"          Font-Size="17px"  runat="server" Text="0"></asp:Label> </span> <br />
                VAT:<span  style="padding-left:40px">   <asp:Label ID="lblvat"              Font-Size="17px" runat="server" Text="0"></asp:Label>  </span> <br />
                Net Total:  <span  style="padding-left:10px"> <asp:Label ID="lbltotal"      Font-Size="17px"  runat="server" Text="0"></asp:Label> </span>  <br />
                               Paid:  <span  style="padding-left:40px"> <asp:Label ID="Label1"              Font-Size="17px"  runat="server" Text="0"></asp:Label></span> <br />

                 Due:  <span  style="padding-left:40px"> <asp:Label ID="lblDue"              Font-Size="17px"  runat="server" Text="0"></asp:Label></span> 
            
            </div>
    </div>
</div>
            </div>
             <div>
<div style="text-align:left ;width:100%">  
     <div class="panel panel-info" style="text-align:left" > 
        <div class="panel-heading"> Receive payment  </div>
            <div class="panel-body">                      
                <table class="style1">
                    <tr>
                            <td class="style2">
                                Payment:  
                            </td>
                            <td>
                                <asp:TextBox ID="txtPaid" placeholder="Customer paid Amount"  ToolTip="Customer paid Amount" runat="server" class="form-control" 
                                    AutoPostBack="True"  ></asp:TextBox> 

                                <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtPaid" ValidationGroup="vr12"  Font-Size="11px" 
                                ID="RequiredFieldValidator1" runat="server"   ErrorMessage="Enter paid amount"></asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator ForeColor="Red" Font-Size="11px" ValidationGroup="vr12" 
                                ControlToValidate="txtPaid" ID="RegularExpressionValidator1" 
                                runat="server" ErrorMessage="Enter a valid number" ValidationExpression="[0-9]*\.?[0-9]*">
                                </asp:RegularExpressionValidator> 
                                                                  
                            </td>
                        </tr> 
                        
                        <tr>
                                <td class="style2">
                                    Paid by:  <br />    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLPaidBy" runat="server" class="form-control">
                                        <asp:ListItem>Cash</asp:ListItem>
                                        <asp:ListItem>Cheque</asp:ListItem>
                                        <asp:ListItem>Debit Card</asp:ListItem>
                                        <asp:ListItem>Credit Card</asp:ListItem>
                                        <asp:ListItem>Paypal</asp:ListItem>
                                        <asp:ListItem>Payza</asp:ListItem>
                                        <asp:ListItem>Skrill</asp:ListItem>
                                        <asp:ListItem>Neteller</asp:ListItem>
                                        <asp:ListItem>Perfect money</asp:ListItem>                                       
                                        <asp:ListItem>Online</asp:ListItem>
                                        <asp:ListItem>Others</asp:ListItem>
                                    </asp:DropDownList>  <br />    
                                </td>
                        </tr>      
                       
                            <tr>
                                <td class="style2">
                                    Date:  <br />  <br />
                                </td>
                            
                                <td>
                                        <atk:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDate" />
                                        <asp:TextBox ID="txtDate" runat="server"  class="form-control" ToolTip="Order Date" placeholder="Select Date"></asp:TextBox>  
                                         <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtDate" ValidationGroup="vr12"  Font-Size="11px" 
                                    ID="RequiredFieldValidator2" runat="server"   ErrorMessage="Please select Date"></asp:RequiredFieldValidator>  <br />
                                    <asp:Button ID="btnReceivedPayment" runat="server"  ValidationGroup="vr12" class="btn btn-success btn-sm"  Text="Submit"  onclick="btnReceivedPayment_Click"  /> 
                                </td>
                            </tr>                      
                </table>
                    
            </div>
    </div>
</div>


                 </div>
                  </div>
</ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

