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

public partial class ReportSales : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                dtpFromDate.Date = DateTime.Today.Date;
                dtpToDate.Date = DateTime.Today.Date;
                FillReport();
                this.Authorise();
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

    private void Authorise()
    {
        try
        {
            if (Master.LoggedUser.UserRoleID == (Int16)LankaTiles.Common.Structures.UserRoles.Manager)
            {
                btnExtraColumns.Visible = false;
                dtpFromDate.MinDate = DateTime.Parse(Constant.SalesReport_Manager_MinBackDate_Date.Trim());

                //
                //Grid columns hide
                //
                dxgvInvoice.Columns["BranchCode"].Visible = false;
                dxgvInvoice.Columns["UserName"].Visible = false;
                dxgvInvoice.Columns["ChequeNumber"].Visible = false;
                dxgvInvoice.Columns["ChequeDate"].Visible = false;
                dxgvInvoice.Columns["DueAmount"].Visible = false;
                dxgvInvoice.Columns["DaysCount"].Visible = false;
                dxgvInvoice.Columns["TotalProfit"].Visible = false;
                dxgvInvoice.Columns["TotalCostOfInvoice"].Visible = false;
                dxgvInvoice.Columns["Remarks"].Visible = false;
                dxgvInvoice.Columns["CardComisionRate"].Visible = false;
                dxgvInvoice.Columns["BankComision"].Visible = false;
                dxgvInvoice.Columns["Banked_Ammount"].Visible = false;
                dxgvInvoice.Columns["CardType"].Visible = false;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "private void Authorise()");
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

            if (Session["InvoiceReport"] == null)
            {
                this.FillSession();
            }

            dxgvInvoice.DataSource = (DataSet)Session["InvoiceReport"];
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
            InvoiceSearch search = new InvoiceSearch();

            search.FromDateRep = dtpFromDate.Date;
            search.ToDateRep = dtpToDate.Date;

            if ((int)rblReportOption.SelectedItem.Value == 0)
            {
                Session["InvoiceReport"] = new InvoiceDAO().GetAllInvoicesAndDetailsForReporting(search);
            }
            else if ((int)rblReportOption.SelectedItem.Value == 1)
            {
                //load all active only
                Session["InvoiceReport"] = new InvoiceDAO().GetAllActiveInvoicesForReporting(search);
            }
            else if ((int)rblReportOption.SelectedItem.Value == 2)
            {
                //load all cancelled only
                Session["InvoiceReport"] = new InvoiceDAO().GetAllCancelledInvoicesForReporting(search);
            }
            else if ((int)rblReportOption.SelectedItem.Value == 3)
            {
                //load all pending payments only
                Session["InvoiceReport"] = new InvoiceDAO().GetAllPaymentDueInvoicesForReporting(search);
            }

            this.FilterDataByUserRole();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void FilterDataByUserRole()
    {
        try
        {
            if (Master.LoggedUser.UserRoleID == (int)Structures.UserRoles.Manager)
            {
                if (Session["InvoiceReport"] != null)
                {
                    DataSet dsReport = (DataSet)Session["InvoiceReport"];
                    DataView dvReport = dsReport.Tables[0].DefaultView;
                    dvReport.RowFilter = String.Format("BranchId={0} AND I_in={1}", Master.LoggedUser.BranchID, "0");
                    DataSet temp = new DataSet();
                    temp.Tables.Add(dvReport.ToTable());
                    Session["InvoiceReport"] = temp;
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        if (Session["InvoiceReport"] != null)
        {
            dxgvInvoice.DataSource = (DataSet)Session["InvoiceReport"];
            dxgvInvoice.DataBind();
        }
        this.gveInvoice.WriteXlsxToResponse();
    }

    protected void dxgvInvoiceLineItems_BeforePerformDataSelect(object sender, EventArgs e)
    {
        try
        {
            if ((sender as ASPxGridView).GetMasterRowKeyValue() == null)
            {
                return;
            }
            Int32 InvoiceId = Int32.Parse((sender as ASPxGridView).GetMasterRowKeyValue().ToString());
            Invoice invoice = new Invoice();
            invoice.InvoiceId = InvoiceId;
            DataSet ds = new InvoiceDAO().GetInvoiceDetailsByInvoiceIDForReporting(invoice);
            (sender as ASPxGridView).DataSource = ds;

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvInvoiceLineItems_BeforePerformDataSelect(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
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
