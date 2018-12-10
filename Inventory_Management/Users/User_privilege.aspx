<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="User_privilege.aspx.cs" Inherits="User_privilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"><br />
<div class="col-lg-3">
          <div class="panel panel-primary" >     
                  <div class="panel-body"> <p>User Info </p>
                    <p>  <asp:Image ID="imgUser" class="img-thumbnail" Width="158" Height="158" runat="server" />  </p>
                      <asp:Label ID="lblFname" Font-Size="16px" runat="server" Text="Label"></asp:Label>
                      <asp:Label ID="lblLname" Font-Size="16px" runat="server" Text="Label"></asp:Label><br />
                      <asp:Label ID="lblContact" ToolTip="Contact Number" runat="server" Text="Label"></asp:Label><br />
                      <asp:Label ID="lblSupervisor" ToolTip="Supervisor Name" runat="server" Text="Label"></asp:Label><br /> <br />
                                      <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"   OnClientClick="JavaScript: window.history.back(1); return false;"> </asp:button>                
                   <br /> <br /> <asp:Button ID="btnCreateRole"  OnClick="btnCreateRole_click" class="btn btn-warning btn-sm"  runat="server" Text="Create New Role" />         
     
                  </div>
            </div>
</div>

<div class="col-lg-8">
<%--///////////////////////////////////  Popup User Role Page Access  //////////////////////////  --%>    

<asp:Panel ID="pnlpopupUserRole"  class="panel panel-primary" runat="server" BackColor="White"> 

        <div class="panel-body"  > <p>User privilege </p>
            <asp:Panel ID="Panel2" runat="server"  ScrollBars="Vertical" Height="360px" >             
            <asp:GridView ID="grdviewUserPageAccess" Font-Size="11px"  class="table table-striped table-hover"  runat="server">
                   <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                <asp:CheckBox ID="chkAllow"   runat="server"  ValidationGroup="vG2" Font-Size="17px"  ToolTip="Allow "    />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                </asp:GridView>
            </asp:Panel>  
        </div>
      <div class="panel-footer"> <asp:Label ID="lblmessage" ForeColor="Red" runat="server" Text=""></asp:Label><p></p>
             <asp:Button ID="btnUserRoleSave"  OnClick="btnUserRoleSave_click" class="btn btn-primary btn-sm"  runat="server" Text="Save" />         
      </div>
</asp:Panel>
</div>
</asp:Content>

