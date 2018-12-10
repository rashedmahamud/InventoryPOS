<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ShopMenu.aspx.cs" Inherits="ShopMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />
    <br />
    <br />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
    <asp:Label ID="Label1" runat="server" Text="1461"></asp:Label>
     <div class="panel  panel-success">  
          <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="70px" Width="100%" BackColor="#00395d"> 
            <asp:DataList ID="DataList2" runat="server"   RepeatColumns="4" RepeatDirection="Horizontal"  BorderStyle="None" CaptionAlign="Bottom" CssClass="active left" HorizontalAlign="Left"  style="vertical-align:top;text-align:left" ShowFooter="False" ShowHeader="False" CellPadding="0"  >
        
           <ItemStyle BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
        
           <ItemTemplate>
                <table class="auto-style1" style="border:none;background-color:#00395d;width:100%"  >
                    <tr  >
                        <td class="auto-style6" style=" color:white "  >
                           
                   <asp:LinkButton ID="lnkbtnRelatedLinks" class="roundedcorners btn btn-primary btn-sm " Width ="307px" Height="100%" BackColor="#00395d" Style="line-height:50px;text-align:center;" ForeColor="white" runat="server" Text='<%#Eval("ItemCategory") %>' OnClick="lnkbtnRelatedLinks_Click"></asp:LinkButton>
                        </td>
                    </tr>

                </table>
            </ItemTemplate>
            <SelectedItemStyle  Font-Bold="True" ForeColor="Black" />
        </asp:DataList>

                         </asp:Panel> 
        </div>
    
 <div style="width:100%">
     <table style="width:100%">
         <tr >
             <td>
                  <asp:DataList ID="DataList1" runat="server" Font-Names="Verdana" Font-Size="Small" RepeatLayout="Flow"  Width="100%"   RepeatDirection="Horizontal" CssClass="row">
            <ItemStyle ForeColor="Black"/>
            <ItemTemplate>
               
                   
                   
                               <table style="width:100%;margin-left:15px">
                                   <br />
                                   <hr />
                                   <tr style="width:100%;">
                                       <td style="width:15%;vertical-align:top ;margin-left:10px">
                                                                       <asp:Image ID="imgPhoto" class="img-circle" runat="server" Width="100px" Height="80px"   ImageUrl='<%# Eval("Photo")%>' />

                                           
                                          
                                      <td style="width:40%;vertical-align:top ;text-align:center">    
                                <asp:Label ID="LblID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    <asp:Label ID="LblCode" Visible="false" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    <asp:Label ID="LblItemName" Visible="false" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label> 
                                    <asp:Label ID="LblQty" Visible="false" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                    <asp:Label ID="LblPrice" Visible="false" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                    <asp:Label ID="LblDisc" Visible="false" runat="server" Text='<%# Eval("Disc") %>'></asp:Label>
                                    <asp:Label ID="LblTotal" Visible="false" runat="server" Text='<%# Eval("Total") %>'></asp:Label>                                   

                                    <asp:Label   ID="lblitemNametop" Font-Size="20px" runat="server" Font-Bold="true" ForeColor="#006600" Text='<%# Bind("ItemName") %>'></asp:Label>
                                  <br />  <asp:Label ID="Label2"  runat="server" Text='<%# Eval("Description") %>'></asp:Label>                                   
                               
                                      </td>
                                </td>
                                       <td style="width:20%;vertical-align:top ;text-align:center">                           
                                      <span> Price: <asp:Label ID="Label9" runat="server" Text='<%# Bind("Total") %>'></asp:Label>   
                                           <asp:Label ID="Label8" ForeColor="Black" Visible="false"  Font-Size="10px" runat="server" Text='<%# Bind("Qty") %>'></asp:Label></span>  </li>                                    
                                 
                                         <asp:TextBox ID="txtqty" runat="server" Font-Size="11px"  Visible="false" Text="1" Width="25px" ToolTip="Item Quantity"></asp:TextBox> <p></p> 
                                                                            <asp:Button ID="btnGo"  runat="server" OnClick="btn_Goclick"  Text="Add to Cart" Width="100%"  height="40px"  ValidationGroup="vG2"  Font-Size="12px"  ToolTip="Add to Cart" class="btn btn-primary btn-xs"  />  <br />           

                                       </td>
                             
                                            </tr>
                                   </table>
                        
                
            
            </ItemTemplate>
        </asp:DataList>
             </td>
             <td style="width:35%;vertical-align:top ;text-align:center">
                  



                   
   
           

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
             
                                                                             
                  
       
 
 <asp:Panel ID="Panel3" runat="server" ScrollBars="Vertical" Height="220px" Width="100%" > 

        <table style="width:100%">
               <tr style="width:100%;font-size:medium">
   <th style ="margin-left:10px;vertical-align:top;text-align:left">PRODUCT NAME</th>
   <th style ="margin-left:10px;vertical-align:top;text-align:left">QUANTITY</th>
         <th style ="margin-left:10px;vertical-align:top;text-align:left">PRICE </th>
        <th style ="margin-left:10px;vertical-align:top;text-align:left">DISC% </th>
         <th style ="margin-left:10px;vertical-align:top;text-align:left">TOTAL </th>
        <th style ="margin-left:10px;vertical-align:top;text-align:left"> </th>
 </tr>
        </table>
            <asp:DataList ID="dtlistgrid" runat="server" Font-Names="Verdana" Font-Size="Small"  RepeatLayout="Flow"  Width="100%"   RepeatDirection="Horizontal" CssClass="row" >
                <ItemStyle ForeColor="Black"/>
                <ItemTemplate>
            
                       
