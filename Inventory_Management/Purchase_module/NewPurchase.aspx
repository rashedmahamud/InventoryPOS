<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="NewPurchase.aspx.cs" Inherits="Purchase_module_NewPurchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="col-lg-5">
    <div class="input-group">
        <span class="input-group-addon"> <span class="fa fa-search"> </span> </span>
        <asp:TextBox ID="txtItemSearch" runat="server"
            placeholder="Search to Scan Products" class="form-control"  ToolTip="Search by item code or item name or item barcode"
            ontextchanged="txtItemSearch_TextChanged" AutoPostBack="true"  ></asp:TextBox>
            <span class="input-group-addon"></span>
    </div>

    <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtItemSearch"
            MinimumPrefixLength="1" EnableCaching="true"   CompletionSetCount="1" CompletionInterval="100"
            ServiceMethod="GetMDN">
            </atk:AutoCompleteExtender>

    <div class="panel panel-primary" >
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="320px">

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
                Total =     <asp:Label ID="lbltotal" runat="server" Font-Bold="true" Text="-"></asp:Label> <br />

                <asp:Label ID="Label7" Font-Size="11px" runat="server" Text="Total Items"> </asp:Label>
                <asp:Label ID="lblTotalQty"  Font-Size="11px"  runat="server" Text="-"></asp:Label>

            </div>
    </div>

            <asp:Button ID="btnsuspen" runat="server" class="btn btn-danger" Text="Suspen"   onclick="btnsuspen_Click" />
            <asp:Button ID="btnPayment" runat="server" class="btn btn-success"
        Text="Payment" onclick="btnPayment_Click" />

</div>


<%--/////////<div class="input-group input-group-sm">//// Item list --%>

<div class="col-lg-7">
<div class="panel  panel-success">
<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="410px">  <br />
       <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small"  RepeatLayout="Flow"   RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
              <div class="col-md-4">
                <div id="pricePlansmsg">
                    <ul id="plans">
                        <li class="plan" >
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
                                <atk:NumericUpDownExtender ID="NumericUpDownExtender1"  runat="server"
                                TargetControlID="txtqty" Minimum="1" Maximum="555" Width="100"  ViewStateMode="Enabled">  </atk:NumericUpDownExtender>
                                <asp:TextBox ID="txtqty" runat="server" Font-Size="11px"  Text="1" ToolTip="Item Quantity"></asp:TextBox><br /><p></p>
                                 <asp:Button ID="btnGo"  runat="server"  Text="Add to Cart"    ValidationGroup="vG2"  Font-Size="12px"  ToolTip="Add to Cart" class="btn btn-success btn-xs" OnClick="btn_Goclick" />  <br />
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
</asp:Panel>
        <div class="panel-body">
            Search by Category:
            <asp:DropDownList ID="DDLCategory"   class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown"  runat="server"  AutoPostBack="true"  onselectedindexchanged="DDLCategory_SelectedIndexChanged">
                        </asp:DropDownList>
        </div>
    </div>
</div>



 <%--<<<<<<<<<<<<<<<<<<<<< --------------- payment panel Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
     <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
    <atk:ModalPopupExtender ID="ModalPopupPayment" runat="server" TargetControlID="btnShowPopup"
    PopupControlID="pnlpopupPayment"  CancelControlID="btnClosePayment" BackgroundCssClass="modalBackground">
    </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupPayment"  class="panel panel-primary" runat="server" BackColor="White" style="display:none;text-align:left"  Width="400px"  DefaultButton="bntPay" >
    <asp:Label ID="Label2" runat="server" Font-Size="19px" Text="  __Payment"></asp:Label>    <br />
    <div class="panel-body">
            <div class="panel-SR-mainbody">

                        <table class="style1" width="100%">
                            <tr>
                                <td class="style2"  >
                                    Total Payable:  <br /> <br />
                                </td>
                                <td>
                                    <asp:Label ID="lbltotalpay" runat="server" Font-Bold="true" Font-Size="15px"  Text="-"></asp:Label>  <br /><br />
                                </td>
                            </tr>
                            <tr>
                                <td class="style2">
                                    Paid by:  <br />    <br />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLPaidBy" runat="server" class="form-control">
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
                                    Payment: <br />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPaid" ToolTip="Customer paid Amount" runat="server" class="form-control"></asp:TextBox>

                                    <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtPaid" ValidationGroup="vr12"  Font-Size="11px"
                                    ID="RequiredFieldValidator1" runat="server"   ErrorMessage="Enter paid amount"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ForeColor="Red" Font-Size="11px" ValidationGroup="vr12"
                                    ControlToValidate="txtPaid" ID="RegularExpressionValidator1"
                                    runat="server" ErrorMessage="Enter a valid number" ValidationExpression="[0-9]*\.?[0-9]*">
                                    </asp:RegularExpressionValidator>
                                                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>Supplier: </td>
                                <td>

                                    <asp:DropDownList ID="DDLSuppliername" ToolTip="Select supplier" class="form-control" runat="server" AutoPostBack="True"  onselectedindexchanged="DDLSuppliername_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="DDLSuppliername" ValidationGroup="vr12"  Font-Size="11px"
                                    ID="RequiredFieldValidator3" runat="server"   ErrorMessage="Select Supplier"></asp:RequiredFieldValidator>

                                    <asp:Label ForeColor="Black" Font-Size="5px" ID="lblsuppname"        runat="server" Text=""></asp:Label>
                                    <asp:Label ForeColor="Black" Font-Size="11px" ID="lblSuppID"        runat="server" Text=""></asp:Label>
                                    <asp:Label ForeColor="Black" Font-Size="11px" ID="lblSuppContact"   runat="server" Text=""></asp:Label><br /> <br />
                                </td>
                            </tr>

                            <tr>
                                <td class="style2">
                                    Date:  <br />
                                </td>

                                <td>
                                        <atk:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDate" />
                                        <asp:TextBox ID="txtDate" runat="server"  class="form-control" ToolTip="Order Date"
                                        placeholder="Select Date"   ></asp:TextBox>
                                         <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtDate" ValidationGroup="vr12"  Font-Size="11px"
                                    ID="RequiredFieldValidator2" runat="server"   ErrorMessage="Please select Date"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                             <tr>
                                <td class="style2"><br />
                                    <asp:Label ID="Label5" runat="server" ToolTip="Optional" Text="Note/Condition "></asp:Label> <br />
                                     <asp:Label ID="Label6" runat="server" Font-Size="8px" ToolTip="Optional" Text="Optional"></asp:Label>
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtNote"  placeholder="Note"  runat="server" ToolTip="Optional"  Height="60px" class="form-control" ></asp:TextBox>
                                </td>
                            </tr>

                        </table>


         </div>
    </div>
        <div class="panel-footer">
                <asp:Button ID="btnClosePayment" class="btn btn-danger btn-sm" runat="server" Text="Back" />
               <span style="padding-left:240px">
               <asp:Button ID="bntPay" class="btn btn-primary btn-sm" runat="server" OnClick="bntPay_click" ValidationGroup="vr12"  Text="Pay" />
                </span>
        </div>
</asp:Panel>

</asp:Content>

