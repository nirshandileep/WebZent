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
using LankaTiles.VoucherManagement;

public partial class ReportSales : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
            }
            if (IsCallback)
            {
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
            dxgvExpenseReport.DataSource = (DataSet)Session["DayBookReport"];
            dxgvExpenseReport.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        if (Session["DayBookReport"] != null)
        {
            dxgvExpenseReport.DataSource = (DataSet)Session["DayBookReport"];
            dxgvExpenseReport.DataBind();
        }
        this.gveExpenceReport.WriteXlsxToResponse();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            VoucherSearch search = new VoucherSearch();
            if (dtpFromDate.Text == string.Empty)
            {
                search.FromDate = null;
            }
            else
            {
                search.FromDate = dtpFromDate.Date;
            }
            
            if (dtpToDate.Text == string.Empty)
            {
                search.ToDate = null;
            }
            else
            {
                search.ToDate = dtpToDate.Date;
            }

            DataSet ds = new VoucherDAO().GetDaybookDetailsForReporting(search);
            Session["DayBookReport"] = ds;
            dxgvExpenseReport.DataSource = ds;
            dxgvExpenseReport.DataBind();
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
