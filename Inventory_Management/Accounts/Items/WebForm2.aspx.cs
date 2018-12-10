using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Accounts_Items_WebForm2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DirectoryInfo src = new DirectoryInfo(@"C:\Rainforestfusionpos\Inventory_Management\Accounts\Items\ItemsPhoto");
        DirectoryInfo dest = new DirectoryInfo(@"C:\Rainforestwebsite\StripeAspNet\Accounts\Items\ItemsPhoto");
        //CopyDirectory(src, dest);
        Response.Redirect("~/Accounts/Items/AddItem.aspx");
    }

    //static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
    //{
    //    if (!destination.Exists)
    //    {
    //        destination.Create();
    //    }

    //    // Copy all files.
    //    FileInfo[] files = source.GetFiles();
    //    foreach (FileInfo file in files)
    //    {
    //        file.CopyTo(Path.Combine(destination.FullName,
    //            file.Name),true);
    //    }

    //    // Process subdirectories.
    //    DirectoryInfo[] dirs = source.GetDirectories();
    //    foreach (DirectoryInfo dir in dirs)
    //    {
    //        // Get destination directory.
    //        string destinationDir = Path.Combine(destination.FullName, dir.Name);

    //        // Call CopyDirectory() recursively.
    //        CopyDirectory(dir, new DirectoryInfo(destinationDir));
    //    }
    //}
}