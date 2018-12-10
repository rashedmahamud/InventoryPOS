<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="AddCustomer.aspx.cs" Inherits="AddCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="col-lg-8">
    <div class="note note-success note-shadow">
       <asp:HyperLink ID="hlnkManageItems" Font-Size="20px" ForeColor="Black" ToolTip="Manage Customers" ValidationGroup="vlpg11"   class="fa fa-th-list" NavigateUrl="~/Customers/ManageCustomers.aspx"    runat="server"></asp:HyperLink>
            Manage Customers
    </div>
 
       <div class="panel panel-primary" style="text-align:left">       
          <div class="panel-body">
             <asp:Label ID="Label8" class="label label-warning" Font-Size="13px" runat="server" Text="Add New Customer"></asp:Label> 
              <asp:Label ID="Label1" runat="server" Font-Size="11px" Text="Please enter customer details below"></asp:Label> 
              <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Font-Size="11px" Text=""></asp:Label>           
              <hr />
               <div class="col-lg-6">
                        <asp:Label ID="Label2" runat="server" Font-Size="12px"  Text="Name"></asp:Label>
                         <asp:RequiredFieldValidator  Font-Size="11px"  ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCustName" ErrorMessage="Please Fill Name"></asp:RequiredFieldValidator>                  
                        <asp:TextBox ID="txtCustName" placeholder="Customer full Name"  class="form-control"  ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                        <asp:Label ID="Label4" runat="server" Font-Size="12px"  Text="Phone"></asp:Label>    
                        <asp:RequiredFieldValidator Font-Size="11px"   ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCustPhone" ErrorMessage="Please Fill Phone"></asp:RequiredFieldValidator>   
                       <asp:TextBox ID="txtCustPhone"  placeholder="Contact number"  class="form-control"   ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                        <asp:Label ID="Label3" runat="server" Font-Size="12px"  Text="Email Address [optional]"></asp:Label>
                     <asp:TextBox ID="txtCustEmail"  placeholder="you@Email.com"  class="form-control"  ValidationGroup="vlpg43"  runat="server"></asp:TextBox>              

                        <asp:Label ID="Label6" runat="server" Font-Size="12px"   Text="Customer type [optional]"></asp:Label> 
                        <asp:DropDownList ID="DDLCustType" class="form-control" runat="server">
                        <asp:ListItem>Power Elite *****</asp:ListItem>
                        <asp:ListItem>Elite ****</asp:ListItem>
                        <asp:ListItem>Ultra ***</asp:ListItem>
                        <asp:ListItem>Diamond **</asp:ListItem>
                        <asp:ListItem>Exclusive *</asp:ListItem>
                        </asp:DropDownList>
                        <p></p>
                     
                    <asp:Label ID="Label5" runat="server" Font-Size="12px"   Text="Address [optional]"></asp:Label> 
                    <asp:TextBox ID="txtCustAddress"  placeholder="Address"  TextMode="MultiLine" class="form-control"  ValidationGroup="vlpg43"   runat="server"></asp:TextBox>
                    <p></p>
                    
                  <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vlpg43" ForeColor="Red" 
                    runat="server" ErrorMessage="Please Enter valid Email address"  
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ControlToValidate="txtCustEmail"></asp:RegularExpressionValidator>
 
                </div>
               <div class="col-lg-6">
                        <asp:Label ID="Label7" runat="server" Font-Size="12px"  Text="Customer Login ID"></asp:Label>
                         <asp:RequiredFieldValidator Font-Size="11px" ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCustID" ErrorMessage="Please Enter Customer Login ID"></asp:RequiredFieldValidator>                  
                        <asp:TextBox ID="txtCustID"  placeholder="Customer Login ID"  class="form-control"  ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                        <asp:Label ID="Label9" runat="server" Font-Size="12px"  Text="Password"></asp:Label>    
                        <asp:RequiredFieldValidator Font-Size="11px"  ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtCustPassword" ErrorMessage="Please Fill Customer Password"></asp:RequiredFieldValidator>   
                       <asp:TextBox ID="txtCustPassword"  placeholder="Password"  class="form-control"   ValidationGroup="vlpg43"  runat="server"></asp:TextBox>
                        <p></p>
                     <asp:Label ID="Label10" runat="server" Font-Size="12px"  Text="Reawards Point "></asp:Label>    
                       <asp:TextBox ID="TextBox1"  placeholder="Reawards Point"  class="form-control"     runat="server"></asp:TextBox>
                       <asp:Label ID="Label11" runat="server" Font-Size="12px"  Text="Reference Number "></asp:Label>    
                       <asp:TextBox ID="TextBox2"  placeholder="Reference Number"  class="form-control"     runat="server"></asp:TextBox>
                       
                        <br />
                       <asp:Button ID="btnSubmit" runat="server"  ValidationGroup="vlpg43"  
                        class="btn btn-primary btn-sm" Text="Submit" onclick="btnSubmit_Click" />
              
               </div>
          </div>
      </div>
</div>
</asp:Content>

