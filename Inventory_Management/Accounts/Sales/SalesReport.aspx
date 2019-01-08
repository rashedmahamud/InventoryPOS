<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile= "~/Accounts/MasterPage.master" CodeFile="SalesReport.aspx.cs" Inherits="Sales_SalesReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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


          <div >
                <asp:Label ID="l1" Visible="false" runat="server" Text="Label"></asp:Label>
  <input type="button111111" class="btn btn-success btn-xs"  value="Daily Sales Report" style="width:100%" />

</div>
    <div >
            <div  >
                <br />
                 <div style="margin-left:30%" > Store or Invoice Number  :

                       <asp:TextBox ID="txtsearch"   Width="320px"
                                                ToolTip="Search by : Invoice No Or Store Number"   Placeholder="Search" runat="server" AutoPostBack="True"
                                                ontextchanged="txtsearch_TextChanged"></asp:TextBox>
                                     <br />
                                    <br />
                       Start Date:    <atk:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateFrom" />
                        <asp:TextBox ID="txtDateFrom" runat="server" ToolTip="Your Starting Date"
                        placeholder="Starting Date" AutoPostBack="True"
                        ontextchanged="txtDateFrom_TextChanged"></asp:TextBox>

                    End Date:    <atk:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateTo" />
                        <asp:TextBox ID="txtDateTo" runat="server"  ToolTip="End Date"  AutoPostBack="True"
                        placeholder="End Date" ontextchanged="txtDateTo_TextChanged"></asp:TextBox>

                           <asp:Button ID="Button3" runat="server" Text="Print Due Invoice" class="btn btn-danger btn-xs" OnClick="Button3_Click"/>
                    </div>
                <br />
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="294px" Width="936px"></rsweb:ReportViewer>


                   <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearch"
                    MinimumPrefixLength="1" EnableCaching="true"      CompletionSetCount="1" CompletionInterval="100"
                    ServiceMethod="GetMDN" FirstRowSelected="True">
                    </atk:AutoCompleteExtender>

            </div>

        <div >

                <div >

                            <input type="button" class="btn btn-success btn-xs"  style="width:100%" />

                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="100%" Width="100%">
                        <div id="wrapper" style="width:100%;text-align:center">
                                                       <span style="text-align:center">
                                <asp:Label ID="lblshopTitle" Font-Size="23px" runat="server" Text=""  Font-Names="High Tower Text"></asp:Label> <br />
                                <asp:Label ID="lblshopAddress"  Font-Size="11px" runat="server" Text=""></asp:Label>  <br />
                                <asp:Label ID="lblwebAddress"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br />

                                <asp:Label ID="Label16" Font-Size="11px" runat="server" Text="Store Number:"></asp:Label>
                                <asp:Label ID="lblPhone" Font-Size="11px" runat="server" Text=""></asp:Label>  <br />
                                  <asp:Label ID="lblDatetime" Font-Size="11px" runat="server" Text=""></asp:Label>
                               </span>
                         <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label>
                                                       <br />
                                                       <br />
                                                    Date:   <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                                       <hr />
                            <asp:GridView ID="grdviewSalesReport" runat="server"
                                Font-Size="17px" CssClass="Grid"  GridLines="None" HeaderStyle-BackColor="silver"
                                onrowdatabound="grdviewSalesReport_RowDataBound" ShowFooter="True" Width="100%">


                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
         </div>
</div>
    </asp:Content>




