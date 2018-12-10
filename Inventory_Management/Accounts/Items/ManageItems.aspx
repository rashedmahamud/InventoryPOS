<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master"  AutoEventWireup="true" CodeFile="ManageItems.aspx.cs" Inherits="ManageItems" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <link href="../assets/css/Hover.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <div class="col-lg-12"> 
            <div class="well well-sm" >
               <p>
                     <asp:Button ID="btnItems"   CssClass="btn btn-warning btn-xs" ToolTip="Add new Item"  runat="server" Text="Add new Item"   PostBackUrl="~/Items/AddItem.aspx" />  |
                      <asp:Button ID="Button1"   CssClass="btn btn-danger btn-xs" ToolTip="Create new Purchase invoice for supplier"  runat="server" Text="Purchase New Items"   PostBackUrl="~/Purchase_module/NewPurchase.aspx" /> |            
                    <asp:Button ID="btnprint" runat="server" ToolTip="Print Stock Report" Text="Print" class="btn btn-info btn-xs" PostBackUrl="~/Report/StockItemReport.aspx" />
               </p>                

                    <atk:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtsearch"
                    MinimumPrefixLength="1" EnableCaching="true"      CompletionSetCount="1" CompletionInterval="100" 
                    ServiceMethod="GetMDN" FirstRowSelected="True">
                    </atk:AutoCompleteExtender>   
                              
            </div>
         <div class="panel panel-default" > 
                <asp:TextBox ID="txtsearch"   ToolTip="Search by : Code, Item Name, Purchase Price, Retail Price,Category" 
                        Placeholder="Search" runat="server" AutoPostBack="True" 
                        ontextchanged="txtsearch_TextChanged"></asp:TextBox>

                          <asp:DropDownList        ID="ddlpagesize" runat="server"  AutoPostBack="True" 
                                onselectedindexchanged="ddlpagesize_SelectedIndexChanged"> 
                                <asp:ListItem Value="2"  >2</asp:ListItem>  
                                <asp:ListItem Value="5"   >5</asp:ListItem>    
                                <asp:ListItem Value="10">10</asp:ListItem>   
                                <asp:ListItem Value="20">20</asp:ListItem>   
                                <asp:ListItem Value="30">30</asp:ListItem> 
                                <asp:ListItem Value="50">50</asp:ListItem>  
                                <asp:ListItem Value="100" Selected="True">100</asp:ListItem>           
                                </asp:DropDownList> records per page  
        </div>
        <div class="panel panel-default" > 
       

      
                <div class="panel-body">
                           
                    <asp:Label ID="lbtotalRow" runat="server" Text=""></asp:Label>
                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="1200px">
                        <asp:GridView ID="grdItemList" runat="server"   ForeColor="#333333" GridLines="None"
                            class="table table-striped table-hover" Font-Size="11px" AllowPaging="True" 
                              onpageindexchanging="grdItemList_PageIndexChanging"    >
                          <Columns>
                              <asp:TemplateField HeaderText="Action" HeaderStyle-Width="180px">
                                    <ItemTemplate>                                     
                                    <span class="dropt">  
                                       <asp:Image ID="imgItemPic" class="img-circle"   Height="50px" Width="60px"  runat="server"  ImageUrl='<%# Eval("Photo")%>' /> <p></p>
                                        <span>                                  
                                            <asp:Image ID="Image2"  Height="130px" Width="130px" runat="server" ImageUrl='<%# Eval("Photo")%>'  />   
                                                                                                                   
                                        </span>
                                    </span>
                                  
                                      <asp:LinkButton       ID="LinkEdit"       runat="server"  ForeColor="Red"  Font-Size="15px"   ToolTip="Edit" class="fa fa-edit"  OnClick="LinkEdit_Click" />  |
                                        <asp:LinkButton     ID="linkBarcode"    runat="server" ForeColor="Red"  Font-Size="15px"     ToolTip="Create Bar-code" class="fa fa-barcode" OnClick="LinkBarcode_Click"  /> |                                  
                                        <asp:LinkButton     ID="btnDelete"      runat="server" ForeColor="Red"   Font-Size="15px"   ToolTip="Delete" class="fa fa-times"  OnClick="LinkItemsDelete_Click"  />
                                       
                                     </ItemTemplate>
                                    <HeaderStyle Width="100px" />
                              </asp:TemplateField>  
                              <%--        <asp:BoundField HeaderText="ID" DataField="ID" />    
                                    <asp:BoundField HeaderText="Code" DataField="Code" />  
                                    <asp:BoundField HeaderText="Item Name" DataField="Item Name" />  
                                    <asp:BoundField HeaderText="Qty" DataField="Qty" />  
                                    <asp:BoundField HeaderText="Purchase Price" DataField="Purchase Price" />   
                                  <asp:BoundField HeaderText="Retail Price" DataField="Retail Price" /> 
                                    <asp:BoundField HeaderText="Discount %" DataField="Discount %" /> 
                                    <asp:BoundField HeaderText="Total" DataField="Total" />     
                                    <asp:BoundField HeaderText="Category" DataField="Category" />  
                                    <asp:BoundField HeaderText="Lastupdate" DataField="Lastupdate" /> --%>                  
                              </Columns>                              
                                    <PagerSettings      FirstPageText="First" LastPageText="Last" 
                                    Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="10" PreviousPageText="Previous" />                                 
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
</div>
                               
