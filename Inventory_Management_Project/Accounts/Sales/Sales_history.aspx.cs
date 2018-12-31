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

public partial class Sales_module_Sales_history : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Title = "Sales_Report_" + DateTime.Now.ToString("MMM_dd_yyyy_HHmmss"); 
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


            grdItemList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SalesList", con);
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


      // //////// Search item by ID , Code , 
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        try
        { 

            grdItemList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_Saleslist_search", con);
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


  

    // //// Redirect to Order invoice  
    protected void linkViewInvoice_Click(object sender, EventArgs e)
    {
        //lblmsg.Text ="";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        Session["invoice"] = gvrow.Cells[2].Text;
        Session["date"] = gvrow.Cells[4].Text;
        Session["customer"] = gvrow.Cells[3].Text;
        Response.Redirect("~/Sales/Sales_Invoice.aspx"); 
    }

        // //// Redirect to Take payment page  
    protected void linktakepayment_Click(object sender, EventArgs e)
    {
        //lblmsg.Text ="";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        Session["invoice"] = gvrow.Cells[2].Text;         
        Response.Redirect("~/Sales/TakePayment.aspx");
    }

    
    //AutoComplete  AutoCompleteExtender  ////////////////////////////////////////////   AutoCompleteExtender
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetMDN(string prefixText)
    {

        string constr = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ToString();
        SqlConnection con = new SqlConnection(constr);
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT TOP 8 * from    tbl_SalesPayment " +
          " where   (CustName like '%' + @value + '%' and  trxtype = 'sales')  or ( ID like '%' + @value + '%'  and  trxtype = 'sales' ) ", con);
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

    protected void grdItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           // e.Row.Cells[0].Width = 10;
            e.Row.Cells[1].Width = 2;
        }
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