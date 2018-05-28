using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WoWDetails;

public partial class SelectChars : System.Web.UI.Page
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

        List<WoWCharacter> proudmooreChars = new List<WoWCharacter>();
        proudmooreChars = (List<WoWCharacter>)Cache.Get("uploadedChars");

        if (!IsPostBack)
        {
            // to do - allow selecting of bank/no bank etc
            foreach (WoWCharacter currChar in proudmooreChars)
            {
                CbxCharacters.Items.Add(currChar.Name);
            }
        }
    }
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Roster.aspx");
    }
    protected void ButtonUpload_Click(object sender, EventArgs e)
    {
        List<WoWCharacter> proudmooreChars = new List<WoWCharacter>();
        proudmooreChars = (List<WoWCharacter>)Cache.Get("uploadedChars");

        if (IsPostBack)
        {
            // Sort the character list into Alphabetic order, coz thats nice.
            proudmooreChars.Sort();

            // Work through the list of items and tag the selected
            // ones for uploading to the database

            List<string> SelectedChars = new List<string>();

            foreach (ListItem li in CbxCharacters.Items)
            {
                if (li.Selected)
                {
                    SelectedChars.Add(li.ToString());
                }
            }

            if (SelectedChars.Count < 1)
            {
                LabelResult.Text = "You need to select at least one character to upload, or Cancel to get out.";
            }
            else
            {
                foreach (String selectedChar in SelectedChars)
                {
                    foreach (WoWCharacter liChar in proudmooreChars)
                    {
                        if (liChar.Name == selectedChar)
                        {
                            liChar.UpdateToDB();
                        }
                    }
                }
                LabelResult.Text = "Characters uploaded successfully!";
            }
        }
        else
        {
            // to do - allow selecting of bank/no bank etc
            foreach (WoWCharacter currChar in proudmooreChars)
            {
                CbxCharacters.Items.Add(currChar.Name);
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Roster.aspx");
    }
}
