<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Bootstrap.master" AutoEventWireup="true" CodeFile="AccessLog.aspx.cs" Inherits="Report_AccessLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script src="../assets/scripts/PrintPosCopy.js" type="text/javascript"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

                  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"
                            ViewStateMode="Enabled">
                   <ContentTemplate>
                             <div class="panel-body">  
                                     <input type="button" class="btn btn-success btn-xs" value="Print"  onclick="javascript:printDiv('wrapper')" />   <br />                                                     
                                        <asp:Label ID="lbtotalRow" runat="server" Text="------"></asp:Label>[Auto update in Every 5 minute]
                                       
                                        <div id="wrapper">  Access Log
                                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="550px"> 
                                                    <asp:GridView ID="grdAccessDataList" runat="server"   class="table table-striped table-hover" Font-Size="11px">                                                 
                                              
                                                    </asp:GridView>
                                                </asp:Panel>   
                                        </div>                
                            </div>
                   </ContentTemplate>
                   <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                   </Triggers>
                </asp:UpdatePanel>
                <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                </asp:Timer>

</asp:Content>


