<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="ItemwiseSalesReport.aspx.cs" Inherits="Sales_ItemwiseSalesReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
          <title>
           REPORT- ITEM WISE 
        </title>
         <script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script> 
    <link href="../mydatagrid.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <div > 
                <asp:Label ID="l1" Visible="false" runat="server" Text="Label"></asp:Label>
               
                  
</div>

    <div class="divTable">
<div class="divTableBody">
<div class="divTableRow">
<div class="divTableCell">  
    <div  style="text-align:center" >
                 <div  style="text-align:center" > 
            <table style="vertical-align:top;text-align:center">  

                <tr>
                    <td>Store Number  :   </td>
                    <td>
                          <asp:TextBox ID="txtsearch"   Width="300px"   
                                ToolTip="Search by :  Store Number"   Placeholder="Search" runat="server" AutoPostBack="True" 
                                ontextchanged="txtsearch_TextChanged"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td> Scan Barcode :</td>
                    <td>
                  
                       <asp:TextBox ID="TextBox1"   Width="300px"   
                                ToolTip="Search by :  Barcode"   Placeholder="Search" runat="server" AutoPostBack="True" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                    Start Date: 
                    </td>
                    <td>
  <atk:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateFrom" />
        <asp:TextBox ID="txtDateFrom" runat="server" ToolTip="Your Starting Date"   
        placeholder="Starting Date" AutoPostBack="True" 
        ontextchanged="txtDateFrom_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>End Date:</td>
                    <td>
                          <atk:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateTo" />
        <asp:TextBox ID="txtDateTo" runat="server"  ToolTip="End Date"  AutoPostBack="True"   
        placeholder="End Date" ontextchanged="txtDateTo_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td> <asp:Button ID="Button8" runat="server" Text="Search" OnClick="Button8_Click" /><input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript: printDiv('wrapper')" /> </td>
                </tr>
            </table>
 
                </div><br />
                   <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearch"
                    MinimumPrefixLength="1" EnableCaching="true"      CompletionSetCount="1" CompletionInterval="100" 
                    ServiceMethod="GetMDN" FirstRowSelected="True">
                    </atk:AutoCompleteExtender>   
                         
            </div>

</div>
<div class="divTableCell" style="vertical-align:top">

      <div >
                           
                    
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="100%" Width="100%">
                        <div id="wrapper" style="width:100%; text-align:center">
                                                         <div style="text-align:center"><h1 >ITEM WISE SALES REPORT  </h1></div>
 
                               <span style="text-align:center"> 
                              
                                                             <asp:Label ID="lblshopTitle" Font-Size="23px" runat="server" Text=""  Font-Names="High Tower Text"></asp:Label> <br />
                                <asp:Label ID="lblshopAddress"  Font-Size="11px" runat="server" Text=""></asp:Label>  <br />
                                <asp:Label ID="lblwebAddress"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br />

                                <asp:Label ID="Label16" Font-Size="11px" runat="server" Text="Store Number:"></asp:Label> 
                                <asp:Label ID="lblPhone" Font-Size="11px" runat="server" Text=""></asp:Label>  <br />
                                  <asp:Label ID="lblDatetime" Font-Size="11px" runat="server" Text=""></asp:Label> 
                               </span>   
                         <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label> 
                                                
                                                    Date:   <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                                      
                          <h3>Total Sales  : <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Green" Font-Bold="true"></asp:Label></h3> 
                         <h3> Total Profit :  <asp:Label ID="Label2" runat="server" Text="Label" ForeColor="Green" Font-Bold="true"></asp:Label></h3>
                            <asp:GridView ID="grdviewSalesReport" runat="server"   
                                Font-Size="14px" CssClass="Grid" 
                             onrowdatabound="grdviewSalesReport_RowDataBound"   ShowFooter="True" Width="100%">
                                                 
                                              
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
</div>
</div>
</div>
</div>
</asp:Content>
