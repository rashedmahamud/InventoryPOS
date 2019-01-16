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

        }
    }

    public void UpdateBindUpdate()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_SettingsUpdate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Location", Session["ShopID"].ToString());
            cn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(sdr);
            List<DataRow> dr = dt.AsEnumerable().ToList();



            TextBox2.Text = dr[0].ItemArray[0].ToString();
            txtCompanyName.Text = dr[0].ItemArray[2].ToString();
            txtCompanyAddress.Text = dr[0].ItemArray[3].ToString();
            TextBox1.Text = dr[0].ItemArray[4].ToString();
            txtPhone.Text = dr[0].ItemArray[5].ToString();
            txtEmailAddress.Text = dr[0].ItemArray[6].ToString();
            txtWebAddress.Text   = dr[0].ItemArray[7].ToString();
            txtVatRate.Text = dr[0].ItemArray[8].ToString();
            txtvatRegiNo.Text   = dr[0].ItemArray[9].ToString();
            txtFooterMessage.Text = dr[0].ItemArray[10].ToString();
            txtCurrency.Text = dr[0].ItemArray[15].ToString();
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
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Update dbo.[tbl_settings] set CompanyAddress = @CompanyAddress, CompanyName = @CompanyName,Location=@Location,  Phone =	@Phone,	[EmailAddress] = @EmailAddress , [WebAddress] = @WebAddress ,[VatRate] = @VatRate , [VATRegistration] = @VATRegistration,	[Footermsg] =  @Footermsg , LastUpdate =  GETDATE() , LastUpdateBy =	@LastUpdateBy, [Currency] = @Currency	where ID =' " + TextBox2.Text + "' ";
                cmd.Connection = cn;
                cn.Open();


                cmd.Parameters.AddWithValue("@CompanyName",     txtCompanyName.Text);
                cmd.Parameters.AddWithValue("@CompanyAddress",  txtCompanyAddress.Text);
                cmd.Parameters.AddWithValue("@Location",        TextBox1.Text);
                cmd.Parameters.AddWithValue("@Phone",           txtPhone.Text);
                cmd.Parameters.AddWithValue("@EmailAddress",    txtEmailAddress.Text);
                cmd.Parameters.AddWithValue("@WebAddress",      txtWebAddress.Text);
                cmd.Parameters.AddWithValue("@VatRate",         txtVatRate.Text);
                cmd.Parameters.AddWithValue("@VATRegistration", txtvatRegiNo.Text);
                cmd.Parameters.AddWithValue("@Currency",        txtCurrency.Text);
                cmd.Parameters.AddWithValue("@Footermsg",       txtFooterMessage.Text);
                cmd.Parameters.AddWithValue("@LastUpdateBy",    Request.Cookies["InventMgtCookies"]["UserID"].ToString());
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
}