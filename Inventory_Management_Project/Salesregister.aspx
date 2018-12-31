<%@ Page Language="C#" AutoEventWireup="true" Inherits="sales" Codebehind="sales.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CmsnPos-Inventory Management </title>
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
        <link href="2column.css" rel="stylesheet" />

         <style type="text/css">

             .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        width: 800px;
        height :800px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0
      
    }
    .roundedcorners
{
-webkit-border-radius: 10px;
-khtml-border-radius: 10px;	
-moz-border-radius: 10px;
border-radius: 10px;
background-color:black;
color:white;
text-decoration:none;
border:outset;
}


        .roundedcorners:hover
{

-webkit-border-radius: 10px;
-khtml-border-radius: 10px;	
-moz-border-radius: 10px;
border-radius: 10px;
border:outset;
background-color:ButtonShadow; 


}

    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
         width: 800px;
        height :800px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
        .auto-style1 {
            height: 27px;
        }
         .auto-style6 {
             width: 71px;
         }

         </style>

</head>
<body>
    <form id="form1" runat="server">
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
          <div > 
                <asp:Label ID="l1" Visible="false" runat="server" Text="Label"></asp:Label>
                    <div class="panel panel-primary"  > 
                            <header class="panel-heading" style="background-color:#00395d" >
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Return Sales" PostBackUrl="ReturnProudct.aspx" />
                           <asp:Button ID="Button2" class="btn btn-info" runat="server" Text="Delivery" PostBackUrl="printtest.aspx" />
                            <asp:Button ID="btnStartSales" class="btn btn-danger" runat="server" Text="Search Item" PostBackUrl="~/Sales/NewSale.aspx" /> 
                            <asp:Button ID="Button3" class="btn btn-success" runat="server" Text="Damage Entry" PostBackUrl="~/Report/AccessLog.aspx" OnClick="Button3_Click" /> 
                                 <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="New Customer" PostBackUrl="~/Salesregister.aspx" />
                           <asp:Button ID="Button5" class="btn btn-info" runat="server" Text="Due" PostBackUrl="DueReport.aspx" OnClick="Button5_Click" />
                            <asp:Button ID="Button6" class="btn btn-danger" runat="server" Text="Admin Login" PostBackUrl="~/Accounts/Default.aspx" /> 
                            <asp:Button ID="Button7" class="btn btn-success" runat="server" Text="Logout" PostBackUrl="~/Accounts/SalesReport.aspx" />
                                <asp:Button ID="Button11" class="btn btn-success" runat="server" Text="Accounts" PostBackUrl="~/Accounts/Outlate.aspx" />
                                                     
               


                            </header>
                     
                                      
               
                </div>
                  
