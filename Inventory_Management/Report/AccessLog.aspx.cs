using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data; 

public partial class Report_AccessLog : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Title = "AccessLog_Reports_" + DateTime.Now.ToString("MMM_dd_yyyy_HHmmss"); 
            AccessLogDataBind();        
        }
    }

    // ///////  Item list Databind
    public void AccessLogDataBind()
    {
        try
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("[SP_INV_DataBind_AccessLog]", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cn.Open();

            grdAccessDataList.DataSource = cmd.ExecuteReader();
            grdAccessDataList.EmptyDataText = "No Records Found";
            grdAccessDataList.DataBind();
            cn.Close();
            lbtotalRow.Text = "Total : " + Convert.ToString(grdAccessDataList.Rows.Count) + " Records found" + "<br />";

        }
        catch
        {
            lbtotalRow.Text = "No Records Found";
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        AccessLogDataBind();
    }
}