using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Accounts_Outlate : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    string accountnumber = "123456";
    string s;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            CategoryDDLDataBind();
        }

    }

    public void CategoryDDLDataBind()
    {
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);


            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select CompanyName from tbl_settings  where  AccountNumber='" + accountnumber + "'";

            cmd.Connection = cn;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds1);

            cmd.ExecuteNonQuery();

            DataList2.Dispose();
            DataList2.DataSource = ds1;
            DataList2.DataBind();

            cn.Close();


        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


     protected void lnkbtnRelatedLinks_Click(object sender, EventArgs e)
    {
       
         LinkButton btn = sender as LinkButton;
        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select   Location	 from    tbl_settings where CompanyName ='" + btn.Text.Trim() + "'  and AccountNumber = '" + accountnumber + "'";
            cmd.Connection = cn;
           
            cmd.ExecuteNonQuery();
  SqlDataReader rd4 = cmd.ExecuteReader();

        if (rd4.HasRows)
        {

            while (rd4.Read())
            {

             s   = rd4["Location"].ToString();
     
            }

             Session["ShopID1"]= s.ToString();
            Response.Redirect("~/Accounts/Default.aspx");
            cn.Close();
        }

        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
   

    }

    }








   
