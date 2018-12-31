using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class User_privilege : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;    
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!IsPostBack)
        {            
            if (Request.QueryString["ID"] != null)
            {
               btnCreateRole.Visible = false;
                string ID = Request.QueryString["ID"].ToString();
                string UserID = Request.QueryString["UserID"].ToString();
                loadrecord(ID);
                PageDataBind(UserID);
            }
            else
            {
                Response.Redirect("~/Users/ManageUsers.aspx", true);
            }
        }
    }


    public void loadrecord(string ID)
    {

        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("SP_POS_DataBind_UsersDetails", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        cmd.Parameters.AddWithValue("@ID", ID);

        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.HasRows)
        {
            rd.Read();

            //Edit Part //////////////////
            imgUser.ImageUrl = rd["User_Photo"].ToString();             
            lblFname.Text = rd["Fname"].ToString();
            lblLname.Text = rd["LName"].ToString();             
            lblContact.Text = "Contact: "       + rd["UserPhone"].ToString();
            lblSupervisor.Text = "Supervisor: " + rd["Supervisor"].ToString();
            cn.Close();

        }


        cn.Close();
    }

    public void PageDataBind(string UserID)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_PageDatabind", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserName", UserID);
            cn.Open();

            grdviewUserPageAccess.DataSource = cmd.ExecuteReader();
            grdviewUserPageAccess.EmptyDataText = "No Records Found";
            grdviewUserPageAccess.DataBind();
            cn.Close();


            // Select the checkboxes from the GridView control
            for (int i = 0; i < grdviewUserPageAccess.Rows.Count; i++)
            {
                if (grdviewUserPageAccess.Rows[i].Cells[4].Text == "Allow")
                {
                    GridViewRow row = grdviewUserPageAccess.Rows[i];
                    bool isChecked = ((CheckBox)row.FindControl("chkAllow")).Checked;
                    ((CheckBox)row.FindControl("chkAllow")).Checked = true;
                }
            }

            //if (grdviewUserPageAccess.Rows[0].Cells[3].Text != "Allow")
            //{
            //    btnCreateRole.Visible = true;
            //}
          
            
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }

    //Create a new Role | Onetime
    protected void btnCreateRole_click(object sender, EventArgs e)
    {
        try
        {
            string UserName = Request.QueryString["UserID"].ToString();  
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_Auto_role_insert", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@userID", Request.QueryString["ID"].ToString());
            cmd.Parameters.AddWithValue("@UserName", UserName);           

            cmd.ExecuteNonQuery();
            cn.Close();

            lblmessage.Text = "Successfully role created";

            PageDataBind(UserName);

        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }

    // Set user page Privilege
    protected void btnUserRoleSave_click(object sender, EventArgs e)
    {
        try
        {
            string UserID = Request.QueryString["UserID"].ToString(); 
            // Select the checkboxes from the GridView control
            for (int i = 0; i < grdviewUserPageAccess.Rows.Count; i++)
            {
                GridViewRow row = grdviewUserPageAccess.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("chkAllow")).Checked;

                if (isChecked)
                {
                    SqlConnection cn = new SqlConnection(ConnectionString);
                    SqlCommand cmd = new SqlCommand("SP_POS_UserRole_UpdateAccess", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", UserID);
                    cmd.Parameters.AddWithValue("@PageID", grdviewUserPageAccess.Rows[i].Cells[1].Text);
                    cmd.Parameters.AddWithValue("@status", 1);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    lblmessage.Text = "Saved"; 
                }

                else
                {
                    SqlConnection cn = new SqlConnection(ConnectionString);
                    SqlCommand cmd = new SqlCommand("SP_POS_UserRole_UpdateAccess", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", UserID);
                    cmd.Parameters.AddWithValue("@PageID", grdviewUserPageAccess.Rows[i].Cells[1].Text);
                    cmd.Parameters.AddWithValue("@status", 0);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    lblmessage.Text =  "Saved"; 
                }
            }
                       
            PageDataBind(UserID);       
          
        }
        catch// (Exception ex)
        {
        }
    }
    

}
 