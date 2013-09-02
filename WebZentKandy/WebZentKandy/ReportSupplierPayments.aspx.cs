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

public partial class ReportSupplierPayments : System.Web.UI.Page
{

    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.LoadInitialData();
            }

            if (IsCallback)
            {
                this.GenerateReport();
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
        try
        {

            GenerateReport();
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

    private void GenerateReport()
    {
        try
        {

            DataSet dsSupplierReport    = new DataSet();
            SupplierSearch supSearch    = new SupplierSearch();
            supSearch.FromDate          = dtpFromDate.Date;
            supSearch.ToDate            = dtpToDate.Date;
            supSearch.Sup_Id            = Int32.Parse(ddlSupplier.SelectedValue.ToString());
            dsSupplierReport            = new SupplierDAO().SelectSupplierRelatedExpences(supSearch);

            Session["SupPayReport"] = dsSupplierReport;

            dxgvSupplierPaymentReport.DataSource = dsSupplierReport;
            dxgvSupplierPaymentReport.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["SupPayReport"] != null)
            {
                dxgvSupplierPaymentReport.DataSource = (DataSet)Session["SupPayReport"];
                dxgvSupplierPaymentReport.DataBind();
            }
            this.gveExpenceReport.WriteXlsxToResponse();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnExportReport_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    private void LoadInitialData()
    {
        try
        {
            //
            // Suppliers
            //
            DataSet dsSuppliers = (new SupplierDAO()).GetAllSuppliers();
            if (dsSuppliers == null || dsSuppliers.Tables.Count == 0)
            {
                ddlSupplier.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("SupplierName", "SupId", dsSuppliers, ddlSupplier);
                ddlSupplier.Items.Insert(0, new ListItem("--Please Select--", "-1"));
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "private void LoadInitialData()");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.GenerateReport();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }
}
