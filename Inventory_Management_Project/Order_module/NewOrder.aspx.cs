using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Order_module_NewOrder : System.Web.UI.Page
{ 

    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    DataTable tableOrder = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtItemSearch.Focus();
            CategoryDDLDataBind();
          //  ItemsListDataBind(DDLCategory.Text);
            CustomerNameDDLDataBind();
            VatRate();
            BindData(DDLCategory.Text);     
        }    

    }


    // Vat Rate from Settings default VAT Rate is 5 % 
    public void VatRate()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_SettingsUpdate", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);         
            lblVatRate.Text = dt.Rows[0].ItemArray[6].ToString();
            cn.Close();
        }
        catch
        {
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
            double Qty = Convert.ToDouble(txtqty.Text);
            decimal QtyStock = Convert.ToDecimal(LblQty.Text);
            double Price = Convert.ToDouble(LblPrice.Text);           
            double Disc = Convert.ToDouble(LblDisc.Text);
            double Total = Math.Round((Price - (Price * Disc / 100)) * Qty, 2); 
            


            //Check Item Quantity less than 1 
            if (Convert.ToDecimal(Qty) <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Purchase the Item from supplier')", true);
            }
            if (Convert.ToDecimal(Qty) > QtyStock)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your given quantity is Greater than Stock Quantity')", true);
            }
            else
            {
                ///    Add item from item list            
                if (Session["valueOrders"] != null)
                {
                    tableOrder = Session["valueOrders"] as DataTable;
                }
                else
                {
                    //Add item from item list            
                    tableOrder.Columns.Add("Code", typeof(string));
                    tableOrder.Columns.Add("Item Name", typeof(string));
                    tableOrder.Columns.Add("Qty", typeof(string));
                    tableOrder.Columns.Add("Price", typeof(string));
                    tableOrder.Columns.Add("Disc%", typeof(string));
                    tableOrder.Columns.Add("Total", typeof(string));
                    Session["valueOrders"] = tableOrder;
                }
                tableOrder.Rows.Add(Code, ItemName, Qty, Price, Disc, Total);
                Session.Add("valueOrders", tableOrder); 

                grdSelectedItem.DataSource = tableOrder;
                grdSelectedItem.DataBind();

                // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
                double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
                // lbldisc.Text =  pricetotal - 
                lblVat.Text = Math.Round(tex, 2).ToString();
                lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
 
            }
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
    public void CustomerNameDDLDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Customers_name", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            DDLCustname.DataSource = dt;
            DDLCustname.DataTextField = "Name";
            DDLCustname.DataValueField = "Name";
            DDLCustname.DataBind();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }
        
 

    // Sum of total cost
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
            //  Label pricetotal = (Label)e.Row.FindControl("pricetotal");
            lblsubTotal.Text = total.ToString();
            lblTotalQty.Text = Qty.ToString();
            //lbldisc.Text = Disc.ToString();
        }
    }
       

    protected void grdSelectedItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["valueOrders"];  
        dt.Rows.RemoveAt(e.RowIndex);
        grdSelectedItem.DataSource = dt;
        grdSelectedItem.DataBind();


        if (grdSelectedItem.Rows.Count == 0)
        {
            lblsubTotal.Text = "0";
            lblVat.Text = "0";
            lbltotal.Text = "0";
            lblTotalQty.Text = "0";
        }
        else
        {
           // double tex = (Convert.ToDouble(lblsubTotal.Text) * 5) / 100;
            double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
            lblVat.Text = Math.Round(tex, 2).ToString();
            lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();
        }
      
        
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
                string Disc = dtable.Rows[0].ItemArray[3].ToString();
                string Price = dtable.Rows[0].ItemArray[2].ToString();
                string Total = dtable.Rows[0].ItemArray[4].ToString();
              
                if (Session["valueOrders"] != null)
                {
                    tableOrder = Session["valueOrders"] as DataTable;
                }
                else
                {
                    //Add item from item list            
                    tableOrder.Columns.Add("Code", typeof(string));
                    tableOrder.Columns.Add("Item Name", typeof(string));
                    tableOrder.Columns.Add("Qty", typeof(string));
                    tableOrder.Columns.Add("Price", typeof(string));
                    tableOrder.Columns.Add("Disc%", typeof(string));
                    tableOrder.Columns.Add("Total", typeof(string));
                    Session["valueOrders"] = tableOrder;
                }
               
                tableOrder.Rows.Add(Code, itemName, "1", Price, Disc, Total);
                Session.Add("valueOrders", tableOrder);

                grdSelectedItem.DataSource = tableOrder;
                grdSelectedItem.DataBind();
                cn.Close();

                double tex = ((Convert.ToDouble(lblsubTotal.Text) * Convert.ToDouble(lblVatRate.Text)) / 100);
                lblVat.Text = Math.Round(tex, 2).ToString();
                lbltotal.Text = (Convert.ToDouble(lblsubTotal.Text) + Convert.ToDouble(lblVat.Text)).ToString();

                txtItemSearch.Text = string.Empty;
                txtItemSearch.Focus();
              //  btnPayment.Focus();
        }
        catch
        {
            txtItemSearch.Text = string.Empty;
            txtItemSearch.Focus();
            //lbtotalRow.Text = "No Records Found";
        }    
    }

    protected void btnsuspen_Click(object sender, EventArgs e)
    {
        Session.Remove("valueOrders");        
        grdSelectedItem.DataSource = null;
        grdSelectedItem.DataBind();  
         

        lblsubTotal.Text = "0";
        lblVat.Text = "0";
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
            txtdiscount.Text = "00";
            txtPaid.Text = string.Empty;
            lblChange.Text = "-";
            lblDue.Text = "-";

            lbltotalpay.Text = lbltotal.Text;
            lblvatamt.Text = lblVat.Text;
            lblsubtot.Text = lblsubTotal.Text;
             
            txtPaid.Focus();
            this.ModalPopupPayment.Show();
        }
    }
    
    //// Paid Amount from customer   - Popup Panel
    protected void txtPaid_TextChanged(object sender, EventArgs e)
    {
        double changeAmt  = Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotalpay.Text);
        lblChange.Text = changeAmt.ToString();
        if (Convert.ToDouble(lbltotalpay.Text) < Convert.ToDouble(txtPaid.Text))
        {
            lblChange.Text = Math.Round((Convert.ToDouble(txtPaid.Text) - Convert.ToDouble(lbltotalpay.Text)),2).ToString();
            lblDue.Text = "0";
            DDlpaymentstatus.Text = "Paid";
        }
        else
        {
            lblChange.Text = "0";
            lblDue.Text = Math.Round((Convert.ToDouble(lbltotalpay.Text) - Convert.ToDouble(txtPaid.Text)), 2).ToString();
        }

        this.ModalPopupPayment.Show();        
        bntPay.Focus();

    }

        //// discount calculation Amount from customer   - Popup Panel
    protected void txtdiscount_TextChanged(object sender, EventArgs e)
    {

        if (txtdiscount.Text != string.Empty)
        {
            double subtowithdiscount = Convert.ToDouble(lblsubTotal.Text) - Convert.ToDouble(txtdiscount.Text);

            double tax = ((subtowithdiscount * Convert.ToDouble(lblVatRate.Text)) / 100);
            lblvatamt.Text = Math.Round(tax, 2).ToString();
            double totalpayable = (subtowithdiscount + Convert.ToDouble(lblvatamt.Text));
            lbltotalpay.Text = totalpayable.ToString();

            this.ModalPopupPayment.Show();
            txtPaid.Focus();
        }
        else
        {
            txtdiscount.Text = "0";
            double subtowithdiscount = Convert.ToDouble(lblsubTotal.Text) - Convert.ToDouble(txtdiscount.Text);

            double tax = ((subtowithdiscount * Convert.ToDouble(lblVatRate.Text)) / 100);
            lblvatamt.Text = Math.Round(tax, 2).ToString();
            double totalpayable = (subtowithdiscount + Convert.ToDouble(lblvatamt.Text));
            lbltotalpay.Text = totalpayable.ToString();

            this.ModalPopupPayment.Show();
            txtPaid.Focus();
        }

    }
    

    //Inset Multiple Row in single trXID  - Function method
    protected void SaveSaleItem()
    {
        try
        {             
             for (int i = 0; i < grdSelectedItem.Rows.Count; i++)
             {
                GridViewRow row = grdSelectedItem.Rows[i];

                string Code     = grdSelectedItem.Rows[i].Cells[1].Text;
                string ItemName = grdSelectedItem.Rows[i].Cells[2].Text;
                string Qty      = grdSelectedItem.Rows[i].Cells[3].Text;  
                string Price    = grdSelectedItem.Rows[i].Cells[4].Text;  
                string Disc     = grdSelectedItem.Rows[i].Cells[5].Text;                 
                string Total    = grdSelectedItem.Rows[i].Cells[6].Text;  

                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_POS_Insert_SalesItems", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@Code",        Code);
                cmd.Parameters.AddWithValue("@ItemName",    ItemName);
                cmd.Parameters.AddWithValue("@Qty",         Qty);
                cmd.Parameters.AddWithValue("@Price",       Price);
                cmd.Parameters.AddWithValue("@Disc",        Disc);
                cmd.Parameters.AddWithValue("@Total",       Total);

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


    //Insert One Row Order payment info every one trXID
    protected void SaveSalePaymentInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_Insert_ReceivePayment", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            cmd.Parameters.AddWithValue("@SalesQty",    lblTotalQty.Text);
            cmd.Parameters.AddWithValue("@Subtotal",    lblsubTotal.Text);
            cmd.Parameters.AddWithValue("@discount",    txtdiscount.Text);
            cmd.Parameters.AddWithValue("@Vat",         lblvatamt.Text);
            cmd.Parameters.AddWithValue("@totalpayable",lbltotalpay.Text);
            cmd.Parameters.AddWithValue("@payType",     DDLPaidBy.Text);
            cmd.Parameters.AddWithValue("@paidAmount",  txtPaid.Text);
            cmd.Parameters.AddWithValue("@changeAmount", lblChange.Text);
            cmd.Parameters.AddWithValue("@dueAmount",   lblDue.Text);            
            cmd.Parameters.AddWithValue("@note",        txtNote.Text);           
            cmd.Parameters.AddWithValue("@CustID",      lblCustID.Text);
            cmd.Parameters.AddWithValue("@CustName",    DDLCustname.Text);
            cmd.Parameters.AddWithValue("@CustContact", lblCustContact.Text);

            //delivery info
            cmd.Parameters.AddWithValue("@date",        txtDate.Text);
            cmd.Parameters.AddWithValue("@orderstatus", DDlOrderstatus.Text);
            cmd.Parameters.AddWithValue("@paystatus",   DDlpaymentstatus.Text);
            cmd.Parameters.AddWithValue("@trxtype",     "order");
            cmd.Parameters.AddWithValue("@deliveryaddress", txtAddress.Text);

            cmd.Parameters.AddWithValue("@ShopId",          Request.Cookies["InventMgtCookies"]["ShopID"].ToString());
            cmd.Parameters.AddWithValue("@ServedBy",        Request.Cookies["InventMgtCookies"]["UserID"].ToString());
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }


    //END Point - Order Completed and Move to ----------------- >>>>>>>>>>> Invoice Print Page
    protected void bntPay_click(object sender, EventArgs e)
    {
        SaveSaleItem();
        SaveSalePaymentInfo();
    
        Session["invoice"] = Session["InvoiceNoOutPut"].ToString(); 
        Session["date"] =       txtDate.Text;
        Session["customer"] = DDLCustname.Text;
        Session.Remove("valueOrders");
        Response.Redirect("~/Order_module/Order_Invoice.aspx"); 
    }


    //Get customer information from customer table 
    protected void DDLCustname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_CustomersEvent", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@CustName",DDLCustname.Text);

            
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);

            lblCustContact.Text     = dt.Rows[0].ItemArray[1].ToString();
            lblCustID.Text          =   dt.Rows[0].ItemArray[0].ToString();
            txtAddress.Text         = dt.Rows[0].ItemArray[2].ToString();
            cn.Close();

            this.ModalPopupPayment.Show();
            bntPay.Focus();
        }
        catch
        {
            lblCustContact.Text = "";
            lblCustID.Text = "";             
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