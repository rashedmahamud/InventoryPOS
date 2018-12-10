<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="Returnhistory.aspx.cs" Inherits="Accounts_Sales_Returnhistory" %>

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
<!-- END THEME STYLES -->
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
    <style type="text/css">
    .header-center{
        text-align:center;
    }
</style>
 <%--   <script type="text/javascript">

        function printing() {
            window.print();


        }
    </script>--%>
    <link type="text/css" href="css/ui-lightness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
<script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>
     <script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script> 
<script type="text/javascript">
    $(function () {
        $("#TextBox6").datepicker();
    });
</script>
<style type="text/css">
.ui-datepicker { font-size:8pt !important}
</style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"> 

   
          <div > 
                <asp:Label ID="l1" Visible="false" runat="server" Text="Label"></asp:Label>
  <input type="button" class="btn btn-success btn-xs"  value="Return Report" style="width:100%" />  
                  
</div>
    <div > 
            <div  >
                <br />
                 <div style="margin-left:30%" > Store or Invoice Number  :   
            
                       <asp:TextBox ID="txtsearch"   Width="320px"   
                                ToolTip="Search by : Invoice No Or Store Number"   Placeholder="Search" runat="server" AutoPostBack="True" 
                                ontextchanged="txtsearch_TextChanged"></asp:TextBox>
                     <br /><br />
                                    Start Date:    <atk:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateFrom" />
        <asp:TextBox ID="txtDateFrom" runat="server" ToolTip="Your Starting Date"   
        placeholder="Starting Date" AutoPostBack="True" 
        ontextchanged="txtDateFrom_TextChanged"></asp:TextBox>

    End Date:    <atk:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDateTo" />
        <asp:TextBox ID="txtDateTo" runat="server"  ToolTip="End Date"  AutoPostBack="True"   
        placeholder="End Date"></asp:TextBox> 
     
         
                         </div><br />
                   <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearch"
                    MinimumPrefixLength="1" EnableCaching="true"      CompletionSetCount="1" CompletionInterval="100" 
                    ServiceMethod="GetMDN" FirstRowSelected="True">
                    </atk:AutoCompleteExtender>   
                          
            </div>
    
        <div >       
         <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript: printDiv('wrapper')" />
                          <asp:Button ID="printButton" runat="server" Text="Print" OnClientClick="javascript:window.print('wrapper');" />           

                <div id="wrapper" >
                           
                            <input type="button" class="btn btn-success btn-xs"  style="width:100%" />    
                    
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="100%" Width="100%">
                        <div  style="width:100%;text-align:center">
                                                       <span style="text-align:center"> 
                                <asp:Label ID="lblshopTitle" Font-Size="23px" runat="server" Text=""  Font-Names="High Tower Text"></asp:Label> <br />
                                <asp:Label ID="lblshopAddress"  Font-Size="11px" runat="server" Text=""></asp:Label>  <br />
                                <asp:Label ID="lblwebAddress"  Font-Size="11px"  runat="server" Text=""></asp:Label> <br />

                                <asp:Label ID="Label16" Font-Size="11px" runat="server" Text="Store Number:"></asp:Label> <asp:Label ID="Label1" Font-Size="11px" runat="server" Text=""></asp:Label> <br />
                               Phone: <asp:Label ID="lblPhone" Font-Size="11px" runat="server" Text=""></asp:Label>  <br />
                                  <asp:Label ID="lblDatetime" Font-Size="11px" runat="server" Text=""></asp:Label> 
                               </span>   
                        <%-- <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label> --%>
                        
                                                      
                                                    Date:   <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                                                       <hr /> 
                            <asp:GridView ID="grdviewReturnReport" runat="server" Font-Size="17px" CssClass="Grid" GridLines="None" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerstyle"  HeaderStyle-VerticalAlign="Top" HeaderStyle-BackColor="silver"  ShowFooter="True" Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                                                 
                                              
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" CssClass="headerstyle" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Top" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle ForeColor="#000066" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                                 
                                              
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
         </div>
</div>
    </asp:Content>




