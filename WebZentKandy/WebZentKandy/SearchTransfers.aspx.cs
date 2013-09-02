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
using LankaTiles.ItemsManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class SearchTransfers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                DataSet dsSearchTrasnfers;
                dsSearchTrasnfers = new ItemTransferDAO().GetAllItemTransfer();
                Session["SearchTransfers"] = dsSearchTrasnfers;
                dxgvTransferList.DataSource = dsSearchTrasnfers;
                dxgvTransferList.DataBind();
            }
            if (IsCallback)
            {
                if (Session["SearchTransfers"] != null)
                {
                    dxgvTransferList.DataSource = (DataSet)Session["SearchTransfers"];
                    dxgvTransferList.DataBind();
                }
                else
                {
                    dxgvTransferList.DataSource = new ItemTransferDAO().GetAllItemTransfer();
                    dxgvTransferList.DataBind();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void btnExportToReport_Click(object sender, EventArgs e)
    {
        if (Session["SearchTransfers"] != null)
        {
            dxgvTransferList.DataSource = (DataSet)Session["SearchTransfers"];
            dxgvTransferList.DataBind();
        }
        this.dxgveTransferReport.WriteXlsToResponse();
    }
}
