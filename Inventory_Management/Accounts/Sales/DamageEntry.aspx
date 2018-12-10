<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="DamageEntry.aspx.cs" Inherits="Accounts_Sales_DamageEntry" %>
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

    <link href="../assets/css/style.css" rel="stylesheet" type="text/css" /> 
            <link href="../assets/css/style.css" rel="stylesheet" type="text/css" /> 
    <link href="../assets/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css"/>
<link href="../assets/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css"/> 
<link href="../assets/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css"/>
      <link href="3columntable.css" rel="stylesheet" />
<link href="../assets/css/components-md.css" rel="stylesheet" type="text/css"   id="style_components" />
<link href="../assets/css/plugins-md.css" rel="stylesheet" type="text/css"/>
<link href="../assets/css/layout.css" rel="stylesheet" type="text/css"/>
<link href="../assets/css/themes/light.css" rel="stylesheet" type="text/css" id="style_color" />
<link href="../assets/css/custom.css" rel="stylesheet" type="text/css"/>
<!-- END THEME STYLES -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">     

<%--/////////<div class="input-group input-group-sm">//// Item list --%>
<div style="width:100%">   <asp:Button ID="Button1"  runat="server"  Text="Damage Entry " Width="100%"  height="100px"  ValidationGroup="vG2"  Font-Size="12px"  BackColor="#ff9900"  class="btn btn-primary btn-xs" />  <br /></div>           

<div style="width:100%"> 
 <h1> 
                                 <asp:Button ID="inv"  runat="server"  Text="Inventory List " Width="100%"  height="50px"  ValidationGroup="vG2"  Font-Size="12px"  ToolTip="Add to Cart" class="btn btn-primary btn-xs" OnClick="btn_Goclick" />  <br />           
   
       
 </h1>
<div class="panel  panel-success">      
<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="300px">  <br /> 
       <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small" RepeatLayout="Flow"    RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
                <div class="col-md-4">  
                    <div id="pricePlansmsg">
                    <ul id="plans">
                        <li class="plan">
                            <ul class="planContainer">
                                <li class="title">
                                    <asp:Label ID="LblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    <asp:Label ID="LblCode" Visible="false" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    <asp:Label ID="LblItemName" Visible="false" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label> 
                                    <asp:Label ID="LblQty" Visible="false" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                    <asp:Label ID="LblPrice" Visible="false" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                    <asp:Label ID="LblDisc" Visible="false" runat="server" Text='<%# Eval("Disc") %>'></asp:Label>
                                    <asp:Label ID="LblTotal" Visible="false" runat="server" Text='<%# Eval("Total") %>'></asp:Label>                                   

                                    <h5><asp:Label   ID="lblitemNametop" Font-Size="12px" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label></h5>
                                </li>
                                <li class="title">
                                    <asp:Image ID="imgPhoto" class="img-circle" runat="server" Width="50px" Height="50px"   ImageUrl='<%# Eval("Photo")%>' />
                                </li>
                                <li>
                                    <ul class="options">                                      
                                       <li><span> Price: <asp:Label ID="Label9" runat="server" Text='<%# Bind("Total") %>'></asp:Label>   
                                           (<asp:Label ID="Label8" ForeColor="Black" Font-Size="10px" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>)</span>  </li>                                    
                                    </ul>
                                </li>
                                    <atk:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" 
                                            TargetControlID="txtqty" Minimum="1" Maximum="999" Width="50"  ViewStateMode="Enabled">  </atk:NumericUpDownExtender>
                                            <asp:TextBox ID="txtqty" runat="server" Font-Size="11px"  Text="1" Width="25px" ToolTip="Item Quantity"></asp:TextBox> <p></p> 
                                 <asp:Button ID="btnGo"  runat="server"  Text="Add to Cart" Width="100%"  height="50px"  ValidationGroup="vG2"  Font-Size="12px"  ToolTip="Add to Cart" class="btn btn-primary btn-xs" OnClick="btn_Goclick" />  <br />           
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            </ItemTemplate>
        </asp:DataList>
     
             
        </asp:Panel> 
        <div class="panel-body">         
             Search by Category:  <asp:DropDownList ID="DDLCategory"   class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown"  runat="server"  AutoPostBack="true"  onselectedindexchanged="DDLCategory_SelectedIndexChanged"></asp:DropDownList>                
        </div> 
    </div>
</div>

<div style="width:100%"> <h1>  <asp:Button ID="Button100"  runat="server"  Text="Damage Entry " Width="100%"  height="50px"  ValidationGroup="vG2"  Font-Size="12px"  ToolTip="Add to Cart" class="btn btn-primary btn-xs" />  <br />           
</h1>
    <div class="input-group">
        <span class="input-group-addon"> <span class="fa fa-search"> </span> </span>
        <asp:TextBox ID="txtItemSearch" runat="server" 
            placeholder="Search to Scan Products" class="form-control"  ToolTip="Search by item code or item name"
            ontextchanged="txtItemSearch_TextChanged" AutoPostBack="true"  ></asp:TextBox> 
            <span class="input-group-addon"></span>
    </div>   
  

    <div class="panel panel-primary" > 


            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="180px">
                <asp:GridView ID="grdSelectedItem" BorderStyle="None" HeaderStyle-BackColor="WindowFrame" Font-Bold="true" GridLines="None" ForeColor="#00000" class="table table-striped table-hover" onrowdatabound="grdSelectedItem_RowDataBound" 
                    runat="server" Font-Size="11px"   onrowdeleting="grdSelectedItem_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Del">
                                <ItemTemplate> 
                                    <asp:LinkButton  ID="LinkDel" runat="server" ForeColor="Red"    Font-Size="15px"  CommandName="Delete"  ToolTip="Delete item" class="fa fa-times"     />                      
                                </ItemTemplate>
                            </asp:TemplateField>                 
                        </Columns>
                </asp:GridView>

            </asp:Panel>

         
    </div>
            
            <asp:Button ID="btnsuspen" runat="server" class="btn btn-danger" Text="Suspen"   onclick="btnsuspen_Click" />
          
</div>

                   <asp:Button ID="bntPay" class="btn btn-primary btn-sm" runat="server" OnClick="bntPay_click" ValidationGroup="vr12"  Text="Submit" />






</asp:Content>

