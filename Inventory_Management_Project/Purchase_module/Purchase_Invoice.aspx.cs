using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Purchase_module_Purchase_Invoice : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["purchaseCode"] != null)
            {
                this.Title = "Purchase_Invoice_" + Session["purchaseCode"].ToString();
                SystemInfo();
                ItemsListDataBind();
                InvoiceInfo();
            }
        }

    }

    //header part  System information
    public void SystemInfo()
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


            lblshopTitle.Text = dt.Rows[0].ItemArray[1].ToString();
            lblshopAddress.Text = dt.Rows[0].ItemArray[2].ToString();
            lblcompany.Text = dt.Rows[0].ItemArray[1].ToString();
            lblcomaddr.Text = dt.Rows[0].ItemArray[2].ToString();

            lblPhone.Text = dt.Rows[0].ItemArray[3].ToString();
            lblcontact.Text = dt.Rows[0].ItemArray[3].ToString();
            lblwebAddress.Text = dt.Rows[0].ItemArray[5].ToString();
            lblEmail.Text = dt.Rows[0].ItemArray[4].ToString();
            lblemailaddress.Text = dt.Rows[0].ItemArray[4].ToString();
            //lblFooterMessage.Text = dt.Rows[0].ItemArray[8].ToString();
            //lblVATRegiNo.Text = dt.Rows[0].ItemArray[7].ToString();
            cn.Close();
            lblInvoiceNo.Text   = "Invoice # " + Session["purchaseCode"].ToString();
            lblSupplier.Text    = "Supplier # " + Session["Supplier"].ToString(); 
            lbldate.Text        = "Date   # " + Session["puchase_date"].ToString();
        }
        catch
        {
        }
    }


    // ///////  Item list Databind
    public void ItemsListDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("[SP_INV_DataBind_PurchaseItemList]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["purchaseCode"].ToString());
            cn.Open();

            grdItemList.DataSource = cmd.ExecuteReader();
            grdItemList.EmptyDataText = "No Records Found";
            grdItemList.DataBind();
            cn.Close();
          //  lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }

    // ///////  Invoice Info
    public void InvoiceInfo()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseInvoice", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["purchaseCode"].ToString()); 

            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                lblSuppliername.Text = rd["Name"].ToString();
                lblComment.Text = rd["comment"].ToString();
                lblcompanyName.Text = rd["CompanyName"].ToString();
               // lblcity.Text = rd["City"].ToString();
                lblsuppaddress.Text = rd["Address"].ToString();
                lblphoneno.Text = rd["Phone"].ToString();
                lblEmailaddr.Text = rd["Email"].ToString();
            }
            cn.Close();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }
 
    decimal total = 0;
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total = total + Convert.ToDecimal(e.Row.Cells[4].Text); 
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;            
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[2].Width = 10;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total Amount = ";

            //  Total Calculation
            e.Row.Cells[4].Text = total.ToString("");
            string totalat = total.ToString("c");
            int totalati = totalat.IndexOf('$');
            string totalatd = totalat.Substring(totalati + 1);
            e.Row.Cells[4].Text =  totalatd + " /-";

            e.Row.Font.Bold = true;
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        }
    }
}