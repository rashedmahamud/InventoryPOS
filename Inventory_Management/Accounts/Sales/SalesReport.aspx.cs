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

public partial class Sales_SalesReport : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    String test = DateTime.Now.ToString("dd.MM.yyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Title = "Report_" + DateTime.Now.ToString("MMM_dd_yyyy_HH:mm:ss");
            //ItemsListDataBind();
            txtsearch.Focus();
            // lblmsg.Visible = false;
            Label17.Text=test.ToString();
            report();
            SystemInfo();
        }

    }

    // ///////  Item list Databind
    public void ItemsListDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesReport", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(" @ShopId", Session["ShopID"].ToString());
          cn.Open();

            grdviewSalesReport.DataSource = cmd.ExecuteReader();
            //grdviewSalesReport.EmptyDataText = "No Records Found";
            grdviewSalesReport.DataBind();
            cn.Close();
            lbtotalRow.Text = "Last : " + Convert.ToString(grdviewSalesReport.Rows.Count) + " Transactions" + "<br />";
            SystemInfo();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    // total Calculation  show on footer part

    double totalpayable = 0;
    double totalDue = 0;


    protected void grdviewSalesReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            totalpayable += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            totalDue += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Due"));


            e.Row.Cells[0].Width = 10;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].Font.Bold = true;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          //  e.Row.Cells[2].Font.Size = 11;
           // e.Row.Cells[3].Font.Size = 11;
             e.Row.Cells[6].Font.Size = 9;
            e.Row.Cells[4].Font.Size = 11;
            e.Row.Cells[5].Font.Size = 11;



            // Total Pay calculation
            e.Row.Cells[5].Text = totalpayable.ToString("");
            string tpay = totalpayable.ToString("c");
            int tpaycut = tpay.IndexOf('$');
            string tpaycutsum = tpay.Substring(tpaycut + 1);

            e.Row.Cells[5].Text = tpaycutsum;
            e.Row.Cells[5].Font.Bold = true;


            // Total Due calculation
            e.Row.Cells[6].Text = totalDue.ToString();
            string tDue = totalDue.ToString();
           // int tDuecut = tpay.IndexOf('$');
          //  string tDuecutsum = tDue.Substring(tDuecut + 1);

            e.Row.Cells[6].Text = tDue;

            e.Row.Cells[6].Font.Bold = true;



            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[4].Text = "Total";

            e.Row.Cells[4].HorizontalAlign = e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[5].HorizontalAlign = e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[6].HorizontalAlign = e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;

        }
    }

    //header part  System information
    public void SystemInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_SettingsUpdate", cn);
            cmd.Parameters.AddWithValue("@Location", Session["ShopID"].ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);


            lblshopTitle.Text = dt.Rows[0].ItemArray[1].ToString();
            lblshopAddress.Text = dt.Rows[0].ItemArray[2].ToString();
            lblPhone.Text = dt.Rows[0].ItemArray[3].ToString();
            lblwebAddress.Text = dt.Rows[0].ItemArray[5].ToString();

            cn.Close();
        }
        catch
        {
        }
    }

      // //////// Search item by ID , Code ,
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesReport_Search", cn);
            cmd.Parameters.AddWithValue("@value", txtsearch.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            grdviewSalesReport.DataSource = cmd.ExecuteReader();
            grdviewSalesReport.EmptyDataText = "No Records Found";
            grdviewSalesReport.DataBind();
            cn.Close();
            lbtotalRow.Text = txtsearch.Text  + "  Report | Total : " + Convert.ToString(grdviewSalesReport.Rows.Count) + " Records found" + "<br />";
            SystemInfo();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {
        Report_DateToDate_DataBind(txtDateFrom.Text, txtDateTo.Text);
    }

    protected void txtDateTo_TextChanged(object sender, EventArgs e)
    {
        Report_DateToDate_DataBind(txtDateFrom.Text, txtDateTo.Text);
    }

    public void Report_DateToDate_DataBind(string DateFrom, string DateTo)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesReport_DateToDate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);

            grdviewSalesReport.DataSource = cmd.ExecuteReader();
            grdviewSalesReport.EmptyDataText = "No Records Found";
            grdviewSalesReport.DataBind();
            cn.Close();
            lbtotalRow.Text = "Report From : " + txtDateFrom.Text + " To " + txtDateTo.Text +  "<br />";
            SystemInfo();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    public void report()
    {

        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesReport_DateToDate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@DateFrom", Convert.ToDateTime( test));
            cmd.Parameters.AddWithValue("@DateTo", Convert.ToDateTime(test));

            grdviewSalesReport.DataSource = cmd.ExecuteReader();
            grdviewSalesReport.EmptyDataText = "No Records Found";
            grdviewSalesReport.DataBind();
            cn.Close();
            lbtotalRow.Text = "Report From : " + test + " To " + test + "<br />";
            SystemInfo();
            ItemsListDataBind();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    //AutoComplete  AutoCompleteExtender  ////////////////////////////////////////////   AutoCompleteExtender
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMDN(string prefixText)
    {

        string constr = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ToString();
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT TOP 8 * from    tbl_SalesPayment " +
          " where   ( ID like '%' + @value + '%') ", con);
        cmd.Parameters.AddWithValue("@Value", prefixText);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        List<string> MDN = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            // string var = dt.Rows[i][0].ToString() + " " + dt.Rows[i][12].ToString();
            string var = dt.Rows[i][0].ToString();
            MDN.Add(var);
            con.Close();
        }
        return MDN;
    }

    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // e.Row.Cells[0].Width = 10;
            e.Row.Cells[1].Width = 10;
        }
    }
    //Print Report
    protected void Button3_Click(object sender, EventArgs e)
    {
        string InvocieNo = null;

        if (txtsearch.Text != "")
        {
            InvocieNo = txtsearch.Text;
        }


        List<SalesReports> SalesList = new List<SalesReports>();
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesReport_DateToDate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@DateFrom",  txtDateFrom.Text);
            cmd.Parameters.AddWithValue("@DateTo", txtDateTo.Text);

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
                list.Total = Convert.ToDouble( dr[i].ItemArray[5]);
                list.Due = Convert.ToDouble(dr[i].ItemArray[6]);
                list.Date = dr[i].ItemArray[7].ToString();
                list.ServedBy = dr[i].ItemArray[8].ToString();
                list.ShopId = dr[i].ItemArray[9].ToString();

                SalesList.Add(list);
            }


            cn.Close();
           // lbtotalRow.Text = "Report From : " + txtDateFrom.Text + " To " + txtDateTo.Text + "<br />";
            SystemInfo();
        }

        catch
        {
            lbtotalRow.Text = "No Records Found";
        }


        var reportParameters = new ReportParameterCollection
           {
               //new ReportParameter("CompanyName",CompanyName),
               //new ReportParameter("ComapanyAddress",ComapanyAddress),
               //new ReportParameter("CompanyMobileNumber",CompanyMobileNumber),
               //new ReportParameter("CompanyWebsite",CompanyWebsite),
               //new ReportParameter("CompanyFooterMassage",CompanyFooterMassage),


               new ReportParameter("DateFrom",txtDateFrom.Text),
               new ReportParameter("DateTo",txtDateTo.Text),
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