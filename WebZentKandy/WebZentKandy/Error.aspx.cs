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
using LankaTiles.Common;
using LankaTiles.UserManagement;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Error"] != null)
        {
            lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
        else
        {
            SetErrorMessage();
        }
    }

    #region Set Error Message

    private void SetErrorMessage()
    {
        lblError.Text = string.Format(String.Format("{0} {1}", Constant.Error_System, String.Format(Constant.Error_Code, Request.QueryString["LogId"].ToString())));

    }

    #endregion
}
