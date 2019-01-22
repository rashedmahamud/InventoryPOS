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

public partial class Purchase_module_PrintPurchase_Invoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

            PrintPurchase_Invoice();
        }
    }

    public void PrintPurchase_Invoice() {

        string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

        string ss = (string)Session["ShopID"];
        string CompanyName = null;
        string ComapanyAddress = null;
        string CompanyMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;



        List<PurchaseInvoiceItemList> PurchaseInvoiceItemList = new List<PurchaseInvoiceItemList>();
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("[SP_INV_DataBind_PurchaseItemList]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["purchaseCode"].ToString());
            cn.Open();




            SqlDataReader rd = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.Load(rd);
            List<DataRow> dr = dt.AsEnumerable().ToList();


            for (int i = 0; i < dr.Count; i++)
            {
                PurchaseInvoiceItemList list = new PurchaseInvoiceItemList();

                list.ItemCode = dr[i].ItemArray[0].ToString();
                list.Description = dr[i].ItemArray[1].ToString();
                list.Quantity = dr[i].ItemArray[2].ToString();
                list.UnitPrice = dr[i].ItemArray[3].ToString();
                list.Total = Convert.ToDouble(dr[i].ItemArray[4]);

                PurchaseInvoiceItemList.Add(list);
            }
            cn.Close();

        }

        catch { }


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
                }
                cn.Close();
            }
        }
        catch { }

        string SupplierName = null;
        string SupplierComment = null;
        string SupplierCompanyName = null;
        string SupplierAddress = null;
        string SuulierPhoneNumber = null;
        string SupplierEmailAddress = null;

        // Invoice Info
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseInvoice", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["purchaseCode"].ToString());

            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                SupplierName = rd["Name"].ToString();
                SupplierComment = rd["comment"].ToString();
                SupplierCompanyName = rd["CompanyName"].ToString();
                SupplierAddress = rd["Address"].ToString();
                SuulierPhoneNumber = rd["Phone"].ToString();
                SupplierEmailAddress = rd["Email"].ToString();
            }
            cn.Close();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }


        var reportParameters = new ReportParameterCollection
           {
               new ReportParameter("CompanyName",CompanyName),
               new ReportParameter("ComapanyAddress",ComapanyAddress),
               new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
               new ReportParameter("CompanyWebsite",CompanyWebsite),
               new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),

                new ReportParameter("SupplierName",SupplierName),
               new ReportParameter("SupplierComment",SupplierComment),
               new ReportParameter("SupplierCompanyName",SupplierCompanyName),
               new ReportParameter("SupplierAddress",SupplierAddress),
               new ReportParameter("SuulierPhoneNumber",SuulierPhoneNumber),
               new ReportParameter("SupplierEmailAddress",SupplierEmailAddress),
               new ReportParameter("PurchaseInvoiceNo", Session["purchaseCode"].ToString())

           };


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/Purchase_Invoice.rdlc");

        ReportDataSource datasource = new ReportDataSource("PurchaseInvoiceItemListDataSet", PurchaseInvoiceItemList);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SetParameters(reportParameters);
    }
}