using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class DueReport : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ShopID"].ToString()!=null)
        { 
        BindData();
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void BindData()
    {



        try
        {

            SqlConnection cn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds2 = new DataSet();
            cn.Open();
            cmd.CommandText = "Select ShopId, CustID ,CustName, CustContact,shippingaddress,ordedate,dueAmount,comment,note, ServedBy from tbl_SalesPayment where ShopId='" + Session["ShopID"].ToString() + "' and  dueAmount >0";

            cmd.Connection = cn;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds2);

            cmd.ExecuteNonQuery();

            DueCollection.Dispose();
            DueCollection.DataSource = ds2;
            DueCollection.DataBind();

            cn.Close();


        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }

    }
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
      
        LinkButton btn = (LinkButton)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label lblID = (Label)item.FindControl("lblID");      
        //TextBox1.Text = lblID.Text ;
        //this.ModalPopupPayment.Show();
      
    }
}