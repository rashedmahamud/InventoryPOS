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

public partial class Accounts_Sales_DamageReport : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    string ShopId = "1461";
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            Label1.Text = ShopId.ToString();

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
        SqlCommand cmd = new SqlCommand("SP_Damagedate", con);
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
        //try
        //{
        string RP_ID = txtsearch.Text.Trim();
        //string ShopID = Response.Cookies["InventMgtCookies"]["ShopID"];
        string ShopID = Label1.Text.ToString();
        string Logdate = txtDateFrom.Text.Trim();
        string strcon = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
        SqlConnection con = new SqlConnection(strcon);
        SqlCommand cmd = new SqlCommand("SP_Damge", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Logdate", Logdate);
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

        Session["DateFrom_DamageReport"] = txtDateFrom.Text;
        Response.Redirect("~/Accounts/Sales/PrintDamageReport.aspx");
    }
}