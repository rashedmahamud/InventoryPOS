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

public partial class Customers_Purchase_historyCustomers : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ItemsListDataBind();
          
            // lblmsg.Visible = false;
        }

    }

    // ///////  Item list Databind
    public void ItemsListDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseItemCustomerPanel", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CusID", Request.Cookies["InventMgtCustomerCookies"]["ID"]);
            cn.Open();

            grdItemList.DataSource = cmd.ExecuteReader();
            grdItemList.EmptyDataText = "No Records Found";
            grdItemList.DataBind();
            cn.Close();
            lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }


   

    // //// Go to Invoice page
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

        Session["invoice"] = gvrow.Cells[2].Text;
       // Session["puchase_date"] = gvrow.Cells[4].Text;
       // Session["Supplier"] = gvrow.Cells[3].Text;
        Response.Redirect("~/Customers/Purchase_InvoiceCustomer.aspx"); 
    }
    
    
  
}