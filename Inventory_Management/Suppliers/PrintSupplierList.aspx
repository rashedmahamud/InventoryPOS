<%@ Page Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="PrintSupplierList.aspx.cs" Inherits="Suppliers_PrintSupplierList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<div class="col-lg-12">
    <div class="note note-success note-shadow">
       <asp:HyperLink ID="hlnkManageItems" Font-Size="20px" ForeColor="Black" ToolTip="Manage Customers" ValidationGroup="vlpg11"   class="fa fa-th-list" NavigateUrl="~/Suppliers/ManageSuppliers.aspx"    runat="server"></asp:HyperLink>
            Manage Suppliers
    </div>

       <div class="panel panel-primary" style="text-align:left">
          <div class="panel-body" style="height:845px">
                 <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%" ZoomMode="PageWidth"></rsweb:ReportViewer>
          </div>
      </div>
</div>
</asp:Content>