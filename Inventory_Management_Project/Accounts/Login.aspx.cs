using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblLogMsg.Visible = false;

            //this default login uid/pass  please remove this code 
            txtuser.Text = "admin";
            txtpass.Text = "admin";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string UserID = txtuser.Text.Trim();
        string pass = txtpass.Text.Trim();
        string ShopID = TextBox1.Text.Trim();
        string strcon = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
        SqlConnection con = new SqlConnection(strcon);

        SqlCommand cmd = new SqlCommand("Sp_User_Authentication", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@LOGINID", UserID);
        cmd.Parameters.AddWithValue("@PASSWORD", pass);
        cmd.Parameters.AddWithValue("@ShopID", ShopID);
   
        con.Open();

        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.HasRows)
        {
            rd.Read();
            lblLogMsg.Text = "Login successful.";

               // //Session to Master Page                
               //Session["UserID"] = UserID;
            //Session["ShopID"] = rd["ShopID"].ToString();

               Response.Cookies["InventMgtCookies"]["UserID"] = UserID;
               Response.Cookies["InventMgtCookies"]["ShopID"] = rd["ShopID"].ToString();
               Response.Cookies["InventMgtCookies"]["UserPhoto"] = rd["User_Photo"].ToString();
               Response.Cookies["InventMgtCookies"].Expires = DateTime.Now.AddDays(9965);
               Session["ShopID"] = TextBox1.Text;
          

                hitcounter();
                Response.Redirect("sales.aspx", false);

                //Query string
               //  Response.Redirect("Default.aspx?userID=" + UserID + "&ShopID=" + rd["ShopID"].ToString());
               // Response.Redirect("Default.aspx?userID=" + HttpUtility.UrlEncode(Request.QueryString["UserID"]) + "&ShopID=" + rd["ShopID"].ToString());


            ///Customer cookies will not same please InventMgtCustomerCookies
        }
        else
        {
            lblLogMsg.Visible = true;
            lblLogMsg.Text = "We don't recognize this user ID or password.";
        }
        con.Close();
    }

    public void hitcounter()
    {
        try
        {          
            string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
           
            SqlConnection cn = new SqlConnection(ConnectionString);
           
            SqlCommand cmd = new SqlCommand("SP_POS_Insert_HitCounter", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userID", txtuser.Text);
            cmd.Parameters.AddWithValue("@IPaddress", ip);
             
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch
        {
        }
    }
    
}