<%--<<<<<<<<<<<<<<<<<<<<< --------------- Edit Item Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />         
        <atk:ModalPopupExtender ID="MpeEditItemShow" runat="server" TargetControlID="btnShowPopup" 
        PopupControlID="pnlpopupEditItemView"  CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:UpdatePanel ID="UpdatePanelImageUpload" runat="server"  UpdateMode="Conditional">
            <ContentTemplate> 

<asp:Panel ID="pnlpopupEditItemView"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="480px"> 
      <div class="panel-heading" style="text-align:left"> 
          <asp:Label ID="lblItemID" runat="server" Text="Label"></asp:Label>:     
            <asp:Label ID="lblitemName" runat="server" Text="Label"></asp:Label>                             
        </div>
               
    <div class="panel-body" style="text-align:left">        
               
               <asp:Label ID="lblmessage" ForeColor="Red" runat="server" Font-Size="11px" Text=""></asp:Label> 
              <br />
                        <div class="col-lg-6">
                                <asp:Label ID="Label2" runat="server" Font-Size="12px" Text="Item Code"></asp:Label>
                                <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtProductCode" runat="server"  ErrorMessage="*"></asp:RequiredFieldValidator> <br />                  
                                <asp:TextBox  ReadOnly="true"  ID="txtProductCode"  ValidationGroup="vlpg43" runat="server"></asp:TextBox><br />

                                <asp:Label ID="Label3" runat="server" Font-Size="12px" Text="Item Name"></asp:Label> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtItemName" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtItemName"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox><br />

                                <asp:Label ID="Label4" runat="server"  Font-Size="12px" Text="Purchase Price"></asp:Label>  
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtpurchasePrice" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>  <br />  
                                <asp:TextBox ID="txtPurchasePrice"   ValidationGroup="vlpg43"  runat="server"></asp:TextBox><br />             

                                <asp:Label ID="Label6" runat="server"  Font-Size="12px" Text="Retail Price"></asp:Label> 
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtRetailPrice" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtRetailPrice" Font-Bold="true"    ValidationGroup="vlpg43" runat="server"></asp:TextBox><br />
<%--
                                <asp:Label ID="Label9" runat="server"  Font-Size="12px" Text="Item Qutantity"></asp:Label><br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtItemQty" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> 
                                <asp:TextBox ID="txtItemQty"   ToolTip="add Item Qutantity e.g: 10"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
                               <br />--%>
                                 
                                <asp:Label ID="Label5" runat="server"  Font-Size="12px" Text="Item Discount Rate"></asp:Label>   
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="vlpg43"   ForeColor="Red"  ControlToValidate="txtItemDiscRate" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> <br />
                                <asp:TextBox ID="txtItemDiscRate"   ToolTip="Disc Rate without % sign" placeholder="Disc Rate without % sign"   ValidationGroup="vlpg43" Text="0.00"  runat="server"></asp:TextBox>
                              
                                                          <asp:Label ID="Label9" runat="server"  Font-Size="12px" Text="Description"></asp:Label>  
                            <asp:TextBox ID="txtDescription" ToolTip="Description" 
                            placeholder="Description" class="form-control" ValidationGroup="vlpg43" 
                            Text=""  runat="server" Height="55px"></asp:TextBox> 
                                <br />
                        

                        </div>
                        <div class="col-lg-6"> 
                             <asp:Label ID="Label7" runat="server"  Font-Size="12px" Text="Item Category"></asp:Label> <br />
                            <p><asp:DropDownList ID="DDLCategory"    ValidationGroup="vlpg43" runat="server"></asp:DropDownList></p>
                       
                            <asp:Image ID="imgItemPhoto" Height="120px" Width="150px"   runat="server"   /><br /> 
                            .jpg and .png  only 
                            <br />  <asp:FileUpload ID="FUpimg"   class="btn btn-success btn-sm"  runat="server"  Width="190px" />
                            <br />
                            <asp:Label ID="lblmsg" runat="server" Style="font-family: Arial; font-size: small;"></asp:Label>
                                                       
                         
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="Please add decimal value e.g: 20.11 or 20" ForeColor="Red"  ControlToValidate="txtpurchasePrice" ValidationGroup="vlpg43" 
                            ValidationExpression="^[0-9][\.\d]*(,\d+)?$"></asp:RegularExpressionValidator>  

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ErrorMessage="Please add decimal value" ForeColor="Red"  ControlToValidate="txtRetailPrice" ValidationGroup="vlpg43" 
                            ValidationExpression="^[0-9][\.\d]*(,\d+)?$"> </asp:RegularExpressionValidator>  

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ErrorMessage="Please  add  Item Discount Rate" ForeColor="Red"  ControlToValidate="txtItemDiscRate" ValidationGroup="vlpg43" 
                            Display="Dynamic" ValidationExpression="^[0-9][\.\d]*(,\d+)?$"></asp:RegularExpressionValidator>
                        </div> 

    </div>
 
         <%-- <iframe style=" width:1240px; height:440px;" id="IframeUpdateItem" src="" runat="server"></iframe> --%>  

    <div class="panel-footer">     
      <asp:Button ID="btnUpdate" runat="server" class="btn btn-success btn-xs"  Text="Update" onclick="btnUpdate_Click" />                            
        <asp:Button ID="btnClose" class="btn btn-danger btn-xs" runat="server" Text="Close" />
    </div>  
 </asp:Panel> 

   </ContentTemplate>   
                   <Triggers> <asp:PostBackTrigger   ControlID="btnUpdate"/></Triggers>                   
            </asp:UpdatePanel>

 <%--<<<<<<<<<<<<<<<<<<<<<END --------------- Edit Item Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>


 
 <%--<<<<<<<<<<<<<<<<<<<<< --------------- Inactive user  Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
        <asp:Button ID="btnShowPopupDeleteItem" runat="server" style="display:none" />         
        <atk:ModalPopupExtender ID="ModalPopupDeleteItem" runat="server" TargetControlID="btnShowPopupDeleteItem" 
        PopupControlID="pnlpopupDeleteItem"  CancelControlID="btnCloseDeleteItem" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupDeleteItem"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="300px"  DefaultButton="btnDeleteItem"> 
  <asp:Label ID="Label8" runat="server" class="label label-info" Text="Do you want to Delete this Item ?"> </asp:Label>   
  <hr /> 
   <asp:Label ID="lblCodeDeleteItem" runat="server" Text="0"></asp:Label>. 
    <asp:Label ID="lblDeleteItem_CustName" runat="server" Text="0"></asp:Label>  <br />
        <div class="panel-footer"> 
                <asp:Button ID="btnDeleteItem" class="btn btn-success btn-xs" runat="server"   Text="Yes"   onclick="btnDeleteItem_Click"  />
                <asp:Button ID="btnCloseDeleteItem" class="btn btn-danger btn-xs" runat="server" Text="No" />
        </div>
</asp:Panel>


        
 <%-- /////////////////////////// Start /////////////////////  Barcode creator ///////////////////////////////// ///////////////////// Start --%>
 
  <div class="modal fade" id="popupBarcodePanel" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content"    style="width:800px" >
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
              <h4 class="modal-title">
                     Barcode: <asp:Label ID="lblBarcodeID" Font-Bold="true"   runat="server" Text="0"></asp:Label>    
              </h4>
        </div>
        <div class="modal-body">
                 <iframe style=" width: 740px; height: 470px;" id="Iframebarcode" src="" runat="server"></iframe> 
        </div>
        <div class="modal-footer"> 
	 <button type="button" class="btn btn-danger btn-sm"  data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>

</asp:Content>