</div>
          <div class="panel  panel-success">  
          <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="70px" Width="100%" BackColor="#00395d"> 
            <asp:DataList ID="DataList2" runat="server"   RepeatColumns="4" RepeatDirection="Horizontal"  BorderStyle="None" CaptionAlign="Bottom" CssClass="active left" HorizontalAlign="Left"  style="vertical-align:top;text-align:left" ShowFooter="False" ShowHeader="False" CellPadding="0"  >
        
           <ItemStyle BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
        
           <ItemTemplate>
                <table class="auto-style1" style="border:none;background-color:#00395d;width:100%"  >
                    <tr  >
                        <td class="auto-style6" style=" color:white "  >
                           
                   <asp:LinkButton ID="lnkbtnRelatedLinks" class="roundedcorners btn btn-primary btn-sm " Width ="100%" Height="100%" BackColor="#00395d" Style="line-height:50px;text-align:center;" ForeColor="white" runat="server" Text='<%#Eval("ItemCategory") %>' OnClick="lnkbtnRelatedLinks_Click"></asp:LinkButton>
                        </td>
                    </tr>

                </table>
            </ItemTemplate>
            <SelectedItemStyle  Font-Bold="True" ForeColor="Black" />
        </asp:DataList>

                         </asp:Panel> 
        </div>         
   <div > 
     <div>
         <asp:DataList ID="DataList3" runat="server" Width="258px">
             <ItemTemplate>
                 <asp:Label ID="Label19" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
             </ItemTemplate>

         </asp:DataList>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Inventory_ManagementConnectionString %>" SelectCommand="SELECT [Description] FROM [tbl_Item]"></asp:SqlDataSource>
     </div>
    <div style="width:100%">      
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="420px" Width="100%">  
               <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small" RepeatLayout="Flow"   RepeatDirection="Horizontal" CssClass="row">
                    <ItemStyle ForeColor="Black"/>
                    <ItemTemplate>
                        <div style="width:100%"> 
                        <div style="width:100%">
                           <table style="width:100%;margin-left:15px">
                               
                               <caption>
                                   <hr />
                                   <tr>
                                       <td style="width:33%;vertical-align:top ;margin-left:10px">
                                           <asp:ImageButton ID="ImageButton1" runat="server" class="img-circle" Height="50px" ImageUrl='<%# Eval("Photo")%>' OnClick="ImageButton1_Click" Width="50%" />
                                           <%--                                            <asp:Image ID="imgPhoto" class="img-circle"  runat="server" Width="50px" Height="50px"   ImageUrl='<%# Eval("Photo")%>' />--%></td>
                                       <td style="width:33%;vertical-align:top ;margin-left:10px">
                                           <asp:Label ID="LblID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="LblCode" runat="server" Text='<%# Eval("Code") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="LblItemName" runat="server" Text='<%# Eval("ItemName") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="LblQty" runat="server" Text='<%# Eval("Qty") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="LblPrice" runat="server" Text='<%# Eval("Price") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="LblDisc" runat="server" Text='<%# Eval("Disc") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="LblTotal" runat="server" Text='<%# Eval("Total") %>' Visible="false"></asp:Label>
                                           <asp:Label ID="lblitemNametop" runat="server" Font-Bold="True" Font-Size="20px" ForeColor="#006600" Text='<%# Bind("ItemName") %>'></asp:Label>
                                           <br />
<%--                 <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>'></asp:Label>--%>

                                       </td>
                                       <td style="width:33%;vertical-align:top ;margin-left:10px"><span>Price:
                                           <asp:Label ID="Label9" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                                           (<asp:Label ID="Label8" runat="server" Font-Size="10px" ForeColor="Black" Text='<%# Bind("Qty") %>' ToolTip="Item Quantity"></asp:Label>
                                           )</span> </td>
                                       <td>
                                           <atk:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" Maximum="999" Minimum="1" TargetControlID="txtqty" ViewStateMode="Enabled" Width="50">
                                           </atk:NumericUpDownExtender>
                                           <asp:TextBox ID="txtqty" runat="server" Font-Size="11px" Text="1" ToolTip="Item Quantity" Visible="false" Width="25px"></asp:TextBox>
                                           <p>
                                           </p>
                                                                                 <asp:Button ID="btnGo"  runat="server"  Text='<%#Eval("ItemName") %>'  Width="50%" Style="line-height:50px;text-align:center;"  ValidationGroup="vG2"   ToolTip="Add to Cart" class="btn btn-primary btn-xs" OnClick="btn_Goclick" />  <br />  <br />           

                                       </td>
                                   </tr>
                               </caption>
                                    </table>
                        </div>
                     </div>
                    </ItemTemplate>
                </asp:DataList>            
        </asp:Panel> 
        
        <div class="panel-body"> 
               
             <asp:Label ID="Label10" runat="server" Text="All"></asp:Label>
<%--             Search by Category:  <asp:DropDownList ID="DDLCategory"   class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown"  runat="server"  AutoPostBack="true"  onselectedindexchanged="DDLCategory_SelectedIndexChanged"></asp:DropDownList>                --%>
        </div> 
    </div> 
  
</div>                    
                  
    <div>
        <asp:Label ID="Label15" Visible="false" runat="server" Text="Label"></asp:Label>
        
        <asp:Label ID="Label11" runat="server"  Visible="false" Text=""></asp:Label>
            
        <div class="divTable" style="width:100%">

<div class="divTableCell" style="width:35%"> 
    
    <div >
                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="220px" Width="100%" > 
    <div class="panel panel-default"  >   
            <div class="input-group" style="background-color:steelblue">
                
            <span class="input-group-addon" style="background-color:red"> <span class="fa fa-search" style="background-color:red"> </span> </span>
            <asp:TextBox ID="txtItemSearch" runat="server" 
                placeholder="Search to Scan Products" style="background-color:#00395d" ForeColor="White" Font-Bold="true" class="form-control"  ToolTip="Search by item code or item name"
               AutoPostBack="true"  Width="100%" ></asp:TextBox> 

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
             
                                                                             
                  </div>     
          
 
   <div class="divTable">
