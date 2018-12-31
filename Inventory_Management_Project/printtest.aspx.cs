using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Threading;
public partial class printtest : System.Web.UI.Page
{
    private Font printFont;
    private StreamReader streamToPrint;
    static string filePath;
    private Bitmap m_Bitmap;
    private string m_Url;
    private string m_FileName = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        filePath = "c:\\test.txt";
        
        
        //string sampleName = Environment.GetCommandLineArgs()[0];
        //if (args.Length != 1)
        //{
        //    Console.WriteLine("Usage: " + sampleName + " <file path>");
        //    return;
        //}
        //filePath = args[0];
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('printtest.aspx','PrintMe','height=300px,width=300px,scrollbars=1');</script>");
    }
    private void pd_PrintPage(object sender, PrintPageEventArgs ev)
    {
        float linesPerPage = 0;
        float yPos = 0;
        int count = 0;
        float leftMargin = ev.MarginBounds.Left;
        float topMargin = ev.MarginBounds.Top;
        String line = null;

        // Calculate the number of lines per page.
        linesPerPage = ev.MarginBounds.Height /
           printFont.GetHeight(ev.Graphics);

        // Iterate over the file, printing each line.
        while (count < linesPerPage &&
           ((line = streamToPrint.ReadLine()) != null))
        {
            yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
            ev.Graphics.DrawString(line, printFont, Brushes.Black,
               leftMargin, yPos, new StringFormat());
            count++;
        }

        // If more lines exist, print another page.
        if (line != null)
            ev.HasMorePages = true;
        else
            ev.HasMorePages = false;
    }

    // Print the file.
    public void Printing()
    {
        try
        {
            streamToPrint = new StreamReader(filePath);
            try
            {
                printFont = new Font("Arial", 10);
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                // Print the document.
                pd.Print();
            }
            finally
            {
                streamToPrint.Close();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }

    // This is the main entry point for the application.
    //public static void Main(string[] args)
    //{
    //    string sampleName = Environment.GetCommandLineArgs()[0];
    //    if (args.Length != 1)
    //    {
    //        Console.WriteLine("Usage: " + sampleName + " <file path>");
    //        return;
    //    }
    //    filePath = args[0];
    //    new PrintingExample();
    //}
    protected void Button2_Click(object sender, EventArgs e)
    {
        Printing();
    }
}