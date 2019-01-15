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

public partial class Suppliers_PrintSupplierList : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            PrintSupplierList();
        }

    }


    public void PrintSupplierList()
    {

        string ss = (string)Session["ShopID"];
        string CompanyName = null;
        string ComapanyAddress = null;
        string CompanyMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;

        List<SupplierList> SupplierList = new List<SupplierList>();
        try
        {

            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SupplierList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.Load(rd);
            List<DataRow> dr = dt.AsEnumerable().ToList();
            for (int i = 0; i < dr.Count; i++)
            {
                SupplierList list = new SupplierList();

                list.ID = Convert.ToInt32(dr[i].ItemArray[1]);
                list.Name = dr[i].ItemArray[2].ToString();
                list.CompanyName = dr[i].ItemArray[3].ToString();
                list.Phone = dr[i].ItemArray[4].ToString();
                list.Email = dr[i].ItemArray[5].ToString();
                list.Address = dr[i].ItemArray[6].ToString();
                list.City = dr[i].ItemArray[7].ToString();
                list.Type = dr[i].ItemArray[8].ToString();


                SupplierList.Add(list);
            }
            con.Close();


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
               new ReportParameter("CompanyFooterMassage",CompanyFooterMassage)
           };


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/SupplierList.rdlc");

        ReportDataSource datasource = new ReportDataSource("SupplierListDataset", SupplierList);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SetParameters(reportParameters);


    }
}