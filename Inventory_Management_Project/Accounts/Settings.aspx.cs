using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Settings : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateBindUpdate();
           // txtFname.Focus();
            TextBox2.Text = "123456";
        }
    }

    public void UpdateBindUpdate()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_SettingsUpdate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            TextBox2.Text = dt.Rows[0].ItemArray[0].ToString();
            txtCompanyName.Text = dt.Rows[0].ItemArray[1].ToString();
            txtCompanyAddress.Text = dt.Rows[0].ItemArray[2].ToString();
            TextBox1.Text = dt.Rows[0].ItemArray[3].ToString();
            txtPhone.Text = dt.Rows[0].ItemArray[4].ToString();
            txtEmailAddress.Text = dt.Rows[0].ItemArray[5].ToString();            
            txtWebAddress.Text   = dt.Rows[0].ItemArray[6].ToString();
            txtVatRate.Text = dt.Rows[0].ItemArray[7].ToString();
            txtvatRegiNo.Text   = dt.Rows[0].ItemArray[8].ToString();
            txtFooterMessage.Text = dt.Rows[0].ItemArray[9].ToString();
            cn.Close();
        }
        catch
        {
        }
    }


    protected void btnUpdateSettings_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCompanyName.Text == string.Empty || txtCompanyAddress.Text == string.Empty)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Update the information below')", true);
            }
            else
            {
                SqlConnection cn = new SqlConnection(ConnectionString);

                SqlCommand cmd = new SqlCommand("SP_POS_Insert_Settings", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@AccountNumber", TextBox2.Text);
                cmd.Parameters.AddWithValue("@CompanyName",     txtCompanyName.Text);
                cmd.Parameters.AddWithValue("@CompanyAddress",  txtCompanyAddress.Text);
                cmd.Parameters.AddWithValue("@Location",        TextBox1.Text);
                cmd.Parameters.AddWithValue("@Phone",           txtPhone.Text);
                cmd.Parameters.AddWithValue("@EmailAddress",    txtEmailAddress.Text);
                cmd.Parameters.AddWithValue("@WebAddress",      txtWebAddress.Text);
                cmd.Parameters.AddWithValue("@VatRate",         txtVatRate.Text);
                cmd.Parameters.AddWithValue("@VATRegistration", txtvatRegiNo.Text);
                cmd.Parameters.AddWithValue("@Footermsg",       txtFooterMessage.Text);
                cmd.Parameters.AddWithValue("@LastUpdateBy",    Request.Cookies["InventMgtCookies"]["UserID"].ToString());
                cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now);
                cmd.ExecuteNonQuery();
                cn.Close();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Updated')", true);

                UpdateBindUpdate();
               
            }            
        }

        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~\\Accounts\\Settings.aspx");
    }
}