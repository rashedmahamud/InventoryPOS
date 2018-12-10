<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage/Bootstrap.master" AutoEventWireup="true" CodeFile="Purchase_Invoice.aspx.cs" Inherits="Purchase_module_Purchase_Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
<script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div   class="col-lg-9 col-lg-offset-1">
        <div style="text-align:left"     class="col-lg-6">   Purchase Invoice     </div>
        <div style="text-align:right"    class="col-lg-6">      
           <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"   OnClientClick="JavaScript: window.history.back(1); return false;"> </asp:button>                 
           | <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript:printDiv('wrapper')" />           
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
                           <asp:Label ID="Label1" Font-Size="23px" runat="server" ForeColor="BlueViolet" Text="Invoice"> </asp:Label> <br />            
                           <asp:Label ID="lblInvoiceNo"     Font-Size="14px" runat="server" Text="--"></asp:Label>  <br />
                           <asp:Label ID="lbldate"          Font-Size="12px"  runat="server" Text=""></asp:Label> <br />
                            <asp:Label ID="lblSupplier"     Font-Size="12px"  runat="server" Text=""></asp:Label> <br /> <br /> 
                        </td>
                        </tr>
                </table> 
    </div>
      
    <div  id="AddressInfo" style="text-align:left;padding-left:15px">  
                 <table class="style1" style="width:431px">
                    <tr>
                         <td style="width:305px">
                            <asp:Label ID="Label11" Font-Size="15px" runat="server" BackColor="Blue" ForeColor="White" Text="From "  Font-Names="High Tower Text"></asp:Label> <br />
                            <asp:Label ID="lblcompany" Font-Size="17px" runat="server" Text="-"  ></asp:Label> <br />
                            <asp:Label ID="lblcomaddr"  Font-Size="11px" runat="server" Text="-"></asp:Label>  <br />            
                            
                            <asp:Label ID="lblemailaddress"  Font-Size="11px"  runat="server" Text=""></asp:Label>   <br />  
                            <asp:Label ID="Label5" Font-Size="11px" runat="server" Text="Phone:"></asp:Label> 
                            <asp:Label ID="lblcontact" Font-Size="11px" runat="server" Text=""></asp:Label> <br /><br />
                           
                        </td>
         
              <td style="text-align:left">
                <asp:Label ID="Label7" Font-Size="15px" runat="server" Text="To"  BackColor="Blue" ForeColor="White"   Font-Names="High Tower Text"></asp:Label> <br />
            
                <asp:Label ID="lblSuppliername"  Font-Size="15px" Font-Bold="true" runat="server" Text="--"></asp:Label>  <br />
                <asp:Label ID="lblcompanyName"  Font-Size="13px" runat="server" Text="--"></asp:Label>  <br />
                <asp:Label ID="lblsuppaddress"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br />
           
                <asp:Label ID="lblEmailaddr"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br /> 
                <asp:Label ID="Label2" Font-Size="11px" runat="server" Text="Phone:"></asp:Label> 
                <asp:Label ID="lblphoneno"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br /> 
              </td>
        </tr>
     </table> 
 </div>          
                <div    class="col-lg-12" >  <br /> 
                       <asp:GridView ID="grdItemList" runat="server"   
                        class="table table-striped table-hover" Font-Size="11px" ShowFooter="True" 
                        ShowHeaderWhenEmpty="True" onrowdatabound="grdItemList_RowDataBound"></asp:GridView>
                </div>
            <br />

                        
                <div style="text-align:left"    class="col-lg-12">                    
                    <div class="panel"> <u> Other Comments or special instructions</u> <br />
                    <asp:Label ID="lblComment" Font-Size="12px" runat="server" Text="--"></asp:Label></div>
                </div>

                 <div     class="col-lg-12"  style="text-align:center">                      
                    <div class="panel panel-info">
                            If you have any questions about this <b>Purchase invoice </b>, please contact<br />
                             <asp:Label ID="lblEmail" Font-Size="12px" runat="server" Text="-"></asp:Label><br />
                            Thanks you for your Business!
                    </div>
                </div>

   </div>   
 </div>
</asp:Content>

