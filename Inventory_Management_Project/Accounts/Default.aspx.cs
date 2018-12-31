using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;

public partial class _Default : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.Cookies["InventMgtCookies"] != null)
            {
                //For Multiple values in single cookie
                string cookiesValues;
                cookiesValues = Request.Cookies["InventMgtCookies"]["UserID"];
                DataBind_DashBoardSummary();
                //TaskListDataBind(Request.Cookies["InventMgtCookies"]["UserID"].ToString());
                 
            }
            else
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }

    }

    //public void TaskListDataBind(string UserID)
    //{
    //    try
    //    {
    //        SqlConnection cn = new SqlConnection(ConnectionString);
    //        SqlCommand cmd = new SqlCommand("SP_POS_DataBind_TaskList", cn);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cn.Open();
    //        cmd.Parameters.AddWithValue("@userID", UserID);

    //        grdViewTasklist.DataSource = cmd.ExecuteReader();
    //        grdViewTasklist.EmptyDataText = "No Records Found";
    //        grdViewTasklist.DataBind();
    //        cn.Close();
    //        // lbtotalRow.Text = "Total : " + Convert.ToString(grdViewReport.Rows.Count) + " Records found" + "<br />";

    //    }
    //    catch
    //    {
    //        //lbtotalRow.Text = "No Records Found";
    //    }
    //}

    public void DataBind_DashBoardSummary()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("SP_INV_DataBind_DashBoardSummary", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();
            //cmd.Parameters.AddWithValue("@userID", UserID);

            grdViewInvoSummary.DataSource = cmd.ExecuteReader();
            grdViewInvoSummary.EmptyDataText = "No Records Found";
            grdViewInvoSummary.DataBind();
            cn.Close();
            // lbtotalRow.Text = "Total : " + Convert.ToString(grdViewReport.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            //lbtotalRow.Text = "No Records Found";
        }
    }


    [WebMethod]
    public static List<SalesDetails> GetChartData()
    {
        String ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;      
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(ConnectionString))
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(" SELECT top 8  CONVERT(VARCHAR(10), Logdate,121 )   as [DATE]  , SUM([totalpayable]) as [Total] " + 
			    " FROM tbl_SalesPayment " +
              //  " WHERE CONVERT(VARCHAR(11),Logdate,106)  between   GETDATE() - 3330    and  GETDATE() " +
                " group by  CONVERT(VARCHAR(10), Logdate,121 ) order by  CONVERT(VARCHAR(10), Logdate,121 )  desc  ", con); 
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        List<SalesDetails> dataList = new List<SalesDetails>();
        foreach (DataRow dtrow in dt.Rows)
        {
            SalesDetails details = new SalesDetails();
            details.DATE = dtrow[0].ToString();
            details.Total = Convert.ToInt32(dtrow[1]);                       
            dataList.Add(details);
        }
        return dataList;
    }

    public class SalesDetails
    {
        public string DATE { get; set; }
        public int Total { get; set; }
        // public string Total11 { get; set; }        
    } 
}