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
using WoWDetails;

public partial class Character : System.Web.UI.Page
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
        }

        // Retrieve the character based on the passed in char variable

        string currentChar;
        currentChar = Request.QueryString["char"];
        WoWCharacter viewChar = new WoWCharacter(currentChar);

        // Set up the detail URLs based on character name.
        Quests.NavigateUrl = "Character.aspx?char=" + currentChar + "&detail=quests";
        Recipes.NavigateUrl = "Character.aspx?char=" + currentChar + "&detail=recipes";

        CharHeader.Text = currentChar;

    }
    public void CreateQuestList(object sender, ObjectDataSourceEventArgs e)
    {
        if (Request.QueryString["detail"] == "quests")
        {
            string currentChar;
            currentChar = Request.QueryString["char"];
            WoWCharacter viewChar = new WoWCharacter(currentChar);
            viewChar.ReadFromDB();
            e.ObjectInstance = viewChar;
        }
    }
    public void CreateRecipeList(object sender, ObjectDataSourceEventArgs e)
    {
        if (Request.QueryString["detail"] == "recipes")
        {
            string currentChar;
            currentChar = Request.QueryString["char"];
            WoWCharacter viewChar = new WoWCharacter(currentChar);
            viewChar.ReadFromDB();
            e.ObjectInstance = viewChar;
        }
    }
}