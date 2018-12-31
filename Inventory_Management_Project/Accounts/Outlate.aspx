<%@ Page Language="C#" AutoEventWireup="true" Inherits="Accounts_Outlate" Codebehind="Outlate.aspx.cs" %>
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

</head>
<body>
    <form id="form1" runat="server">
   
        
         <div>
               <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
             
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
          <div > 
                <asp:Label ID="l1" Visible="false" runat="server" Text="Label"></asp:Label>
                    <div class="panel panel-primary"  > 
                            <header class="panel-heading" style="background-color:#00395d" >
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="Add New Company " PostBackUrl="~\Accounts\Settings.aspx" />
                                            
               


                            </header>
                     
                                      
               
                </div>
    </div>

      <div style="text-align:center; margin-left:10%; width:100%">  
       
           
              <br /><br /><br /><br /><br /><br /><br /><br />
               <asp:DataList ID="DataList2" runat="server"   RepeatColumns="3" RepeatDirection="Horizontal"  BorderStyle="None" CaptionAlign="Bottom" CssClass="active left" HorizontalAlign="Left"  style="vertical-align:top;text-align:left" ShowFooter="False" ShowHeader="False" CellPadding="0"  >
        
           <ItemStyle BorderStyle="None" HorizontalAlign="Left" VerticalAlign="Top" Wrap="True" />
        
           <ItemTemplate>
                <table class="auto-style1" style="border:none;background-color:white;width:100%;text-align:center"  >
                    <tr  >
                        <td class="auto-style6" style=" color:white "  >
                           
                   <asp:LinkButton ID="lnkbtnRelatedLinks" class="roundedcorners btn btn-primary btn-sm " Width ="100%" Height="250px" BackColor="#ff6600" Style="line-height:250px;text-align:center;" ForeColor="white" Font-Size="XX-Large" runat="server" Text='<%#Eval("CompanyName") %>' OnClick="lnkbtnRelatedLinks_Click"></asp:LinkButton>
                        </td>
                    </tr>

                </table>
            </ItemTemplate>
            <SelectedItemStyle  Font-Bold="True" ForeColor="Black" />
        </asp:DataList>

                         </asp:Panel> 
        </div>

                  </ContentTemplate>
         </asp:UpdatePanel>
</div>
    </form>
</body>
</html>
