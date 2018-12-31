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
using System.Drawing.Imaging;
using System.Drawing;

public partial class AddItem : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;

    decimal qty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["ShopID"].ToString()!=null)
            //{ 
            TextBox1.Focus();
            CategoryDDLDataBind();
            supplier();
            TextBox11.Text = "0.00";
            TextBox12.Text = "0.00";
            //Label18.Text = Session["ShopID"].ToString();
            TextBox10.Text = Session["ShopID"].ToString();
        }
        //}
        //else
        //{
        //    Response.Redirect("Login.aspx");
        //}
    }

    public void CategoryDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CategoryDDL", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLCategory.DataSource = dt;
            DDLCategory.DataTextField = "ItemCategory";
            DDLCategory.DataValueField = "ItemCategory";
            DDLCategory.DataBind();
            cn.Close();

        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


    void supplier()
    {
        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandType = CommandType.Text;
        cmd1.CommandText = "select *from tbl_supplier ";
        cmd1.Connection = cn;
        cn.Open();
        SqlDataReader rd1 = cmd1.ExecuteReader();
        if (rd1.HasRows)
        {

            //SqlCommand cmdzs = new SqlCommand("select *from Supplierdetails ", con1);


            while (rd1.Read())
            {
                DropDownList1.Items.Add((rd1["ID"].ToString()));


            }

            rd1.Close();
            cn.Close();


        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {




            SqlConnection con1 = new SqlConnection(ConnectionString);

            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = " SELECT COUNT(*) FROM tbl_Item where ItemCode='" + TextBox1.Text.Trim() + "' ";
            cmd1.Connection = con1;
            con1.Open();
            //SqlDataReader rd1 = cmd1.ExecuteReader();
            int RecordCount = Convert.ToInt32(cmd1.ExecuteScalar());
            //rd1.Dispose();
            cmd1.Dispose();
            if (RecordCount == 0)
            {

                con1.Close();
                //try
                //{
                string fileName = Path.GetFileName(FUpimg.PostedFile.FileName);
                string extension = Path.GetExtension(FUpimg.PostedFile.FileName);

                SqlConnection cn = new SqlConnection(ConnectionString);
                //string sql = "Insert into		 tbl_Item ([ItemCode],[ItemName],[PurchasePrice] ,[RetailPrice],[MinQty],[MaxQty] ,[MDate],[ExDate] ,[SupplierCode],[Status], [ItemQty],[ItemCategory], [Discount],[LogBy],[Photo],[Description])	values  	(@ItemCode,@ItemName,@PurchasePrice,@RetailPrice,@ItemQty,@MinQty,@MaxQty ,@MDate,@ExDate ,@SupplierCode,@Status, @ItemCategory,@Discount,@LogBy,@Itemphoto,@Description)	";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " Insert into tbl_Item (ItemCode,ItemName,PurchasePrice ,RetailPrice, ItemQty,MinQty,MaxQty ,MDate,ExDate ,SupplierCode,ItemCategory,Tax, Discount,LogBy,Lastupdateby,Photo,Description,Status,Lastupdate,Logtime,ShopID)	values  	(@ItemCode,@ItemName,@PurchasePrice ,@RetailPrice, @ItemQty,@MinQty,@MaxQty ,@MDate,@ExDate ,@SupplierCode,@ItemCategory,@Tax, @Discount,@LogBy,@Lastupdateby,@Itemphoto,@Description,@Status,@Lastupdate,@Logtime,@ShopID)";

                cmd.Connection = cn;
                cn.Open();

                cmd.Parameters.AddWithValue("@ItemCode", TextBox1.Text);
                cmd.Parameters.AddWithValue("@ItemName", TextBox2.Text);
                cmd.Parameters.AddWithValue("@PurchasePrice", Convert.ToDecimal(TextBox3.Text));
                cmd.Parameters.AddWithValue("@RetailPrice", Convert.ToDecimal(TextBox4.Text));
                cmd.Parameters.AddWithValue("@ItemQty", Convert.ToDecimal(TextBox5.Text));
                cmd.Parameters.AddWithValue("@MinQty", Convert.ToDecimal(TextBox6.Text));
                cmd.Parameters.AddWithValue("@MaxQty", Convert.ToDecimal(TextBox7.Text));


                cmd.Parameters.AddWithValue("@MDate", TextBox8.Text);

                cmd.Parameters.AddWithValue("@ExDate", TextBox9.Text);
                cmd.Parameters.AddWithValue("@SupplierCode", DropDownList1.Text);

                cmd.Parameters.AddWithValue("@ItemCategory", DDLCategory.Text);
                cmd.Parameters.AddWithValue("@Tax", Convert.ToDecimal(TextBox11.Text));
                cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(TextBox12.Text));
                cmd.Parameters.AddWithValue("@LogBy", Request.Cookies["InventMgtCookies"]["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Lastupdateby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());

                if (FUpimg.HasFile)
                {
                   


                    string sSavePath;
                    string sThumbExtension;
                    int intThumbWidth;
                    int intThumbHeight;

                    // Set constant values
                    sSavePath = "~/Accounts/Items/ItemsPhoto/";
                    sThumbExtension = "_thumb";
                    intThumbWidth = 160;
                    intThumbHeight = 120;

                    // If file field isn’t empty
                    if (FUpimg.PostedFile != null)
                    {
                        // Check file size (mustn’t be 0)
                        HttpPostedFile myFile = FUpimg.PostedFile;
                        int nFileLen = myFile.ContentLength;
                        if (nFileLen == 0)
                        {
                            lblmessage.Text = "No file was uploaded.";
                            return;
                        }

                        // Check file extension (must be JPG)
                        if (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".jpg")
                        {
                            lblmessage.Text = "The file must have an extension of JPG";
                            return;
                        }
                        //Server.MapPath("../ItemsPhoto/") + TextBox1.Text + extension;
                        // Read file into a data stream
                        byte[] myData = new Byte[nFileLen];
                        myFile.InputStream.Read(myData, 0, nFileLen);

                        // Make sure a duplicate file doesn’t exist.  If it does, keep on appending an 
                        // incremental numeric until it is unique
                        string sFilename = System.IO.Path.GetFileName(myFile.FileName);
                        int file_append = 0;
                        while (System.IO.File.Exists(Server.MapPath(sSavePath + sFilename)))
                        {
                            file_append++;
                            sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName)
                                             + file_append.ToString() + ".jpg";
                        }

                        // Save the stream to disk
                        System.IO.FileStream newFile
                                = new System.IO.FileStream(Server.MapPath(sSavePath + sFilename),
                                                           System.IO.FileMode.Create);
                        newFile.Write(myData, 0, myData.Length);
                        newFile.Close();

                        // Check whether the file is really a JPEG by opening it
                        System.Drawing.Image.GetThumbnailImageAbort myCallBack =
                                       new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                        Bitmap myBitmap;
                        try
                        {
                            myBitmap = new Bitmap(Server.MapPath(sSavePath + sFilename));

                            // If jpg file is a jpeg, create a thumbnail filename that is unique.
                            file_append = 0;
                            string sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName)
                                                                     + sThumbExtension + ".jpg";
                            while (System.IO.File.Exists(Server.MapPath(sSavePath + sThumbFile)))
                            {
                                file_append++;
                                sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) +
                                               file_append.ToString() + sThumbExtension + ".jpg";
                            }

                            // Save thumbnail and output it onto the webpage
                            System.Drawing.Image myThumbnail
                                    = myBitmap.GetThumbnailImage(intThumbWidth,
                                                                 intThumbHeight, myCallBack, IntPtr.Zero);
                            myThumbnail.Save(Server.MapPath(sSavePath + TextBox1.Text + sThumbFile));
                            //imgPicture.ImageUrl = sSavePath + sThumbFile;
                            cmd.Parameters.AddWithValue("@Itemphoto",sSavePath + TextBox1.Text + sThumbFile);
                            // Displaying success information
                            lblmessage.Text = "File uploaded successfully!";

                            // Destroy objects
                            myThumbnail.Dispose();
                            myBitmap.Dispose();
                        }
                        catch (ArgumentException errArgument)
                        {
                            // The file wasn't a valid jpg file
                            lblmessage.Text = "The file wasn't a valid jpg file.";
                            System.IO.File.Delete(Server.MapPath(sSavePath + sFilename));
                        }
                    }





                }
                else
                {
                    cmd.Parameters.AddWithValue("@Itemphoto", "~/ItemsPhoto/item.png"); //DBNull.Value for Database NULL value
                }
                cmd.Parameters.AddWithValue("@Description", TextBox13.Text);
                cmd.Parameters.AddWithValue("@Status", 1);
                DateTime today = DateTime.Today;
                cmd.Parameters.AddWithValue("@Lastupdate", today);

                cmd.Parameters.AddWithValue("@Logtime", today);
                cmd.Parameters.AddWithValue("@ShopID", TextBox10.Text);


                cmd.ExecuteNonQuery();
                cn.Close();
                TextBox1.Text = string.Empty;
                lblmessage.Text = "Successfully Saved";
                Response.Redirect("WebForm2.aspx");
                //}
                //catch (Exception ex)
                //{
                //    lblmessage.Text = ex.Message;
                //}http://localhost:32387/WebForm2.aspx
            }
            else
            {

                con1.Close();

                decimal h;
                h = qty + Convert.ToDecimal(TextBox5.Text);
                string fileName = Path.GetFileName(FUpimg.PostedFile.FileName);
                string extension = Path.GetExtension(FUpimg.PostedFile.FileName);

                SqlConnection cn = new SqlConnection(ConnectionString);
                //string sql = "Insert into		 tbl_Item ([ItemCode],[ItemName],[PurchasePrice] ,[RetailPrice],[MinQty],[MaxQty] ,[MDate],[ExDate] ,[SupplierCode],[Status], [ItemQty],[ItemCategory], [Discount],[LogBy],[Photo],[Description])	values  	(@ItemCode,@ItemName,@PurchasePrice,@RetailPrice,@ItemQty,@MinQty,@MaxQty ,@MDate,@ExDate ,@SupplierCode,@Status, @ItemCategory,@Discount,@LogBy,@Itemphoto,@Description)	";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = " UPDATE tbl_Item SET  ItemCode=@ItemCode,ItemName=@ItemName,PurchasePrice=@PurchasePrice ,RetailPrice=@RetailPrice, ItemQty=@ItemQty,MinQty=@MinQty,MaxQty=@MaxQty ,MDate=@MDate,ExDate=@ExDate ,SupplierCode=@SupplierCode,ItemCategory=@ItemCategory,Tax=@Tax, Discount=@Discount,LogBy=@LogBy,Lastupdateby=@Lastupdateby,Photo=@Itemphoto,Description=@Description,Status=@Status,Lastupdate=@Lastupdate,Logtime=@Logtime,ShopID=@ShopID where ItemCode= '" + TextBox1.Text.Trim() + "' ";


                cmd.Connection = cn;
                cn.Open();



                cmd.Parameters.AddWithValue("@ItemCode", TextBox1.Text);
                cmd.Parameters.AddWithValue("@ItemName", TextBox2.Text);
                cmd.Parameters.AddWithValue("@PurchasePrice", Convert.ToDecimal(TextBox3.Text));
                cmd.Parameters.AddWithValue("@RetailPrice", Convert.ToDecimal(TextBox4.Text));
                cmd.Parameters.AddWithValue("@ItemQty", h);
                cmd.Parameters.AddWithValue("@MinQty", Convert.ToDecimal(TextBox6.Text));
                cmd.Parameters.AddWithValue("@MaxQty", Convert.ToDecimal(TextBox7.Text));


                cmd.Parameters.AddWithValue("@MDate", TextBox8.Text);

                cmd.Parameters.AddWithValue("@ExDate", TextBox9.Text);
                cmd.Parameters.AddWithValue("@SupplierCode", DropDownList1.Text);

                cmd.Parameters.AddWithValue("@ItemCategory", DDLCategory.Text);
                cmd.Parameters.AddWithValue("@Tax", Convert.ToDecimal(TextBox11.Text));
                cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(TextBox12.Text));
                cmd.Parameters.AddWithValue("@LogBy", Request.Cookies["InventMgtCookies"]["UserID"].ToString());
                cmd.Parameters.AddWithValue("@Lastupdateby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());






                if (FUpimg.HasFile)
                {
                    //if (extension == ".png" || extension == ".jpg" || extension == ".PNG" || extension == ".JPG")
                    //{
                    cmd.Parameters.AddWithValue("@Itemphoto", "~/ItemsPhoto/" + TextBox1.Text + extension);
                    //cmd.Parameters.AddWithValue("@Itemphoto", sSavePath + TextBox1.Text + extension);


                    //    string SaveLocation = Server.MapPath("../ItemsPhoto/") + TextBox1.Text + extension;
                    //    //using (MemoryStream stream = new MemoryStream())
                    //    //{
                    //    //    image.Save(stream, ImageFormat.Png);
                    //    //    stream.WriteTo(context.Response.OutputStream);
                    //    //}



                    //    string fileExtention = FUpimg.PostedFile.ContentType;
                    //    int fileLenght = FUpimg.PostedFile.ContentLength;
                    //    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FUpimg.PostedFile.InputStream);
                    //    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 96);

                    //    objImage.Save(SaveLocation,System.Drawing.Imaging.ImageFormat.Png);


                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('.jpg and .Png Format can be support')", true);
                    //}

                    string sSavePath;
                    string sThumbExtension;
                    int intThumbWidth;
                    int intThumbHeight;

                    // Set constant values
                    sSavePath = "ItemsPhoto/";
                    sThumbExtension = "_thumb";
                    intThumbWidth = 160;
                    intThumbHeight = 120;

                    // If file field isn’t empty
                    if (FUpimg.PostedFile != null)
                    {
                        // Check file size (mustn’t be 0)
                        HttpPostedFile myFile = FUpimg.PostedFile;
                        int nFileLen = myFile.ContentLength;
                        if (nFileLen == 0)
                        {
                            lblmessage.Text = "No file was uploaded.";
                            return;
                        }

                        // Check file extension (must be JPG)
                        if (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".jpg")
                        {
                            lblmessage.Text = "The file must have an extension of JPG";
                            return;
                        }
                        //Server.MapPath("../ItemsPhoto/") + TextBox1.Text + extension;
                        // Read file into a data stream
                        byte[] myData = new Byte[nFileLen];
                        myFile.InputStream.Read(myData, 0, nFileLen);

                        // Make sure a duplicate file doesn’t exist.  If it does, keep on appending an 
                        // incremental numeric until it is unique
                        string sFilename = System.IO.Path.GetFileName(myFile.FileName);
                        int file_append = 0;
                        while (System.IO.File.Exists(Server.MapPath(sSavePath + sFilename)))
                        {
                            file_append++;
                            sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName)
                                             + file_append.ToString() + ".jpg";
                        }

                        // Save the stream to disk
                        System.IO.FileStream newFile
                                = new System.IO.FileStream(Server.MapPath(sSavePath + sFilename),
                                                           System.IO.FileMode.Create);
                        newFile.Write(myData, 0, myData.Length);
                        newFile.Close();

                        // Check whether the file is really a JPEG by opening it
                        System.Drawing.Image.GetThumbnailImageAbort myCallBack =
                                       new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                        Bitmap myBitmap;
                        try
                        {
                            myBitmap = new Bitmap(Server.MapPath(sSavePath + sFilename));

                            // If jpg file is a jpeg, create a thumbnail filename that is unique.
                            file_append = 0;
                            string sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName)
                                                                     + sThumbExtension + ".jpg";
                            while (System.IO.File.Exists(Server.MapPath(sSavePath + sThumbFile)))
                            {
                                file_append++;
                                sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) +
                                               file_append.ToString() + sThumbExtension + ".jpg";
                            }

                            // Save thumbnail and output it onto the webpage
                            System.Drawing.Image myThumbnail
                                    = myBitmap.GetThumbnailImage(intThumbWidth,
                                                                 intThumbHeight, myCallBack, IntPtr.Zero);
                            myThumbnail.Save(Server.MapPath(sSavePath + TextBox1.Text + sThumbFile));
                            //imgPicture.ImageUrl = sSavePath + sThumbFile;

                            // Displaying success information
                            lblmessage.Text = "File uploaded successfully!";

                            // Destroy objects
                            myThumbnail.Dispose();
                            myBitmap.Dispose();
                            DirectoryInfo src = new DirectoryInfo(@"C:\Rainforestfusionpos\Inventory_Management\Accounts\Items\ItemsPhoto");
                            DirectoryInfo dest = new DirectoryInfo(@"C:\Rainforestwebsite\StripeAspNet\Accounts\Items\ItemsPhoto");
                            CopyDirectory(src, dest);
                            
                      





                        }
                        catch (ArgumentException errArgument)
                        {
                            // The file wasn't a valid jpg file
                            lblmessage.Text = "The file wasn't a valid jpg file.";
                            System.IO.File.Delete(Server.MapPath(sSavePath + sFilename));
                        }
                    }







                }
                else
                {
                    cmd.Parameters.AddWithValue("@Itemphoto", "~/ItemsPhoto/item.png"); //DBNull.Value for Database NULL value
                }
                cmd.Parameters.AddWithValue("@Description", TextBox13.Text);
                cmd.Parameters.AddWithValue("@Status", 1);
                DateTime today = DateTime.Today;
                cmd.Parameters.AddWithValue("@Lastupdate", today);

                cmd.Parameters.AddWithValue("@Logtime", today);
                cmd.Parameters.AddWithValue("@ShopID", TextBox10.Text);


                cmd.ExecuteNonQuery();
                cn.Close();

                lblmessage.Text = "Successfully Saved";
                //}


            }


            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox8.Text = string.Empty;
            TextBox9.Text = string.Empty;

            TextBox11.Text = string.Empty;
            TextBox12.Text = string.Empty;
            TextBox13.Text = string.Empty;


        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.Message;
        }
    }

    public bool ThumbnailCallback()
    {
        return false;
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


    void Barcodesearch()
    {




        SqlConnection cn = new SqlConnection(ConnectionString);



        cn.Close();
        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandType = CommandType.Text;
        cmd1.CommandText = " select *from tbl_Item where ItemCode='" + TextBox1.Text.Trim() + "' ";
        cmd1.Connection = cn;
        cn.Open();
        SqlDataReader rd1 = cmd1.ExecuteReader();

        if (rd1.HasRows)
        {

            while (rd1.Read())
            {
                qty = Convert.ToDecimal(rd1["ItemQty"].ToString());

                TextBox2.Text = (rd1["ItemName"].ToString());

            }

            rd1.Close();


        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Barcodesearch();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    
    }



    //public static void Copy(string sourceDirectory, string targetDirectory)
    //{
    //    DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
    //    DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

    //    CopyAll(diSource, diTarget);
    //}

    //public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
    //{
    //    Directory.CreateDirectory(target.FullName);

    //    // Copy each file into the new directory.
    //    foreach (FileInfo fi in source.GetFiles())
    //    {
    //        Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
    //        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
    //    }

    //    // Copy each subdirectory using recursion.
    //    foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
    //    {
    //        DirectoryInfo nextTargetSubDir =
    //            target.CreateSubdirectory(diSourceSubDir.Name);
    //        CopyAll(diSourceSubDir, nextTargetSubDir);
    //    }
    //}

 

    // Output will vary based on the contents of the source directory.

    static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
    {
        if (!destination.Exists)
        {
            destination.Create();
        }

        // Copy all files.
        FileInfo[] files = source.GetFiles();
        foreach (FileInfo file in files)
        {
            file.CopyTo(Path.Combine(destination.FullName,
                file.Name));
        }

        // Process subdirectories.
        DirectoryInfo[] dirs = source.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            // Get destination directory.
            string destinationDir = Path.Combine(destination.FullName, dir.Name);

            // Call CopyDirectory() recursively.
            CopyDirectory(dir, new DirectoryInfo(destinationDir));
        }
    }
}