<table style="width:100%">
 
    <tr style="width:100%">
        <td style ="margin-left:10px;vertical-align:top;text-align:left">
             <asp:Label  Visible="false" Font-Size="10px" width="50px" ID="lblid" runat="server" Text='<%# Bind("Code") %>'></asp:Label> </div>
            <asp:Label   ID="lblitemname" Font-Size="10px" width="100px" runat="server" Text='<%# Bind("ItemName") %>' ForeColor="#0084B4"></asp:Label>  </div>

        </td>
        <td style ="margin-left:5px;vertical-align:top;text-align:left">
            <asp:Label ForeColor="Black" Font-Size="10px" width="30px"  ToolTip="Item Quantity"    Font-Bold="true" ID="Label16" runat="server" Text='<%# Bind("Qty") %>'></asp:Label> </div>

        </td>
        <td style ="margin-left:5px;vertical-align:top;text-align:left">
<asp:Label ID="LblPrice" Font-Size="10px"  width="30px" runat="server" Text='<%# Eval("Price") %>'></asp:Label>    </div>

        </td>
        <td style ="margin-left:5px;vertical-align:top;text-align:left">
            <asp:Label ForeColor="Black" Font-Size="10px" width="30px" ToolTip="Disc"  ID="lblDisc" runat="server" Text='<%# Bind("Disc") %>'></asp:Label></div>

        </td>
        <td style ="margin-left:5px;vertical-align:top;text-align:left">
<asp:Label ID="Label17" Font-Size="10px" width="30px" runat="server"  ForeColor="Black" Text='<%# Bind("Total") %>' ToolTip="Price"></asp:Label></div>

        </td>
        <td style ="margin-left:5px;vertical-align:top;text-align:left">
             <asp:LinkButton Font-Size="10px"  ID="LinkDele" runat="server" ForeColor="Red"      ToolTip="Remove item" class="fa fa-times" OnClick="btnDeleteitem_Click"   /></div>

        </td>
    </tr>

</table>
    
                    
                </ItemTemplate>
            </asp:DataList>   

    
      
   
                        </asp:Panel>
            <div class="panel-footer"  style="text-align:right">     
                Subtotal =  <asp:Label ID="lblsubTotal" runat="server" Font-Bold="true"  Text="-"></asp:Label><br />
              <%--  <asp:Label ID="Label7" Font-Size="11px" runat="server" Text="Disc% ="></asp:Label>
                <asp:Label ID="lbldisc"  Font-Size="11px"  runat="server" Text="-"></asp:Label><br /> --%>
        

                <asp:Label ID="Label3" Font-Size="11px" runat="server" Text="GST ="></asp:Label>
                (<asp:Label ID="lblVatRate" Font-Size="9px" runat="server" Text=""></asp:Label>)
                <asp:Label ID="lblVat"  Font-Size="11px"  runat="server" Text="-"></asp:Label><br />  
                Total =     <asp:Label ID="lbltotal" runat="server" Font-Bold="true" Text="-"></asp:Label> <br />

                <asp:Label ID="Label7" Font-Size="11px" runat="server" Text="Total Items"> </asp:Label>
                <asp:Label ID="lblTotalQty"  Font-Size="11px"  runat="server" Text="-"></asp:Label>  
                
            </div>
                <asp:Label ID="Label4"  Font-Size="11px"  runat="server" Text=""></asp:Label>  

   <asp:ImageButton ID="ImageBtncustomizeampunt" runat="server"  onclick="btncustomizeampunt_Click" 
                        Width="180" Height="55" ImageUrl="~/globalRes/homeimg/paypal-paynow-button.png" />
             </td>
         </tr>

     </table>
       </div>
     </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

