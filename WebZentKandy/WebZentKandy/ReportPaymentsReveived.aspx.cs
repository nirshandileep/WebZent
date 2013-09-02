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
using LankaTiles.SupplierManagement;
using LankaTiles.POManagement;
using DevExpress.Web.ASPxGridView;
using LankaTiles.VoucherManagement;

public partial class ReportPurchaseOrderMain : System.Web.UI.Page
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
                dxgvPaymentsReceived.DataSource = (DataSet)Session["PaymentsRecieved"];
                dxgvPaymentsReceived.DataBind();
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
            VoucherRecievableSearch voucherSearch = new VoucherRecievableSearch();
            voucherSearch.FromDate = dtpFromDate.Date;
            voucherSearch.ToDate = dtpToDate.Date;
            DataSet dsPOs = (new VoucherRecievableDAO()).GetPaymentsReveivedForReporting(voucherSearch);
            Session["PaymentsRecieved"] = dsPOs;

            if (dsPOs != null && dsPOs.Tables[0].Rows.Count > 0)
            {
                dxgvPaymentsReceived.DataSource = dsPOs;
                dxgvPaymentsReceived.DataBind();
            }
            else
            {
                dxgvPaymentsReceived.DataSource = dsPOs;
                dxgvPaymentsReceived.DataBind();
            }
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
        if (Session["PaymentsRecieved"] != null)
        {
            dxgvPaymentsReceived.DataSource = (DataSet)Session["PaymentsRecieved"];
            dxgvPaymentsReceived.DataBind();
        }
        this.vgePaymentsReceivable.WriteXlsxToResponse();
    }

    protected void dxgvPODetails_BeforePerformDataSelect(object sender, EventArgs e)
    {
        try
        {
            if ((sender as ASPxGridView).GetMasterRowKeyValue() == null)
            {
                return;
            }
            Int32 POId = Int32.Parse((sender as ASPxGridView).GetMasterRowKeyValue().ToString());
            PO tempPO = new PO();
            tempPO.POId = POId;
            DataSet ds = new PODAO().GetPOItemsByPOID(tempPO);
            (sender as ASPxGridView).DataSource = ds;

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvPODetails_BeforePerformDataSelect(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }
}