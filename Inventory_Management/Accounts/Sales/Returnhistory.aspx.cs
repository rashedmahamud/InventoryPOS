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

        Session["DateFrom"] = dateFrom;
        Session["DateTo"] = dateTo;
        Session["StoreId"] = txtsearch.Text;

        Response.Redirect("~/Accounts/Sales/PrintReturnHistory.aspx");


    }

}