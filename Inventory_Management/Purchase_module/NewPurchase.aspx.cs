using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Purchase_module_NewPurchase : System.Web.UI.Page
{
    List<string> lv = new List<string>();

    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    DataTable tablePurchase = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtItemSearch.Focus();
            CategoryDDLDataBind();
          //  ItemsListDataBind(DDLCategory.Text);
            SupplierNameDDLDataBind();      
            BindData(DDLCategory.Text);

            //Add item from item list 
     
            tablePurchase.Columns.Add("Code", typeof(string));
            tablePurchase.Columns.Add("Item Name", typeof(string));
            tablePurchase.Columns.Add("Qty", typeof(string));
            tablePurchase.Columns.Add("Price", typeof(string));
            tablePurchase.Columns.Add("Disc%", typeof(string));
            tablePurchase.Columns.Add("Total", typeof(string));
            Session["valuePurchase"] = tablePurchase;           
        }    

    }

 
    
    //Datalits Item List   
    protected void BindData(string category)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_Inv_DataBind_Item_Purchase");
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@category", category);
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
        // string Total = LblTotal.Text; 
        decimal Total = Math.Round((Convert.ToDecimal(Price) - (Convert.ToDecimal(Price) * Convert.ToDecimal(Disc) / 100)) * Convert.ToDecimal(Qty), 2);
 
                
       
        //Code	ItemsName	Available_Qty	Price	Disc%	Total
        DataTable dt = (DataTable)Session["valuePurchase"];
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

    // Customer Name Data bind into dropdown list 
    public void SupplierNameDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_Suppliers_name", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLSuppliername.DataSource = dt;
            DDLSuppliername.DataTextField = "Name";
            DDLSuppliername.DataValueField = "ID";
            DDLSuppliername.DataBind();
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
            total   += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));
            Disc    += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Disc%"));
            Qty     += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Qty"));

            e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 210;
            e.Row.Cells[6].Font.Bold = true;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          //  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
            lbltotal.Text = total.ToString();
            lblTotalQty.Text = Qty.ToString();
            //lbldisc.Text = Disc.ToString();
        }
    }
  

    protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["valuePurchase"];
        dt.Rows.RemoveAt(e.RowIndex);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind(); 

        if (grdSelectedItem.Rows.Count == 0)
        {
           // lblsubTotal.Text = "0";
            //lblVat.Text = "0";
            lbltotal.Text = "0";
            lblTotalQty.Text = "0";
        }
        else
        {
           // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
          //  double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
           // lblVat.Text = Math.Round(tex, 2).ToString();
           /// lbltotal.Text = lblsubTotal.Text; // + Convert.ToDouble(lblVat.Text)).ToString();
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
        SqlCommand cmd = new SqlCommand("SELECT TOP 5 * from    tbl_Item where ( ItemCode like '%'+ @Value +'%'  and [Status] = 1) "
                              + "  or ( ItemName like '%'+ @Value +'%'  and [Status] = 1)", con);

        cmd.Parameters.AddWithValue("@Value", prefixText);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        List<string> MDN = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            // string var = dt.Rows[i][1].ToString() + " " + dt.Rows[i][2].ToString();
            MDN.Add(dt.Rows[i][2].ToString());
            con.Close();
        }
        return MDN;
    }


    ////// Barcode scan search box  
    protected void txtItemSearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_ItemBarCodeSearch", cn);
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


            DataTable dt = (DataTable)Session["valuePurchase"];
            dt.Rows.Add(Code, itemName, "1", Price, disc, Total);
            grdSelectedItem.DataSource = dt;
            grdSelectedItem.DataBind();
            cn.Close();

            // double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            // lblVat.Text = Math.Round(tex, 2).ToString();
          //  lbltotal.Text = lblsubTotal.Text;

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
        //Session.Remove("value");
        //DataTable dt = (DataTable)Session["value"];
        //dt.Rows.Remove("value");
        //grdSelectedItem.DataSource = dt;
        //grdSelectedItem.DataBind();

        grdSelectedItem.DataSource = null;        
        grdSelectedItem.DataBind();
      //  lblsubTotal.Text = "0";
       // lblVat.Text = "0";
        lbltotal.Text = "0";
        lblTotalQty.Text = "0";
    }

    // Open Payment Popup window 
    protected void btnPayment_Click(object sender, EventArgs e)
    {
        //LinkButton Linkdetails = sender as LinkButton;
        //GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        if (grdSelectedItem.Rows.Count == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Add at least one item')", true);
            //  lbltotal.Text = "Record is empty";
        }
        else
        {
            //txtdiscount.Text = "00";            
            //lblChange.Text = "-";
            //lblDue.Text = "-"; 
            //lblvatamt.Text = lblVat.Text;
            //lblsubtot.Text = lblsubTotal.Text;
            
            txtPaid.Text = string.Empty;
            lbltotalpay.Text = lbltotal.Text;
           

            txtPaid.Focus();
            this.ModalPopupPayment.Show();
        }
    }
    
     //Inset Multiple Row in single trXID  - Function method
    protected void SavePurchaseItems()
    {
        try
        {             
             for (int i = 0; i < grdSelectedItem.Rows.Count; i++)
             {
                GridViewRow row = grdSelectedItem.Rows[i];

                string product_id = grdSelectedItem.Rows[i].Cells[1].Text;
                string ItemName = grdSelectedItem.Rows[i].Cells[2].Text;
                string Qty      = grdSelectedItem.Rows[i].Cells[3].Text;  
                string Price    = grdSelectedItem.Rows[i].Cells[4].Text;  
                //string Disc     = grdSelectedItem.Rows[i].Cells[5].Text;                 
                string Total    = grdSelectedItem.Rows[i].Cells[6].Text;  

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_INV_Insert_PurchaseItems", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@product_id",  product_id);
                cmd.Parameters.AddWithValue("@ItemName",    ItemName);
                cmd.Parameters.AddWithValue("@Qty",         Qty);
                cmd.Parameters.AddWithValue("@Price",       Price);
                cmd.Parameters.AddWithValue("@supplierid",  lblSuppID.Text);
                cmd.Parameters.AddWithValue("@Total",       Total);
                cmd.Parameters.AddWithValue("@puchase_date", txtDate.Text);
                 

                // This is output Parameter send to Invoice number for Invoice printing Page
                cmd.Parameters.Add("@InvoiceNoOutPut", SqlDbType.Int).Direction = ParameterDirection.Output;             
                cmd.ExecuteNonQuery();
                cn.Close();

                Session["InvoiceNoOutPut"] = cmd.Parameters["@InvoiceNoOutPut"].Value.ToString();                
             } 
           
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


    //Insert One Row Purchase payment info every one trXID
    protected void SavePurchasePaymentInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_Insert_PurchasePayment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@itemQty",     lblTotalQty.Text);            
            cmd.Parameters.AddWithValue("@total",       lbltotalpay.Text);
            cmd.Parameters.AddWithValue("@method",      DDLPaidBy.Text);
            cmd.Parameters.AddWithValue("@paidAmount",  txtPaid.Text);    
            cmd.Parameters.AddWithValue("@puchase_date", txtDate.Text);
            cmd.Parameters.AddWithValue("@Comment",     txtNote.Text);
            cmd.Parameters.AddWithValue("@supplierid",  lblSuppID.Text);
            cmd.Parameters.AddWithValue("@supplier",    lblsuppname.Text);
            cmd.Parameters.AddWithValue("@ShopId",      Request.Cookies["InventMgtCookies"]["ShopID"].ToString());
            cmd.Parameters.AddWithValue("@purchaseby",  Request.Cookies["InventMgtCookies"]["UserID"].ToString());

            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch
        {
             // lbtotalRow.Text = "No Records Found";
        }
    }
    

    //END Point - Purchase Completed and Move to ----------------- >>>>>>>>>>> Purchase invoice Print Page
    protected void bntPay_click(object sender, EventArgs e)
    {
        SavePurchaseItems();
        SavePurchasePaymentInfo();

        Session["purchaseCode"]     = Session["InvoiceNoOutPut"].ToString();
        Session["puchase_date"]     = txtDate.Text;
        Session["Supplier"]         = DDLSuppliername.Text;
        Session.Remove("valuePurchase");
        Response.Redirect("~/Purchase_module/Purchase_Invoice.aspx"); 
    }
    
    //Get customer information from customer table 
    protected void DDLSuppliername_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SuppliersEvent", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@ID", DDLSuppliername.SelectedValue);

            
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            lblsuppname.Text        =   dt.Rows[0].ItemArray[2].ToString();
            lblSuppContact.Text     =   dt.Rows[0].ItemArray[1].ToString();
            lblSuppID.Text          =   dt.Rows[0].ItemArray[0].ToString();
            cn.Close();

            this.ModalPopupPayment.Show();
            bntPay.Focus();
        }
        catch
        {
            lblSuppContact.Text = "";
            lblSuppID.Text = "";             
        }
    }
    
    //Items filter by category  | Categoy list from store items Table
    protected void DDLCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
       // ItemsListDataBind(DDLCategory.Text);
        BindData(DDLCategory.Text);
       
    }

    ////////////////////////////////// Plesae Rate Us  ***** http://codecanyon.net/downloads ///
}