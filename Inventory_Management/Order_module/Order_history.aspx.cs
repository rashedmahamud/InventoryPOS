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

public partial class Order_module_Order_history : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Title = "Orders_Report_" + DateTime.Now.ToString("MMM_dd_yyyy_HHmmss"); 
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
            //SqlCommand cmd = new SqlCommand("[SP_INV_DataBind_OrderList]", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cn.Open();

            //grdItemList.DataSource = cmd.ExecuteReader();
            //grdItemList.EmptyDataText = "No Records Found";
            //grdItemList.DataBind();
            //cn.Close();
            //lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

            grdItemList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_OrderList", con);
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
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_Orderlist_search", con);
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

        //lblItemID.Text = gvrow.Cells[1].Text;
        //lblitemName.Text = gvrow.Cells[3].Text;
        ////lblBarCode.Text = gvrow.Cells[2].Text;
 

        //CategoryDDLDataBind();
        //LoadDetailsData(gvrow.Cells[1].Text);
        //this.MpeEditItemShow.Show();

        Session["invoice"] = gvrow.Cells[2].Text;
        Session["date"] = gvrow.Cells[4].Text;
        Session["customer"] = gvrow.Cells[3].Text;
        Response.Redirect("~/Order_module/Order_Invoice.aspx"); 
    }

        // //// Redirect to Edit Order   page
    protected void lnkChangeStatus_Click(object sender, EventArgs e)
    {
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        Session["invoice"]      = gvrow.Cells[2].Text;
        Response.Redirect("~/Order_module/ChangeStatus.aspx"); 
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
          " where   (CustName like '%' + @value + '%' and  trxtype = 'order')  or ( ID like '%' + @value + '%'  and  trxtype = 'order' ) ", con);
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