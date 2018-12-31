using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Order_module_Order_Invoice : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["invoice"] != null)
            {
                this.Title = "Order_Invoice_" + Session["invoice"].ToString();
                SystemInfo();
                ItemsListDataBind();
                paymentListDataBind();
                InvoiceInfo();
                btntakepayment.Visible = false;

                if (Convert.ToDouble(lblDue.Text) > 0.00)
                {
                    btntakepayment.Visible = true;
                }
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
            lblInvoiceNo.Text = Session["invoice"].ToString();
          //  lblSupplier.Text = "Customer # " + Session["customer"].ToString();
            lbldate.Text = Session["date"].ToString(); 
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
            SqlCommand cmd = new SqlCommand("[SP_INV_DataBind_OrderItemList]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["invoice"].ToString());
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

    // ///////  Payment  list Databind
    public void paymentListDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesPaymentlist", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["invoice"].ToString());
            cn.Open();

            grdpaymentlist.DataSource = cmd.ExecuteReader();
            grdpaymentlist.EmptyDataText = "No Records Found";
            grdpaymentlist.DataBind();
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
            SqlCommand cmd = new SqlCommand("[SP_INV_DataBind_OrderInvoice]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            cmd.Parameters.AddWithValue("@InvoiceNo", Session["invoice"].ToString()); 

            SqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                rd.Read();
                lblCustname.Text = rd["CustName"].ToString();
                lblComment.Text = rd["comment"].ToString();                         
                lblCustaddress.Text = rd["CustAddress"].ToString();
                lblphoneno.Text = rd["CustPhone"].ToString();
                lblEmailaddr.Text = rd["CustEmail"].ToString();
                lblorderstatus.Text = rd["order_status"].ToString();
                lblpaystatus.Text = rd["payment_status"].ToString();
                lbldeliveryAddress.Text = rd["shippingaddress"].ToString();
                lblsubtotal.Text = rd["Subtotal"].ToString();
                lblvat.Text = rd["Vat"].ToString();
                lbldis.Text = rd["discount"].ToString();
                lbltotal.Text = rd["totalpayable"].ToString();
                lblpaid.Text = rd["paidAmount"].ToString();
                lblDue.Text = rd["dueAmount"].ToString();
                lblDuemanount.Text = rd["dueAmount"].ToString();
                lblInvoiceNoTop.Text = Session["invoice"].ToString();
            }
            cn.Close();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }


    // Total calculation
    decimal total = 0;
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total = total + Convert.ToDecimal(e.Row.Cells[4].Text); 
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;            
            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Sub Total = ";

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

    protected void btntakepayment_Click(object sender, EventArgs e)
    {
        Session["invoice"] = Session["invoice"].ToString();
        Response.Redirect("~/Order_module/TakePayment.aspx");
    }
}