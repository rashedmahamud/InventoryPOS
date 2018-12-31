using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Email_CreateNewMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtMailto.Text = Session["mailfrom"].ToString();

    }
}