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

public partial class Sales_ItemwiseSalesReport : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    String test = DateTime.Now.ToString("dd.MM.yyy");
    double totalSales = 0;
    double totalProfit = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SystemInfo();
            this.Title = "Report_" + DateTime.Now.ToString("MMM_dd_yyyy_HH:mm:ss");
            //ItemsListDataBind();
            txtsearch.Focus();
            // lblmsg.Visible = false;
            Label17.Text = test.ToString();


        }

    }

    // ///////  Item list Databind



    // total Calculation  show on footer part

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

    }

    protected void txtDateFrom_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtDateTo_TextChanged(object sender, EventArgs e)
    {

    }

    //public void Report_DateToDate_DataBind()
    //{
    //    try
    //    {
    //        SqlConnection cn = new SqlConnection(ConnectionString);
    //        SqlCommand cmd = new SqlCommand("select   ItemCode as Bar_Code ,ItemName as Product_Name ,Qty as Quantity ,Price as Retail_Price ,DiscRate as Discount ,total as Total , Profit ,SP_ID as Recipt_Number ,ShopId as Branch_Number 	from   	 where ShopId=@ShopId and ItemCode=@ItemCode and  Logdate >=  @DateFrom     and  Logdate <= @DateTo   ", cn);
    //        cmd.CommandType = CommandType.Text;
    //        cn.Open();
    //        cmd.Parameters.AddWithValue("@ItemCode", TextBox1.Text.ToString());
    //        cmd.Parameters.AddWithValue("@ShopId", txtsearch.Text.ToString());
    //        cmd.Parameters.AddWithValue("@DateFrom", txtDateFrom.Text);
    //        cmd.Parameters.AddWithValue("@DateTo", txtDateFrom.Text);

    //        grdviewSalesReport.DataSource = cmd.ExecuteReader();
    //        grdviewSalesReport.EmptyDataText = "No Records Found";
    //        grdviewSalesReport.DataBind();
    //        cn.Close();
    //        lbtotalRow.Text = "Report From : " + txtDateFrom.Text + " To " + txtDateTo.Text + "<br />";
    //        SystemInfo();
    //    }
    //    catch
    //    {
    //        lbtotalRow.Text = "No Records Found";
    //    }
    //}

       // select * from tbl_sales where ItemCode =1 and ShopId=1461 and Logdate>='2018-11-24' and Logdate<='2019/01/01'
    public void Report_DateToDate_DataBind()
    {
        DateTime DateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
        string dateFrom = DateFrom.ToString("yyyy/MM/dd");

        DateTime DateTo = DateTime.Parse(txtDateTo.Text.ToString());
        string dateTo = DateTo.ToString("yyyy/MM/dd");

        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();

            cn.Open();
            cmd.CommandText = "select ItemCode as Bar_Code ,ItemName as Product_Name ,Qty as Quantity ,Price as Retail_Price ,DiscRate as Discount ,total as Total , Profit ,SP_ID as Recipt_Number ,ShopId as Branch_Number	from tbl_sales  where ShopId='" + txtsearch.Text.ToString() + "' and ItemCode= '" + TextBox1.Text.ToString() + "' and  Logdate >=  '" + dateFrom + "'     and  Logdate <= '" + dateTo + "'  ";
            cmd.Connection = cn;

            SqlDataReader rd = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.Load(rd);
            List<DataRow> dr = dt.AsEnumerable().ToList();

            grdviewSalesReport.DataSource = cmd.ExecuteReader();
            grdviewSalesReport.EmptyDataText = "No Records Found";
            grdviewSalesReport.DataBind();
            cn.Close();
            lbtotalRow.Text = "Report From : " + txtDateFrom.Text + " To " + txtDateTo.Text + "<br />";
            SystemInfo();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    protected void grdviewSalesReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            totalSales += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            totalProfit += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Profit"));


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
            e.Row.Cells[5].Text = totalSales.ToString("");
            string tpay = totalSales.ToString("c");
            int tpaycut = tpay.IndexOf('$');
            string tpaycutsum = tpay.Substring(tpaycut + 1);

            e.Row.Cells[5].Text = tpaycutsum;
            e.Row.Cells[5].Font.Bold = true;
            Label1.Text = e.Row.Cells[5].Text;

            // Total Due calculation
            e.Row.Cells[6].Text = totalProfit.ToString();
            string tDue = totalProfit.ToString();
            // int tDuecut = tpay.IndexOf('$');
            //  string tDuecutsum = tDue.Substring(tDuecut + 1);

            e.Row.Cells[6].Text = tDue;

            e.Row.Cells[6].Font.Bold = true;
            Label2.Text = e.Row.Cells[6].Text;


            e.Row.Cells[4].Font.Bold = true;
            e.Row.Cells[4].Text = "Total";

            e.Row.Cells[4].HorizontalAlign = e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[6].HorizontalAlign = e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;

        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        Report_DateToDate_DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime DateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
        string dateFrom = DateFrom.ToString("yyyy/MM/dd");

        DateTime DateTo = DateTime.Parse(txtDateTo.Text.ToString());
        string dateTo = DateTo.ToString("yyyy/MM/dd");

        Session["DateFrom"] = dateFrom;
        Session["DateTo"] = dateTo;

        Session["ItemCode"] = TextBox1.Text;
        Session["ShopID"] = txtsearch.Text;

        Response.Redirect("~/Accounts/Sales/PrintItemWiseSalesReport.aspx");


    }
}



