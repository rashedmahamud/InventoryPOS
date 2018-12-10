<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="POS_printPage.aspx.cs" Inherits="POS_printPage" %>

<html>
    <head runat="server">
        <title></title>
   
     
    <style type="text/css">
        .style1
        {
             
            color:Black;
           width: 280px;
          /*  font-family:SimSun; */
        }
    </style>
    <style type="text/css" media="all">
        body 
        {    
            color:#000; 
            font-family: Arial, Helvetica, sans-serif; 
            
        }
        #wrapper 
        {  
            width: 280px;
            margin: 0 auto; 
        } 

        .style2
        {
            width: 100%;
            font-size:10px;
        }

        .auto-style1 {
            height: 206px;
        }

    </style>
<%--         <script type="text/javascript" >
             function changeScreenSize(w, h)
             {
                 window.resizeTo(w, h)
             }
</script>--%>
        <link href="../thermal.css" rel="stylesheet" />
          <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/3.0.3/normalize.css">
  <link rel="stylesheet" href="../paper.css">
<%--  <style>@page { size: 58mm 100mm }</style>--%>
<script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script> 
     </head>
<body >
    
    <form runat="server">
<div class="col-lg-3 col-lg-offset-1">
             <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"
            OnClientClick="JavaScript: window.history.back(1); return false;">
            </asp:button> | 
            <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript:printDiv('wrapper')" />
             <asp:Label ID="Label22" runat="server" Text="Label"></asp:Label>
          <hr />

    <div  id="wrapper" style="text-align:left">       
 
            <table class="style1">
                <tr>
                    <td style="text-align:center">
                        <asp:Label ID="Label18" Font-Size="20px" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                        <br />
                        <asp:Label ID="Label19" Font-Size="11px" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                        <br />
                        <asp:Label ID="Label20" Font-Size="11px" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                        <br />

                        <asp:Label ID="Label16" Font-Size="11px" runat="server" Text="Phone:"></asp:Label> 
                        <asp:Label ID="Label21" runat="server" Font-Size="11px" Text="Label"></asp:Label>
                     <br /> <br />  </td>
                </tr>  
                  <tr>
                    <td>                       

                        <asp:Label ID="Label1" Font-Size="11px" runat="server" Text="Date:"></asp:Label>
                        <asp:Label ID="lblDatetime" Font-Size="11px" runat="server" Text=""></asp:Label> <br />                                               
                       
                        <table class="style2">
                            <tr>
                                <td>
                        <asp:Label ID="Label3" runat="server"  Font-Size="11px" Text="ABN:"></asp:Label>
                        <asp:Label ID="lblVATRegiNo"  Font-Size="11px" runat="server" Text=""></asp:Label> 
                                </td>
                                <td>

                        <asp:Label ID="Label4" runat="server"  Font-Size="11px" Text="Shop ID:"></asp:Label> 
                        <asp:Label ID="lblShopID" runat="server"  Font-Size="11px" Text=""></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td>

                       
                        <asp:Label ID="Label2" runat="server"  Font-Size="11px" Text="Customer ID:"></asp:Label>
                        <asp:Label ID="lblCustID" runat="server"  Font-Size="11px" Text=""></asp:Label> 
                                </td>
                                <td>

                          <asp:Label ID="Label6"  Font-Size="11px" runat="server" Text="Contact No:"></asp:Label> 
                        <asp:Label ID="lblCustContactNo" Font-Size="11px"  runat="server" Text=""></asp:Label> 
                                </td>
                            </tr>
                            <tr>
                                <td>

                       
                        <asp:Label ID="Label5"  Font-Size="11px" runat="server" Text="Name:"></asp:Label> 
                        <asp:Label ID="lblCustName"  Font-Size="11px" runat="server" Text=""></asp:Label> 
                                </td>
                                <td>

                        <asp:Label ID="Label7" Font-Size="11px"   runat="server" Text="ServedBy:"></asp:Label> 
                        <asp:Label ID="lblServedBy" Font-Size="11px"   runat="server" Text=""></asp:Label> 
                                </td>
                            </tr>
                        </table> <br />
                        
                        <asp:Label ID="Label15" Font-Size="11px"  runat="server" Text="Invoice #"></asp:Label>  
                        <asp:Label ID="lblInvoice" Font-Size="11px"  runat="server" Text="1003"></asp:Label> 
                        <br />                       
                        <asp:GridView ID="grdItemList" Font-Size="11px" Width="100%" runat="server"  
                            OnRowDataBound="grdItemList_RowDataBound" BackColor="White" 
                            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                            ForeColor="Black" GridLines="Horizontal"> </asp:GridView> 
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1">
                        <table class="style1">
                           
                            <tr>    
                                <td > <asp:Label ID="Label17"  Font-Size="11px" runat="server" Text="Total Sales Qty:"></asp:Label> <br /></td>
                                <td  style="text-align: right;" class="text-right">  <asp:Label ID="lblTotalQty" Font-Bold="true"  Font-Size="11px" runat="server" Text="0"></asp:Label> <br /> </td>                                                            
                             </tr>
                            <tr>    
                                <td> <asp:Label ID="Label14"  Font-Size="11px" runat="server" Text="Total Amount:"></asp:Label>  </td>
                                <td  class="text-right" style="text-align: right">  <asp:Label ID="lblsubTotal" Font-Bold="true"  Font-Size="11px" runat="server" Text="0"></asp:Label>   </td>                                                            
                            </tr>


                            <tr>
                                 <td style="border-bottom-style: solid; border-bottom-width: thin;" >
                                 <asp:Label   Font-Size="11px" ID="lblvatRate" runat="server" Text="0"></asp:Label></td>
                                 <td style="border-bottom-style: solid; border-bottom-width: thin;text-align: right;" class="text-right"><asp:Label Font-Size="11px"  ID="lblvat" runat="server" Text="-"></asp:Label></td>
                            </tr>
                            <tr> 
                                <td ><asp:Label ID="Label9"  Font-Size="12px" runat="server" Text="Net Amount:"></asp:Label>  <br /> <br />     </td>
                                <td style="text-align: right;" class="text-right"> 
                                <asp:Label ID="lbltotalpay"  Font-Size="12px"  Font-Bold="true" runat="server" Text="0"></asp:Label>   <br /> <br />   </td>
                            </tr>
                            <tr>    
                                <td><asp:Label ID="Label8"  Font-Size="11px" runat="server" Text="Pay Type:"></asp:Label>  <br />   </td>
                                <td class="text-right" style="text-align: right"><asp:Label ID="lblpaidby" Font-Size="11px" runat="server" Text="0"></asp:Label> <br />    </td>                                                            
                            </tr>

                            <tr>
                                <td><asp:Label ID="Label10"  Font-Size="11px" runat="server" Text="Paid Amount"></asp:Label> </td>
                                <td class="text-right" style="text-align: right"><asp:Label ID="lblPaidAmt" Font-Size="11px" runat="server" Text="0"></asp:Label></td>                               
                            </tr>

                            <tr>
                                <td><asp:Label ID="Label11"  Font-Size="11px" runat="server" Text="Changed Amount:"></asp:Label> <br /> <br /></td>
                                <td class="text-right" style="text-align: right"><asp:Label ID="lblChange" Font-Size="11px" runat="server" Text="0"></asp:Label> <br /> <br /></td>                               
                            </tr>

                             <tr>
                                <td style="border-bottom-style: solid; border-bottom-width: thin;">
                                <asp:Label ID="Label12"  Font-Size="11px" runat="server" Text="Due Amount:"></asp:Label> </td>
                               <td style="border-bottom-style: solid; border-bottom-width: thin;text-align: right">
                                <asp:Label ID="lblDue" Font-Size="11px" runat="server" Text="0"></asp:Label> </td>                               
                            <div style="border-top:1px solid #000; padding-top:10px;"></div></tr>
                        </table>
                       
                     <span style="text-align:center"> 
                            <asp:Label  ID="lblFooterMessage"  Font-Size="12px" runat="server" Text="-------------------"></asp:Label>
                          </span>                 
                        
                  </td>
                </tr>

            </table>
          
     </div>
 </div>
 <div class="col-lg-5 col-lg-offset-2">

  </div> 
    </form>
     </body>       
</html>

