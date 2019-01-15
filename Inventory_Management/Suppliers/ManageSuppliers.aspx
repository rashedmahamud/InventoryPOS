<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="ManageSuppliers.aspx.cs" Inherits="Suppliers_ManageSuppliers" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--  <div class="col-lg-11"> --%>
            <div class="well well-sm" >
                <div class="col-lg-8">  <span style="text-align:left"> Manage Suppliers  </span> </div>
                 <span style="text-align:right">
                 <asp:Button ID="btnadd"   CssClass="btn btn-warning btn-xs" ToolTip="Create new Suppliers"  runat="server" Text="Create new Suppliers"   PostBackUrl="~/Suppliers/AddSupplier.aspx" />
                     <asp:Button ID="Button1"   CssClass="btn btn-success btn-xs" ToolTip="Print List of Supplier"  runat="server" Text="Print Suppliers List"   PostBackUrl="~/Suppliers/PrintSupplierList.aspx" />
                 </span>

                   <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearch"
                    MinimumPrefixLength="1" EnableCaching="true"      CompletionSetCount="1" CompletionInterval="100"
                    ServiceMethod="GetMDN" FirstRowSelected="True">
                    </atk:AutoCompleteExtender>

            </div>

        <div class="panel panel-default" >



                <div class="panel-body">
                           <asp:TextBox ID="txtsearch"
                        ToolTip="Search by : Supplier ID , supplier name"   Placeholder="Search" runat="server" AutoPostBack="True"
                        ontextchanged="txtsearch_TextChanged"></asp:TextBox> |

                <asp:DropDownList        ID="ddlpagesize" runat="server"  AutoPostBack="True"
                                onselectedindexchanged="ddlpagesize_SelectedIndexChanged">
                                <asp:ListItem Value="5" Selected="True" >5</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList> records per page
                    <asp:Label ID="lbtotalRow" runat="server" Text=""></asp:Label>  <br />    <br />
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="350px">
                        <asp:GridView ID="grdSupplierList" runat="server" ForeColor="#333333" GridLines="None"  ShowFooter="True"   class="table table-striped table-hover "
                         Font-Size="11px"  AllowPaging="True"      onpageindexchanging="grdSupplierList_PageIndexChanging"   >
                          <Columns>
                              <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkView" runat="server"  ForeColor="Red"  Font-Size="15px"   ToolTip="View Details" class="fa   fa-folder-open-o"     OnClick="Linkdtview_Click"   />  |
                                        <asp:LinkButton ID="btnEdit" runat="server"  ForeColor="Red"  Font-Size="15px"   ToolTip="Edit" class="fa fa-edit "     OnClick="LinkEdit_Click"   />  |
                                         <asp:LinkButton ID="LinkbanCustomer" runat="server" ForeColor="Red"   Font-Size="15px"   OnClick="LinkbanCustomer_Click"  ToolTip="Ban or Inactive Customer" class="fa fa-ban"  />
                                    </ItemTemplate>
                                     <HeaderStyle Width="100" />
                              </asp:TemplateField>
                              </Columns>
                                <PagerSettings      FirstPageText="First" LastPageText="Last"
                                    Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="10" PreviousPageText="Previous" />
                                <PagerStyle Font-Bold="true" Font-Size="Large"    HorizontalAlign="Center"   />
                                     <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                    </asp:Panel>
                </div>

         </div>
