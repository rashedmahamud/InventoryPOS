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

public partial class Suppliers_ManageSuppliers : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBindsupplier();
            txtsearch.Focus();
            // lblmsg.Visible = false;
        }

    }

    // ///////    list Databind
    public void DataBindsupplier()
    {
        try
        { 

            grdSupplierList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SupplierList", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            grdSupplierList.DataSource = ds;
            grdSupplierList.EmptyDataText = "No Records Found";
            grdSupplierList.DataBind();
            con.Close();
        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    // ///////     Details purchase databind 
    public void DataBindsupplierPurchase(string suppid)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_PurchaseListSupplier", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@supplierid",suppid);
            cn.Open();

            grdviewpurchasehistory.DataSource = cmd.ExecuteReader();
            grdviewpurchasehistory.EmptyDataText = "No Records Found";
            grdviewpurchasehistory.DataBind();
            cn.Close();
           // lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }

      // //////// Search item by ID , Code , 
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        try
        {
            
            grdSupplierList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SupplierListSearch", con);
            cmd.Parameters.AddWithValue("@value", txtsearch.Text);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            grdSupplierList.DataSource = ds;
            grdSupplierList.EmptyDataText = "No Records Found";
            grdSupplierList.DataBind();
            con.Close();
         

        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    protected void Linkdtview_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";        
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblsuppid.Text   = gvrow.Cells[2].Text;
        lblCompany.Text = gvrow.Cells[4].Text;
        lblname.Text = gvrow.Cells[3].Text;
        lblsuppname.Text = gvrow.Cells[3].Text;
        lblEmail.Text = gvrow.Cells[6].Text;
        lblphone.Text =    gvrow.Cells[5].Text;
        DataBindsupplierPurchase(lblsuppid.Text);
       // this.MpedtviewShow.Show();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#pnlpopupView').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "pnlpopupView", sb.ToString(), false);
    }
    
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        btnSave.Text = "Save";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblID.Text = gvrow.Cells[2].Text;
        txtName.Text = gvrow.Cells[3].Text;
        txtContact.Text = gvrow.Cells[5].Text;
        txtEmail.Text = gvrow.Cells[6].Text;
        txtaddress.Text = gvrow.Cells[7].Text;
        txtType.Text = gvrow.Cells[9].Text;
        txtcity.Text = gvrow.Cells[8].Text;
        txtCompanyName.Text = gvrow.Cells[4].Text;

        if (gvrow.Cells[8].Text == "Inactive")
        {
            btnSave.Text = "Save & Active";
        }
        this.MpeEditShow.Show();
    }

    protected void LinkbanCustomer_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblInactiveID.Text = gvrow.Cells[2].Text;
        lblInactiveSupplierName.Text = gvrow.Cells[3].Text;

        this.ModalPopupInactive.Show(); 
        //LinkButton Linkdetails = sender as LinkButton;
        //GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        ////lblItemID.Text = gvrow.Cells[1].Text;

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append(@"<script type='text/javascript'>");
        //sb.Append("$('#detailModal').modal('show');");
        //sb.Append(@"</script>");
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);
    }
    
    //Update Supplier info
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_Update_Supplier", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id",              lblID.Text);
            cmd.Parameters.AddWithValue("@Name",            txtName.Text);
            cmd.Parameters.AddWithValue("@Phone",           txtContact.Text);
            cmd.Parameters.AddWithValue("@Email",           txtEmail.Text);
            cmd.Parameters.AddWithValue("@Address",         txtaddress.Text);
            cmd.Parameters.AddWithValue("@City",            txtcity.Text);
            cmd.Parameters.AddWithValue("@Type",            txtType.Text);
            cmd.Parameters.AddWithValue("@CompanyName",     txtCompanyName.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby",    Request.Cookies["InventMgtCookies"]["UserID"].ToString());

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Updated";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Updated')", true);
            DataBindsupplier();
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
            this.MpeEditShow.Show();
        }

    }

    

    // Inactive or delete supplier
    protected void btnInactive_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_Inactive_Supplier", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblInactiveID.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Inactivated";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Inactivated')", true);
            DataBindsupplier();

           
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
            this.MpeEditShow.Show();
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
        SqlCommand cmd = new SqlCommand("SELECT TOP 8 * from    tbl_supplier " +
          " where  ID like '%' + @value + '%' or [Name] like '%' + @value + '%' 	or [CompanyName] like '%' + @value + '%'  ", con);
        cmd.Parameters.AddWithValue("@Value", prefixText);

        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        List<string> MDN = new List<string>();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            // string var = dt.Rows[i][0].ToString() + " " + dt.Rows[i][12].ToString();
            string var = dt.Rows[i][1].ToString();
            MDN.Add(var);
            con.Close();
        }
        return MDN;
    }

    protected void grdSupplierList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSupplierList.PageIndex = e.NewPageIndex;
        DataBindsupplier();
    }
    protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtsearch.Text = string.Empty;
        DataBindsupplier();
    }
}