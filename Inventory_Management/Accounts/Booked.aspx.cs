using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Booked : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }
    public void CategoryDDLDataBind()
    {
        try
        {
            //string s = "04/10/2018";
           
            SqlConnection cn = new SqlConnection(ConnectionString);



            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ID, Customer_Name,Customer_Mobile,Arrival_Date,Time_Slot, Table_Number from Bookingdetails  where  Arrival_Date='" + TextBox1.Text + "' and Status='"+ "1" +"'";

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
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        //Label3.Text = TextBox1.Text.ToString();
    }
    protected void lnkbtnRelatedLinks_Click(object sender, EventArgs e)
    {
        LinkButton btn = (sender as LinkButton);
        Label ID = btn.FindControl("Label8") as Label;
        Label Time_Slot = btn.FindControl("Label7") as Label;


        int i;
        ////lbl.Text = DateTime.Now.ToLongTimeString();
        i = Convert.ToInt32(ID.Text.ToString());
        //Label9.Text = i.ToString();

        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("SP_POS_Reservation_update", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();

        cmd.Parameters.AddWithValue("@ID", i);
        cmd.Parameters.AddWithValue("@Time_Slot", Time_Slot.Text);
        cmd.Parameters.AddWithValue("@Table_Number", btn.Text);
       
        cmd.ExecuteNonQuery();
        cn.Close();
        Response.Redirect("~/Accounts/Booked.aspx");

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CategoryDDLDataBind();
    }
}