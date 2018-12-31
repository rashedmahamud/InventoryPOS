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

public partial class Purchase_module_Purchase_history : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Title = "Purchase_Report_" + DateTime.Now.ToString("MMM_dd_yyyy_HHmmss"); 
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
         //   SqlConnection cn = new SqlConnection(ConnectionString);
         //   SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseItem", cn);
         //   cmd.CommandType = CommandType.StoredProcedure;
         //   cn.Open();

         //   grdItemList.DataSource = cmd.ExecuteReader();
         //   grdItemList.EmptyDataText = "No Records Found";
         //   grdItemList.DataBind();
         //   cn.Close();
         //// //  lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";


            grdItemList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseItem", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            grdItemList.DataSource = ds;
            grdItemList.EmptyDataText = "No Records Found";
            grdItemList.DataBind();
            con.Close();
           // lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";
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
           // SqlConnection cn = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseItem_search", cn);
		
	    //grdItemList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseItem_search", con);
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
            lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }


    //Load Data detail and Edit Part
    public void LoadDetailsData(string ID)
    {

        //SqlConnection cn = new SqlConnection(ConnectionString);
        //SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item_Details", cn);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cn.Open();
        //cmd.Parameters.AddWithValue("@ID", ID);

        //// SqlDataReader sdr = cmd.ExecuteReader();
        //// DataTable dt = new DataTable();
        ////   dt.Load(sdr);

        //SqlDataReader rd = cmd.ExecuteReader();
        //if (rd.HasRows)
        //{
        //    rd.Read();
        //    // txtProductCode.Text = dt.Rows[0].ItemArray[1].ToString();
        //    txtProductCode.Text = rd["ItemCode"].ToString();
        //    txtItemName.Text = rd["ItemName"].ToString();
        //    txtItemQty.Text = rd["ItemQty"].ToString();
        //    txtPurchasePrice.Text = rd["PurchasePrice"].ToString();
        //    txtRetailPrice.Text = rd["RetailPrice"].ToString();
        //    DDLCategory.Text = rd["ItemCategory"].ToString();
        //    txtItemDiscRate.Text = rd["Discount"].ToString();
        //    imgItemPhoto.ImageUrl = rd["Photo"].ToString();

        //}


        //cn.Close();
    }

    // //// Open Edit Item popup window
    protected void linkViewInvoice_Click(object sender, EventArgs e)
    {
        //lblmsg.Text ="";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        //lblItemID.Text = gvrow.Cells[1].Text;
        //lblitemName.Text = gvrow.Cells[3].Text;
        ////lblBarCode.Text = gvrow.Cells[2].Text;
 

        //CategoryDDLDataBind();
        //LoadDetailsData(gvrow.Cells[1].Text);
        //this.MpeEditItemShow.Show();

        Session["purchaseCode"] = gvrow.Cells[2].Text;
        Session["puchase_date"] = gvrow.Cells[4].Text;
        Session["Supplier"] = gvrow.Cells[3].Text;
        Response.Redirect("~/Purchase_module/Purchase_Invoice.aspx"); 
    }
    
    //AutoComplete  AutoCompleteExtender  ////////////////////////////////////////////   AutoCompleteExtender
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMDN(string prefixText)
    {

        string constr = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ToString();
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT TOP 8 * from    tbl_purchase_payment  where   supplier like '%' + @value + '%'  or  purchaseCode like '%' + @value + '%'  ", con);
        cmd.Parameters.AddWithValue("@Value", prefixText);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        List<string> MDN = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            // string var = dt.Rows[i][0].ToString() + " " + dt.Rows[i][12].ToString();
            string var = dt.Rows[i][0].ToString();
            MDN.Add(var);
            con.Close();
        }
        return MDN;
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