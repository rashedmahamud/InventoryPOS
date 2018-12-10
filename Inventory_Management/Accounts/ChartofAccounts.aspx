<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="ChartofAccounts.aspx.cs" Inherits="Accounts_ChartofAccounts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <br />
 <%--<div class="col-lg-8">--%>
       <div class="panel panel-default"  >
            <div class="panel-heading" >
            <asp:Button ID="btnCategoryLink" class="btn btn-success btn-xs"  runat="server"  ValidationGroup="vlgp123"  Text="New Chart of Accounts" onclick="btnCategoryLink_Click" />     
           <span  style="padding-left:380px" class="fa fa-list"> </span> Chart of Accounts  list  
           </div>
                <div class="panel-body">
                <asp:Label ID="lbtotalRow" runat="server" Text=""></asp:Label>
                       <asp:DropDownList        ID="ddlpagesize" runat="server"  AutoPostBack="True" 
                                onselectedindexchanged="ddlpagesize_SelectedIndexChanged"> 
                                <asp:ListItem Value="5" Selected="True" >5</asp:ListItem>       
                                <asp:ListItem Value="10"   >10</asp:ListItem>   
                                <asp:ListItem Value="20">20</asp:ListItem>   
                                <asp:ListItem Value="30">30</asp:ListItem> 
                                <asp:ListItem Value="50">50</asp:ListItem>        
                                </asp:DropDownList> records per page  
                     <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="320px">
                      <asp:GridView ID="grdCategoryList" runat="server" class="table table-striped table-hover" Font-Size="11px"
                      AllowPaging="True"     onpageindexchanging="grdCategoryList_PageIndexChanging"  > 
                      <Columns>
                          <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>                          
                                    <asp:LinkButton ID="LinkDelete" runat="server"  ForeColor="Red"   Font-Size="15px"   ToolTip="Delete" class="fa fa-times"    OnClick="LinkDelete_Click"        />  
                                </ItemTemplate>
                          </asp:TemplateField>                     
                          </Columns>
                                 <PagerSettings      FirstPageText="First" LastPageText="Last" 
                                    Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="6" PreviousPageText="Previous" />                                 
                                    <PagerStyle Font-Bold="true" Font-Size="Large"    HorizontalAlign="Center"   />  
                            <EditRowStyle BackColor="#7C6F57" />
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


<%--<<<<<<<<<<<<<<<<<<<<< ---------------Add Category Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
<asp:Button ID="btnShowPopup" runat="server" style="display:none" />         
        <atk:ModalPopupExtender ID="MpeAddCategoryShow" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopupAddCategoryView"
            CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupAddCategoryView"  class="panel panel-primary"  runat="server" BackColor="White" style="display:none"  Width="230px" DefaultButton="btnSubmit"> 
      <div class="panel-heading"> 
           Chart of Accounts             
                  <span style="padding-left:60px"> 
               
            </span>                               
       </div>
               
    <div class="panel-body">    
        
                      <asp:Label ID="Label2" runat="server" Font-Size="12px"  Text="Accounts Name"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="txtCategory" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                        <asp:TextBox  ID="txtCategory" ToolTip="Please Enter Chart of Accounts Name" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
        <br />                
         <asp:Label ID="Label1" runat="server" Font-Size="12px"  Text="Type"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="txtCategory" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                        <asp:TextBox  ID="TextBox1" ToolTip="Please Enter Chart of Accounts Name" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
         <br /> 
         <asp:Label ID="Label3" runat="server" Font-Size="12px"  Text="Details"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="txtCategory" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                        <asp:TextBox  ID="TextBox2" ToolTip="Details " class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
         <br /> 
                     <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="-"  Font-Size="11px"></asp:Label>        
                
    </div>
    <div class="panel-footer">              
            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="vlpg43" class="btn btn-primary btn-xs"   Text="Save"  onclick="btnSubmit_Click"   />   
             <asp:Button ID="btnCancel" runat="server" ValidationGroup="vlpg43" class="btn btn-danger btn-xs"   Text="Cancel"  />  
           
    </div>  
 </asp:Panel> 

</asp:Content>


