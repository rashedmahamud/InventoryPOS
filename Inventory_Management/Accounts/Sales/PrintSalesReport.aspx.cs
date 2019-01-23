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

public partial class Accounts_Sales_PrintSalesReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

            PrintSalesReport();
        }
    }

    public void PrintSalesReport()
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

        string ss = (string)Session["ShopID"];
        string CompanyName = null;
        string ComapanyAddress = null;
        string CompanyMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;
        string InvocieNo = null;
        string CompanyLogo = null;
        if (Session["InvocieNo"] != "")
        {
            InvocieNo = Session["InvocieNo"].ToString();
        }


        List<SalesReports> SalesList = new List<SalesReports>();
        try
        {
            if (InvocieNo == "")
            {
                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesReport_DateToDate", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.Parameters.AddWithValue("@DateFrom", Session["DateFrom"].ToString());
                cmd.Parameters.AddWithValue("@DateTo", Session["DateTo"].ToString());

                SqlDataReader rd = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(rd);
                List<DataRow> dr = dt.AsEnumerable().ToList();


                for (int i = 0; i < dr.Count; i++)
                {
                    SalesReports list = new SalesReports();

                    list.InvoiceNo = dr[i].ItemArray[1].ToString();
                    list.CustomerName = dr[i].ItemArray[2].ToString();
                    list.CustomerId = dr[i].ItemArray[3].ToString();
                    list.PhoneNumber = dr[i].ItemArray[4].ToString();
                    list.Total = Convert.ToDouble(dr[i].ItemArray[5]);
                    list.Due = Convert.ToDouble(dr[i].ItemArray[6]);
                    list.Date = dr[i].ItemArray[7].ToString();
                    list.ServedBy = dr[i].ItemArray[8].ToString();
                    list.ShopId = dr[i].ItemArray[9].ToString();

                    SalesList.Add(list);
                }
                cn.Close();
            }
            else
            {

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesReport_Search", cn);
                //cmd.Parameters.AddWithValue("@DateFrom", txtDateFrom.Text);
                //cmd.Parameters.AddWithValue("@DateTo", txtDateTo.Text);
                cmd.Parameters.AddWithValue("@value", Session["InvocieNo"].ToString());
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader rd = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(rd);
                List<DataRow> dr = dt.AsEnumerable().ToList();


                for (int i = 0; i < dr.Count; i++)
                {
                    SalesReports list = new SalesReports();

                    list.InvoiceNo = dr[i].ItemArray[1].ToString();
                    list.CustomerName = dr[i].ItemArray[2].ToString();
                    list.CustomerId = dr[i].ItemArray[3].ToString();
                    list.PhoneNumber = dr[i].ItemArray[4].ToString();
                    list.Total = Convert.ToDouble(dr[i].ItemArray[5]);
                    list.Due = Convert.ToDouble(dr[i].ItemArray[6]);
                    list.Date = dr[i].ItemArray[7].ToString();
                    list.ServedBy = dr[i].ItemArray[8].ToString();
                    list.ShopId = dr[i].ItemArray[9].ToString();

                    SalesList.Add(list);
                }
                cn.Close();

            }

        }

        catch
        {
        }


        // Company Info

        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select * from tbl_settings where Location='" + ss + "'";
            cmd.Connection = cn;
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {
                while (rd4.Read())
                {
                    CompanyName = (rd4["CompanyName"].ToString());
                    ComapanyAddress = rd4["CompanyAddress"].ToString();
                    CompanyWebsite = rd4["WebAddress"].ToString();
                    CompanyMobileNumber = rd4["Phone"].ToString();
                    CompanyFooterMassage = rd4["Footermsg"].ToString();
                    CompanyLogo = rd4["CompanyLogo"].ToString();
                }
                cn.Close();
            }
        }
        catch { }
         string imagePath = new Uri(Server.MapPath(CompanyLogo)).AbsoluteUri;
        var reportParameters = new ReportParameterCollection
           {
               new ReportParameter("CompanyName",CompanyName),
               new ReportParameter("ComapanyAddress",ComapanyAddress),
               new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
               new ReportParameter("CompanyWebsite",CompanyWebsite),
               new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),
               new ReportParameter("ComapanyLogo",imagePath ),

               new ReportParameter("DateFrom",Session["DateFrom"].ToString()),
               new ReportParameter("DateTo",Session["DateTo"].ToString()),
               new ReportParameter("InvocieNo",InvocieNo)

           };


                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/SalesReport.rdlc");

                ReportDataSource datasource = new ReportDataSource("SalesReport", SalesList);
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportViewer1.LocalReport.EnableExternalImages = true;
                ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.SetParameters(reportParameters);

    }
}