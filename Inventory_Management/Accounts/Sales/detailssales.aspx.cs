﻿using System;
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
public partial class Sales_detailssales : System.Web.UI.Page
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

            report();
        }

    }

    // ///////  Item list Databind
    public void ItemsListDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("select   ItemCode as Bar_Code ,ItemName as Product_Name ,Qty as Quantity ,Price as Retail_Price ,DiscRate as Discount ,total as Total , Profit ,SP_ID as Recipt_Number ,ShopId as Branch_Number 	from  tbl_sales	 where ShopId=@ShopId  ", cn);
            cmd.CommandType = CommandType.Text;
            cn.Open();
            cmd.Parameters.AddWithValue("@ShopId", txtsearch.Text.ToString());


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
        ItemsListDataBind();
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
            SqlCommand cmd = new SqlCommand("select   ItemCode as Bar_Code ,ItemName as Product_Name ,Qty as Quantity ,Price as Retail_Price ,DiscRate as Discount ,total as Total , Profit ,SP_ID as Recipt_Number ,ShopId as Branch_Number 	from  tbl_sales	 where ShopId=@ShopId and Logdate between   @DateFrom     and   @DateTo   ", cn);
            cmd.CommandType = CommandType.Text;
            cn.Open();
            cmd.Parameters.AddWithValue("@ShopId", txtsearch.Text.ToString());
            cmd.Parameters.AddWithValue("@DateFrom", DateFrom);
            cmd.Parameters.AddWithValue("@DateTo", DateTo);

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

    public void report()
    {

      try
      {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("select   ItemCode as Bar_Code ,ItemName as Product_Name ,Qty as Quantity ,Price as Retail_Price ,DiscRate as Discount ,total as Total , Profit ,SP_ID as Recipt_Number ,ShopId as Branch_Number 	from  tbl_sales	 where ShopId=@ShopId and  Logdate=@Logdate	 ", cn);
            cmd.CommandType = CommandType.Text;
            cn.Open();
            cmd.Parameters.AddWithValue("@ShopId",lblPhone.Text.ToString());
            cmd.Parameters.AddWithValue("@Logdate", Convert.ToDateTime( Label17.Text));


            grdviewSalesReport.DataSource = cmd.ExecuteReader();
            grdviewSalesReport.EmptyDataText = "No Records Found";
            grdviewSalesReport.DataBind();
            cn.Close();
            lbtotalRow.Text = "Report From : " + test + " To " + test + "<br />";
            SystemInfo();
            //ItemsListDataBind();
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




    protected void Button1_Click(object sender, EventArgs e)
    {
        DateTime DateFrom = DateTime.Parse(txtDateFrom.Text.ToString());
        string dateFrom = DateFrom.ToString("yyyy/MM/dd");

        DateTime DateTo = DateTime.Parse(txtDateTo.Text.ToString());
        string dateTo = DateTo.ToString("yyyy/MM/dd");


        Session["DateFrom"] = dateFrom;
        Session["DateTo"] = dateTo;
        Session["StoreID"] = txtsearch.Text;


        Response.Redirect("~/Accounts/Sales/PrintProfitLossReport.aspx");
    }
}