<div class="divTableBody" >
<div class="divTableRow" style="background:#000000;font-size:11px;font-weight:bold;color:white" >

<div class="divTableCell" > PRODUCT NAME</div>
<div class="divTableCell" > QUANTITY </div>
<div class="divTableCell" >PRICE   </div>
<div class="divTableCell" >DISC%</div>
<div class="divTableCell" >TOTAL</div>
</div>
</div>
</div>

        
            <asp:DataList ID="dtlistgrid" runat="server" Font-Names="Verdana" Font-Size="Small"  RepeatLayout="Flow"    RepeatDirection="Horizontal" CssClass="row" >
                <ItemStyle ForeColor="Black"/>
                <ItemTemplate>
            
                       
                        <div class="divTable "  >
<div class="divTableBody" >
<div class="divTableRow" >
    
<div class="divTableCell" > <asp:Label  Visible="false" Font-Size="10px" width="50px" ID="lblid" runat="server" Text='<%# Bind("Code") %>'></asp:Label> </div>
<div class="divTableCell" ><asp:Label   ID="lblitemname" Font-Size="10px" width="100px" runat="server" Text='<%# Bind("ItemName") %>' ForeColor="#0084B4"></asp:Label>  </div>
<div class="divTableCell" ><asp:Label ForeColor="Black" Font-Size="10px" width="30px"  ToolTip="Item Quantity"    Font-Bold="true" ID="Label16" runat="server" Text='<%# Bind("Qty") %>'></asp:Label> </div>
<div class="divTableCell" ><asp:Label ID="LblPrice" Font-Size="10px"  width="30px" runat="server" Text='<%# Eval("Price") %>'></asp:Label>    </div>
<div class="divTableCell" ><asp:Label ForeColor="Black" Font-Size="10px" width="30px" ToolTip="Disc"  ID="lblDisc" runat="server" Text='<%# Bind("Disc") %>'></asp:Label></div>
<div class="divTableCell" ><asp:Label ID="Label17" Font-Size="10px" width="30px" runat="server"  ForeColor="Black" Text='<%# Bind("Total") %>' ToolTip="Price"></asp:Label></div>
<div class="divTableCell" >  <asp:LinkButton Font-Size="10px"  ID="LinkDele" runat="server" ForeColor="Red"      ToolTip="Remove item" class="fa fa-times" OnClick="btnDeleteitem_Click"   /></div>
</div>
</div>
</div>
                          
                </ItemTemplate>
            </asp:DataList>   

    
      
      </div>  
                        </asp:Panel>
          <div class="panel-footer"  >  
           
            <table style="vertical-align:top;width:100%">
                     <tr >
                                <td style="text-align:left">
                                                   <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="15px" ForeColor="#009933" Text="Total =   "></asp:Label>   <asp:Label ID="lbltotal" runat="server" Font-Bold="true" Font-Size="15px" ForeColor="#009900" Text="0"></asp:Label>
                                   <br /> <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Paid by:"></asp:Label><br />
                                                   <asp:DropDownList ID="DDLPaidBy" runat="server" class="form-control">
                                                       <asp:ListItem>Cash</asp:ListItem>
                                                       <asp:ListItem>Cheque</asp:ListItem>
                                                       <asp:ListItem>Debit Card</asp:ListItem>
                                                       <asp:ListItem>Credit Card</asp:ListItem>
                                                       <asp:ListItem>Paypal</asp:ListItem>
                                                       <asp:ListItem>Payza</asp:ListItem>
                                                       <asp:ListItem>Skrill</asp:ListItem>
                                                       <asp:ListItem>Neteller</asp:ListItem>
                                                       <asp:ListItem>Perfect money</asp:ListItem>
                                                       <asp:ListItem>Online</asp:ListItem>
                                                       <asp:ListItem>Others</asp:ListItem>
                                                   </asp:DropDownList>
                                       <asp:Label ID="Label14" runat="server" Font-Bold="true" Text=" Paid:"></asp:Label>
                                                  <asp:TextBox ID="txtPaid" runat="server" AutoPostBack="True" Font-Bold="true" Font-Size="20px" class="form-control" ontextchanged="txtPaid_TextChanged" Width="100%" ToolTip="Customer paid Amount"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPaid" ErrorMessage="Enter paid amount" Font-Size="11px" ForeColor="Red" ValidationGroup="vr12"></asp:RequiredFieldValidator>
                                                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPaid" ErrorMessage="Enter a valid number" Font-Size="11px" ForeColor="Red" ValidationExpression="[0-9]*\.?[0-9]*" ValidationGroup="vr12">
                                    </asp:RegularExpressionValidator><br />
                                      <asp:Label ID="Label3" runat="server" Text="Change" ToolTip="Change to customer"></asp:Label>
                                                     <asp:Label ID="lblChange" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                <asp:Label ID="Label4" runat="server" Text="Due" ToolTip="Receive from customer"></asp:Label>
                                                     <asp:Label ID="lblDue" runat="server" Font-Bold="true" Text="-"></asp:Label>
                                      <br />
                                                  
                                     </td>
                            
                                  <td  style="text-align:right;vertical-align:top">
                                          
                                                   Subtotal =
                                                   <asp:Label ID="lblsubTotal" runat="server" Font-Bold="true" Text="0"></asp:Label>
                                                   <br />
                                                   <asp:Label ID="Label1" runat="server" Font-Size="11px" Text="Vat ="></asp:Label>
                                                   (<asp:Label ID="lblVatRate" runat="server" Font-Size="9px" Text="0"></asp:Label>
                                                   )
                                                   <asp:Label ID="lblVat" runat="server" Font-Size="11px" Text="0"></asp:Label>
                                                   <br />
                                                   <asp:Label ID="Label7" runat="server" Font-Size="11px" Text="Total Items"> </asp:Label>
                                                   <asp:Label ID="lblTotalQty" runat="server" Font-Size="11px" Text="0"></asp:Label>
                                                   <br />
                                            
                                           
           <table style="vertical-align:top;width:100%;">
                     <tr >
                                <td style="text-align:left">
                                    <asp:Button ID="Button15" runat="server"  Text="Mobile " Width="100%" OnClick="Button15_Click"  />  <br />               
                                     <asp:TextBox ID="TextBox2" runat="server" Width="100%"  AutoPostBack="true" OnTextChanged="TextBox2_TextChanged" ></asp:TextBox><br />
                                    
                                     <asp:Button ID="Button10" runat="server"  OnClick="Button10_Click" Text="Search" Width="100%"  />
                            <asp:Button ID="Button37" runat="server"  Text="Customer Name " Width="100%" />
                                     <asp:Button ID="Button9" runat="server" class="btn btn-info" Width="100%" Text="Guest "  />
                                     <asp:Label ID="Label5" runat="server" Text="Note" ToolTip="Optional"></asp:Label>
                                    <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>
                                                   <asp:Label ID="Label6" runat="server" Font-Size="8px" Text="(Optional)" ToolTip="Optional"></asp:Label>
                                                   <asp:TextBox ID="txtNote" runat="server" class="form-control" Height="30px" ToolTip="Optional" ></asp:TextBox>
                                    
                                </td>
                         </tr>
                    </table>                
     
            
                                    </td>

                                </tr>
             
          
                  
             </table>             
            
              
        
             </div> 
         
       
