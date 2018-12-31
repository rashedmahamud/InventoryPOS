﻿using System;
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
using System.Drawing.Imaging;

public partial class ManageItems : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ItemsListDataBind();
            txtsearch.Focus();            
           // lblmsg.Visible = false;
           
        }

    }

    // ///////  Item list Databind
    public void ItemsListDataBind()
    {
        try
        {
            //SqlConnection cn = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cn.Open();

            //grdItemList.DataSource = cmd.ExecuteReader();
            //grdItemList.EmptyDataText = "No Records Found";
            //grdItemList.DataBind();
            //cn.Close();
            //lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

            grdItemList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            grdItemList.DataSource = ds;
            grdItemList.EmptyDataText = "No Records Found";
            grdItemList.DataBind();
            con.Close();

        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }


    // // //// Item category list
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

        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    
    // //////// Search item by ID , Code , 
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //SqlConnection cn = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ItemSearch", cn);
            //cmd.Parameters.AddWithValue("@value", txtsearch.Text);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cn.Open();

            //grdItemList.DataSource = cmd.ExecuteReader();
            //grdItemList.EmptyDataText = "No Records Found";
            //grdItemList.DataBind();
            //cn.Close();
            //lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";


           // grdItemList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ItemSearch", con);
            cmd.Parameters.AddWithValue("@value", txtsearch.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            grdItemList.DataSource = ds;
            grdItemList.EmptyDataText = "No Records Found";
            grdItemList.DataBind();
            con.Close();

        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }


    //Load Data detail and Edit Part
    public void LoadDetailsData(string ID)
    {

        SqlConnection cn = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item_Details", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        cmd.Parameters.AddWithValue("@ID", ID);

       // SqlDataReader sdr = cmd.ExecuteReader();
       // DataTable dt = new DataTable();
     //   dt.Load(sdr);

        SqlDataReader rd = cmd.ExecuteReader();
        if (rd.HasRows)
        {
            rd.Read();
           // txtProductCode.Text = dt.Rows[0].ItemArray[1].ToString();
            txtProductCode.Text = rd["ItemCode"].ToString();
            txtItemName.Text = rd["ItemName"].ToString();
           // txtItemQty.Text = rd["ItemQty"].ToString();
            txtPurchasePrice.Text = rd["PurchasePrice"].ToString();
            txtRetailPrice.Text = rd["RetailPrice"].ToString();
            DDLCategory.Text = rd["ItemCategory"].ToString();
            txtItemDiscRate.Text = rd["Discount"].ToString();
            txtDescription.Text = rd["Description"].ToString();
            imgItemPhoto.ImageUrl = rd["Photo"].ToString();		 		
		 	 
        }          
       
         
        cn.Close();
    }

    // //// Open Edit Item popup window
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        lblmsg.Text ="";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblItemID.Text  = gvrow.Cells[1].Text;
        lblitemName.Text = gvrow.Cells[3].Text;
        //lblBarCode.Text = gvrow.Cells[2].Text;
          
        CategoryDDLDataBind();
        LoadDetailsData(gvrow.Cells[1].Text); 
       
        this.MpeEditItemShow.Show();   
              
    }

    
    // //// Open Bar-Code Creator popup window
    protected void LinkBarcode_Click(object sender, EventArgs e)
    {
       
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblBarcodeID.Text = gvrow.Cells[2].Text;
        Session["Barcode"] = gvrow.Cells[2].Text;
        Session["ItemName"] = gvrow.Cells[3].Text;
        Session["RetailsPrice"] = gvrow.Cells[6].Text;


        // Here is the page address //http://localhost:32387/Inventory_Management/Barcode/    
        String pa = Page.Request.Url.AbsoluteUri;
        // Take the page name    //
        String pn = Page.Request.Url.LocalPath;
        // Here is the server address   //////http://localhost:32387
        String sa = pa.Replace(pn, "");

        // So your picture's address is    
        String path = sa + "/Inventory_Management/Barcode/"; //sa + final;
        string sub = pa.Substring(0, pa.Length - 22) + "Barcode/"; //-22 means "Items/ManageItems.aspx"
        //String path = "http://citputer.com/test45/Barcode"; //sa + final;

        Iframebarcode.Attributes.Add("src", sub);
       // this.ModalPopupBarcodePanel.Show();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#popupBarcodePanel').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);
    }
    
    //// ///// //Update Item
    //Update item
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = Path.GetFileName(FUpimg.PostedFile.FileName);
            string extension = Path.GetExtension(FUpimg.PostedFile.FileName);

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Update_Item", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@ItemCode", txtProductCode.Text);
            cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text);
       //     cmd.Parameters.AddWithValue("@ItemQty", Convert.ToDecimal(txtItemQty.Text));
            cmd.Parameters.AddWithValue("@PurchasePrice", Convert.ToDecimal(txtPurchasePrice.Text));
            cmd.Parameters.AddWithValue("@RetailPrice", Convert.ToDecimal(txtRetailPrice.Text));
            cmd.Parameters.AddWithValue("@Discount", Convert.ToDecimal(txtItemDiscRate.Text));
            cmd.Parameters.AddWithValue("@ItemCategory", DDLCategory.Text);
            cmd.Parameters.AddWithValue("@Description",  txtDescription.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());            

            if (FUpimg.HasFile)
            {
                if (extension == ".png" || extension == ".jpg" || extension == ".PNG" || extension == ".JPG")
                {
                    cmd.Parameters.AddWithValue("@Itemphoto", "~/ItemsPhoto/" + txtProductCode.Text + extension);

                   // string strPath = MapPath("../ItemsPhoto/") + txtProductCode.Text + extension;
                    string fn = System.IO.Path.GetExtension(FUpimg.PostedFile.FileName);
                    string SaveLocation = Server.MapPath("../ItemsPhoto/") + txtProductCode.Text + extension;

                    string fileExtention = FUpimg.PostedFile.ContentType;
                    int fileLenght = FUpimg.PostedFile.ContentLength;
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FUpimg.PostedFile.InputStream);
                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 96);
                    // Saving image in jpeg format
                    objImage.Save(SaveLocation, ImageFormat.Png);

                 //   FUpimg.SaveAs(strPath);
                    LoadDetailsData(lblItemID.Text);
                   // imgItemPhoto.ImageUrl = "/ItemsPhoto/" + txtProductCode.Text + extension;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Only .jpg and .Png Format can be support')", true);                     
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@Itemphoto", imgItemPhoto.ImageUrl);
            }

            cmd.ExecuteNonQuery();
            cn.Close();


            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Updated";
            
            this.MpeEditItemShow.Show();
            LoadDetailsData(lblItemID.Text);
            ItemsListDataBind();
             
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
            this.MpeEditItemShow.Show(); 
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
    
    protected void LinkItemsDelete_Click(object sender, EventArgs e)
    {
        //lblmsg.Text = "";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblCodeDeleteItem.Text = gvrow.Cells[2].Text;
        lblDeleteItem_CustName.Text = gvrow.Cells[3].Text;    

        this.ModalPopupDeleteItem.Show();
    }


    // /////// Ban  Item : just changing status 1 to = 2 |  
    protected void btnDeleteItem_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Delete_Item", cn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", lblCodeDeleteItem.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());
            
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            //lblmsg.Visible = true;
            //lblmsg.Text = "Successfully Deleted";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Deleted Item')", true);
            ItemsListDataBind();
        }
        catch //(Exception ex)
        {
            //lblmsg.Visible = true;
            //lblmsg.Text = ex.Message;
            this.ModalPopupDeleteItem.Show();
        }
    }


    //AutoComplete  AutoCompleteExtender  ////////////////////////////////////////////   AutoCompleteExtender
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMDN(string prefixText)
    {

        string constr = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ToString();
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT TOP 8 * from    tbl_item where   ItemCode like '%' + @value + '%' " +
                                        " or  ItemName like '%' + @value + '%' or  ItemCategory like '%' + @value + '%'  ", con);       
        cmd.Parameters.AddWithValue("@Value", prefixText);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        List<string> MDN = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            // string var = dt.Rows[i][0].ToString() + " " + dt.Rows[i][12].ToString();
            string var = dt.Rows[i][2].ToString();
            MDN.Add(var);
            con.Close();
        }
        return MDN;        
    }

    
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[12].Visible = true;
    }


    protected void grdItemList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdItemList.PageIndex = e.NewPageIndex;
        ItemsListDataBind();
    }
    protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtsearch.Text = string.Empty;
        ItemsListDataBind();
    }
}