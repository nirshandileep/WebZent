using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LankaTiles.InvoiceManagement;
using LankaTiles.Common;
using LankaTiles.Exception;
using DevExpress.Web.ASPxGridView;
using DevExpress.Data;
using LankaTiles.ChequeManagement;

public partial class ReportCheques : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();
                dtpFromDate.Date = DateTime.Today.Date;
                dtpToDate.Date = DateTime.Today.Date;
                FillReport();
            }
            if (IsCallback)
            {
                this.FillSession();
                FillReport();
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

    private void FillReport()
    {
        try
        {

            if (Session["PDChequeReport"] == null)
            {
                this.FillSession();
            }

            dxgvInvoice.DataSource = (DataSet)Session["PDChequeReport"];
            dxgvInvoice.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void FillSession()
    {
        try
        {
            Session["PDChequeReport"] = new ChequesDAO().GetAllChequesByDateRangeForReporting(dtpFromDate.Date, dtpToDate.Date);
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        if (Session["PDChequeReport"] != null)
        {
            dxgvInvoice.DataSource = (DataSet)Session["PDChequeReport"];
            dxgvInvoice.DataBind();
        }
        this.gveInvoice.WriteXlsxToResponse();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.FillSession();
            this.FillReport();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSearch_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }
}
