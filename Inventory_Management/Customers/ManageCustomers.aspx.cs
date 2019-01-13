using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class ManageCustomers : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CustomersListDataBind();
        }

    }
    public void CustomersListDataBind()
    {
        try
        {
            //SqlConnection cn = new SqlConnection(ConnectionString);
            //SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Customers", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cn.Open();

            //grdVcustomersList.DataSource = cmd.ExecuteReader();
            //grdVcustomersList.EmptyDataText =  "No Records Found";
            //grdVcustomersList.DataBind();
            //cn.Close();
           // lbtotalRow.Text = "Total : " + Convert.ToString(grdVcustomersList.Rows.Count) + " Records found" + "<br />";

            grdVcustomersList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Customers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            grdVcustomersList.DataSource = ds;
            grdVcustomersList.EmptyDataText = "No Records Found";
            grdVcustomersList.DataBind();
           // lbtotalRow.Text = "Total : " + Convert.ToString(grdVcustomersList.Rows.Count) + " Records found" + "<br />";
            con.Close();


        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    // ///////     Details sales to customer  transactions databind
    public void DataBindcustomersales(string custid)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_SoldListToCustomer", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@custid", custid);
            cn.Open();

            grdviewSoldhistory.DataSource = cmd.ExecuteReader();
            grdviewSoldhistory.EmptyDataText = "No Records Found";
            grdviewSoldhistory.DataBind();
            cn.Close();
            // lbtotalRow.Text = "Total : " + Convert.ToString(grdItemList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }

    // Details View
    protected void Linkdtview_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblcustid.Text = gvrow.Cells[1].Text;
        lblcusname.Text = gvrow.Cells[2].Text;
        lblcname.Text = gvrow.Cells[2].Text;
        lblEmail.Text = gvrow.Cells[4].Text;
        lblphone.Text = gvrow.Cells[3].Text;
        DataBindcustomersales(lblcustid.Text);

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<script type='text/javascript'>");
        sb.Append("$('#myModal').modal('show');");
        sb.Append(@"</script>");
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DetailModalScript", sb.ToString(), false);
       // this.MpedtviewShow.Show();
    }


    //Update attribute
    protected void LinkEdit_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        btnSave.Text = "Save";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblID.Text = gvrow.Cells[1].Text;
        txtCustName.Text = gvrow.Cells[2].Text;
        txtContact.Text = gvrow.Cells[3].Text;
        txtEmail.Text = gvrow.Cells[4].Text;
        txtaddress.Text = gvrow.Cells[5].Text;
        txtCustType.Text = gvrow.Cells[6].Text;
        txtDiscount.Text = gvrow.Cells[7].Text;
        txtCustID.Text = gvrow.Cells[8].Text;
        txtCustPassword.Text = gvrow.Cells[9].Text;
        if (gvrow.Cells[10].Text == "Inactive")
        {
            btnSave.Text = "Save & Active";
        }
        this.MpeEditShow.Show();
    }

    //ban / Inactive user
    protected void LinkbanCustomer_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        LinkButton Linkdetails = sender as LinkButton;
        GridViewRow gvrow = (GridViewRow)Linkdetails.NamingContainer;

        lblInactiveID.Text = gvrow.Cells[1].Text;
        lblInactiveCustName.Text = gvrow.Cells[2].Text;
        this.ModalPopupInactive.Show();
    }

    protected void btnSave_Click(object sender, EventArgs e) //Update Customer
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Update_Customer", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id",lblID.Text );
            cmd.Parameters.AddWithValue("@CustName",        txtCustName.Text);
            cmd.Parameters.AddWithValue("@CustPhone",       txtContact.Text);
            cmd.Parameters.AddWithValue("@CustEmail",       txtEmail.Text);
            cmd.Parameters.AddWithValue("@CustAddress",     txtaddress.Text);
            cmd.Parameters.AddWithValue("@CustType",        txtCustType.Text);
            cmd.Parameters.AddWithValue("@DiscountRate",    txtDiscount.Text);
            cmd.Parameters.AddWithValue("@custpaword",      txtCustPassword.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby",    Request.Cookies["InventMgtCookies"]["UserID"].ToString());

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Updated";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Updated')", true);
            CustomersListDataBind();
            this.MpeEditShow.Show();
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
            this.MpeEditShow.Show();
        }

    }


    protected void btnInactive_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_Inactive_Customer", cn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", lblInactiveID.Text);
            cmd.Parameters.AddWithValue("@Lastupdateby", Request.Cookies["InventMgtCookies"]["UserID"].ToString());

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

            lblmsg.Visible = true;
            lblmsg.Text = "Successfully Inactivated";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully Inactivated')", true);
            CustomersListDataBind();
        }
        catch (Exception ex)
        {
            lblmsg.Visible = true;
            lblmsg.Text = ex.Message;
            this.ModalPopupInactive.Show();
        }
    }
    protected void grdVcustomersList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdVcustomersList.PageIndex = e.NewPageIndex;
        CustomersListDataBind();
    }

    protected void ddlpagesize_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtsearch.Text = string.Empty;
        CustomersListDataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        List<CustomerList> CustomerList = new List<CustomerList>();
        try
        {
            grdVcustomersList.PageSize = Convert.ToInt32(ddlpagesize.SelectedValue);
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_POS_DataBind_Customers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            var dt = new DataTable();
            dt.Load(rd);
            List<DataRow> dr = dt.AsEnumerable().ToList();
            for (int i = 0; i < dr.Count; i++)
            {
                CustomerList list = new CustomerList();

                list.ID = Convert.ToInt16(dr[i].ItemArray[0]);
                list.Name = dr[i].ItemArray[1].ToString();
                list.Contact = dr[i].ItemArray[2].ToString();
                list.Email = dr[i].ItemArray[3].ToString();
                list.Address = dr[i].ItemArray[4].ToString();
                list.CustomerType = dr[i].ItemArray[5].ToString();
                list.DsicountPercent = dr[i].ItemArray[6].ToString();
                list.CustID = dr[i].ItemArray[7].ToString();
                list.password = dr[i].ItemArray[8].ToString();
                list.Status = dr[i].ItemArray[9].ToString();

                CustomerList.Add(list);
            }
            con.Close();


        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }
}