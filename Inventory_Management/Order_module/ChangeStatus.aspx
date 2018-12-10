<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage/Bootstrap.master" AutoEventWireup="true" CodeFile="ChangeStatus.aspx.cs" Inherits="Order_module_ChangeStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 102px;           
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <div class="col-lg-8 col-lg-offset-2"> 
            <div class="well well-sm" >   
             <asp:button id="btnBack" runat="server"  class="btn btn-warning btn-xs"   Text="Back"
                        OnClientClick="JavaScript: window.history.back(1); return false;">
                        </asp:button> 
               Order | Edit                
            </div>
        </div> 

<div class="col-lg-8 col-lg-offset-2">  
     <div class="panel panel-info" style="text-align:left" > 
        <div class="panel-heading">Ordered Items | Invoice No: 
            <asp:Label ID="lblInvoiceNo" runat="server" Text="Label"></asp:Label> </div>
            <div class="panel-body">                      
                            <asp:GridView ID="grdItemList" runat="server"   
                            class="table table-striped table-hover" Font-Size="11px"  
                            ShowHeaderWhenEmpty="True" onrowdatabound="grdItemList_RowDataBound"></asp:GridView>
                    
            </div>
    </div>
</div>

<div class="col-lg-4 col-lg-offset-2">  
     <div class="panel panel-success" style="text-align:left" > 
        <div class="panel-heading"> Status  </div>
            <div class="panel-body">                      
                <table class="style1">
                            <tr>
                                <td class="style2">
                                    Order Status:  <br />    
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDlOrderstatus" runat="server" class="form-control">
                                        <asp:ListItem>Pending</asp:ListItem>
                                        <asp:ListItem>Approved</asp:ListItem>
                                        <asp:ListItem>Delivered</asp:ListItem>
                                        <asp:ListItem>Rejected</asp:ListItem>                                         
                                    </asp:DropDownList>  <br />    
                                </td>
                            </tr>


                            <tr>
                                <td class="style2">
                                    Payment Status:  <br />  
                                   
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDlpaymentstatus" runat="server" class="form-control">
                                        <asp:ListItem>Unpaid</asp:ListItem>
                                        <asp:ListItem>Paid</asp:ListItem>
                                        <asp:ListItem>Partially Paid</asp:ListItem>                                         
                                    </asp:DropDownList>    
                                </td>
                            </tr>

                            
                             <tr>
                                <td class="style2"><br />
                                    <asp:Label ID="Label11" runat="server" ToolTip="Optional" Text="Address: "></asp:Label> <br />
                                     
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtAddress" placeholder="Address"  runat="server" ToolTip="Delivery address"  Height="60px" class="form-control" ></asp:TextBox> 
                                       <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtAddress" ValidationGroup="vr12"  Font-Size="11px" 
                                    ID="RequiredFieldValidator3" runat="server"   ErrorMessage="Please add delivery address"></asp:RequiredFieldValidator> 
                                </td>
                            </tr>
                            
                             <tr>
                                <td class="style2"><br />
                                    <asp:Label ID="Label5" runat="server" ToolTip="Optional" Text="Note/Condition "></asp:Label> <br />
                                     <asp:Label ID="Label6" runat="server" Font-Size="8px" ToolTip="Optional" Text="Optional"></asp:Label>
                                </td>
                                <td><br />
                                    <asp:TextBox ID="txtNote"  placeholder="Note"  runat="server" ToolTip="Optional Note/Condition "  Height="60px" class="form-control" ></asp:TextBox> <p> </p>
                                    <asp:Button ID="btnChangeStatus" runat="server"  ValidationGroup="vr12" class="btn btn-info btn-sm"  Text="Submit"  onclick="btnChangeStatus_Click"  /> 
                                  <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>    
                                             
                </table>
                    
            </div>
    </div>
</div>

<div class="col-lg-4">  
     <div class="panel panel-success" style="text-align:left" > 
        <div class="panel-heading"> Info  </div>
            <div class="panel-body">  
                 <table class="style1"> 
                        <tr>
                            <td class="style2">
                                Date:  <br />    <br />
                            </td>
                            
                            <td>
                                    <atk:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" TargetControlID="txtDate" />
                                    <asp:TextBox ID="txtDate" runat="server"  class="form-control" ToolTip="Order Date"   
                                    placeholder="Select Date"   ></asp:TextBox>  
                                        <asp:RequiredFieldValidator  ForeColor="Red"  ControlToValidate="txtDate" ValidationGroup="vr12"  Font-Size="11px" 
                                ID="RequiredFieldValidator1" runat="server"   ErrorMessage="Please select Date"></asp:RequiredFieldValidator>   
                            </td>
                        </tr>
                                           
                        <tr>
                                <td>Customer:  
                                </td>
                                <td>
                                             
                                    <asp:DropDownList ID="DDLCustname" ToolTip="Select customer" class="form-control" runat="server">
                                    </asp:DropDownList>                
                                  
                                </td>
                            </tr>
                    </table>
            </div>
    </div>
</div>

</asp:Content>

