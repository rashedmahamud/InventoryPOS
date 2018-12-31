using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class EmployeesProfile : System.Web.UI.Page
{
    String ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            if (Request.QueryString["ID"] != null)
            {
                loadrecord(Request.QueryString["ID"].ToString());
                DataBindsalary(Request.QueryString["ID"].ToString());
            }
            else
            {
                Response.Redirect("~/ManageUsers.aspx", true);
            }
        }

    }

    // ///////    list Databind
    public void DataBindsalary(string empid)
    {
        try
        {

          //  grdempsalary.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_Salary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("value", empid);
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            grdempsalary.DataSource = ds;
            grdempsalary.EmptyDataText = "No Records Found";
            grdempsalary.DataBind();
            con.Close();
        }
        catch
        {
           // lbtotalRow.Text = "No Records Found";
        }
    }

    protected void loadrecord(string ID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_UsersProfile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            con.Open();


            dtviewlist.EmptyDataText = "No Records Found";
            dtviewlist.DataSource = cmd.ExecuteReader();
            dtviewlist.DataBind();

        }
        catch
        {
        }
    }

    protected void grdempsalary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdempsalary.PageIndex = e.NewPageIndex;
        DataBindsalary(Request.QueryString["ID"].ToString());
    }
    protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtsearch.Text = string.Empty;
        DataBindsalary(Request.QueryString["ID"].ToString());
    }
    protected void btnAddSalary_Click(object sender, EventArgs e)
    {
        //  txtCategory.Text = string.Empty;
        lblmsg.Visible = false;
        txtAmount.Focus();
        ddyear.Text = DateTime.Now.ToString("yyyy");
        ddlmonth.Text = DateTime.Now.ToString("MMMM");
        this.MpeAddCategoryShow.Show();
    }


    // /////// Add Salary 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string empid = Request.QueryString["ID"].ToString();
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_Add_Salary", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empid",           empid);
            cmd.Parameters.AddWithValue("@saltype",         ddltype.Text);
            cmd.Parameters.AddWithValue("@salaryamount",    txtAmount.Text);
            cmd.Parameters.AddWithValue("@saldate",         txtdate.Text);
            cmd.Parameters.AddWithValue("@salmonth",        ddlmonth.Text);
            cmd.Parameters.AddWithValue("@salyr",           ddyear.Text);
            cmd.Parameters.AddWithValue("@paidby",          Request.Cookies["InventMgtCookies"]["UserID"].ToString());
            cmd.Parameters.AddWithValue("@note",            txtnote.Text);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Inserted";
            txtAmount.Text = string.Empty;
            DataBindsalary(empid);
          
            this.MpeAddCategoryShow.Show();

        }
        catch
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error";
        }
    }
}