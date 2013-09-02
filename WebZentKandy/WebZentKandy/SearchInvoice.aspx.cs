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
using LankaTiles.InvoiceManagement;
using LankaTiles.Exception;
using LankaTiles.Common;

public partial class SearchInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                DataSet dsSelectAllInv = new InvoiceDAO().GetInvoiceByRole(Master.LoggedUser.UserRoleID);
                dxgvInvoice.DataSource = dsSelectAllInv;
                dxgvInvoice.DataBind();
                Session["SearchInvoice"] = dsSelectAllInv;
            }
            if (IsCallback)
            {
                if (Session["SearchInvoice"] != null)
                {
                    dxgvInvoice.DataSource = (DataSet)Session["SearchInvoice"];
                    dxgvInvoice.DataBind();
                }
                else
                {
                    dxgvInvoice.DataSource = new InvoiceDAO().GetAllInvoices();
                    dxgvInvoice.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
}
