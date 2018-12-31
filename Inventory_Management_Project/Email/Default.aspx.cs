using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Email_Default : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            indexDataBind();
            
        }
    }

    public void indexDataBind()  //Message Subject
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_mail_DataBind_index", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            grdsubject.DataSource = cmd.ExecuteReader();
            grdsubject.EmptyDataText = "No Records Found";
            grdsubject.DataBind();
            cn.Close();
            ///Show total number of Gridview  Row 
            //lbtotalRow.Text = "Total : " + Convert.ToString(grdsubject.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }


    protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdsubject, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }
    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int index = grdsubject.SelectedRow.RowIndex;
        int  ID = Convert.ToInt32(grdsubject.SelectedRow.Cells[0].Text);

       // lblmailbody.Text = subject;

        mailbodyDataBind(ID);
      //  string message = "Row Index: " + index + "\\nName: " + subject + "\\nCountry: " + subject;
       // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
    }


    public void mailbodyDataBind(int ID)  //Full message Body
    {
        try
        {         

            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_mail_DataBind_mailbody", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", ID);
            cn.Open();           

           //////Using Sql DataAdapter   ........................
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);


            ////// Or we can use Sql DataReader  
            //SqlDataReader dr = cmd.ExecuteReader();
            //DataTable dt = new DataTable();
            //dt.Load(dr);

            /////////Data Load on Gridview
            //grdmailbody.DataSource = cmd.ExecuteReader();
            //grdmailbody.EmptyDataText = "No Records Found";
            //grdmailbody.DataBind();

            lblmailbody.Text = dt.Rows[0].ItemArray[0].ToString();
            lblSubject.Text = dt.Rows[0].ItemArray[1].ToString();
            lblmailFrom.Text = dt.Rows[0].ItemArray[2].ToString();
            lblmailTo.Text = "To "+ dt.Rows[0].ItemArray[3].ToString();
            // cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch
        {
           // lblmailbody.Text = "No Records Found";
        }
    }

    protected void lnkDelete_Click(object sender, EventArgs e) // Delete Email or Move to  Trash
    {

    }
    protected void lnkReply_Click(object sender, EventArgs e)
    {
        Session["mailfrom"] = lblmailFrom.Text;
        Response.Redirect("CreateNewMail.aspx");
    }
}