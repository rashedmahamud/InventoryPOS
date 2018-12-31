<%@ Page Title="" Language="C#"  MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" Inherits="User_Adduser" Codebehind="ManageUsers.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> 
    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<div class="col-lg-12">
    <div class="well well-sm" > 
         <asp:Button ID="btnAdduser" CssClass="btn btn-warning btn-xs" runat="server" Text="Add new user"  ToolTip="Create New User"      PostBackUrl="~/Users/Adduser.aspx" />
     | Users list |
     <asp:Button ID="btnAddTask" CssClass="btn btn-success btn-xs" runat="server" Text="Add Task" ToolTip="Add new task"   PostBackUrl="~/Settings/AddTask.aspx" />
    </div>
</div>

<div class="col-lg-12">
        <div class="panel panel-primary"> 
     <%--    <div class="panel-heading" ><span  class="fa fa-list"> </span> Users list </div>--%>
          <div class="panel-body">
              <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label>
              <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="470px"> <br />
        <asp:DataList ID="DTusers" runat="server" Font-Names="Verdana" Font-Size="Small"  RepeatLayout="Flow"   RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
              <div class="col-md-6">  
                <div id="pricePlansmsg">
                    <ul id="plans">
                        <li class="plan" > 
                         
                                        <div class="col-md-4">  
                                            <ul class="planContainer"> 
                                                <li class="title">
                                                    <asp:Label  Visible="false"  ID="lblID" Font-Size="14px" runat="server" Text='<%# Eval("ID") %>'></asp:Label>                                                
                                                </li>
                                                <li><br />
                                                    <asp:HyperLink ID="HyperLink1" ToolTip="Show User Profile and Salary"   runat="server" NavigateUrl='<%# string.Format("~/Users/EmployeesProfile.aspx?ID={0}", Eval("ID")) %>'   >
                                                        <asp:Image ID="imgPhoto" runat="server" class="img-circle"  Width="100px" Height="100px"   ImageUrl='<%# Eval("User_Photo")%>' />
                                                    </asp:HyperLink> 
                                                    </a>
                                               
                                                </li> 
                                                 <asp:LinkButton ID="LinkEdit" runat="server"   Font-Size="15px"  ForeColor="Red"     Font-Bold="true"    ToolTip="Edit User Info" class="fa fa-edit"    OnClick="LinkEdit_Click"  />                            
                                                 <asp:LinkButton ID="LinkProfile" runat="server"   PostBackUrl='<%# string.Format("~/Users/EmployeesProfile.aspx?ID={0}", Eval("ID")) %>'      Font-Size="20px"  ForeColor="Red"     Font-Bold="true"    ToolTip="Show User Profile and Salary" class="glyphicon glyphicon-usd"  /> 
                                                 <asp:LinkButton ID="Linkpriv" runat="server"   PostBackUrl='<%# String.Format("~/Users/User_privilege.aspx?ID={0}&UserID={1}",Eval("ID"),Eval("UserID")) %>'   Font-Size="20px"  ForeColor="Red"     Font-Bold="true"    ToolTip="User privilege" class="fa fa-check"  /> 
                                                
                                            </ul>
                                        </div> 
                                         <div class="col-md-8">   
                                            <ul class="planContainer">
                                             <li> 
                                                <ul class="options" style="text-align:left">     
                                                    <li  class="title"><h1><asp:Label   ID="lblUserName" Font-Size="14px" runat="server" Text='<%# Bind("Name") %>'></asp:Label></h1>                                
                                                     Department: <span> <asp:Label ID="Label9" runat="server" Text='<%# Bind("Department") %>'></asp:Label>  </span> <br />
                                                     Email: <span> <asp:Label ID="Label11" runat="server" Text='<%# Bind("Email") %>'></asp:Label>  </span> <br />
                                                     Contact: <span> <asp:Label ID="Label12" runat="server" Text='<%# Bind("Contact") %>'></asp:Label>  </span> <br /> </li>
                                                                                   
                                                </ul> 
                                            </li>
                                             
                                              <%--  <a href="~/Inventory_Management/Users/EmployeesProfile.aspx?ID=<%# Eval("ID")%>"   style="font-size:20px;color:Red"      class="fa fa-bullseye" title="Show User Profile and Salary"> </a>
                                                <a href="/Inventory_Management/Users/User_privilege.aspx?ID=<%# Eval("ID")%>&UserID=<%# Eval("UserID")%>"   style="font-size:20px;color:Red"     class="fa fa-check" title="User privilege"     > </a>                      
                                 --%>
                                            </ul>
                                         </div>
                                
                        </li>
                    </ul>
                </div> 
            </div>         
        </ItemTemplate>
    </asp:DataList>              
</asp:Panel>
     </div>
    </div>
