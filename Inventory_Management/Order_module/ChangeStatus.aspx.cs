using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class Order_module_ChangeStatus : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["invoice"] != null)
            {
                CustomerNameDDLDataBind();
                lblInvoiceNo.Text = Session["invoice"].ToString();
                ItemsListDataBind();
                InvoiceInfo();
                
            }
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
            DDLCustname.DataValueField = "ID";
            DDLCustname.DataBind();
            cn.Close();
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
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
             //   DDLCustname.Items.Add(new ListItem(txt_box1.Text));        = rd["CustName"].ToString();
                DDLCustname.SelectedItem.Text   = rd["CustName"].ToString();
                DDlOrderstatus.Text     = rd["order_status"].ToString();
                DDlpaymentstatus.Text   = rd["payment_status"].ToString();
                txtDate.Text            = rd["ordedate"].ToString();
                txtAddress.Text         = rd["shippingaddress"].ToString();
                txtNote.Text            = rd["comment"].ToString();
                          
                //lblDue.Text = rd["dueAmount"].ToString();
            }
            cn.Close();
        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }



    // list column width
    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // e.Row.Cells[0].Width = 10;
            e.Row.Cells[2].Width = 10;
        }
    }

    protected void btnChangeStatus_Click(object sender, EventArgs e)
    {
        try
        {
          
                SqlConnection cn = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand("SP_INV_Update_changestatus", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                cmd.Parameters.AddWithValue("@date", txtDate.Text);
                cmd.Parameters.AddWithValue("@orderstatus",     DDlOrderstatus.Text);
                cmd.Parameters.AddWithValue("@payment_status",  DDlpaymentstatus.Text);
                cmd.Parameters.AddWithValue("@deliveryaddress", txtAddress.Text);                
                cmd.Parameters.AddWithValue("@comment",         txtNote.Text);
                cmd.Parameters.AddWithValue("@InvoiceNo",       lblInvoiceNo.Text);
             //   cmd.Parameters.AddWithValue("@CustID",          DDLCustname.SelectedValue);
               // cmd.Parameters.AddWithValue("@CustName",        DDLCustname.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ServedBy",        Request.Cookies["InventMgtCookies"]["UserID"].ToString());
                cmd.ExecuteNonQuery();
                cn.Close();

                InvoiceInfo();
                lblmsg.Text = "Saved";
    
        }
        catch
        {
            // lbtotalRow.Text = "No Records Found";
        }
    }
}