using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class User_Adduser : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadUserList();
        }

    }

    //Datalist  Item List
    protected void loadUserList()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Users");
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@category", "All");
            con.Open();

            DTusers.DataSource = cmd.ExecuteReader();
            DTusers.DataBind();
            con.Close();
            lbtotalRow.Text = "Total : " + Convert.ToString(DTusers.Items.Count) + " Users" + "<br />";
        }
        catch
        {
        }
    }

    //User List Data Load for Gridview
    public void UserListDataBind()
    {
        //try
        //{
        //    SqlConnection cn = new SqlConnection(ConnectionString);
        //    SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Users", cn);  
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cn.Open();
           
        //    grdUserList.DataSource = cmd.ExecuteReader();
        //    grdUserList.EmptyDataText =  "No Records Found";
        //    grdUserList.DataBind();
        //    cn.Close();
        //    lbtotalRow.Text = "Total : " + Convert.ToString(grdUserList.Rows.Count) + " Users" + "<br />";
        //    loadUserList();
        //}
        //catch
        //{
        //    lbtotalRow.Text = "No Records Found";
        //}
    }


    //Load Data detail and Edit Part poup data show
    public void LoadDetailsData(string ID)
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
            lblIDView.Text = rd["ID"].ToString();
            txtFName.Text = rd["Fname"].ToString();
            txtLName.Text = rd["LName"].ToString();
            txtDesignation.Text = rd["Designation"].ToString();
            txtContact.Text = rd["UserPhone"].ToString();
            txtAddress.Text = rd["UserAddress"].ToString();
            txtPassword.Text = rd["Password"].ToString();
            txtDept.Text = rd["Department"].ToString();
            txtEmail.Text = rd["Email"].ToString();
            string DOB = rd["DateofBirth"].ToString();
            txtDOB.Text = DOB;// DOB.Substring(0, 10);    
            DDLShopID.Text =rd["ShopID"].ToString();
            txtSupervisor.Text = rd["Supervisor"].ToString();
           // cn.Close();	 		
		 	 
        }          
       
         
        cn.Close();
    }

    
    // ////// Open Edit popup window 
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        LinkButton btn = (LinkButton)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label lblID = (Label)item.FindControl("lblID");
       

        //Call Function       
        LoadDetailsData(lblID.Text);       
        this.MpeEditShow.Show();
        ShopIDDDLDataBind();
    }

    //Shop List databind
    public void ShopIDDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ShopIdDDL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLShopID.DataSource = dt;
            DDLShopID.DataTextField = "TerminalID";
            DDLShopID.DataValueField = "TerminalID";
            DDLShopID.DataBind();
            cn.Close();

        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }

    //Update user info
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = Path.GetFileName(FUpimg.PostedFile.FileName);
            string extension = Path.GetExtension(FUpimg.PostedFile.FileName);
            string UserID = Request.Cookies["InventMgtCookies"]["UserID"].ToString();
         

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Update_User", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
    
            cmd.Parameters.AddWithValue("@id",          lblIDView.Text);
            cmd.Parameters.AddWithValue("@Fname",       txtFName.Text);
            cmd.Parameters.AddWithValue("@LName",       txtLName.Text);
            cmd.Parameters.AddWithValue("@UserPhone",   txtContact.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
            cmd.Parameters.AddWithValue("@Supervisor",  txtSupervisor.Text);
            cmd.Parameters.AddWithValue("@Department",  txtDept.Text);
            cmd.Parameters.AddWithValue("@UserAddress", txtAddress.Text);
            cmd.Parameters.AddWithValue("@ShopID",      DDLShopID.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby", UserID);
            cmd.Parameters.AddWithValue("@Password",    txtPassword.Text);             
            cmd.Parameters.AddWithValue("@DOB",         Convert.ToString(txtDOB.Text));
            cmd.Parameters.AddWithValue("@Email",       txtEmail.Text);
           
            if (FUpimg.HasFile)
            {
                if (extension == ".png" || extension == ".jpg" || extension == ".PNG" || extension == ".JPG")
                {
                   cmd.Parameters.AddWithValue("@User_Photo", "~/User_Photo/" + lblIDView.Text + extension);

                    string strPath = MapPath("../User_Photo/") + lblIDView.Text + extension;

                   // Guid uid = Guid.NewGuid();
                    string fn = System.IO.Path.GetExtension(FUpimg.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("../User_Photo/") + lblIDView.Text + extension;

                    string fileExtention = FUpimg.PostedFile.ContentType;
                    int fileLenght = FUpimg.PostedFile.ContentLength;
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FUpimg.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 128);
                    // Saving image in jpeg format
                    objImage.Save(SaveLocation, ImageFormat.Png);

                  //  FUpimg.SaveAs(strPath);
			UserListDataBind();
                    LoadDetailsData(lblIDView.Text);  
                    //imgUser.ImageUrl = "~/User_Photo/" + lblIDView.Text + extension;
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = ".jpg and .Png Format can be support";
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@User_Photo", imgUser.ImageUrl);
            }

            cmd.ExecuteNonQuery();
            cn.Close();                  
                  
                    
            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Updated";           
            this.MpeEditShow.Show();
	    UserListDataBind();
            LoadDetailsData(lblIDView.Text);
            loadUserList();

        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
            this.MpeEditShow.Show();
            //n9r3FuDl7dbwNWQ7
        }

    }


    /// Image resizing
    public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
    {
        var ratio = (double)maxHeight / image.Height;
        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
        }
        return newImage;
    }


}