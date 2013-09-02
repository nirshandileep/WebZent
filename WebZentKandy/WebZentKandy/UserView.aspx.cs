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
using LankaTiles.UserManagement;

public partial class UserView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0 && Request.QueryString[0].ToString() == DateTime.Now.ToString("HH"))
        {
            UsersDAO usersdao = new UsersDAO();
            gvUsers.DataSource = usersdao.GetAllUsers();
            gvUsers.DataBind();
        }
        else
        {
            Response.Redirect("~/UserLogin.aspx", false);
        }
    }
}