<%--</div>--%>

 <%--<<<<<<<<<<<<<<<<<<<<< --------------- Details view    Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>

   <!-- Modal -->
  <div class="modal fade" id="pnlpopupView" role="dialog">
    <div class="modal-dialog">

      <!-- Modal content-->
      <div class="modal-content"   style="width:100%;text-align:left" >
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">
                    <asp:Label ID="lblsuppid" runat="server" Text="-"></asp:Label>:   Details     Supplier

          </h4>
        </div>
        <div class="modal-body">
            <div  class="col-lg-4">
                    <div class="panel panel-info" style="text-align:left">
                            <div class="panel-heading">
                                    Basic info
                            </div>
                        <asp:Label ID="lblCompany" runat="server" Font-Size="20px" Text="-"></asp:Label> <br />
                        <asp:Label ID="lblname" runat="server" Text="-"></asp:Label> <br />
                        <asp:Label ID="lblEmail" runat="server" Text="-"></asp:Label> <br />
                         <asp:Label ID="lblphone" runat="server" Text="-"></asp:Label> <br />
                    </div>
             </div>
            <div  class="col-lg-8">
                      <div class="panel panel-warning" style="text-align:left">
                            <div class="panel-heading">
                                   Purchase History From  <asp:Label ID="lblsuppname" Font-Size="14px" runat="server" Text="-"></asp:Label>
                            </div>
                            <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Vertical">
                            <asp:GridView ID="grdviewpurchasehistory" runat="server"   class="table table-striped table-hover" Font-Size="11px">  </asp:GridView>
                            </asp:Panel>
                    </div>
             </div>
        </div>
        <div class="modal-footer">

	 <button type="button" class="btn btn-danger btn-sm"  data-dismiss="modal" aria-hidden="true">Close</button>
        </div>
      </div>

    </div>
  </div>
 <%--<<<<<<<<<<<<<<<<<<<<<END --------------- Details view   Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>





 <%--<<<<<<<<<<<<<<<<<<<<< --------------- Edit  Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
        <atk:ModalPopupExtender ID="MpeEditShow" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlpopupEditView"  CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupEditView"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="300px"  DefaultButton="btnSave">
      <div class="panel-heading">
          <asp:Label ID="lblID" runat="server" Text="Label"></asp:Label>: Edit Supplier
    </div>

    <div class="panel-body" style="text-align:left" >

        <asp:Label ID="Label1" runat="server" Font-Size="11px" Text="Supplier Name"></asp:Label><br />
        <asp:TextBox ID="txtName"    Font-Bold="true"  Width="275px"     runat="server"></asp:TextBox><br />

        <asp:Label ID="Label8" runat="server" Font-Size="11px" Font-Bold="true" Text="Company Name"></asp:Label><br />
        <asp:TextBox ID="txtCompanyName"      Width="275px"       runat="server"></asp:TextBox><br />

        <asp:Label ID="Label4" runat="server" Font-Size="11px" Text="Contact"></asp:Label><br />
        <asp:TextBox ID="txtContact"     Width="275px"     runat="server"></asp:TextBox><br />

        <asp:Label ID="Label2" runat="server" Font-Size="11px" Text="Email"></asp:Label><br />
        <asp:TextBox ID="txtEmail"     Width="275px"     runat="server"></asp:TextBox><br />



        <hr />
        <asp:Label ID="Label5" runat="server" Font-Size="11px" Font-Bold="true" Text="Supplier Type"></asp:Label><br />
        <asp:TextBox ID="txtType"      Width="275px"       runat="server"></asp:TextBox><br />

        <asp:Label ID="Label6" runat="server" Font-Size="11px" Text="City"></asp:Label><br />
        <asp:TextBox ID="txtcity"      Width="275px"       runat="server"></asp:TextBox><br />

         <asp:Label ID="Label3" runat="server" Font-Size="11px" Text="Address"></asp:Label><br />
        <asp:TextBox ID="txtaddress"  Font-Size="11px"   Width="275px"  TextMode="MultiLine"    runat="server"></asp:TextBox><br />

        <asp:Label ID="lblmsg" runat="server" Font-Size="11px" ForeColor="Red" Text="-"></asp:Label>

    </div>
    <div class="panel-footer">
        <asp:Button ID="btnSave" class="btn btn-success btn-sm" runat="server"   Text="Save"   onclick="btnSave_Click"  />
        <asp:Button ID="btnClose" class="btn btn-danger btn-sm" runat="server" Text="Close" />
    </div>
 </asp:Panel>
 <%--<<<<<<<<<<<<<<<<<<<<<END --------------- Edit   Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>


 <%--<<<<<<<<<<<<<<<<<<<<< --------------- Inactive user  Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
        <asp:Button ID="btnShowPopupInAct" runat="server" style="display:none" />
        <atk:ModalPopupExtender ID="ModalPopupInactive" runat="server" TargetControlID="btnShowPopupInAct"
        PopupControlID="pnlpopupInactive"  CancelControlID="btnCloseInactive" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupInactive"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="300px"  DefaultButton="btnInactive">
   <asp:Label ID="lblInactiveID" runat="server" Text="0"></asp:Label>.
    <asp:Label ID="lblInactiveSupplierName" runat="server" Text="0"></asp:Label>  <hr />
     <asp:Label ID="Label7" runat="server" class="label label-info" Text="Do you want to Inactive this Supplier ???"></asp:Label> <br />
 <br />
<div class="panel-footer">
        <asp:Button ID="btnInactive" class="btn btn-success btn-sm" runat="server"   Text="Yes"   onclick="btnInactive_Click"  />
        <asp:Button ID="btnCloseInactive" class="btn btn-danger btn-sm" runat="server" Text="No" />
 </div>
</asp:Panel>
</asp:Content>


