<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" Inherits="Accounts_Invoice" Codebehind="Invoice.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="atk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Textboxcss.css" rel="stylesheet" />
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

 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
   <div class="panel panel-default "  >
          

           <div>
           
               <div style="margin-left:0 ;"class="btn btn-info"  >
               <div style="margin-left:150px ">
                   <table style="border:none; width:100%; margin-left:50px ; vertical-align:top" >
                 
                    <tr >
                       <td style="width:33%">
                           <asp:Label ID="Label1"  runat="server" Font-Bold="true" Font-Size="XX-Large" Text=" Bargainnshop "></asp:Label><br />
                           <asp:Label ID="Label4"  runat="server" Text=" House# 19,Road # 4,Section A ,Mirpur 11 "></asp:Label>
                           
                                                      <asp:Label ID="Label5"  runat="server" Text=" Phone:01772223060 "></asp:Label>

                       </td>
                        <td style="width:33%">

                       </td>
                        <td style="width:33% ">
                           
       	
                       </td>
                   </tr>

               </table>
                   </div>
    	</div>
    	<div style="margin-left:50px" >
        	   <asp:TextBox ID="TextBox1" class="effect-4 input-effect" runat="server" type="text" Width="250px" AutoPostBack="true" placeholder="Customer ID"  > </asp:TextBox>
         
        </div>
        <div >
         <asp:TextBox  ID="TextBox2" style="margin-left:50px"  class="effect-4 input-effect" runat="server" type="text" Width="250px" placeholder="Customer Name "> </asp:TextBox>
        
        </div>
       
    
        <div >
         <asp:TextBox  ID="TextBox3" style="margin-left:50px"  class="effect-4 input-effect" runat="server" type="text" Width="250px" placeholder="Mobile Number"> </asp:TextBox>
         
        </div>
                 <div   style="margin-left:700px" >
                     <b>Invoice #</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox  ID="TextBox14"     class="effect-4 input-effect"  runat="server" type="text" Width="150px"  placeholder="Invoice Number"> </asp:TextBox>
                     <b>Invoice Date : &nbsp;&nbsp;</b><asp:TextBox  ID="TextBox15"  Enabled="false"  BackColor="#cc9900" class="effect-4 input-effect"  runat="server" type="text" Width="150px"  placeholder="Invoice Date"> </asp:TextBox>

<b>Payment Term :</b><asp:TextBox  ID="TextBox5"    class="effect-4 input-effect"  runat="server" type="text" Width="150px"  placeholder="Payment Term"> </asp:TextBox>
         
   <b>Payment Date :</b>      <asp:TextBox  ID="TextBox6"    class="effect-4 input-effect" runat="server" type="text"  Width="150px"  placeholder="Payment Date"> </asp:TextBox>
           
               
        </div>
        <div >
     <b> Bill To  </b> <asp:TextBox  ID="TextBox4"  class="effect-4 input-effect" style="margin-left:50px"  runat="server" type="text" Width="250px" Height="100px" placeholder="Bill To" > </asp:TextBox>
   <b> Ship To </b> <asp:TextBox  ID="TextBox13"  class="effect-4 input-effect" style="margin-left:50px"  runat="server" type="text" Width="250px" Height="100px" placeholder="Ship To"  > </asp:TextBox>
            <asp:Label ID="Label2"  class="effect-4 input-effect" runat="server" Font-Size="XX-Large" ForeColor="Black" Height="83px" Font-Bold="true" Text="  TOTAL  "></asp:Label>
                          
                                  <asp:Label ID="Label3"  class="effect-4 input-effect" Font-Size="XX-Large" Height="83px" ForeColor="Green" Font-Bold="true" runat="server" Text=" 0.00  "></asp:Label>

           
        </div>
      
       
   
   
              
  <div >
         <asp:TextBox  ID="TextBox12" Enabled="false"  class="btn btn-info" style="margin-left:0px" runat="server" type="text" Width="100%" > </asp:TextBox>
            
            	<i></i>
         
        </div>
                <div>
                    <asp:DataList ID="Datalist1" runat="server" Width="100%" >
                        <ItemTemplate>
                            <div>
                                <asp:TextBox id="TextBox7" runat="server"></asp:TextBox>
                            </div>

                        </ItemTemplate>

                    </asp:DataList>
                    </div>
                    </ContentTemplate>
               </asp:UpdatePanel>
    
    </asp:Content>