</div>
    
                
              
</div>

       <div class="divTableCell" style="width:17%">
            <table style="vertical-align:top;width:100%" >
                     
                             
                              
                  <tr >
                                                   <td>
                                                       <asp:Button ID="Button31" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button31_Click" Text="100" Width="31%" />
                                                       <asp:Button ID="Button32" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button32_Click" Text="500" Width="31% " />
                                                       <asp:Button ID="Button33" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"  Height="40px" OnClick="Button33_Click" Text="1000" Width="31%" />
                                                       <asp:Button ID="Button34" Visible="false" runat="server"   Height="60px" OnClick="Button34_Click" Text="2000" Width="19% " />
                                                       <asp:Button ID="Button35" Visible="false" runat="server"    Height="60px" OnClick="Button35_Click" Text="5000" Width="19% " />

                                                   </td>
                                                   </tr>
                                              

                                               <tr>
                                                   <td style="text-align:left">
                                                       <br />
                                                       <asp:Button ID="Button16" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button16_Click" Text="1" Width="31%" />
                                                       <asp:Button ID="Button17" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button17_Click" Text="2" Width="31%" />
                                                       <asp:Button ID="Button18" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button18_Click" Text="3" Width="31%" />
                                                   </td>
                                               </tr>
                                               <tr style="text-align:left">
                                                   <td style="text-align:left">
                                                       <br />
                                                       <asp:Button ID="Button19" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px" OnClick="Button19_Click" Text="4" Width="31% " />
                                                       <asp:Button ID="Button20" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button20_Click" Text="5" Width="31% " />
                                                       <asp:Button ID="Button21" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px" OnClick="Button21_Click" Text="6" Width="31% " />
                                                   </td>
                                               </tr>
                                               <tr style="text-align:left">
                                                   <td style="text-align:left">
                                                       <br />
                                                       <asp:Button ID="Button22" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button22_Click" Text="7" Width="31% " />
                                                       <asp:Button ID="Button23" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button23_Click" Text="8" Width="31% " />
                                                       <asp:Button ID="Button24" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button24_Click" Text="9" Width="31% " />
                                                   </td>
                                               </tr>
                                               <tr style="text-align:left">
                                                   <td style="text-align:left">
                                                       <br />
                                                       <asp:Button ID="Button25" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick=" Button25_Click" Text="." Width="26% " />
                                                       <asp:Button ID="Button26" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px" OnClick="Button26_Click" Text="0" Width="31% " />
                                                       <asp:Button ID="Button27" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px" Text="No Sale" Width="37% " />
                                                   </td>
                                               </tr>
                                               <tr style="text-align:left">
                                                   <td style="text-align:left">
                                                       <br />
                                                       <asp:Button ID="Button28" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"  Height="40px" Text="Cash " Width="31% " />
                                                       <asp:Button ID="Button29" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"  Height="40px" Text="Card  " Width="31% " />
                                                       <asp:Button ID="Button30" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"  Height="40px" OnClick="Button30_Click" Text="Clear" Width="31% " />
                                                   </td>
                                               </tr>
                                               <%--  <tr>
                                               <td>
                                                   <asp:Button ID="Button14" runat="server" class="btn btn-danger" Text="" Width-="97%" />
                                               </td>
                                             
                                           </tr>--%>
                                               <tr >
                                                   <td>    
                                                       <asp:Label ID="lblCustID" runat="server" Font-Size="11px" ForeColor="Black" Text="00"></asp:Label>
                                                       <asp:Label ID="lblCustContact" runat="server" Font-Size="11px" ForeColor="Black" Text="121"></asp:Label>
                                                       <div class="panel-footer">
                                                           <%--                <asp:Button ID="btnPayment" runat="server" class="btn btn-success"   Text="Payment" onclick="btnPayment_Click" /> --%>
                                                       </div>
                                                   </td>
                                               </tr>
                                               <tr >
                                                   <td>
                                                           <table  style="vertical-align:top;width:100%">
                    <tr>

                                         <td> 
                       <asp:Button ID="Button13" runat="server"  Width="100%" Height="50px" BackColor="#ee6123" ForeColor="White" OnClick="bntPay_click" Text="Pay" ValidationGroup="vr12" />
                     </td>
                        <td>
                                             <asp:Button ID="btnsuspen" runat="server" BackColor="#ff0000" ForeColor="White"  Width="100%" Height="50px" onclick="btnsuspen_Click" Text="Suspen" />
    </td>
                         <td>  
                        
                         <asp:Button ID="Button12" runat="server"  Width="100%" Height="50px" BackColor="#005387" ForeColor="White" Text="Back"  />
                                <br />
                                </td>
                              
                                 </tr>
              </table>
                                                   </td>
                                                   </tr>

                        </table>

                </div>     
