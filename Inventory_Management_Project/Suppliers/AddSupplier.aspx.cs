using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class AddSupplier : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtName.Focus();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_Insert_Supplier", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
           
            cmd.Parameters.AddWithValue("@Name",        txtName.Text );
            cmd.Parameters.AddWithValue("@Phone",       txtPhone.Text);
            cmd.Parameters.AddWithValue("@Email",       txtEmail.Text);
            cmd.Parameters.AddWithValue("@Address",     txtAddress.Text);
            cmd.Parameters.AddWithValue("@City",        txtcity.Text);
            cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
            cmd.Parameters.AddWithValue("@Type",        DDLType.Text);
            cmd.Parameters.AddWithValue("@LogBy",       Request.Cookies["InventMgtCookies"]["UserID"].ToString());           
            cmd.ExecuteNonQuery();
            cn.Close();

            lblMessage.Text = "Successfully Saved";

            //Move to Customer List Page 
            Response.Redirect("~/Suppliers/ManageSuppliers.aspx");

        }
        catch
        {
            lblMessage.Text = "No Records Found";
        }
      
    }


}