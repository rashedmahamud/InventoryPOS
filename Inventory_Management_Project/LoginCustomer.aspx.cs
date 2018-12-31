using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class LoginCustomer : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblLogMsg.Visible = false;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string CustID = txtuser.Text.Trim();
        string pass = txtpass.Text.Trim();

        string strcon = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
        SqlConnection con = new SqlConnection(strcon);

        SqlCommand cmd = new SqlCommand("SP_User_Authentication_Customer", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@LOGINID", CustID);
        cmd.Parameters.AddWithValue("@PASSWORD", pass);       
        con.Open();

        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.HasRows)
        {
            rd.Read();
            lblLogMsg.Text = "Login successful.";


                Response.Cookies["InventMgtCustomerCookies"]["CustID"] = CustID; // Customer User ID
                Response.Cookies["InventMgtCustomerCookies"]["ID"] = rd["ID"].ToString(); // Main Cst id to generate all  
                Response.Cookies["InventMgtCustomerCookies"]["CustName"] = rd["CustName"].ToString();
                Response.Cookies["InventMgtCustomerCookies"]["CustPhone"] = rd["CustPhone"].ToString();
                Response.Cookies["InventMgtCustomerCookies"].Expires = DateTime.Now.AddDays(9965);

              ///  hitcounter();
               Response.Redirect("Customers/Default.aspx", false);
           
        }
        else
        {
            lblLogMsg.Visible = true;
            lblLogMsg.Text = "We don't recognize this user ID or password.";
        }
        con.Close();
    }

 
    
}