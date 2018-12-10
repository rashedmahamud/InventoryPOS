<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnProudct.aspx.cs" Inherits="ReturnProudct" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 102px;
        }
    </style>

    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" /> 
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

</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
    <div class="col-lg-6"> Item List 
<div class="panel  panel-success">      
<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="400px">  <br /> 
       <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small" RepeatLayout="Flow"    RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
                <div class="col-md-4">  
                    <div id="pricePlansmsg">
                    <ul id="plans">
                        <li class="plan">
                            <ul class="planContainer">
                                <li class="title">
                                    <asp:Label ID="LblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    <asp:Label ID="LblCode" Visible="false" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    <asp:Label ID="LblItemName" Visible="false" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label> 
                                    <asp:Label ID="LblQty" Visible="false" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                    <asp:Label ID="LblPrice" Visible="false" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                    <asp:Label ID="LblDisc" Visible="false" runat="server" Text='<%# Eval("Disc") %>'></asp:Label>
                                    <asp:Label ID="LblTotal" Visible="false" runat="server" Text='<%# Eval("Total") %>'></asp:Label>                                   

                                    <h5><asp:Label   ID="lblitemNametop" Font-Size="12px" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label></h5>
                                </li>
                                <li class="title">
                                    <asp:Image ID="imgPhoto" class="img-circle" runat="server" Width="50px" Height="50px"   ImageUrl='<%# Eval("Photo")%>' />
                                </li>
                                <li>
                                    <ul class="options">                                      
                                       <li><span> Price: <asp:Label ID="Label9" runat="server" Text='<%# Bind("Total") %>'></asp:Label>   
                                           (<asp:Label ID="Label8" ForeColor="Black" Font-Size="10px" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>)</span>  </li>                                    
                                    </ul>
                                </li>
                                    <atk:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" 
                                            TargetControlID="txtqty" Minimum="1" Maximum="999" Width="50"  ViewStateMode="Enabled">  </atk:NumericUpDownExtender>
                                            <asp:TextBox ID="txtqty" runat="server" Font-Size="11px"  Text="1" Width="25px" ToolTip="Item Quantity"></asp:TextBox> <p></p> 
                                 <asp:Button ID="btnGo"  runat="server"  Text="Add to Cart"    ValidationGroup="vG2"  Font-Size="12px"  ToolTip="Add to Cart" class="btn btn-primary btn-xs" OnClick="btn_Goclick" />  <br />           
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            </ItemTemplate>
        </asp:DataList>
     
             
        </asp:Panel> 
       <div class="panel  panel-success">  
          <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="350px" Width="100%"> 
            <asp:DataList ID="DataList2" runat="server"   RepeatColumns="4" RepeatDirection="Horizontal"  BorderStyle="None" CaptionAlign="Bottom" CssClass="active left" HorizontalAlign="Left"  style="vertical-align:top;text-align:left" ShowFooter="False" ShowHeader="False" CellPadding="0"  >
        
           <ItemStyle BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
        
           <ItemTemplate>
                <table class="auto-style1" style="border:none;background-color:white;width:100%"  >
                    <tr  >
                        <td class="auto-style6" style=" color:white "  >
                           
                   <asp:LinkButton ID="lnkbtnRelatedLinks" class="roundedcorners btn btn-primary btn-sm " Width ="170" Height="70" BackColor="steelblue" Style="line-height:50px;text-align:center;" ForeColor="white" runat="server" Text='<%#Eval("ItemCategory") %>' OnClick="lnkbtnRelatedLinks_Click"></asp:LinkButton>
                        </td>
                    </tr>

                </table>
            </ItemTemplate>
            <SelectedItemStyle  Font-Bold="True" ForeColor="Black" />
        </asp:DataList>

                         </asp:Panel> 
        </div>
    </div>
</div>

<div class="col-lg-6"> Return Sales Item
    <div class="input-group">
        <span class="input-group-addon"> <span class="fa fa-search"> </span> </span>
        <asp:TextBox ID="txtItemSearch" runat="server" 
            placeholder="Search to Scan Products" class="form-control"  ToolTip="Search by item code or item name"
            ontextchanged="txtItemSearch_TextChanged" AutoPostBack="true"  ></asp:TextBox> 
            <span class="input-group-addon"></span>
    </div>   
  

    <div class="panel panel-primary" > 
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="270px">

                <asp:GridView ID="grdSelectedItem" class="table table-striped table-hover" onrowdatabound="grdSelectedItem_RowDataBound" 
                    runat="server" Font-Size="11px"   onrowdeleting="grdSelectedItem_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Del">
                                <ItemTemplate> 
                                    <asp:LinkButton  ID="LinkDel" runat="server" ForeColor="Red"    Font-Size="15px"  CommandName="Delete"  ToolTip="Delete item" class="fa fa-times"     />                      
                                </ItemTemplate>
                            </asp:TemplateField>                 
                        </Columns>
                </asp:GridView>

            </asp:Panel>

            <div class="panel-footer"  style="text-align:right">     
                Subtotal =  <asp:Label ID="lblsubTotal" runat="server" Font-Bold="true"  Text="-"></asp:Label><br />
                <%--  <asp:Label ID="Label7" Font-Size="11px" runat="server" Text="Disc% ="></asp:Label>
                <asp:Label ID="lbldisc"  Font-Size="11px"  runat="server" Text="-"></asp:Label><br /> --%>
        

                <asp:Label ID="Label1" Font-Size="11px" runat="server" Text="Vat ="></asp:Label>
                (<asp:Label ID="lblVatRate" Font-Size="9px" runat="server" Text=""></asp:Label>)
                <asp:Label ID="lblVat"  Font-Size="11px"  runat="server" Text="-"></asp:Label><br />  
                Total =     <asp:Label ID="lbltotal" runat="server" Font-Bold="true" Text="-"></asp:Label> <br />

                <asp:Label ID="Label7" Font-Size="11px" runat="server" Text="Total Items"> </asp:Label>
                <asp:Label ID="lblTotalQty"  Font-Size="11px"  runat="server" Text="-"></asp:Label>  
                
            </div>
    </div>
            
            <asp:Button ID="btnsuspen" runat="server" class="btn btn-danger" Text="Suspen"   onclick="btnsuspen_Click" />
            <asp:Button ID="btnPayment" runat="server" class="btn btn-success" 
        Text="Payment" onclick="btnPayment_Click" />
         
