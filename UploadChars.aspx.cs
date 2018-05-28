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
using System.Text;
using System.IO;
using ParseCharacters;
using WoWDetails;

public partial class UploadChars : System.Web.UI.Page
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

        if (IsPostBack)
        {
            if (CharUpload.HasFile)
            {
                String InputFileName = CharUpload.FileName;
                if (InputFileName != "CharacterProfiler.lua")
                {
                    LabelStatus.Text = "Sorry, you need to upload the CharacterProfiler.lua file";
                }
                else
                {
                    HttpPostedFile InputFile = CharUpload.PostedFile;
                    Stream theStream = InputFile.InputStream;

                    // Call the CharacterParser class to read and parse out the Proudmoore
                    // chars in the file.  Once done, save the object to the Cache.

                    CharacterParser myChars = new CharacterParser();
                    List<WoWCharacter> proudmooreChars = new List<WoWCharacter>();
                    proudmooreChars = myChars.DoParse(theStream);
                    Cache.Insert("uploadedChars", proudmooreChars);

                    // Count how many characters are in the file - reject if there arent any

                    int CharCount = 0;
                    foreach (WoWCharacter TempChar in proudmooreChars)
                    {
                        CharCount++;
                    }
                    if (CharCount > 0)
                    {
                        Response.Redirect("SelectChars.aspx");
                    }
                    else
                    {
                        LabelStatus.Text = "Sorry - that file doesn't have any characters from Proudmoore in it";
                    }
                }
            }
            else
            {
                LabelStatus.Text = "Sorry - you need to select a file to upload";
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Roster.aspx");
    }
    protected void btnViewChars_Click(object sender, EventArgs e)
    {
        Response.Redirect("Roster.aspx");
    }
    protected void btnSelectChars_Click(object sender, EventArgs e)
    {

    }
}
