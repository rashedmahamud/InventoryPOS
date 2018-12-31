<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" Inherits="ManageCustomers" Codebehind="ManageCustomers.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../assets/scripts/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript"> 
$(document).ready(function() {
	    $('#<%=lblNoRecords.ClientID%>').css('display','none');

	    $('#<%=txtSearch.ClientID%>').click(function (e)
	    {
	        // Hide No records to display label.
	        $('#<%=lblNoRecords.ClientID%>').css('display','none');
	        //Hide all the rows.
	        $("#<%=grdVcustomersList.ClientID%> tr:has(td)").hide();
	         
	        var iCounter = 0;
	        //Get the search box value
	        var sSearchTerm = $('#<%=txtSearch.ClientID%>').val();
	         
	        //if nothing is entered then show all the rows.
	        if(sSearchTerm.length == 0)
	        {
	            $("#<%=grdVcustomersList.ClientID%> tr:has(td)").show();
	          return false;
	      }

	        //Iterate through all the td.
	      $("#<%=grdVcustomersList.ClientID%> tr:has(td)").children().each(function ()
	        {
	            var cellText = $(this).text().toLowerCase();
	            //Check if data matches
	            if(cellText.indexOf(sSearchTerm.toLowerCase()) >= 0)
            {   
	                $(this).parent().show();
	                iCounter++;
	                return true;
	            }
	        });
	        if(iCounter == 0)
	        {
	            $('#<%=lblNoRecords.ClientID%>').css('display','');
	        }
	        e.preventDefault();
	    })
	})
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="col-lg-12"> 
        <div class="well well-sm"  >
            <asp:Button ID="btnAdduser"  ValidationGroup="vlg656766" CssClass="btn btn-warning btn-xs" runat="server" Text="Add Customer" PostBackUrl="~/Customers/AddCustomer.aspx" />
        </div>
 
        <div class="panel panel-info"> 
            <div class="panel-heading" ><span  class="fa fa-list"> </span> Customers list </div>
                <div class="panel-body">
                    <asp:Label ID="lbtotalRow" runat="server" Text=""></asp:Label>
                       <asp:DropDownList        ID="ddlpagesize" runat="server"  AutoPostBack="True" 
                                onselectedindexchanged="ddlpagesize_SelectedIndexChanged"> 
                                <asp:ListItem Value="5" Selected="True" >5</asp:ListItem>    
                                <asp:ListItem Value="10">10</asp:ListItem>   
                                <asp:ListItem Value="20">20</asp:ListItem>   
                                <asp:ListItem Value="30">30</asp:ListItem> 
                                <asp:ListItem Value="50">50</asp:ListItem>        
                                </asp:DropDownList> records per page  

            <div class="input-group"> <span class="input-group-addon">Filter</span>
                <asp:TextBox ID="txtSearch"  class="form-control" placeholder="Type here..." runat="server"></asp:TextBox>
                 
            </div>
           <%-- class="table table-bordered table-striped table-hover table-heading table-datatable"--%>
                    <asp:Label ID="lblNoRecords" runat="server" Text=""></asp:Label>
                  

            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="360px">
                <asp:GridView ID="grdVcustomersList" runat="server" data-search="true" class="table table-striped"
                   Font-Size="11px" AllowPaging="True" 
                    onpageindexchanging="grdVcustomersList_PageIndexChanging"  >
                  <Columns>
                      <asp:TemplateField HeaderText="Action" >
                            <ItemTemplate>
                               <asp:LinkButton ID="lnkView" runat="server"  Font-Size="15px"  ForeColor="Red"   ToolTip="View Details" class="fa   fa-bullseye"     OnClick="Linkdtview_Click"   />  |
                                <asp:LinkButton ID="btnEdit" runat="server"  Font-Size="15px"  ForeColor="Red"    ToolTip="Edit" class="fa fa-edit "     OnClick="LinkEdit_Click"   />  |   
                                 <asp:LinkButton ID="LinkbanCustomer" runat="server" ForeColor="Red"  Font-Size="15px"   OnClick="LinkbanCustomer_Click"  ToolTip="Ban or Inactive Customer" class="fa fa-ban"  />                                 
                            </ItemTemplate>
                            <HeaderStyle Width="100" />
                      </asp:TemplateField>                       
                
                      </Columns>
                                    <PagerSettings      FirstPageText="First" LastPageText="Last" 
                                    Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="10" PreviousPageText="Previous" />
                                 
                                    <PagerStyle Font-Bold="true" Font-Size="Large"    HorizontalAlign="Center"   />  
                    </asp:GridView>
                    </asp:Panel>
                </div>
         </div>
  