</div>



   
  <%-- ///////////////////////Edit Panel ////////////////////   --%>    
 <asp:Button ID="btnEditShowPopup" runat="server" style="display:none" />         
        <atk:ModalPopupExtender ID="MpeEditShow" runat="server" TargetControlID="btnEditShowPopup" PopupControlID="pnlpopupEditView"
            CancelControlID="btnEditCancel" BackgroundCssClass="modalBackground">
        </atk:ModalPopupExtender>
<asp:UpdatePanel ID="UpdatePanelImageUpload" runat="server"  UpdateMode="Conditional">
            <ContentTemplate> 
                 
<asp:Panel ID="pnlpopupEditView"  class="panel panel-primary" runat="server" BackColor="White" style="display:none"  Width="500px" DefaultButton="btnSave">
   
    <div class="panel-heading" style="text-align:left"> 
        <asp:Label ID="lblIDView" runat="server"   Text="ID"></asp:Label>:Edit View
                        
    </div> 

    <div class="panel-body" style="text-align:left">
            <div class="col-lg-6">                                       
            <asp:Label ID="Label1" runat="server" Font-Size="11px"   Text="First Name"></asp:Label><br />
            <asp:TextBox ID="txtFName" ForeColor="Black" runat="server"></asp:TextBox><br /> 

            <asp:Label ID="Label2" runat="server" Font-Size="11px" Text="Last Name"></asp:Label><br />
            <asp:TextBox ID="txtLName" ForeColor="Black"  runat="server"></asp:TextBox><br /> 

            <asp:Label ID="Label10" runat="server" Font-Size="11px" Text="Date of Birth"></asp:Label><br />
            <asp:TextBox ID="txtDOB"  ForeColor="Black"  runat="server" ToolTip="Date of birth sample : 1993-02-09"></asp:TextBox><br /> 

            <asp:Label ID="Label3" runat="server" Font-Size="11px" Text="Contact"></asp:Label><br />
            <asp:TextBox ID="txtContact" ForeColor="Black"  runat="server"></asp:TextBox><br />   
            
            <asp:Label ID="Label8" runat="server" Font-Size="11px" Text="Email"></asp:Label><br />
            <asp:TextBox ID="txtEmail"  ForeColor="Black" runat="server"></asp:TextBox>
          <br />
           <asp:Label ID="Label4" runat="server" Font-Size="12px" Text="Department"></asp:Label>
            <asp:TextBox ID="txtDept"   ForeColor="Black"  ValidationGroup="vlpg43" runat="server"   ></asp:TextBox>


            <asp:Label ID="Label6" runat="server" Font-Size="11px" Text="Designation"></asp:Label><br />
            <asp:TextBox ID="txtDesignation" ForeColor="Black"  runat="server"   ></asp:TextBox> 

        </div>
        <div class="col-lg-6">
                     
            <asp:Label ID="Label9" runat="server" Font-Size="11px" Text="Supervisor"></asp:Label><br />
            <asp:TextBox ID="txtSupervisor" ForeColor="Black"  runat="server" Width="185px" ></asp:TextBox>  <br /> 

            <asp:Label ID="Label5" runat="server" Font-Size="11px"  Text="Address"></asp:Label><br />
            <asp:TextBox ID="txtAddress" ForeColor="Black"  Font-Size="10px"   Width="185px" runat="server"></asp:TextBox>

                      <asp:Label ID="Label15" runat="server" Font-Size="12px" Text="Shop ID"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ForeColor="Red" ValidationGroup="vlpg43"   ControlToValidate="DDLShopID" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>                
                        <asp:DropDownList ID="DDLShopID" Width="185px"  runat="server" ValidationGroup="vlpg43"  ></asp:DropDownList>
                       
              <asp:Label ID="Label7" runat="server" Font-Size="11px"  Text="Password"></asp:Label><br />
            <asp:TextBox ID="txtPassword"  ForeColor="Black"   runat="server" Width="185px" ></asp:TextBox> <br /><br />
              
            <asp:Image ID="imgUser" class="img-thumbnail" Width="100" Height="100" runat="server" />                 
             <br />  <br /> 
           
            <asp:FileUpload ID="FUpimg"   class="btn btn-warning btn-xs"  runat="server" Width="190px"   /> <br /> 
                       
                    
        </div>
                           
    </div>
    
     <div class="panel-footer"> <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text=""></asp:Label><br />           
           <asp:Button ID="btnSave" class="btn btn-primary btn-sm" runat="server" Text="Save" OnClick="btnSave_Click"   />             
            <asp:Button ID="btnEditCancel" class="btn btn-danger btn-sm" runat="server" Text="Close" />

     </div>
                 
</asp:Panel>
  </ContentTemplate>   
                   <Triggers> <asp:PostBackTrigger   ControlID="btnSave"/></Triggers>                   
            </asp:UpdatePanel>
    
</asp:Content>

