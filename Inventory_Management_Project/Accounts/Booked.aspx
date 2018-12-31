<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" Inherits="Booked" Codebehind="Booked.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
        .auto-style2 {
            height: 4px;
        }
         .auto-style4 {
             width: 212px;
         }
         .auto-style6 {
             width: 71px;
         }

         </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <br />
    <br />
    <br />
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
                  <div style="height:100px ;width:100%;background-color:black">
                      <div style="margin-left:30%;">
                          <h1 style="color:white;font-size:55px"> Reservation check </h1>
                      </div>
                  </div>
   <div style="margin-left:30%">
                   <asp:Label ID="Label1" runat="server" Visible="false" Text="1"></asp:Label>
       <asp:Label ID="Label3" runat="server"  Text=""></asp:Label>
  
    <asp:Label ID="Label2" runat="server" Text="Date( DD/MM/YYYY)"></asp:Label>
      
   
    <asp:TextBox ID="TextBox1"  Width="200px" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"  runat="server">

    </asp:TextBox>  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="SEARCH" />
    </div>
                  
               <asp:Label ID="Label9" Visible="false" runat="server" Text="Label"></asp:Label>
                  <div style="width:100%">
                      <asp:DataList ID="DataList2" runat="server"  Width="100%"   RepeatColumns="5" RepeatDirection="Horizontal"  BorderStyle="None" CaptionAlign="Bottom" CssClass="active left" HorizontalAlign="Left"  style="vertical-align:top;text-align:left;margin-left:50px" ShowFooter="False" ShowHeader="False" CellPadding="0"  >
        
           <ItemStyle BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
        
           <ItemTemplate>
                <table  style="border:none;background-color:#00395d;width:100%"  >
                     <tr  >
                        <td>
                                                                      <asp:Label   ID="Label8" Font-Size="20px" Visible="false" ForeColor="white"  runat="server" Text='<%# Bind("ID") %>'></asp:Label> <br />

                                          <asp:Label   ID="Label4" Font-Size="20px" ForeColor="white"  runat="server" Text='<%# Bind("Customer_Name") %>'></asp:Label> <br />
                            </td>
                        </tr>
                    <tr>
                          <td>
                                                           <asp:Label   ID="Label5" Font-Size="20px" ForeColor="white"  runat="server" Text='<%# Bind("Customer_Mobile") %>'></asp:Label> 
                                 </td>
                            </tr>
                    <tr>
                          <td>
                                                                                              <asp:Label   ID="Label6" Font-Size="20px" ForeColor="white"  runat="server" Text='<%# Bind("Arrival_Date") %>'></asp:Label> 


                        </td>
                        
                    </tr>
                    <tr>
                          <td>
                                                                                             <asp:Label   ID="Label7" Font-Size="20px" ForeColor="white"  runat="server" Text='<%# Bind("Time_Slot") %>'></asp:Label> 


                        </td>
                        
                    </tr>
                    <tr  >
                        
                        <td  style=" color:white " >
                   <asp:LinkButton ID="lnkbtnRelatedLinks" class="roundedcorners btn btn-primary btn-sm " Width ="307px" Height="150px" BackColor="Black" Style="line-height:170px;text-align:center;text-decoration:none" ForeColor="white" Font-Size="70px" runat="server" Text='<%#Eval("Table_Number") %>' OnClick="lnkbtnRelatedLinks_Click"> <br /><br />  </asp:LinkButton><br /><br />
                        </td>
                    </tr>

                </table>
            </ItemTemplate>
            <SelectedItemStyle  Font-Bold="True" ForeColor="Black" />
        </asp:DataList>
      <hr style="border:double;width:100%;color:green;" />  
                      </div>
                  </ContentTemplate>
        </asp:UpdatePanel>
       
</asp:Content>

