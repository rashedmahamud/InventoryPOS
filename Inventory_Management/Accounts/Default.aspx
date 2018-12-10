<%@ Page Title="" Language="C#" MasterPageFile="~/Accounts/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>  
    <script src="../assets/scripts/jsapi" type="text/javascript"></script>     
    <script src="../assets/scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { packages: ['corechart'] });
        // google.load('visualization', '1.1', { 'packages': ['bar'] });
    </script>  
    <script type="text/javascript">
        $(function () {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'Default.aspx/GetChartData',
                data: '{}',
                success:
                function (response) {
                    drawchart(response.d);
                },

                error: function () {
                    alert("Error loading data!");
                }
            });
        })
        function drawchart(dataValues) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'DATE');
            data.addColumn('number', 'Total amount');
            //   data.addColumn('string', 'Total amount2');

            for (var i = 0; i < dataValues.length; i++) {
                // data.addRows([dataValues[i].Year, dataValues[i].Total], dataValues[i].Year);
                data.addRow([dataValues[i].DATE, dataValues[i].Total]);
            }



            //   new google.visualization.PieChart
            //  new google.visualization.BarChart(  //LineChart  //ColumnChart  //ComboChart
            new google.visualization.ColumnChart(document.getElementById('myChartDiv')).
            draw(data, { title: "Last 8 sales" });

            new google.visualization.PieChart(document.getElementById('myChartDivPie')).
            draw(data, { title: "Last 8 sales Pie chart" });

           // new google.visualization.LineChart(document.getElementById('myChartDivLineChart')).
          //  draw(data, { title: "Salary yearly Line Chart" });

           // new google.visualization.BarChart(document.getElementById('myChartDivBarChart')).
           // draw(data, { title: "Salary yearly Bar Chart" });
        }  
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"> 
 
 <div class="col-lg-6">
    
        <div id="myChartDiv" style="width: 500px; height: 400px;">  
            </div>   
       
 </div>
 <div class="col-lg-6">
      
        <div id="myChartDivPie" style="width: 500px; height: 400px;">  
            </div>
         
  </div> 
<div class="col-lg-6"> <hr />
                    <div class="panel panel-primary"  > 
                            <header class="panel-heading">
                                Short link                              
                            </header>
                        <div class="panel-body"> <br />
                            <span  style="font-size:70px" class="fa fa-shopping-cart"></span> <br /><br /><br />
                            <h4>Inventory Management</h4>          

                            <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="POS" PostBackUrl="~/Sales.aspx" />
                         <%--  <asp:Button ID="Button2" class="btn btn-info" runat="server" Text="Order" PostBackUrl="~/Order_module/NewOrder.aspx" />
                            <asp:Button ID="btnStartSales" class="btn btn-danger" runat="server" Text="Start Sales" PostBackUrl="~/Sales/NewSale.aspx" /> 
                            <asp:Button ID="Button3" class="btn btn-success" runat="server" Text="Access Log" PostBackUrl="~/Report/AccessLog.aspx" />--%>
                           <br /> 
                            <p>...</p>
                            Contact <br />
                         

                             <a href="../Report/ChartReport.aspx" title="Chart Report" style="font-size:44px"><span class="fa fa-stats"> </span></a>
                        </div> 
                </div>
</div>

<div class="col-lg-6"> <hr />
                    <div class="panel panel-warning"  > 
                            <header class="panel-heading">
                                Summary 
                            </header>
                        <div class="panel-body"> 
                            <asp:Panel ID="Panel2"    runat="server">              
                                <asp:GridView ID="grdViewInvoSummary"   class="table table-striped table-hover" Font-Size="15px"  runat="server" ShowHeader="False">
                                </asp:GridView>
                            </asp:Panel>
                        </div> 
                </div>
</div>

<%--<div class="col-lg-12"> 
        <div class="portlet box purple">
			<div class="portlet-title">
				<div class="caption">
					<i class="icon-rocket"></i>Task list
				</div>
				<div class="tools">
					<a href="javascript:;" class="collapse">
					</a>
	 
					<a href="javascript:;" class="remove">
					</a>
				</div>
			</div>
            <div class="portlet-body">
                <div class="well well-sm">                      
                        <asp:Label ID="Label1" runat="server" Font-Size="11px" Text="Task list provide all informations, Notices from system admin or Manager"></asp:Label>
                </div> 
				<div class="table-scrollable">
                        <asp:Panel ID="Panel1" Height="250px"   ScrollBars="Vertical" runat="server">              
                                <asp:GridView ID="grdViewTasklist"   class="table table-striped table-hover" Font-Size="11px"  runat="server">
                                </asp:GridView>
                        </asp:Panel>
                </div>
            </div>
        </div>
</div> --%>
</asp:Content>

