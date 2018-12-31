using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;

public partial class Items_UploadItems : System.Web.UI.Page
{
    string ConnectionString = ConfigurationManager.ConnectionStrings["PointofSaleConstr"].ConnectionString;      
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }


    

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string line = null;
            int i = 0;
            if (FileUpload1.HasFile)
            {
                string csvPath = (Server.MapPath("~/CSVFile/" + Path.GetFileName(FileUpload1.PostedFile.FileName)));
                FileUpload1.SaveAs(csvPath);
                //string csvPath = Server.MapPath("~/CSVFile/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                //FileUpload1.SaveAs(csvPath);

                Label1.ForeColor = System.Drawing.Color.Green;


                using (StreamReader sr = File.OpenText(@csvPath))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length > 0)
                        {
                            if (i == 0)
                            {
                                foreach (var item in data)
                                {
                                    dt.Columns.Add(new DataColumn());
                                }
                                i++;
                            }
                            DataRow row = dt.NewRow();
                            row.ItemArray = data;
                            dt.Rows.Add(row);
                        }
                    }
                }

                SqlConnection cn = new SqlConnection(ConnectionString);
                cn.Open();

                using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                {
                    //copy.ColumnMappings.Add(0, 0);
                    copy.ColumnMappings.Add(1, 1);
                    copy.ColumnMappings.Add(2, 2);
                    copy.ColumnMappings.Add(3, 3);
                    copy.ColumnMappings.Add(4, 4);
                    copy.ColumnMappings.Add(5, 5);
                    copy.ColumnMappings.Add(6, 6);
                    copy.ColumnMappings.Add(7, 7);
                    copy.ColumnMappings.Add(8, 8);
                    copy.ColumnMappings.Add(9, 9);
                    copy.ColumnMappings.Add(10, 10);
                    copy.ColumnMappings.Add(11, 11);
                    copy.ColumnMappings.Add(12, 12);
                    copy.ColumnMappings.Add(13, 13);
                    copy.ColumnMappings.Add(14, 14);
                    copy.ColumnMappings.Add(15, 15);
                    copy.ColumnMappings.Add(16, 16);
                    copy.ColumnMappings.Add(17, 17);
                    copy.ColumnMappings.Add(18, 18);
                    copy.ColumnMappings.Add(19, 19);
                    copy.ColumnMappings.Add(20, 20);
                    copy.ColumnMappings.Add(21, 21);

                    copy.DestinationTableName = "tbl_Item";
                    copy.WriteToServer(dt);
                }
            }
            else
            {
                Label1.Text = "Upload File ";
            }

        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
 
    }


    public static DataTable ConvertCSVtoDataTable(string strFilePath)
    {
        StreamReader sr = new StreamReader(strFilePath);
        string[] headers = sr.ReadLine().Split(',');
        DataTable dt = new DataTable();
        foreach (string header in headers)
        {
            dt.Columns.Add(header);
        }
        while (!sr.EndOfStream)
        {
            string[] rows = System.Text.RegularExpressions.Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            DataRow dr = dt.NewRow();
            for (int i = 0; i < headers.Length; i++)
            {
                dr[i] = rows[i];
            }
            dt.Rows.Add(dr);
        }
        return dt;
    } 


 
}