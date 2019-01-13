using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.Reporting.WebForms;


public partial class Accounts_Sales_Returnhistory : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            Label1.Text = Session["ShopID"].ToString();

            Label17.Text = DateTime.Now.ToString();
            SystemInfo();
            TodaysReturn();
        }
        catch
        {

        }

    }

    public void SystemInfo()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "Select * from tbl_settings where Location='" + Label1.Text.ToString() + "'";
            cmd.Connection = cn;
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {

                while (rd4.Read())
                {

                    lblshopTitle.Text = (rd4["CompanyName"].ToString());
                    lblshopAddress.Text = rd4["CompanyAddress"].ToString();
                    lblwebAddress.Text = rd4["WebAddress"].ToString();
                    lblPhone.Text = rd4["Phone"].ToString();
                    //lblFooterMessage.Text = rd4["Footermsg"].ToString();

                }


                cn.Close();
            }

        }
        catch
        {
        }
    }

    public void TodaysReturn()
    {

        string s = DateTime.Now.Date.ToString();
        //string ShopID = Response.Cookies["InventMgtCookies"]["ShopID"];
        string ShopID = Label1.Text.ToString();
        string Logdate = txtDateFrom.Text.Trim();
        string strcon = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
        SqlConnection con = new SqlConnection(strcon);
        SqlCommand cmd = new SqlCommand("SP_Sales_Returndate", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Logdate", Convert.ToDateTime(s));
        cmd.Parameters.AddWithValue("@ShopID", ShopID);
        con.Open();
        grdviewReturnReport.DataSource = cmd.ExecuteReader();
        grdviewReturnReport.EmptyDataText = "No Records Found";
        grdviewReturnReport.DataBind();
        con.Close();
        //  lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";
        //}
        //catch
        //{
        //    //lbtotalRow.Text = "No Records Found";
        //}

    }
    public void ItemsListDataBind()
    {
        DateTime DateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
        string dateFrom = DateFrom.ToString("yyyy/MM/dd");

        DateTime DateTo = DateTime.Parse(txtDateTo.Text.ToString());
        string dateTo = DateTo.ToString("yyyy/MM/dd");

        string ss = (string)Session["ShopID"];

        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();

        cn.Open();
        cmd.CommandText = "select ItemCode as Code,ItemName as Name ,Qty as Quantity,Price,DiscRate as Discount,total as Total, RP_ID as Return_Invoice from tbl_Return where ShopID = '" + ss + "' and Logdate between '" + dateFrom + "' and '" + dateTo + "'";
        cmd.Connection = cn;

        grdviewReturnReport.DataSource = cmd.ExecuteReader();
        grdviewReturnReport.EmptyDataText = "No Records Found";
        grdviewReturnReport.DataBind();
        cn.Close();

    }
    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {
        ItemsListDataBind();
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        ItemsListDataBind();
    }
    protected void printButton_Click(object sender, EventArgs e)
    {

        DateTime DateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
        string dateFrom = DateFrom.ToString("yyyy/MM/dd");

        DateTime DateTo = DateTime.Parse(txtDateTo.Text.ToString());
        string dateTo = DateTo.ToString("yyyy/MM/dd");

        string ss = (string)Session["ShopID"];
        string CompanyName = null;
        string ComapanyAddress = null;
        string CompanyMobileNumber = null;
        string CompanyWebsite = null;
        string CompanyFooterMassage = null;


        List<ReturnReport> ReturnReports = new List<ReturnReport>();


        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "select ItemCode as Code,ItemName as Name ,Qty as Quantity,Price,DiscRate as Discount,total as Total, RP_ID as Return_Invoice from tbl_Return where ShopID = '"+ss+"' and Logdate between '"+dateFrom+"' and '"+dateTo+"'";
            cmd.Connection = cn;



            SqlDataReader rd = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.Load(rd);
            List<DataRow> dr = dt.AsEnumerable().ToList();

            for (int i = 0; i < dr.Count; i++)
            {
                ReturnReport list = new ReturnReport();

                list.Code = dr[i].ItemArray[0].ToString();
                list.Name = dr[i].ItemArray[1].ToString();
                list.Quantity = dr[i].ItemArray[2].ToString();
                list.Price = dr[i].ItemArray[3].ToString();
                list.Discount = dr[i].ItemArray[4].ToString();
                list.Total = Convert.ToDouble(dr[i].ItemArray[5]);
                list.Return_Invoice = dr[i].ItemArray[6].ToString();



                ReturnReports.Add(list);
            }
            cn.Close();

        }
        catch
        {
           // lbtotalRow.Text = "No Records Found";
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



        string Fromdate = txtDateFrom.Text.ToString();
        string Todate = txtDateTo.Text.ToString();
        string ItemCode = txtsearch.Text.ToString();
        string ShopID = txtsearch.Text.ToString();

        var reportParameters = new ReportParameterCollection
           {
               new ReportParameter("CompanyName",CompanyName),
               new ReportParameter("ComapanyAddress",ComapanyAddress),
               new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
               new ReportParameter("CompanyWebsite",CompanyWebsite),
               new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),
               new ReportParameter("DateFrom",Fromdate),
               new ReportParameter("DateTo", Todate)
           };


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RDLCReports/ReturnHistoryReport.rdlc");

        ReportDataSource datasource = new ReportDataSource("ReturnReportsDataset", ReturnReports);
        ReportViewer1.LocalReport.DataSources.Clear();

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.ExportContentDisposition = ContentDisposition.AlwaysInline;

        ReportViewer1.LocalReport.DataSources.Add(datasource);
        ReportViewer1.LocalReport.SetParameters(reportParameters);



    }

}