</div>



 <%--<<<<<<<<<<<<<<<<<<<<< --------------- Details view    Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
    

   <!-- Modal -->
  <div class="modal fade" id="myModal"role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content"  style="width:750px" >
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">   <asp:Label ID="lblcustid" runat="server" Text="-"></asp:Label> | <u>      Customer Details </u>  
                  <br /> 
           </h4>
        </div>
        <div class="modal-body">          
            <div  class="col-lg-4"> 
                    <div class="panel panel-info" style="text-align:left">   
                            <div class="panel-heading"> 
                                    Basic info                                            
                            </div>    
                        <asp:Label ID="lblcusname" runat="server" Font-Size="20px" Text="-"></asp:Label> <br />                         
                        <asp:Label ID="lblEmail" runat="server" Text="-"></asp:Label> <br />
                         <asp:Label ID="lblphone" runat="server" Text="-"></asp:Label> <br />
                    </div>
             </div>
            <div  class="col-lg-8">      
                      <div class="panel panel-warning" style="text-align:left">   
                            <div class="panel-heading"> 
                                   Sold to  <asp:Label ID="lblcname" Font-Size="14px" runat="server" Text="-"></asp:Label>                                          
                            </div>    
                            <asp:Panel ID="Panel2" runat="server" Height="300px" ScrollBars="Vertical">                          
                                <asp:GridView ID="grdviewSoldhistory" runat="server"   class="table table-striped table-hover" Font-Size="11px">  </asp:GridView>
                            </asp:Panel>
                    </div>
             </div>

        </div>
        <div class="modal-footer">		 
          <button type="button" class="btn btn-danger btn-sm"  data-dismiss="modal">Close</button>
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

<asp:Panel ID="pnlpopupEditView"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="650px"  DefaultButton="btnSave"> 
      <div class="panel-heading"> 
          <asp:Label ID="lblID" runat="server" Text="Label"></asp:Label>: Edit Customer    
                                            
    </div>
               
    <div class="panel-body" style="text-align:left" >      
                    <div  class="col-lg-6"> 
                            <asp:Label ID="Label1" runat="server" Font-Size="11px" Text="Customer Name"></asp:Label><br />
                            <asp:TextBox ID="txtCustName"    Font-Bold="true"  Width="275px"     runat="server"></asp:TextBox><br />

                            <asp:Label ID="Label4" runat="server" Font-Size="11px" Text="Contact"></asp:Label><br />
                            <asp:TextBox ID="txtContact"     Width="275px"     runat="server"></asp:TextBox><br />

                            <asp:Label ID="Label2" runat="server" Font-Size="11px" Text="Email"></asp:Label><br />
                            <asp:TextBox ID="txtEmail"     Width="275px"     runat="server"></asp:TextBox><br />

                            <asp:Label ID="Label3" runat="server" Font-Size="11px" Text="Address"></asp:Label><br />
                            <asp:TextBox ID="txtaddress"  Font-Size="11px"   Width="275px"  TextMode="MultiLine"    runat="server"></asp:TextBox>                           
                    </div>
                    <div  class="col-lg-6"> 
                                <asp:Label ID="Label5" runat="server" Font-Size="11px" Font-Bold="true" Text="Customer Type"></asp:Label><br />
                                <asp:TextBox ID="txtCustType"      Width="275px"       runat="server"></asp:TextBox><br />

                                <asp:Label ID="Label6" runat="server" Font-Size="11px" Text="Discount %"></asp:Label><br />
                                <asp:TextBox ID="txtDiscount"      Width="275px"       runat="server"></asp:TextBox><br />
                                <hr />
                                <asp:Label ID="Label8" runat="server" Font-Size="12px"  Text="Customer Login ID"></asp:Label>
                               <asp:TextBox ID="txtCustID"   ReadOnly="true"   Width="275px"      placeholder="Customer Login ID"    ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                                <asp:Label ID="Label9" runat="server" Font-Size="12px"  Text="Password"></asp:Label>    
                                <asp:RequiredFieldValidator Font-Size="11px"  ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCustPassword" ErrorMessage="Please Fill Customer Password"></asp:RequiredFieldValidator>   
                                <asp:TextBox ID="txtCustPassword"     Width="275px"      placeholder="Password"      ValidationGroup="vlpg43"  runat="server"></asp:TextBox>
                                <p></p>
                                <asp:Label ID="lblmsg" runat="server" Font-Size="11px" ForeColor="Red" Text="-"></asp:Label>
                    </div>                
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
    <asp:Label ID="lblInactiveCustName" runat="server" Text="0"></asp:Label>  <hr /> 
     <asp:Label ID="Label7" runat="server" class="label label-info" Text="Do you want to Inactive this Customer ???"></asp:Label> <br />
 <br />
<div class="panel-footer"> 
        <asp:Button ID="btnInactive" class="btn btn-success btn-sm" runat="server"   Text="Yes"   onclick="btnInactive_Click"  />
        <asp:Button ID="btnCloseInactive" class="btn btn-danger btn-sm" runat="server" Text="No" />
 </div>
</asp:Panel>

</asp:Content>



