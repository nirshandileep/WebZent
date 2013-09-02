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
using LankaTiles.POManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class ReportPurchaseOrders : System.Web.UI.Page
{
    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                POSearchParameters searchPO = new POSearchParameters();
                searchPO.FromDateRep = null;
                searchPO.ToDateRep = null;

                Session["PurchaseItemReport"] = searchPO.SearchPOItemsForReporting();
                dxgvPurchaseByItems.DataSource = (DataSet)Session["PurchaseItemReport"];
                dxgvPurchaseByItems.DataBind();
            }

            if (IsCallback)
            {
                dxgvPurchaseByItems.DataSource = (DataSet)Session["PurchaseItemReport"];
                dxgvPurchaseByItems.DataBind();
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

    #endregion

    #region Click events
    
    /// <summary>
    /// Date range search button click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            POSearchParameters searchPO = new POSearchParameters();
            searchPO.FromDateRep = dtpFromDate.Date;
            searchPO.ToDateRep = dtpToDate.Date;

            if (searchPO.ToDateRep < searchPO.FromDateRep)
            {
                return;
            }

            Session["PurchaseItemReport"] = null;
            Session["PurchaseItemReport"] = searchPO.SearchPOItemsForReporting();

            dxgvPurchaseByItems.DataSource = (DataSet)Session["PurchaseItemReport"];
            dxgvPurchaseByItems.DataBind();
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

    /// <summary>
    /// Export data in the grid to an excel sheet as a report
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (Session["PurchaseItemReport"] != null)
        {
            dxgvPurchaseByItems.DataSource = (DataSet)Session["PurchaseItemReport"];
            dxgvPurchaseByItems.DataBind();
        }
        this.gveSalesReport.WriteXlsxToResponse();
    }

    #endregion
}
