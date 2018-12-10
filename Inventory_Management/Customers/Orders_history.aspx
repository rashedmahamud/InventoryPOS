<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master"AutoEventWireup="true" CodeFile="Orders_history.aspx.cs" Inherits="Customers_Orders_history" %>
 
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="col-lg-12"> 
            <div class="well well-sm" >             

                <div class="col-lg-8"> Order History     </div>  
                <div class="col-lg-4">     <asp:Button ID="btnItems"   CssClass="btn btn-warning btn-xs" ToolTip="Create new Order"  runat="server" Text="Create new Order"   PostBackUrl="~/Customers/NewOrder_Customer.aspx" />  
                </div> <br />
 
            </div>
   
        <div class="panel panel-default" > 
    
                <div class="panel-body">
                           
                    <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="350px">
                        <asp:GridView ID="grdItemList" runat="server"   class="table table-striped table-hover" Font-Size="11px">
                          <Columns>
                              <asp:TemplateField HeaderText="Action" HeaderStyle-Width="140px">
                                    <ItemTemplate> 
                                      <asp:LinkButton    ID="linkViewInvoice" runat="server"    Text=" View Invoice"   ToolTip=" View Order Invoice and Take Due Payment"   class="btn btn-primary btn-xs"  OnClick="linkViewInvoice_Click" />   
                                   
                                   </ItemTemplate>
                                    <HeaderStyle Width="140px" />
                              </asp:TemplateField>                             
                              </Columns>                           
                                              
                        </asp:GridView>
                    </asp:Panel>
                </div>
         </div>
</div>

 

 

   
 


</asp:Content>




