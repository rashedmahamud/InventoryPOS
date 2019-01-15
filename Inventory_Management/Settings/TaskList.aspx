<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="TaskList.aspx.cs" Inherits="TaskList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">


  <div class="col-lg-9">

    <div class="panel panel-primary">
                <header class="panel-heading">
                                Task List Page <br />
                        <asp:Label ID="Label1" runat="server" Font-Size="11px" Text="Task list provide all informations, Notices from system admin or Manager"></asp:Label>
                    <span class="tools pull-right">
                        <a href="javascript:;" class="fa fa-chevron-down"></a>
                        <a href="javascript:;" class="fa fa-times"></a>
                        </span>
                </header>
                <div class="panel-body">
                    <asp:Button ID="btnAddTask" CssClass="btn btn-success btn-xs" runat="server" Text="Add New Task" ToolTip="Add new task"   PostBackUrl="~/Settings/AddTask.aspx" />

                        <asp:GridView ID="grdViewTasklist" class="table table-striped table-hover" Font-Size="11px"  runat="server">
                        </asp:GridView>
                </div>
    </div>
 </div>
</asp:Content>

