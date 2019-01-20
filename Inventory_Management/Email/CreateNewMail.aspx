<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="CreateNewMail.aspx.cs" Inherits="Email_CreateNewMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="col-lg-3" style="text-align:left">


          <div class="panel-body">
           <asp:LinkButton ID="lnkBack" Font-Size="25px" class="glyphicon glyphicon-circle-arrow-left" ToolTip="Back to Home" runat="server" PostBackUrl="~/Email/Default.aspx"></asp:LinkButton>
             <asp:LinkButton ID="lnkDelete" Font-Size="25px"        class="glyphicon glyphicon-trash" ToolTip="Discard" runat="server"  ></asp:LinkButton>
              <hr />
              To <br />
                <asp:TextBox ID="txtMailto" class="form-control"  runat="server"></asp:TextBox>

            </div>

    </div>
     <div class="col-lg-7" style="text-align:left">


          <div class="panel-body">
          <asp:LinkButton ID="lnkSend" Font-Size="25px" class="glyphicon glyphicon-send" ToolTip="Send" runat="server"></asp:LinkButton><hr />
              Subject <br />
                <asp:TextBox ID="txtmailSubject" class="form-control" runat="server"></asp:TextBox>
                Message <br />
                <asp:TextBox ID="txtmailbody" class="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
            </div>

    </div>
</asp:Content>

