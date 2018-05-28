<%@ Application Language="C#" %>
<%@ import Namespace="System.Diagnostics" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs
        Exception objErr = Server.GetLastError().GetBaseException();
        string err = "Ohoh!  Theres been an error!<br>Please tell Daedalus that " + Request.Url.ToString() +
                "<br>Error Message:" + objErr.Message.ToString() +
                "<br>Stack Trace:" + objErr.StackTrace.ToString() +
                "<br>Source: " + objErr.Source + 
                "<br>Inner Exception: " + objErr.InnerException +
                "<br>Full error: " + objErr.ToString();
        Response.Write(err);
        Server.ClearError();
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
