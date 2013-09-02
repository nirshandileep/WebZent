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
using LankaTiles.Exception;
using LankaTiles.InvoiceManagement;
using DevExpress.Web.ASPxGridView;

public partial class ReportSalesByItem : System.Web.UI.Page
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
                if (Session["SalesItemReportByRep"] != null)
                {
                    dxgvSalesByItems.DataSource = (DataSet)Session["SalesItemReportByRep"];
                    dxgvSalesByItems.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            InvoiceSearch searchInvoice = new InvoiceSearch();
            searchInvoice.FromDateRep = dtpFromDate.Date;
            searchInvoice.ToDateRep = dtpToDate.Date;

            if (searchInvoice.ToDateRep < searchInvoice.FromDateRep)
            {
                return;
            }

            Session["SalesItemReportByRep"] = null;
            Session["SalesItemReportByRep"] = searchInvoice.SearchInvoicedItemsRepWise();

            dxgvSalesByItems.DataSource = (DataSet)Session["SalesItemReportByRep"];
            dxgvSalesByItems.DataBind();

            dxgvSalesByItems.ExpandAll();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSearch_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (Session["SalesItemReportByRep"] != null)
        {
            dxgvSalesByItems.DataSource = (DataSet)Session["SalesItemReportByRep"];
            dxgvSalesByItems.DataBind();
        }
        this.gveSalesReport.WriteXlsxToResponse();
    }

    protected void dxgvReportsSalesDetails_BeforePerformDataSelect(object sender, EventArgs e)
    {
        try
        {
            if ((sender as ASPxGridView).GetMasterRowKeyValue() == null)
            {
                return;
            }
            Int64 ItemId = Int32.Parse((sender as ASPxGridView).GetMasterRowKeyValue().ToString());//ItemId
            DataSet ds = new InvoiceDAO().GetItemWiseInvoicesForReporting(ItemId,dtpFromDate.Date,dtpToDate.Date);
            (sender as ASPxGridView).DataSource = ds;

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvReportsSalesDetails_BeforePerformDataSelect(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }
}
