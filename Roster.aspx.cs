using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WoWDetails;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies.Get("SAMembers");
        string username;

        if (cookie == null)
        {
            //Response.Redirect("~/NotLoggedIn.aspx");
        }
        else
        {
            username = CheckLoggedIn.Check(cookie.Value);
        }
    }
}
