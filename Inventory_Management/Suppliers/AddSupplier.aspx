<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="AddSupplier.aspx.cs" Inherits="AddSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="col-lg-8">
    <div class="well well-sm"  >
 
            <asp:HyperLink ID="hlnkManageItems" Font-Size="20px" ForeColor="Black" ToolTip="Manage Suppliers" ValidationGroup="vlpg11"   class="fa fa-th-list" NavigateUrl="~/Suppliers/ManageSuppliers.aspx"    runat="server"></asp:HyperLink>
            Manage Suppliers
    </div>
 
       <div class="panel panel-primary" style="text-align:left">       
          <div class="panel-body">
             <asp:Label ID="Label8" class="label label-warning" Font-Size="13px" runat="server" Text="Add New Supplier"></asp:Label> 
              <asp:Label ID="Label1" runat="server" Font-Size="11px" Text="Please enter Supplier details below"></asp:Label> 
              <asp:Label ID="lblMessage" ForeColor="Red" runat="server" Font-Size="11px" Text=""></asp:Label>           
              <hr />
               <div class="col-lg-6">
                   

                        <asp:Label ID="Label2" runat="server" Font-Size="12px"  Text="Name"></asp:Label>
                         <asp:RequiredFieldValidator Font-Size="12px"  ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="Please Fill Name"></asp:RequiredFieldValidator>           
                        <asp:TextBox ID="txtName" placeholder="Supplier Name" class="form-control"  ValidationGroup="vlpg43"  runat="server"></asp:TextBox>

                        <asp:Label ID="Label4" runat="server" Font-Size="12px"  Text="Phone"></asp:Label>     
                        <asp:RequiredFieldValidator  Font-Size="12px"  ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Please Fill Phone"></asp:RequiredFieldValidator>                      
                        <asp:TextBox ID="txtPhone" placeholder="Contact number" class="form-control"   ValidationGroup="vlpg43"  runat="server"></asp:TextBox>
                         <p></p>
                        <asp:Label ID="Label3" runat="server" Font-Size="12px"  Text="Email Address [optional]"></asp:Label>
                        <asp:TextBox ID="txtEmail"  placeholder="you@Email.com" class="form-control"  ValidationGroup="vlpg43"  runat="server"></asp:TextBox>              

                         <p></p>
                        <asp:Label ID="Label6" runat="server" Font-Size="12px"   Text="Supplier type"></asp:Label> 
                        <asp:DropDownList ID="DDLType" class="form-control" runat="server">
                            <asp:ListItem>Dealer</asp:ListItem>
                            <asp:ListItem>Company</asp:ListItem>
                            <asp:ListItem>WholeSaller</asp:ListItem>
                            <asp:ListItem>Distributor</asp:ListItem> 
                        </asp:DropDownList>
 
                </div>
               <div class="col-lg-6">   
                    <asp:Label ID="Label9" runat="server" Font-Size="12px"  Text="Company Name"></asp:Label>
                     <asp:RequiredFieldValidator Font-Size="12px"  ValidationGroup="vlpg43" ForeColor="Red" ID="RequiredFieldValidator3" runat="server"
                      ControlToValidate="txtCompanyName" ErrorMessage="Please Fill Company Name"></asp:RequiredFieldValidator>           
                    <asp:TextBox ID="txtCompanyName" placeholder="Company Name" class="form-control"  ValidationGroup="vlpg43"  runat="server"></asp:TextBox>              

                    <p></p>
                               
                    <asp:Label ID="Label7" runat="server" Font-Size="12px"  Text="City"></asp:Label>
                    <asp:TextBox ID="txtcity" placeholder="City" class="form-control"  ValidationGroup="vlpg43"  runat="server"></asp:TextBox>              

                    <p></p>
                    <asp:Label ID="Label5" runat="server" Font-Size="12px"   Text="Address [optional]"></asp:Label> 
                    <asp:TextBox ID="txtAddress" placeholder="Supplier Address details" TextMode="MultiLine" class="form-control"  ValidationGroup="vlpg43"   runat="server"></asp:TextBox>
                    <p></p>
                    <asp:Button ID="btnSubmit" runat="server"  ValidationGroup="vlpg43"  
                        class="btn btn-primary btn-sm" Text="Submit" onclick="btnSubmit_Click" />
              <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="vlpg43" ForeColor="Red" 
                    runat="server" ErrorMessage="Please Enter valid Email address"  
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    ControlToValidate="txtEmail"></asp:RegularExpressionValidator>
               </div>
          </div>
      </div>
</div>
</asp:Content>

