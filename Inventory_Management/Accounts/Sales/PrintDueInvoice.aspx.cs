using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Accounts_Sales_PrintDueInvoice : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
            PrintDueInvoice();
        }

    }

    public void PrintDueInvoice() {



        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " Select ID,CustName,totalpayable,paidAmount,dueAmount,ShopId,ordedate from tbl_SalesPayment	where  dueAmount > 0 order by ID asc";
            cmd.Connection = cn;
            cn.Open();


            SqlDataReader rd = cmd.ExecuteReader();

             var dt = new DataTable();
             dt.Load(rd);
             List<DataRow> dr = dt.AsEnumerable().ToList();

                List<DueInvoiceList> dueList = new List<DueInvoiceList>();
                for (int i = 0; i < dr.Count; i++)
                {
                    DueInvoiceList list = new DueInvoiceList();

                    list.InvoiceNo = dr[i].ItemArray[0].ToString();
                    list.CustomerName = dr[i].ItemArray[1].ToString();
                    list.Totalpayable = Convert.ToDouble(dr[i].ItemArray[2]);
                    list.PaidAmount = Convert.ToDouble(dr[i].ItemArray[3]);
                    list.DueAmount =Convert.ToDouble(dr[i].ItemArray[4]);
                    list.ShopId = dr[i].ItemArray[5].ToString();
                    list.Ordedate = dr[i].ItemArray[6].ToString();
                    dueList.Add(list);
                }


            //var reportParameters = new ReportParameterCollection
            //   {
            //       new ReportParameter("CompanyName",CompanyName),
            //       new ReportParameter("ComapanyAddress",ComapanyAddress),
            //       new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
            //       new ReportParameter("CompanyWebsite",CompanyWebsite),
            //       new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),

            //       new ReportParameter("CustomerID",CustomerID),
            //       new ReportParameter("CustomerName",CustomerName),
            //       new ReportParameter("CustomerMobileNumber",customerMobileNumber),
            //       new ReportParameter("BillTO",BillTO),

            //       new ReportParameter("BankName",bankName),
            //       new ReportParameter("AccountName",accountName),
            //       new ReportParameter("AccountNumber",accountNumber),
            //       new ReportParameter("SubTotal",SubTotal),
            //       new ReportParameter ("VAT_Calculation_on_Item", VAT_Calculation_on_Item),
            //       new ReportParameter("VAT_Percent", VAT_Percent),
            //       new ReportParameter("Paid",Paid),
            //       new ReportParameter("Due",Due),
            //       new ReportParameter("TotalQty",TotalQty),
            //       new ReportParameter("Total_after_adding_vat",Total_after_adding_vat)

            //   };


            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/DueInvoiceReport.rdlc");

            ReportDataSource datasource = new ReportDataSource("DueInvoice", dueList);
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportViewer1.LocalReport.EnableExternalImages = true;
            ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

            ReportViewer1.LocalReport.DataSources.Add(datasource);
            //ReportViewer1.LocalReport.SetParameters(reportParameters);
            cn.Close();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }



    }
}