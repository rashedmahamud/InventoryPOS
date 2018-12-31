<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" Inherits="EmployeesProfile" Codebehind="EmployeesProfile.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="col-lg-5"> 
        <div class="panel panel-primary"> 
         <div class="panel-heading" >
            <div class="panel-body">
                <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"   OnClientClick="JavaScript: window.history.back(1); return false;"> </asp:button>
                 | Details view 
                                  
            </div>
                 <asp:DetailsView ID="dtviewlist" AutoGenerateRows="false"  Width="100%" class="table  table-hover" runat="server" Height="50px" >
                       <Fields>
                            <asp:ImageField DataImageUrlField="User_Photo"   ItemStyle-HorizontalAlign="Center"   ControlStyle-Width="128px"  ShowHeader="false" ControlStyle-CssClass="img-circle"></asp:ImageField>            
                            <asp:BoundField DataField="EmployeeName" ItemStyle-HorizontalAlign="Center"  ItemStyle-Font-Size="16px"  FooterStyle-ForeColor="Red"  ShowHeader="false" ReadOnly="true" HeaderText="Employee Name" />
                            <asp:BoundField DataField="OfficialPhone"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Font-Size="15px"  ShowHeader="false"   ReadOnly="true" HeaderText="Contact" />
                            <asp:BoundField DataField="Designation"     ReadOnly="true" HeaderText="Designation" />
                            <asp:BoundField DataField="Department" ReadOnly="true" HeaderText="Department" />            
                            <asp:BoundField DataField="Email" ReadOnly="true" HeaderText="Email" />
                            <asp:BoundField DataField="UserAddress" ItemStyle-Font-Size="10px" ReadOnly="true" HeaderText="Address" />
                            <asp:BoundField DataField="DateofBirth" ReadOnly="true" HeaderText="Date of Birth" />
                            <asp:BoundField DataField="Supervisor" ReadOnly="true" HeaderText="Supervisor" />      
                            <asp:BoundField DataField="ShopID" ReadOnly="true" HeaderText="ShopID" />                        
                        </Fields> 
                </asp:DetailsView>
        </div>
    </div>
</div>

<div class="col-lg-7"> 
        <div class="panel panel-info"> 
             <div class="panel-heading"> Salary List </div>
                <div class="panel-body">
                      <asp:Button ID="btnAddSalary" runat="server" Text="Add Salary"    class="btn btn-warning btn-xs" onclick="btnAddSalary_Click" />   
                   <asp:GridView ID="grdempsalary" runat="server" ForeColor="Black"   class="table table-striped table-hover"
                         Font-Size="10px"  AllowPaging="True" PageSize="10"    onpageindexchanging="grdempsalary_PageIndexChanging">
                          <Columns>
                                                      
                              </Columns>                                                
                                <PagerSettings      FirstPageText="First" LastPageText="Last" 
                                    Mode="NumericFirstLast" NextPageText="Next" PageButtonCount="10" PreviousPageText="Previous" />                                 
                                <PagerStyle Font-Bold="true" Font-Size="Large"    HorizontalAlign="Center"   />                                             
                        </asp:GridView>
              
                  
                </div>
     
         
        </div>
</div>

<%--<<<<<<<<<<<<<<<<<<<<< ---------------Add Salary Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
<asp:Button ID="btnShowPopup" runat="server" style="display:none" />         
        <atk:ModalPopupExtender ID="MpeAddCategoryShow" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopupAddCategoryView"
            CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupAddCategoryView"  class="panel panel-primary"  runat="server" BackColor="White" style="display:none"  Width="500px" DefaultButton="btnSubmit"> 
      <div class="panel-heading"> 
             Add Salary             
             <span style="padding-left:350px"> 
                 <asp:LinkButton ID="btnCancel" runat="server"  ForeColor="#FFFFCC" data-dismiss="alert" >X</asp:LinkButton>  
            </span>                                 
       </div>
               
    <div class="panel-body"  style="text-align:left">   
        <div class="col-lg-6"> 
                        <asp:Label ID="Label3" runat="server" Font-Size="12px"  Text="Type"></asp:Label>
                        <asp:DropDownList ID="ddltype"  class="form-control"   runat="server">
                            <asp:ListItem>Monthly</asp:ListItem>
                            <asp:ListItem>Festival Bonus</asp:ListItem>
                            <asp:ListItem>Bonus</asp:ListItem>
                            <asp:ListItem>Vacational</asp:ListItem>
                            <asp:ListItem>Advance</asp:ListItem>
                            <asp:ListItem>Others</asp:ListItem>
                        </asp:DropDownList> 
                      <asp:Label ID="Label2" runat="server" Font-Size="12px"  Text="Amount"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="txtAmount" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                        <asp:TextBox  ID="txtAmount" ToolTip="Please Enter Amount" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
                        
                         <asp:Label ID="Label1" runat="server" Font-Size="12px"  Text="Date"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="txtdate" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                     
                        <atk:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDate" />
                        <asp:TextBox  ID="txtdate" ToolTip="Please Enter Date" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
        </div>   
        <div class="col-lg-6">
                        <asp:Label ID="Label4" runat="server" Font-Size="12px"  Text="Month"></asp:Label>
                        <asp:DropDownList ID="ddlmonth"  class="form-control"   runat="server">
                            <asp:ListItem>January</asp:ListItem>
                            <asp:ListItem>February</asp:ListItem>
                            <asp:ListItem>March</asp:ListItem>
                            <asp:ListItem>April</asp:ListItem>
                            <asp:ListItem>May</asp:ListItem>
                            <asp:ListItem>June</asp:ListItem>
                            <asp:ListItem>July</asp:ListItem>
                            <asp:ListItem>August</asp:ListItem>
                            <asp:ListItem>September</asp:ListItem>
                            <asp:ListItem>October</asp:ListItem>
                            <asp:ListItem>November</asp:ListItem>
                            <asp:ListItem>December</asp:ListItem>
                        </asp:DropDownList> 
                      <asp:Label ID="Label5" runat="server" Font-Size="12px"  Text="Year"></asp:Label>
                            <asp:DropDownList ID="ddyear"  class="form-control"   runat="server">
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                            <asp:ListItem>2017</asp:ListItem>
                            <asp:ListItem>2018</asp:ListItem>
                            <asp:ListItem>2019</asp:ListItem>
                            <asp:ListItem>2020</asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2026</asp:ListItem>
                            <asp:ListItem>2027</asp:ListItem>
                            <asp:ListItem>2028</asp:ListItem>
                            <asp:ListItem>2029</asp:ListItem>
                            <asp:ListItem>2030</asp:ListItem>
                            <asp:ListItem>2031</asp:ListItem>
                            <asp:ListItem>2032</asp:ListItem>
                            <asp:ListItem>2033</asp:ListItem>
                        </asp:DropDownList>  
                        <asp:Label ID="Label6" runat="server" Font-Size="12px"  Text="Note"></asp:Label>
                         <asp:TextBox  ID="txtnote" TextMode="MultiLine" ToolTip="Please Enter Note" class="form-control"  ValidationGroup="vlpg43" runat="server"></asp:TextBox>
                         

        </div> 
                     <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text="-"  Font-Size="11px"></asp:Label>        
                
    </div>
    <div class="panel-footer">              
            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="vlpg43" class="btn btn-primary btn-xs"   Text="Save"  onclick="btnSubmit_Click"   />   
    </div>  
 </asp:Panel> 

 

</asp:Content>

