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
using LankaTiles.VoucherManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class SearchReceivableVouchers : System.Web.UI.Page
{
    private DataSet DsReceivableVouchers
    {
        get 
        {
            if (Session["DsReceivableVouchers"] == null)
            {
                DataSet dsAllVouchers = null;
                dsAllVouchers = new VoucherRecievableDAO().SelectAllVouchers();
                Session["DsReceivableVouchers"] = dsAllVouchers;
                return dsAllVouchers;
            }
            else
            {
                return (DataSet)Session["DsReceivableVouchers"];
            }
        }
        set 
        {
            Session["DsReceivableVouchers"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                dxgvReceivedVouchers.DataSource = DsReceivableVouchers;
                dxgvReceivedVouchers.DataBind();
            }

            if (IsCallback)
            {
                dxgvReceivedVouchers.DataSource = DsReceivableVouchers;
                dxgvReceivedVouchers.DataBind();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (DsReceivableVouchers != null)
        {
            dxgvReceivedVouchers.DataSource = DsReceivableVouchers;
            dxgvReceivedVouchers.DataBind();
        }
        this.gveVoucherReport.WriteXlsToResponse();
    }
}
