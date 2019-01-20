<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="Order_history.aspx.cs" Inherits="Order_module_Order_history" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="col-lg-12">
            <div class="well well-sm" >
                <div class="col-lg-3" style="text-align:left" >
                    <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"
                    OnClientClick="JavaScript: window.history.back(1); return false;">
                    </asp:button>
                </div>
                <div class="col-lg-5" style="text-align:center" >
                  <b>   Order History  </b>
                </div>
                <div class="col-lg-4" style="text-align:right" >
                 <asp:Button ID="btnItems"   CssClass="btn btn-warning btn-xs" ToolTip="Create new Order"  runat="server" Text="Create new Order"   PostBackUrl="~/Order_module/NewOrder.aspx" />
                 | <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript:printDiv('wrapper')" />    </div><br />

                   <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearch"
                    MinimumPrefixLength="1" EnableCaching="true"      CompletionSetCount="1" CompletionInterval="100"
                    ServiceMethod="GetMDN" FirstRowSelected="True">
                    </atk:AutoCompleteExtender>

            </div>

        <div class="panel panel-default">  <br />
            <span style="padding-left:15px" >
               <asp:TextBox ID="txtsearch"
                        ToolTip="Search by : Invoice No , Customer name"  Placeholder="Search" runat="server" AutoPostBack="True"
                        ontextchanged="txtsearch_TextChanged"></asp:TextBox>

                          <asp:DropDownList        ID="ddlpagesize" runat="server"  AutoPostBack="True"
                                onselectedindexchanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem Value="5" Selected="True" >5</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList> records per page
                </span>
                <div class="panel-body">

                    <asp:Label ID="lbtotalRow" runat="server" Text=""></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="350px">
                        <div id="wrapper">
                            <asp:GridView ID="grdItemList" runat="server"   class="table table-striped table-hover" Font-Size="11px"
                             AllowPaging="True"  onpageindexchanging="grdItemList_PageIndexChanging"   >
                              <Columns>
                                  <asp:TemplateField HeaderText="Action" HeaderStyle-Width="250px">
                                        <ItemTemplate>
                                          <asp:LinkButton    ID="linkViewInvoice" runat="server"  Text=" View Invoice "   ToolTip=" View Order Invoice and Take Due Payment"   class="btn btn-primary btn-xs"  OnClick="linkViewInvoice_Click" />
                                          | <asp:LinkButton    ID="lnkChangeStatus" runat="server"    Text=" Change Status "   ToolTip=" Change Status" class="btn btn-info btn-xs"  OnClick="lnkChangeStatus_Click" />

                                       </ItemTemplate>
                                        <HeaderStyle Width="250px" />
                                  </asp:TemplateField>
                                  </Columns>


                            <PagerSettings      FirstPageText="First" LastPageText="Last"
                                Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="10" PreviousPageText="Previous" />

                            <PagerStyle Font-Bold="true" Font-Size="Large"    HorizontalAlign="Center"   />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </div>
         </div>
</div>









</asp:Content>


