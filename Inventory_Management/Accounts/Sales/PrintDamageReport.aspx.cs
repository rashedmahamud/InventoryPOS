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

public partial class Accounts_Sales_PrintDamageRepoert : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PrintDamageReport();

        }
    }

    public void PrintDamageReport()
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

        string ss = (string)Session["ShopID"];
        string CompanyName = null;
        string ComapanyAddress = null;
        string CompanyMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;
        string Date = Session["DateFrom_DamageReport"].ToString();


        List<DamageItemList> DamageItemList = new List<DamageItemList>();
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ItemCode as Code ,ItemName as Name ,Qty   as Quantity ,Price ,total  as Total FROM tbl_Damage where Logdate = '"+Date+"' and ShopID = '"+ss+"'";
            cmd.Connection = cn;
            cn.Open();


            SqlDataReader rd = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.Load(rd);
            List<DataRow> dr = dt.AsEnumerable().ToList();


            for (int i = 0; i < dr.Count; i++)
            {
                DamageItemList list = new DamageItemList();

                list.Code = dr[i].ItemArray[0].ToString();
                list.Name = dr[i].ItemArray[1].ToString();
                list.Quantity = dr[i].ItemArray[2].ToString();
                list.Price = dr[i].ItemArray[3].ToString();
                list.Total = Convert.ToDouble(dr[i].ItemArray[4]);

                DamageItemList.Add(list);
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

        var reportParameters = new ReportParameterCollection
           {
               new ReportParameter("CompanyName",CompanyName),
               new ReportParameter("ComapanyAddress",ComapanyAddress),
               new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
               new ReportParameter("CompanyWebsite",CompanyWebsite),
               new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),
           };


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/DamageItemReport.rdlc");

        ReportDataSource datasource = new ReportDataSource("DamageItemListDataSet", DamageItemList);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SetParameters(reportParameters);

    }
}