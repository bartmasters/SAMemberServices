using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class bart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies.Get("SAMembers");
        string username;

        if (cookie == null)
        {
            Response.Redirect("~/NotLoggedIn.aspx");
        }
        else
        {
            username = CheckLoggedIn.Check(cookie.Value);
            Response.Write("Welcome " + username);
        }
    }
}
