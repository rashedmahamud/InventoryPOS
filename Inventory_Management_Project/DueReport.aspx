<%@ Page Language="C#" AutoEventWireup="true" Inherits="DueReport" Codebehind="DueReport.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customers Due Information  </title>
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
    <link href="mydatagrid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
    <div>

    </div>
        <div  style="width:100%">
        <div class="col-lg-12">
        <div class="panel panel-primary"> 
     <%--    <div class="panel-heading" ><span  class="fa fa-list"> </span> Users list </div>--%>
          <div class="panel-body">
              <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label>
              <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical"  Width="100%"> <br />
        <asp:DataList ID="DueCollection" runat="server" Font-Names="Verdana" Font-Size="Small"  RepeatLayout="Flow"   RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
              <div class="col-md-6">  
                <div id="pricePlansmsg">
                    <ul id="plans">
                        <li class="plan" > 
                         
                                        <div class="col-md-4">  
                                            <ul class="planContainer"> 
                                                <li class="title">
                                                  Store Number :  <asp:Label   ID="lblID" Font-Size="14px" runat="server" Text='<%# Eval("ShopId") %>'></asp:Label>                                                
                                                </li>
                                                <li>
                                                    
                                                <b> Customer Name :</b>
                                                    <br />
                                                    <asp:Label    ID="Label1" Font-Size="14px" class="img-circle"  Width="100px" Height="100px" runat="server" Text='<%# Eval("CustName") %>'></asp:Label>                                                

                                                    
                                               
                                                </li> 
                                                 <asp:LinkButton ID="LinkEdit" runat="server"   Font-Size="15px"  ForeColor="Red"     Font-Bold="true"    ToolTip="Collect Due" class="fa fa-edit"    OnClick="LinkEdit_Click"  />                            
                                                
                                            </ul>
                                        </div> 
                                         <div class="col-md-8">   
                                            <ul class="planContainer">
                                             <li> 
                                                <ul class="options" style="text-align:left">     
                                                 <li  >  <b>Customer Number :</b> <asp:Label   ID="CustID" Font-Size="14px" runat="server" Text='<%# Bind("CustID") %>'></asp:Label> 
                                                            <br />                      
                                                    Due Amount: <span> <asp:Label ID="Label9" runat="server" Text='<%# Bind("dueAmount") %>'></asp:Label>  </span> <br />
                                                     Cust Contact: <span> <asp:Label ID="Label11" runat="server" Text='<%# Bind("CustContact") %>'></asp:Label>  </span> <br />
                                                     Shipping Address: <span> <asp:Label ID="Label12" runat="server" Text='<%# Bind("shippingaddress") %>'></asp:Label>  </span> <br /> </li>
                                                     Order date: <span> <asp:Label ID="Label2" runat="server" Text='<%# Bind("ordedate") %>'></asp:Label>  </span> <br /> </li>
                                                     Comment: <span> <asp:Label ID="Label3" runat="server" Text='<%# Bind("comment") %>'></asp:Label>  </span> <br /> </li>
                                                     Note: <span> <asp:Label ID="Label4" runat="server" Text='<%# Bind("note") %>'></asp:Label>  </span> <br /> </li>
                                                     Served By: <span> <asp:Label ID="Label5" runat="server" Text='<%# Bind("ServedBy") %>'></asp:Label>  </span> <br /> </li>

                                                </ul> 
                                            </li>
                                           
                                            </ul>
                                         </div>
                                
                        </li>
                    </ul>
                </div> 
            </div>         
        </ItemTemplate>
    </asp:DataList>              
</asp:Panel>
     </div>
    </div>
</div>
        </div>
<%--<asp:Button ID="btnShowPopup" runat="server" style="display:none" />      
    <atk:ModalPopupExtender ID="ModalPopupPayment" runat="server" TargetControlID="LinkEdit" 
    PopupControlID="pnlpopupPayment"  CancelControlID="btnClosePayment" BackgroundCssClass="modalBackground">
    </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupPayment"  class="panel panel-primary" runat="server" BackColor="White" style="display:none;text-align:left"   DefaultButton="bntPay" > 
    <asp:Label ID="Label2" runat="server" Font-Size="19px" Text="Mobile Number"></asp:Label>    <hr />
    <div class="panel-body">
      
                    <div class="panel-SR-mainbody">                   
                  
                       <table style="vertical-align:top;width:100%" >
                     
                             
                              <tr style="visibility:hidden">
                                  <td>ID:<asp:TextBox ID="TextBox1" AutoPostBack="true" runat="server"></asp:TextBox></td>
                              </tr>
                

                                               <tr>
                                                   <td style="text-align:left">
                                                       <asp:Label ID="Label6" runat="server" Text="Customer Name">
                                                           <asp:Label ID="Label7" runat="server" Text=""></asp:Label></asp:Label>
                                                   </td>
                                                   <td style="text-align:left">
                                                       <asp:Label ID="Label8" runat="server" Text="Customer Number">
                                                           <asp:Label ID="Label10" runat="server" Text=""></asp:Label></asp:Label>
                                                   </td>
                                                    
                                               </tr>
                                               <tr >
                                                  <td style="text-align:left">
                                                       <asp:Label ID="Label13" runat="server" Text="Phone Number ">
                                                           <asp:Label ID="Label14" runat="server" Text=""></asp:Label></asp:Label>
                                                   </td>
                                                    <td style="text-align:left">
                                                       <asp:Label ID="Label15" runat="server" Text="Order Date ">
                                                           <asp:Label ID="Label16" runat="server" Text=""></asp:Label></asp:Label>
                                                   </td>

                                               </tr>
                                               <tr style="text-align:left">
                                                   <td style="text-align:left">
                                                       <asp:Label ID="Label17" runat="server" Text="Due Amount ">
                                                           <asp:Label ID="Label18" runat="server" Text=""></asp:Label></asp:Label>
                                                   </td>
                                               </tr>
                                               
                           </table>
                     </div>            
         
    </div>
        <div class="panel-footer"> 
                <asp:Button ID="btnClosePayment" class="btn btn-danger btn-sm" runat="server" Text="Back" />         
               <asp:Button ID="bntPay" class="btn btn-primary btn-sm" Visible="false" runat="server"  ValidationGroup="vr12"  Text="Pay" />
               
        </div>
</asp:Panel>--%>

    </div>
    </form>
</body>
</html>