<div class="divTableCell">
   
    </div>
            </div>
        </div>
            
 

 <%--<<<<<<<<<<<<<<<<<<<<< --------------- payment panel Popup -------------- >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>--%>
<asp:Button ID="btnShowPopup" runat="server" style="display:none" />      
    <atk:ModalPopupExtender ID="ModalPopupPayment" runat="server" TargetControlID="Button10" 
    PopupControlID="pnlpopupPayment"  CancelControlID="btnClosePayment" BackgroundCssClass="modalBackground">
    </atk:ModalPopupExtender>

<asp:Panel ID="pnlpopupPayment"  class="panel panel-primary" runat="server" BackColor="White" style="display:none;text-align:left"   DefaultButton="bntPay" > 
    <asp:Label ID="Label2" runat="server" Font-Size="19px" Text="Mobile Number"></asp:Label>    <hr />
    <div class="panel-body">
      
                    <div class="panel-SR-mainbody">                   
                  
                       <table style="vertical-align:top;width:100%" >
                     
                             
                              <tr style="visibility:hidden">
                                  <td>Mobile Number :<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Search" /></td>
                              </tr>
                

                                               <tr>
                                                   <td style="text-align:left">
                                                    
                                                       <asp:Button ID="Button39" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button39_Click" Text="1" Width="31%" />
                                                       <asp:Button ID="Button40" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button40_Click" Text="2" Width="31%" />
                                                       <asp:Button ID="Button41" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button41_Click" Text="3" Width="31%" />
                                                   </td>
                                               </tr>
                                               <tr >
                                                   <td style="text-align:left">
                                                  
                                                       <asp:Button ID="Button42" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px" OnClick="Button42_Click" Text="4" Width="31% " />
                                                       <asp:Button ID="Button43" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button43_Click" Text="5" Width="31% " />
                                                       <asp:Button ID="Button44" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px" OnClick="Button44_Click" Text="6" Width="31% " />
                                                   </td>
                                               </tr>
                                               <tr style="text-align:left">
                                                   <td style="text-align:left">
                                                  
                                                       <asp:Button ID="Button45" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button45_Click" Text="7" Width="31% " />
                                                       <asp:Button ID="Button46" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button46_Click" Text="8" Width="31% " />
                                                       <asp:Button ID="Button47" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick="Button47_Click" Text="9" Width="31% " />
                                                   </td>
                                               </tr>
                                               <tr >
                                                   <td style="text-align:left">
                                                      
                                                       <asp:Button ID="Button48" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"    Height="40px" OnClick=" Button48_Click" Text="0" Width="26% " />
                                                       <asp:Button ID="Button49" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px"  Text="" Width="31% " />
                                                       <asp:Button ID="Button50" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"   Height="40px" Text="" Width="37% " />
                                                   </td>
                                               </tr>
                                               <tr style="text-align:left">
                                                   <td style="text-align:left">
                                                     
                                                       <asp:Button ID="Button51" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"  Height="40px" Text=" " Width="31% " />
                                                       <asp:Button ID="Button52" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"  Height="40px" Text=" " Width="31% " />
                                                       <asp:Button ID="Button53" class="roundedcorners" backcolor="#00395d" ForeColor="White" runat="server"  Height="40px" OnClick="Button53_Click" Text="Clear" Width="31% " />
                                                   </td>
                                               </tr>
                           </table>
                     </div>            
         
    </div>
        <div class="panel-footer"> 
                <asp:Button ID="btnClosePayment" class="btn btn-danger btn-sm" runat="server" Text="Back" />         
               <asp:Button ID="bntPay" class="btn btn-primary btn-sm" Visible="false" runat="server" OnClick="bntPay_click" ValidationGroup="vr12"  Text="Pay" />
               
        </div>
</asp:Panel>


  </ContentTemplate>
        </asp:UpdatePanel>

        
        
        
     
   


<%--/////////<div class="input-group input-group-sm">//// Item list --%>


    </form>
    <!-- Placed js at the end of the document so the pages load faster -->
<script  type="text/javascript" src="../js/jquery-1.10.2.min.js"></script>
<script type="text/javascript" src="../js/jquery-ui-1.9.2.custom.min.js"></script>
<script type="text/javascript" src="../js/jquery-migrate-1.2.1.min.js"></script>
<script type="text/javascript"  src="../js/bootstrap.min.js"></script>
<script type="text/javascript"  src="../js/jquery.nicescroll.js"></script>


<!--common scripts for all pages-->
<script type="text/javascript"  src="../js/scripts.js"></script>
</body>
</html>
