using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Accounts_Sales_DamageEntry : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    DataTable tablereturn = new DataTable();
    string ShopId = "1461";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtItemSearch.Focus();
            CategoryDDLDataBind();
          
            BindData(DDLCategory.Text);

            //Add item from item list 

            tablereturn.Columns.Add("Code", typeof(string));
            tablereturn.Columns.Add("Item Name", typeof(string));
            tablereturn.Columns.Add("Qty", typeof(string));
            tablereturn.Columns.Add("Price", typeof(string));
            tablereturn.Columns.Add("Disc%", typeof(string));
            tablereturn.Columns.Add("Total", typeof(string));
            Session["valuereturn"] = tablereturn;
        }

    }




    //Datalits Item List   
    protected void BindData(string category)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Item_SR");
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@category", category);
            //Request.Cookies["InventMgtCookies"]["ShopID"].ToString()
            cmd.Parameters.AddWithValue("@ShopID", ShopId.ToString());
            con.Open();

            DataList1.DataSource = cmd.ExecuteReader();
            DataList1.DataBind();
            con.Close();
        }
        catch
        {
        }
    }


    //Click add to cart menu
    protected void btn_Goclick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataListItem item = (DataListItem)btn.NamingContainer;
        Label lblId = (Label)item.FindControl("LblID");
        Label LblCode = (Label)item.FindControl("LblCode");
        Label LblItemName = (Label)item.FindControl("LblItemName");
        Label LblQty = (Label)item.FindControl("LblQty");
        Label LblPrice = (Label)item.FindControl("LblPrice");
        Label LblDisc = (Label)item.FindControl("LblDisc");
        Label LblTotal = (Label)item.FindControl("LblTotal");
        TextBox txtqty = (TextBox)item.FindControl("txtqty");

        string ID = lblId.Text;
        string Code = LblCode.Text;
        string ItemName = LblItemName.Text;
        string Qty = txtqty.Text; // LblQty.Text;
        decimal QtyStock = Convert.ToDecimal(LblQty.Text);
        string Price = LblPrice.Text;
        string Disc = LblDisc.Text;
      
        decimal Total = Math.Round((Convert.ToDecimal(Price) - (Convert.ToDecimal(Price) * Convert.ToDecimal(Disc) / 100)) * Convert.ToDecimal(Qty), 2);


        DataTable dt = (DataTable)Session["valuereturn"];
        dt.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind();


      

    }

    //Category Bind on dropdown list
    public void CategoryDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CategoryDDL_SR", cn);
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

 
   


    //Fix Row Width 
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 310;
        }
    }

    //Fix Row Width  and Sum of total cost
    // double pricetotal = 0;
    double total = 0;
    double Disc = 0;
    double Qty = 0;
    protected void grdSelectedItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //pricetotal += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Price"));
            total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            Disc += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Disc%"));
            Qty += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));

            e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 210;
            e.Row.Cells[6].Font.Bold = true;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            ////  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
            //lblsubTotal.Text = total.ToString();
            //lblTotalQty.Text = Qty.ToString();
            ////lbldisc.Text = Disc.ToString();
        }
    }


    protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["valuereturn"];
        dt.Rows.RemoveAt(e.RowIndex);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind();

     


    }

   
    protected void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ReturnItemBarCodeSearch", cn);
            cmd.Parameters.AddWithValue("@ItemCode", txtItemSearch.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();


            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dtable = new DataTable();
            dtable.Load(sdr);


            string Code = dtable.Rows[0].ItemArray[0].ToString();
            string itemName = dtable.Rows[0].ItemArray[1].ToString();
            string disc = dtable.Rows[0].ItemArray[3].ToString();
            string Price = dtable.Rows[0].ItemArray[2].ToString();
            string Total = dtable.Rows[0].ItemArray[4].ToString();


            DataTable dt = (DataTable)Session["valuereturn"];
            dt.Rows.Add(Code, itemName, "1", Price, disc, Total);
            grdSelectedItem.DataSource = dt;
            grdSelectedItem.DataBind();
            cn.Close();

          

            txtItemSearch.Text = string.Empty;
            txtItemSearch.Focus();
            //  btnPayment.Focus();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }

    protected void btnsuspen_Click(object sender, EventArgs e)
    {
     

        grdSelectedItem.DataSource = null;
        grdSelectedItem.DataBind();
      
    }

    // Open Payment Popup window 
  
    protected void SaveReturnItem()
    {
        try
        {
            for (int i = 0; i < grdSelectedItem.Rows.Count; i++)
            {
                GridViewRow row = grdSelectedItem.Rows[i];

                string Code = grdSelectedItem.Rows[i].Cells[1].Text;
                string ItemName = grdSelectedItem.Rows[i].Cells[2].Text;
                string Qty = grdSelectedItem.Rows[i].Cells[3].Text;
                string Price = grdSelectedItem.Rows[i].Cells[4].Text;
                string Disc = grdSelectedItem.Rows[i].Cells[5].Text;
                string Total = grdSelectedItem.Rows[i].Cells[6].Text;

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_POS_Insert_Damage", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@Code", Code);
                cmd.Parameters.AddWithValue("@ItemName", ItemName);
                cmd.Parameters.AddWithValue("@Qty", Qty);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Disc", Disc);
                cmd.Parameters.AddWithValue("@Total", Total);
                cmd.Parameters.AddWithValue("@ShopID", ShopId.ToString());

                cmd.ExecuteNonQuery();
                cn.Close();

            }

        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


    protected void bntPay_click(object sender, EventArgs e)
    {
        SaveReturnItem();
      

       
    }


    //Get customer information from customer table 
   


    //Items filter by category  | Categoy list from store items Table
    protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ItemsListDataBind(DDLCategory.Text);
        BindData(DDLCategory.Text);

    }



}