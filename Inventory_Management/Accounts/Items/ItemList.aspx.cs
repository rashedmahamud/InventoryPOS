using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class ItemList : System.Web.UI.Page
{
    String ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadItemList();
            txtSearch.Focus();
        }
    }

    //Datalist  Item List
    protected void loadItemList()
    {
        try
        {
            //SqlConnection con = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item_SR");
            //cmd.Connection = con;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@category", "All");
            //cmd.Parameters.AddWithValue("@ShopID", "All");
            //con.Open();

            //DTusers.DataSource = cmd.ExecuteReader();
            //DTusers.DataBind();
            //con.Close();

            SqlConnection cn = new SqlConnection(ConnectionString);



            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ID, ItemCode as [Code],	ItemName, CONVERT(int, ItemQty) as [Qty], RetailPrice	 as Price,Discount as [Disc] , CAST((RetailPrice -((RetailPrice * Discount) / 100))	as numeric(18,2)) as Total ,  Photo	, Tax from    tbl_Item where Status = 1 AND ShopID='" + Session["ShopID"].ToString() + "'";

            cmd.Connection = cn;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds2);

            cmd.ExecuteNonQuery();

            DTusers.Dispose();
            DTusers.DataSource = ds2;
            DTusers.DataBind();

            cn.Close();



        }
        catch
        {
        }
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {

        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ItemSearch", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@value", txtSearch.Text);
            con.Open();
             
            DTusers.DataSource = cmd.ExecuteReader();
            DTusers.DataBind();
            con.Close();
        }
        catch
        {
        }
    }
}