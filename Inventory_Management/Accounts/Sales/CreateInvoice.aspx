<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="CreateInvoice.aspx.cs" Inherits="Accounts_CreateInvoice" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Textboxcss.css" rel="stylesheet" />
    <link href="../2column.css" rel="stylesheet" />
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

    <script type="text/javascript">
        $(window).load(function () {
            $(".col-3 input").val("");

            $(".input-effect input").focusout(function () {
                if ($(this).val() != "") {
                    $(this).addClass("has-content");
                } else {
                    $(this).removeClass("has-content");
                }
            })
        });
    </script>
    <script type="text/javascript">
        function printing()
        {
            window.print();
        }
    </script>
    <link type="text/css" href="css/ui-lightness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.19.custom.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#TextBox6").datepicker();
        });
    </script>

    <style type="text/css">
    .ui-datepicker { font-size:8pt !important}
    </style>
    <script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script> 
    <style type="text/css">
        .GridviewDiv {font-size: 100%; font-family: 'Lucida Grande', 'Lucida Sans Unicode', Verdana, Arial, Helevetica, sans-serif; color: #303933;}
        .headerstyle
            {
             color:black;border-right-color:#abb079; font-size: 12px;  border-bottom-color:#abb079;background-color: #f0f5f5;padding:0.5em 0.5em 0.5em 0.5em;text-align:center;
            }
        </style>
 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                  <div class="note note-info note-shadow">
                     <div class="col-lg-7"  style="text-align:left" >  Invoice  </div>  
                     <div class="col-lg-3"   style="text-align:right" >    
                       <%--| <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript: printDiv('wrapper')" />--%>   
                         |<%-- <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="PrintInvoice " />--%><asp:Button ID="Button1" runat="server" Text="Print Invoice" class="btn btn-danger" OnClick="Button1_Click"/>
                         
                         <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                      </div><br />
                         <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="658px" Width="1041px" DocumentMapWidth="50%" SizeToReportContent="True" ZoomMode="FullPage"></rsweb:ReportViewer>
                         <asp:Label ID="Label22" runat="server" Text="123456789"></asp:Label>
                   </div>
                  <table>
                                <tr>
                                   <td style="vertical-align:top">

                                        <div id="wrapper"  style="width:100% ;margin-left:10px; background-color:#f0f5f5;color:black" >
                                             <div  style="width:100% ;margin-left:0px; background-color:#f0f5f5;color:black" class=" GridviewDiv note note-info note-shadow">
                                                   <table>
                                                        <tr>
                                                            <td style="vertical-align:top;width:33%;text-align:left;margin-left:0px">
                                                                    <asp:Label ID="Label13" runat="server" Font-Size="XX-Large" Font-Bold="true" Text="Label"></asp:Label><br />
                                                                    <asp:Label ID="Label14" runat="server" Font-Size="10px" Font-Bold="true"  Text="Label"></asp:Label><br />
                                                                    <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label><br />
                                                                    <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label><br />
                                                            </td>
                                                            <td style="vertical-align:middle;text-align:left;width:33%">
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="Label6"  runat="server" Text=" Invoice " Font-Size="50px"></asp:Label>

                                                            </td>
                                                            <td style="vertical-align:top;text-align:right;width:33%">
                                                                <img src="../img/logo.png" style="width:150px ; height:70px ;"  />  
    	                                                        <asp:Label ID="Label10" runat="server" Visible="false" Text="Label"></asp:Label>
                                                            </td>
                                                        </tr>
                                                   </table>
                                             </div>
                                             <div class="GridviewDiv" style="width:100%">
    	                       
                                                <table style="width:100%">
                                                <tr>
                                                    <td style="width:33%;vertical-align:top"  >
                                                        <b style="font-size:x-large"> Invoice TO: </b><br />
                                                            <asp:TextBox ID="TextBox1" Height="12px" BorderStyle="None" BackColor="#f0f5f5" class="effect-4 input-effect" runat="server" type="text" Font-Size="10px" Width="150px" AutoPostBack="true" placeholder="Customer ID" OnTextChanged="TextBox1_TextChanged" > </asp:TextBox><br />
                                                            <asp:TextBox  Height="12px" ID="TextBox2" BackColor="#f0f5f5"  class="effect-4 input-effect" Font-Size="10px" runat="server" type="text" Width="150px" placeholder="Customer Name "> </asp:TextBox><br />
                                                            <asp:TextBox  Height="12px" ID="TextBox3" BackColor="#f0f5f5"  class="effect-4 input-effect" Font-Size="10px" runat="server" type="text" Width="150px" placeholder="Mobile Number"> </asp:TextBox><br />
                                                            <asp:TextBox  ID="TextBox4"   BackColor="#f0f5f5"  class="effect-4 input-effect"  runat="server" type="text" Width="150px" Font-Size="10px" Height="12px" placeholder="Bill To"  > </asp:TextBox><br />
                                                            <asp:TextBox  ID="TextBox13"  class="effect-4 input-effect" Visible="false"  Height="12px" BackColor="#f0f5f5"  runat="server" type="text" Width="150px" Font-Size="10px"  placeholder="Ship To"  > </asp:TextBox>
                                                    </td>
                                                    <td>

                      
                                                    </td>
    	                                            <td style="width:40% ;vertical-align:top;margin-left:50%"><b>Invoice #</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="TextBox14" Font-Size="10px" runat="server" Height="16px" class="effect-1 input-effect"  BackColor="#f0f5f5" placeholder="Invoice Number" type="text" Width="120px"> </asp:TextBox>
                                                        <br />
                                                        <b>Invoice Date : &nbsp;&nbsp;</b><asp:TextBox ID="TextBox15" Font-Size="10px" runat="server" BackColor="#f0f5f5" Height="16px"  class="effect-1 input-effect"  Enabled="false" placeholder="Invoice Date" type="text" Width="120px"> </asp:TextBox>
                                                        <br />
                                                        <b>Payment Term :</b><asp:TextBox ID="TextBox5" runat="server" Font-Size="10px" class="effect-1 input-effect" Height="16px" BackColor="#f0f5f5" placeholder="Payment Term" type="text" Width="120px" ></asp:TextBox>
                                                        <br />
                                                        <b>Payment Date :</b>
                                                        <asp:TextBox ID="TextBox6" runat="server" class="effect-1 input-effect" Font-Size="10px" BackColor="#f0f5f5" Height="16px" placeholder="Payment Date" type="text" Width="120px" ></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <div>
                                                            <asp:Button ID="btnAdd" runat="server" class="btn btn-info" OnClick="btnAdd_Click" Text="Add Line" />
                                                            <asp:LinkButton ID="LinkButton1" runat="server" class="btn btn-info" OnClick="LinkButton1_Click ">Create Invoice</asp:LinkButton>
                                                        </div>  
                                                        </td>
                                                </tr>
    	                                        </table>
                                                 <hr style="border:solid;height:1px" ></hr>
        
                                              <div class="GridviewDiv" >
         
                                               </div>
                                              <div  style="width:100%">
                                                    <div style="margin-left:60%">
                                                         <asp:Label ID="Label2"   runat="server" Font-Size="20px" ForeColor="Black"  Font-Bold="true" Text="  TOTAL  "></asp:Label>
                                                         <asp:Label ID="Label3"  Font-Size="20px"  ForeColor="Green" Font-Bold="true" runat="server" Text=" 0.00  "></asp:Label>
                                                    </div>
                                                </div>
                                              </div>      
                                             <div class="GridviewDiv">
                                                                                                         <%--<asp:Label ID="Label1"  class="effect-4 input-effect" Font-Size="55px" heigth="10px" ForeColor="Green" Font-Bold="true" runat="server" width=  "100%"></asp:Label>--%>

                                                    <asp:GridView runat="server" ID="gvDetails"   GridLines="none" ShowFooter="false" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" OnRowDeleting="gvDetails_RowDeleting">
                                                        <HeaderStyle CssClass="headerstyle"  />
                                                        <Columns>
                                                            <asp:BoundField DataField="rowid" HeaderText="S/N" ReadOnly="true" Visible="false"/>

                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <%--    <asp:ListBox ID="ListBox1"  OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:ListBox>--%>
                                                                    <asp:TextBox ID="ItemCode" runat="server"   AutoPostBack="true" OnTextChanged="ItemCode_TextChanged1" class="effect-4 input-effect" />
                                                                </ItemTemplate> 
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtName" runat="server"   class="effect-4 input-effect" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "Price">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtPrice" runat="server" AutoPostBack="true" OnTextChanged="txtPrice_TextChanged" Text="0.00" class="effect-4 input-effect" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "QTY" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Qty" runat="server" AutoPostBack="true" OnTextChanged="Qty_TextChanged" Text="0.00" class="effect-4 input-effect" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "Dis">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="Dis" runat="server" AutoPostBack="true" OnTextChanged="Dis_TextChanged" Text="0.00" class="effect-4 input-effect" />
                                                            </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText = "Total">
                                                                <ItemTemplate>       
                                                                    <asp:TextBox ID="Total" runat="server" AutoPostBack="true" OnTextChanged="Total_TextChanged" Text="0.00"  class="effect-4 input-effect" />
                                                               </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Image" SelectImageUrl="~/assets/img/icon-color.png" ItemStyle-Width="5px" DeleteText="X"  />
                                                        </Columns>
                                                    </asp:GridView>
             	  
                                               </div>

                                             <%--  </asp:Panel>--%>
                                             <div style="width:100%" >
                                                    <div style="margin-left:60%" >
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <table>
                                                                    <tr>
                                                                        <td><b>Sub Total &nbsp; &nbsp;</b>
                                                                            <br />
                                                                            <b>VAT(<asp:Label ID="Label1" runat="server" Text=""></asp:Label>%) </b>
                                                                            <br />
                                                                            <b>Total </b>
                                                                            <br />
                                                                            <b>Paid </b>
                                                                            <br />
                                                                            <b>Due </b></td>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text="0.00"></asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="Label5" runat="server" Text="0.00"></asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="Label7" runat="server" Text="0.0"></asp:Label>
                                                                            <br />
                                                                            <asp:TextBox ID="TextBox16" runat="server" BackColor="#f0f5f5" AutoPostBack="true" OnTextChanged="TextBox16_TextChanged" BorderStyle="None" Width="43px"></asp:TextBox>
                                                                            <br />
                                                                            <asp:Label ID="Label9" runat="server" Text="0.00"></asp:Label>
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                   
                                                                </table>
                                
                                                            </td>
                                                        </tr>

                                                    </table>
                                                    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                                                    </div> 
                                                    <hr style="border:solid;height:1px" ></hr>
                                                    <div style="margin-left:15%; margin-top: 0px;">
                                                        <asp:Label ID="Label19" runat="server" Font-Bold="true" Font-Size="Large" Text="Accounts Information"></asp:Label><br />
                                                        <b>Bank Name</b> <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label><br />
                                                        <b>Account Name</b> <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label><br />
                                                        <b>Account Number</b> <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label><br />
                                                        <b>Reference Number </b> <asp:Label ID="Label21" runat="server" Text="Reference NO:YOUR INVOICE NUMBER"></asp:Label><br />
                                                        <asp:Label ID="Label17" runat="server" Font-Bold="true" Font-Size="Large" Text="Label"></asp:Label><br />   

                                                    </div>
                                            </div>    
                                        </div>

                                     </td>
                                   <td style="vertical-align:top">
                   

                                                      </td>
                                </tr>
                      </table>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

