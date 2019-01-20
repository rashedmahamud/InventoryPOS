<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Order_module_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="col-lg-4">
   <table>

   <tr><td>
           <asp:Button ID="Button1" runat="server" Text="Add" onclick="Button1_Click" />
       </td>
   <td><asp:TextBox ID="TextBox1" Visible="false" runat="server" Width="178px"></asp:TextBox></td><td>
       <asp:TextBox ID="TextBox2" Visible="false" runat="server" Width="107px"></asp:TextBox>
       <asp:Label  ID="lblitenam" runat="server" Text="Label"></asp:Label>
           <asp:Label ID="lblitemcode" runat="server" Text=""></asp:Label>
       </td>
   </tr>
   </table>


     <p align="left">

         <asp:GridView ID="grdSelectedItem" runat="server" Width="323px"
             onrowdatabound="Setwidths"    class="table table-striped table-hover"     onrowdeleting="grdSelectedItem_RowDeleting" >
                              <Columns>
                            <asp:TemplateField HeaderText="Del">
                                <ItemTemplate>
                                    <asp:LinkButton  ID="LinkDel" runat="server" ForeColor="Red"    Font-Size="15px"  CommandName="Delete"  ToolTip="Delete item" class="fa fa-times"     />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
         </asp:GridView>

    </p>        <div class="panel-footer"  style="text-align:right">
                Subtotal =  <asp:Label ID="lblsubTotal" runat="server" Font-Bold="true"  Text="0"></asp:Label><br />


                <asp:Label ID="Label1" Font-Size="11px" runat="server" Text="Vat"></asp:Label>
                (<asp:Label ID="lblVatRate" Font-Size="9px" runat="server" Text="5"></asp:Label>% )
                <asp:Label ID="lblVat"  Font-Size="11px"  runat="server" Text="0"></asp:Label><br />
                Total =     <asp:Label ID="lbltotal" runat="server" Font-Bold="true" Text="0"></asp:Label> <br />

                <asp:Label ID="Label7" Font-Size="11px" runat="server" Text="Total Items"> </asp:Label>
                <asp:Label ID="lblTotalQty"  Font-Size="11px"  runat="server" Text="0"></asp:Label>

            </div>
  </div>

<div class="col-lg-8">
<div class="panel  panel-success">

<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="655px">  <br />
       <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small"  RepeatColumns="4" RepeatDirection="Horizontal">
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
                <div id="pricePlans">
                    <ul id="plans">
                        <li class="plan" style="width:142px">
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
                                    <asp:Image  ID="imgPhoto" class="img-circle" runat="server" Width="50px" Height="50px"   ImageUrl='<%# Eval("Photo")%>' />
                                </li>
                                <li>
                                    <ul class="options">
                                       <li><span> Price: <asp:Label ID="Label9" runat="server"  Text='<%# Bind("Total") %>'></asp:Label>
                                           (<asp:Label ID="Label8" ToolTip="Item Quantity" ForeColor="Black" Font-Size="10px" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>)</span>  </li>
                                    </ul>
                                </li>

                                 <asp:Button ID="btnGo"  runat="server"  Text="Add to Cart"    ValidationGroup="vG2"    ToolTip="Add to Cart" class="btn btn-info btn-xs" OnClick="Button1_Click" />  <br />
                            </ul>
                        </li>
                    </ul>
                </div>

            </ItemTemplate>
        </asp:DataList>


        </asp:Panel>
    </div>
</div>

</asp:Content>

