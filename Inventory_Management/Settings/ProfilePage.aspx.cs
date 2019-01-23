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

public partial class ProfilePage : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.Cookies["InventMgtCookies"] != null)
            {
                LoadDetailsData(Request.Cookies["InventMgtCookies"]["UserID"].ToString());
                txtFname.Focus();

            }
            else
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }
    }

    public void LoadDetailsData(string UserID)
    {

        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("SP_POS_DataBind_UsersProfileDetails", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        cmd.Parameters.AddWithValue("@UserID", UserID);

        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.HasRows)
        {
            rd.Read();

            imgUser.ImageUrl    = rd["User_Photo"].ToString();
            lblUID.Text         = rd["ID"].ToString();
            txtFname.Text       = rd["Fname"].ToString();
            txtLName.Text       = rd["LName"].ToString();
            txtPhone.Text       = rd["UserPhone"].ToString();
            txtEmailaddr.Text   = rd["Email"].ToString();
            txtSupervisor.Text = rd["Supervisor"].ToString();
        }
        cn.Close();

    }

    // /// Update user Profile | Change password
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName     = Path.GetFileName(FUpimg.PostedFile.FileName);
            string extension    = Path.GetExtension(FUpimg.PostedFile.FileName);
            string UserID =     Request.Cookies["InventMgtCookies"]["UserID"].ToString();

            string Result;
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Update_UserProfile", cn);
            cn.Open();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Fname", txtFname.Text);
            cmd.Parameters.AddWithValue("@LName", txtLName.Text);
            cmd.Parameters.AddWithValue("@UserPhone", txtPhone.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby", "admin");
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@OldPassword", txtOldPassword.Text);

            if (FUpimg.HasFile)
            {
                if (extension == ".png" || extension == ".jpg" || extension == ".PNG" || extension == ".JPG")
                {
                    cmd.Parameters.AddWithValue("@User_Photo", "~/User_Photo/" + lblUID.Text + extension);

                  //  string strPath = MapPath("../User_Photo/") + lblUID.Text + extension;
                   // string fn = System.IO.Path.GetExtension(FUpimg.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("../User_Photo/") + lblUID.Text + extension;

                    string fileExtention = FUpimg.PostedFile.ContentType;
                    int fileLenght = FUpimg.PostedFile.ContentLength;
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FUpimg.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 128);
                    // Saving image in jpeg format
                    objImage.Save(SaveLocation, ImageFormat.Png);
                 //   FUpimg.SaveAs(strPath);
                    imgUser.ImageUrl = "~/User_Photo/" + lblUID.Text + extension;
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
            cmd.Parameters.Add("@ResultOutPut", SqlDbType.Int).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            cn.Close();


            Result = cmd.Parameters["@ResultOutPut"].Value.ToString();
            if (Result != "1")
            {
                lblmsg.Visible = true;
                lblmsg.Text = "!!! Current Password is incorrect";
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Successfully Updated";
                LoadDetailsData(UserID);
            }

        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
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