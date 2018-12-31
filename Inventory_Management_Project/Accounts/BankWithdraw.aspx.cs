using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Accounts_BankWithdraw : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    string ShopId = "1461";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          
            Bank();
            CategoryListDataBind();

            lblmsg.Visible = false;
            DropDownList3.Items.Add("Cash");

            DropDownList3.Items.Add("Bank cheque");
            TextBox6.Text = ShopId.ToString();
        }
    }

    public void Bank()
    {
        try
        {
         

            grdCategoryList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("Select *from Bank where  Branch_ID='" + ShopId + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {

                while (rd4.Read())
                {

                    DropDownList1.Items.Add((rd4["Bank_Name"].ToString()));
                    DropDownList2.Items.Add((rd4["Accout_Type"].ToString()));


                }

            }

            con.Close();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }
    public void CategoryListDataBind()
    {
        //try
        //{
        string ShopId = Session["ShopID1"].ToString();

        grdCategoryList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
        SqlConnection con = new SqlConnection(ConnectionString);

        SqlCommand cmd = new SqlCommand("Select *from BankWithdraw where  Branch_ID='" + ShopId + "'", con);
        cmd.CommandType = CommandType.Text;
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        grdCategoryList.DataSource = ds;
        grdCategoryList.EmptyDataText = "No Records Found";
        grdCategoryList.DataBind();
        con.Close();
        //}
        //catch
        //{
        lbtotalRow.Text = "No Records Found";
        //}
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ShopId = Session["ShopID1"].ToString();

            grdCategoryList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand("Select *from Bank where  Branch_ID='" + ShopId + "' and Accout_Type='" + DropDownList2.Text + "' and Bank_Name='" + DropDownList1.Text + "'", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader rd4 = cmd.ExecuteReader();

            if (rd4.HasRows)
            {

                while (rd4.Read())
                {

                    TextBox1.Text = ((rd4["Account_Number"].ToString()));
                    TextBox2.Text = ((rd4["Account_Name"].ToString()));


                }

            }

            con.Close();
            this.MpeAddCategoryShow.Show();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }
    protected void btnCategoryLink_Click(object sender, EventArgs e)
    {
        //  txtCategory.Text = string.Empty;
        lblmsg.Visible = false;
        TextBox1.Focus();
        this.MpeAddCategoryShow.Show();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("BankWithdraw_Insert", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Bank_Name", DropDownList1.Text);
            cmd.Parameters.AddWithValue("@Account_Type", DropDownList2.Text);
            cmd.Parameters.AddWithValue("@Account_Number", TextBox1.Text);
            cmd.Parameters.AddWithValue("@Account_Name", TextBox2.Text);
            cmd.Parameters.AddWithValue("@Deposit_Date", TextBox3.Text);
            cmd.Parameters.AddWithValue("@Deposit_By", TextBox4.Text);
            cmd.Parameters.AddWithValue("@Amount", TextBox5.Text);
            cmd.Parameters.AddWithValue("@Deposit_Type", DropDownList3.Text);
            cmd.Parameters.AddWithValue("@Decription", TextBox7.Text);
            cmd.Parameters.AddWithValue("@logby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());
            cmd.Parameters.AddWithValue("@Branch_ID", ShopId);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Inserted";
            CategoryListDataBind();

            this.MpeAddCategoryShow.Show();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox7.Text = "";
        }
        catch
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error";
        }
    }
    protected void LinkDelete_Click(object sender, EventArgs e)
    {

        try
        {
            //LinkButton Linkdetails = sender as LinkButton;
            //GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

            //SqlConnection cn = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("Income_delete", cn);
            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@ID", gvrow.Cells[1].Text);

            //cn.Open();
            //cmd.ExecuteNonQuery();
            //cn.Close();
            //CategoryListDataBind();
            //lbtotalRow.ForeColor = System.Drawing.Color.Red;
            //lbtotalRow.Text = "Deleted";

        }
        catch
        {

            //lbtotalRow.Text = "Error";
        }

    }

    protected void grdCategoryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCategoryList.PageIndex = e.NewPageIndex;
        CategoryListDataBind();
    }

    protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtsearch.Text = string.Empty;
        CategoryListDataBind();
    }
}