</div>



 <%--<<<<<<<<<<<<<<<<<<<<< --------------- payment panel Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
     <asp:Button ID="btnShowPopup" runat="server" style="display:none" />      
    <atk:ModalPopupExtender ID="ModalPopupPayment" runat="server" TargetControlID="btnShowPopup" 
    PopupControlID="pnlpopupPayment"  CancelControlID="btnClosePayment" BackgroundCssClass="modalBackground">
    </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupPayment"  class="panel panel-primary" runat="server" BackColor="White" style="display:none;text-align:left"  DefaultButton="bntPay" > 
    <asp:Label ID="Label2" runat="server" Font-Size="19px" Text="  __Payment"></asp:Label>    <hr />
    <div class="panel-body">
      
                    <div class="panel-SR-mainbody">                   
                  
                        <table class="style1">
                            <tr>
                                <td class="style2">
                                       Sale Invoice No:* <br />  
                                </td>
                                <td  >
                                   <asp:TextBox ID="txtSaleInvoiceNo" ToolTip="Customer paid Amount" runat="server" class="form-control"></asp:TextBox> 

                                    <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtSaleInvoiceNo" ValidationGroup="vr12"  Font-Size="11px" 
                                    ID="RequiredFieldValidator2" runat="server"   ErrorMessage="Enter Sale Invoice Number"></asp:RequiredFieldValidator>
                                </td>
                          
                            </tr>
                            <tr>
                                <td class="style2">
                                    Total Returnable:  <br /> <br />
                                </td>
                                <td>
                                    <asp:Label ID="lbltotalReturnable" runat="server" Font-Bold="true" Font-Size="15px"  Text="-"></asp:Label>  <br /><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Return by:  <br />   
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLReturnBy" runat="server" class="form-control">
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
                                    Return <br />  
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReturn" ToolTip="Return to Customer Amount" runat="server" class="form-control" 
                                        AutoPostBack="True" ontextchanged="txtPaid_TextChanged"></asp:TextBox> 

                                    <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtReturn" ValidationGroup="vr12"  Font-Size="11px" 
                                    ID="RequiredFieldValidator1" runat="server"   ErrorMessage="Enter paid amount"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ForeColor="Red" Font-Size="11px" ValidationGroup="vr12" 
                                    ControlToValidate="txtReturn" ID="RegularExpressionValidator1" 
                                    runat="server" ErrorMessage="Enter a valid number" ValidationExpression="[0-9]*\.?[0-9]*">
                                    </asp:RegularExpressionValidator> <br />  
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    <asp:Label ID="Label3" runat="server" ToolTip="Change to customer" Text="Change"></asp:Label> 
                                </td>
                                <td>
                                    <asp:Label ID="lblChange" runat="server" Font-Bold="true" Text="-"></asp:Label> 
                                </td>
                            </tr>

                              <tr>
                                <td class="style2"><br />
                                    <asp:Label ID="Label4" runat="server" ToolTip="Receive from customer" Text="Due"></asp:Label> 
                                </td>
                                <td><br />
                                    <asp:Label ID="lblDue" runat="server" Font-Bold="true" Text="-"></asp:Label>
                                </td>
                            </tr>

                             <tr>
                                <td class="style2"><br />
                                    <asp:Label ID="Label5" runat="server" ToolTip="Optional" Text="Note"></asp:Label> <br />
                                     <asp:Label ID="Label6" runat="server" Font-Size="8px" ToolTip="Optional" Text="Optional"></asp:Label>
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtNote" runat="server" ToolTip="Optional"  Height="60px" class="form-control" ></asp:TextBox> 
                                </td>
                            </tr>
                            <tr>
                                <td>Customer:  
                                </td>
                                <td>
                                             
                                    <asp:DropDownList ID="DDLCustname" ToolTip="Select customer" class="form-control" runat="server" AutoPostBack="True"  onselectedindexchanged="DDLCustname_SelectedIndexChanged">
                                    </asp:DropDownList>
                                               
                                    <asp:Label ForeColor="Black" Font-Size="11px" ID="lblCustID"        runat="server" Text="00"></asp:Label>            
                                    <asp:Label ForeColor="Black" Font-Size="11px" ID="lblCustContact"   runat="server" Text="121"></asp:Label>
                                </td>
                            </tr>

                        </table>
                     </div>            
         
    </div>
        <div class="panel-footer"> 
                <asp:Button ID="btnClosePayment" class="btn btn-danger btn-sm" runat="server" Text="Back" />
               
               <asp:Button ID="bntPay" class="btn btn-primary btn-sm" runat="server" OnClick="bntPay_click" ValidationGroup="vr12"  Text="Submit" />
                
        </div>
</asp:Panel>
    </div>
    </form>
</body>
</html>
