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

public partial class Report_StockItemReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            PrintStock();
        }
    }

    public void PrintStock()
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

        string ss = (string)Session["ShopID"];
        string CompanyName = null;
        string ComapanyAddress = null;
        string CompanyMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;
        string InvocieNo = null;


        List<StockitemList> StockitemLists = new List<StockitemList>();
        try
        {

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ItemCode as Code ,ItemName as Name ,ItemCategory as Category ,PurchasePrice as Price ,RetailPrice    as Retail_Price ,ItemQty   as Current_Stock ,MinQty    as Minimum_Qty ,MaxQty    as Mazimum_Qty  ,MDate     as Manufacture_date ,ExDate    as Expire_Date ,SupplierCode as Supplier_Id ,Description ,Status ,ShopID FROM tbl_Item order by Current_Stock asc";
                cmd.Connection = cn;
                cn.Open();


                SqlDataReader rd = cmd.ExecuteReader();

                var dt = new DataTable();
                dt.Load(rd);
                List<DataRow> dr = dt.AsEnumerable().ToList();


                for (int i = 0; i < dr.Count; i++)
                {
                    StockitemList list = new StockitemList();

                    list.Code  = dr[i].ItemArray[0].ToString();
                    list.Name = dr[i].ItemArray[1].ToString();
                    list.Category = dr[i].ItemArray[2].ToString();
                    list.Price = dr[i].ItemArray[3].ToString();
                    list.Retail_Price = dr[i].ItemArray[4].ToString();
                    list.Current_Stock = dr[i].ItemArray[5].ToString();
                    list.Minimum_Qty = dr[i].ItemArray[6].ToString();
                    list.Maximum_Qty = dr[i].ItemArray[7].ToString();
                    list.Manufacture_date = dr[i].ItemArray[8].ToString();
                    list.Expire_Date = dr[i].ItemArray[9].ToString();
                    list.Supplier_Id = dr[i].ItemArray[10].ToString();
                    list.Description = dr[i].ItemArray[11].ToString();
                    list.Status = dr[i].ItemArray[12].ToString();
                    list.ShopID = dr[i].ItemArray[13].ToString();

                    StockitemLists.Add(list);
                }
                cn.Close();

        }

        catch{}


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

        var reportParameters = new ReportParameterCollection
           {
               new ReportParameter("CompanyName",CompanyName),
               new ReportParameter("ComapanyAddress",ComapanyAddress),
               new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
               new ReportParameter("CompanyWebsite",CompanyWebsite),
               new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),
           };


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/StockReport.rdlc");

        ReportDataSource datasource = new ReportDataSource("StockitemList", StockitemLists);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SetParameters(reportParameters);
    }
}