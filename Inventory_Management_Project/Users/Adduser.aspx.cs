using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Adduser : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFname.Focus();
            //ShopIDDDLDataBind();
            DDLShopID.Items.Add("1461");
        }
    }

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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName     = Path.GetFileName(FUpimg.PostedFile.FileName);
            string extension    = Path.GetExtension(FUpimg.PostedFile.FileName);
           // string extensionEX = Path.GetFileNameWithoutExtension(FUpimg.PostedFile.FileName);

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Insert_Users", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();            
          
            cmd.Parameters.AddWithValue("@Fname", txtFname.Text);
            cmd.Parameters.AddWithValue("@LName", txtLName.Text);
            cmd.Parameters.AddWithValue("@UserPhone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmailaddr.Text);
            cmd.Parameters.AddWithValue("@Supervisor", txtSupervisor.Text);
            cmd.Parameters.AddWithValue("@Department", txtDept.Text);
            cmd.Parameters.AddWithValue("@UserName", txtUserID.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
            cmd.Parameters.AddWithValue("@DateofBirth", txtDOB.Text);
            cmd.Parameters.AddWithValue("@ShopID",      DDLShopID.Text);
            cmd.Parameters.AddWithValue("@UserAddress", txtAddress.Text);
            cmd.Parameters.AddWithValue("@LogBy", Request.Cookies["InventMgtCookies"]["UserID"].ToString());             

            if (FUpimg.HasFile)
            {
                if (extension == ".png" || extension == ".jpg" || extension == ".PNG" || extension == ".JPG")
                {
                    cmd.Parameters.AddWithValue("@User_Photo", "~/User_Photo/" + txtUserID.Text + extension);

                  //  string strPath = MapPath("../User_Photo/") + txtUserID.Text + extension;

                  //  string fn = System.IO.Path.GetExtension(FUpimg.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("../User_Photo/") + txtUserID.Text + extension;

                    string fileExtention = FUpimg.PostedFile.ContentType;
                    int fileLenght = FUpimg.PostedFile.ContentLength;
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FUpimg.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 128);
                    // Saving image in jpeg format
                    objImage.Save(SaveLocation, ImageFormat.Png);

                   // FUpimg.SaveAs(strPath);                       
                }
                else
                {                                        
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('.jpg and .Png Format can be support')", true);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@User_Photo", "~/User_Photo/man.png"); //DBNull.Value for Database NULL value
            }

            cmd.ExecuteNonQuery();
            cn.Close();                   

            lblmessage.Text = "Successfully Saved";
            Response.Redirect("~/Users/ManageUsers.aspx");            
            
        }
        catch  (Exception ex)
        {
            lblmessage.Text = ex.Message;
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