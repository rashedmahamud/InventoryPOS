<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master"AutoEventWireup="true" CodeFile="Purchase_InvoiceCustomer.aspx.cs" Inherits="Customers_Purchase_InvoiceCustomer" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>Sale Invocie</title>
    <script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<div   class="col-lg-9 col-lg-offset-1"> <p></p>

        <div style="text-align:left"     class="col-lg-6">   
        Your Purchase Invoice  |  <asp:Label ID="lblInvoiceNoTop"  runat="server" Text="" Font-Bold="True"> </asp:Label> |
         Due:  <asp:Label ID="lblDuemanount" runat="server"  Text="" Font-Bold="True">  </asp:Label>  </div>

        <div style="text-align:right"    class="col-lg-6">   
           <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"   OnClientClick="JavaScript: window.history.back(1); return false;"> </asp:button>                 
             |
            <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript:printDiv('wrapper')" />           
        </div><hr />

        <div class="panel panel-info"></div>
    <div  id="wrapper" style="text-align:left">  
      <div  id="HeaderInfo" style="text-align:left;padding-left:15px">  
                 <table class="style1" style="width:700px">
                    <tr>
                         <td>
                            <asp:Label ID="lblshopTitle" Font-Size="23px" runat="server" Text="-"  Font-Names="High Tower Text"></asp:Label> <br />
                            <asp:Label ID="lblshopAddress"  Font-Size="12px" runat="server" Text="-"></asp:Label>  <br />
                            <asp:Label ID="lblwebAddress"  Font-Size="12px"  runat="server" Text=""></asp:Label> <br />

                            <asp:Label ID="Label16" Font-Size="12px" runat="server" Text="Phone:"></asp:Label> 
                            <asp:Label ID="lblPhone" Font-Size="13px" runat="server" Text=""></asp:Label> <br />
                             <asp:Label ID="Label3" Font-Size="12px" runat="server" Text=""></asp:Label> <br /> 
                        </td>
         
                         <td style="text-align:right">
                         <asp:Label ID="Label4" Font-Size="12px" Font-Bold="true" runat="server"  Text="Purchase"> </asp:Label><br /> 
                           <asp:Label ID="Label1" Font-Size="23px" runat="server" ForeColor="BlueViolet" Text="Invoice"> </asp:Label> <br />
            
               Invoice #    <asp:Label ID="lblInvoiceNo"     Font-Size="14px" runat="server" Text="--" Font-Bold="True"></asp:Label>  <br />
               Date #       <asp:Label ID="lbldate"          Font-Size="12px"  runat="server" Text=""></asp:Label> <br />
                            
                           Payment Type: <asp:Label ID="lblpayType"          Font-Size="12px"  runat="server" Text=""></asp:Label> <br />
                            <br />
                        </td>
                    </tr>
                </table> 
    </div>
      
    <div  id="AddressInfo" style="text-align:left;padding-left:15px">  
                 <table  >
                    <tr>
                         <td class="style2" style="width:313px">
                              <asp:Panel ID="Panel1" BackColor="Blue" Width="250" runat="server">
                             <asp:Label ID="Label11" Font-Size="15px" runat="server" Font-Bold="true"   Font-Underline="true"  ForeColor="White" Text="From "  Font-Names="High Tower Text"></asp:Label> <br />  
                             </asp:Panel>
                             <asp:Label ID="lblcompany" Font-Size="17px" runat="server" Text="-"  ></asp:Label> <br />
                            <asp:Label ID="lblcomaddr"  Font-Size="11px" runat="server" Text="-"></asp:Label>  <br />            
                            
                            <asp:Label ID="lblemailaddress"  Font-Size="11px"  runat="server" Text=""></asp:Label>   <br />  
                            <asp:Label ID="Label5" Font-Size="11px" runat="server" Text="Phone:"></asp:Label> 
                            <asp:Label ID="lblcontact" Font-Size="11px" runat="server" Text=""></asp:Label> <br /> 
                           
                        </td>
         
                      <td style="text-align:left;width:305px"  >
                        <asp:Panel ID="Panel2" BackColor="#339933" Width="200" runat="server">
                        <asp:Label ID="Label7" Font-Size="15px" runat="server" Text="To" Font-Bold="true"    Font-Underline="true" ForeColor="White"   Font-Names="High Tower Text"></asp:Label> <br />
                      </asp:Panel>
                        <asp:Label ID="lblCustname"  Font-Size="15px" runat="server" Text="--"></asp:Label>  <br />                       
                        <asp:Label ID="lblCustaddress"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br />
           
                        <asp:Label ID="lblEmailaddr"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br /> 
                        <asp:Label ID="Label2" Font-Size="11px" runat="server" Text="Phone:"></asp:Label> 
                        <asp:Label ID="lblphoneno"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br /> 
                      </td>
                      
               

        </tr>
     </table> 
 </div>          
           <%-- Sold item databind--%>
                <div    class="col-lg-12 " >  <br /> 
                       <asp:GridView ID="grdItemList" runat="server"   
                        class="table table-striped table-hover" Font-Size="12px"  
                        ShowHeaderWhenEmpty="True" onrowdatabound="grdItemList_RowDataBound"></asp:GridView>
                </div>
         
         <%--Received payment transaction list--%>
                <div    class="col-lg-12 " >   
                       <asp:GridView ID="grdpaymentlist" runat="server"   
                        class="table table-striped table-hover" Font-Size="10px"  
                        ShowHeaderWhenEmpty="True"></asp:GridView>
                </div>
              

                        
                <div style="text-align:right"    class="col-lg-12">                    
                    <div class="panel panel-info">  
                     Subtotal:   <asp:Label ID="lblsubtotal"  Font-Bold="true"  Font-Size="14px" runat="server" Text="----"></asp:Label><br />
                     VAT:   <asp:Label ID="lblvat" Font-Size="12px" runat="server" Text="----"></asp:Label><br />
                    <p> <u> Discount: </u> <asp:Label ID="lbldis" Font-Size="12px"  Font-Underline="true"  runat="server" Text="----"></asp:Label></p> 
                     Grand Total:   <asp:Label ID="lbltotal" Font-Bold="true" Font-Size="18px" runat="server" Text="----"></asp:Label><br />
                     Total Paid:   <asp:Label ID="lblpaid" Font-Size="13px" runat="server" Text="----"></asp:Label><br />
                     Due :   <asp:Label ID="lblDue"  Font-Size="15px"  BackColor="DarkGray" runat="server" Text="----"></asp:Label><br />      
                    </div>
                </div>


                <div style="text-align:center"    class="col-lg-12">                    
                    <div class="panel"> <u> Other Comments or special instructions</u> <br />
                        <asp:Label ID="lblComment" Font-Size="12px" runat="server" Text="----"></asp:Label>
                   </div>
                    
                </div>

                 <div     class="col-lg-12" style="text-align:center"   >                     
                    <div class="panel panel-info">
                            If you have any questions about this <b> Purchase invoice </b>, please contact<br />
                             <asp:Label ID="lblEmail" Font-Size="12px" runat="server" Text="-"></asp:Label><br />
                            Thanks you for your Business!
                    </div>
                </div>

   </div>   
</div>
 
</asp:Content>




