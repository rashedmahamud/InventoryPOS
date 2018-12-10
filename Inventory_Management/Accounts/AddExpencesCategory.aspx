﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="AddExpencesCategory.aspx.cs" Inherits="Accounts_AddExpencesCategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <br />
 <%--<div class="col-lg-8">--%>
       <div class="panel panel-default"  >
            <div class="panel-heading" >
            <asp:Button ID="btnCategoryLink" class="btn btn-success btn-xs"  runat="server"  ValidationGroup="vlgp123"  Text="New Expenses Category " onclick="btnCategoryLink_Click" />     
           <span  style="padding-left:380px" class="fa fa-list"> </span> New Expenses Category 
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
                     <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="600px" Width="100%">
                      <asp:GridView ID="grdCategoryList" runat="server" class="table table-striped table-hover" Font-Size="11px"
                      AllowPaging="True"     onpageindexchanging="grdCategoryList_PageIndexChanging"  Height="100%" Width="100%" > 
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

<asp:Panel ID="pnlpopupAddCategoryView"  class="panel panel-primary"  runat="server" BackColor="White" style="display:none"  Width="30%" DefaultButton="btnSubmit"> 
      <div class="panel-heading"> 
           New Income Category        
                  <span style="padding-left:60px"> 
               
            </span>                               
       </div>
               
    <div class="panel-body">    
        

                  <asp:Label ID="Label5" runat="server" Font-Size="12px"  Text="Add New Expences Category "></asp:Label>
                        <asp:TextBox  ID="TextBox1"  ToolTip="Bank Name" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
        <br />  <br />
        
         

        <asp:Label ID="Label9" runat="server" Font-Size="12px"  Text="Brach ID"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="TextBox6" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                        <asp:TextBox  ID="TextBox6" Enabled="false" ToolTip="Brach ID "  class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
        
         <br />
                     <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="-"  Font-Size="11px"></asp:Label>        
                
    </div>
    <div class="panel-footer">              
            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="vlpg43" class="btn btn-primary btn-xs"   Text="Save"  onclick="btnSubmit_Click"   />   
             <asp:Button ID="btnCancel" runat="server" ValidationGroup="vlpg43" class="btn btn-danger btn-xs"   Text="Cancel"  />  
           
    </div>  
 </asp:Panel> 
</asp:Content>
