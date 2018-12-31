<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" Inherits="Customers_Purchase_historyCustomers" Codebehind="Purchase_historyCustomers.aspx.cs" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="col-lg-12"> 
        <div class="panel panel-default" > 
            <div class="panel-heading" style="text-align:center">
            <span  class="glyphicon glyphicon-list"> </span> Products list 
  
        </div>
                <div class="panel-body">
                           
                    <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="350px">
                        <asp:GridView ID="grdItemList" runat="server"   class="table table-striped table-hover" Font-Size="11px">
                          <Columns>
                              <asp:TemplateField HeaderText="Action" HeaderStyle-Width="280px">
                                    <ItemTemplate> 
                                      <asp:LinkButton    ID="linkViewInvoice" runat="server"   Text=" View Invoice"   ToolTip="View purchase Invoice" class="btn btn-primary btn-xs"  OnClick="linkViewInvoice_Click" />   
                                   </ItemTemplate>
                                    <HeaderStyle Width="150px" />
                              </asp:TemplateField>                             
                              </Columns>                           
                                              
                        </asp:GridView>
                    </asp:Panel>
                </div>
         </div>
</div>

 

 

   
 


</asp:Content>




