<%@ Page Title="" Language="C#" MasterPageFile="../MasterPage/Bootstrap.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Email_Default"  EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
   
   <div class="col-lg-2" style="text-align:left">
    
        <div class="panel panel-info" style="text-align:left;height:570px">      
        
          <div class="panel-body">  
                <ul class="nav nav-pills nav-stacked" id="messageBoxList">
                   
                        <li class="active"> <a href="#" ng-click="LoadMessageListFromServer(0)">Inbox (2)</a> </li>
                        <li><a href="#" ng-click="LoadMessageListFromServer(1)">Sent</a></li>
                        <li><a href="#" ng-click="LoadMessageListFromServer(2)">Drafts</a></li>
                        <li><a href="#" ng-click="LoadMessageListFromServer(3)">Trash</a></li>
                </ul> 
                
            </div>
        </div>
    </div>


   <%-- subject title--%>
     <div class="col-lg-10" style="text-align:left">           
                <div class="panel-body"> 
                          <div class="col-lg-3">  
                              <asp:TextBox ID="txtSearch" AutoPostBack="true" placeholder="Search" class="form-control" runat="server"></asp:TextBox>     <br />           
                              <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="450px"> 
                                   <asp:GridView ID="grdsubject"   runat="server" OnRowDataBound="OnRowDataBound"  
                                    OnSelectedIndexChanged="OnSelectedIndexChanged" ShowHeader="false" class="table table-bordered table-condensed  table-hover" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="ID"    ItemStyle-Width="10"  />
                                            <asp:BoundField DataField="Subject"     ItemStyle-Width="150" />                     
                                        </Columns>                  
                                    </asp:GridView>
                              
                              </asp:Panel>
                           </div>
                           <div class="col-lg-7">                     
                                <asp:LinkButton ID="lnkCreateNew" Font-Size="25px" 
                                    class="glyphicon glyphicon-plus-sign" ToolTip="New E-mail" runat="server" 
                                    PostBackUrl="~/Email/CreateNewMail.aspx"></asp:LinkButton>
                                <asp:LinkButton ID="lnkReply" Font-Size="25px" 
                                    class="glyphicon glyphicon-share" ToolTip="Reply" runat="server" 
                                    onclick="lnkReply_Click"></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" Font-Size="25px"    
                                    class="glyphicon glyphicon-trash" ToolTip="trash" runat="server" 
                                    onclick="lnkDelete_Click"></asp:LinkButton>
                                <br />  <br />  
                               
                                <asp:Label ID="lblmailFrom" runat="server" Font-Bold="true" Text=""></asp:Label><br />                          
                                <asp:Label ID="lblmailTo" runat="server" Text=""></asp:Label> <br />
                                <asp:Label ID="lblSubject" runat="server" Font-Size="20px" Text=""></asp:Label> <hr />
                                <asp:Label ID="lblmailbody" runat="server" Text=""></asp:Label>
                           </div>              
                </div>
    </div>



<%--    Message Body --%>

   <%--  <div class="col-lg-1" style="text-align:left">
         <div class="panel panel-info" style="text-align:left;height:570px">     
                <div class="panel-body"> 
                     <asp:GridView ID="grdmailbody"  class="table table-striped table-hover"  runat="server">
                    </asp:GridView>
                    

                </div>
        </div>
    </div>--%>
    
      
</asp:Content>

