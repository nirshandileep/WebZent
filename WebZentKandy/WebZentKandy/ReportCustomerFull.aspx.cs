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
using LankaTiles.CustomerManagement;

public partial class ReportCustomerFull : System.Web.UI.Page
{
    //Session["CustomerFullReport"]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["CustomerFullReport"] = null;
                this.LoadInitialData();
            }

            if (IsCallback)
            {
                BindGrid();
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
        GenerateReport();
    }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        //try
        //{
            if (Session["CustomerFullReport"] != null)
            {
                dxgvSupplierPaymentReport.DataSource = (DataSet)Session["CustomerFullReport"];
                dxgvSupplierPaymentReport.DataBind();
            }
            this.gveExpenceReport.WriteXlsxToResponse();
        //}
        //catch (Exception ex)
        //{
        //    ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnExportReport_Click(object sender, EventArgs e)");
        //    if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
        //        Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
        //    else
        //        Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        //}
    }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateReport();
    }

    private void LoadInitialData()
    {
        try
        {
            //
            // Suppliers
            //
            DataSet dsCustomers = (new CustomerDAO()).GetAllCustomers();
            if (dsCustomers == null || dsCustomers.Tables.Count == 0)
            {
                ddlCustomers.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("Cus_Name", "CustomerID", dsCustomers, ddlCustomers);
                ddlCustomers.Items.Insert(0, new ListItem("--Please Select--", "-1"));
            }

            ListItemCollection listCol = new ListItemCollection();

            foreach (ListItem var in ddlCustomers.Items)
            {
                if (var.Text.ToUpper() == "CARD")
                {
                    listCol.Add(var);
                }

                if (var.Text.ToUpper() == "CASH")
                {
                    listCol.Add(var);
                }
            }

            foreach (ListItem var in listCol)
            {
                ddlCustomers.Items.Remove(var);
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

    private void GenerateReport()
    {
        try
        {

            DataSet dsCustReport = new DataSet();
            CustomerSearch supSearch = new CustomerSearch();
            supSearch.FromDate = dtpFromDate.Date;
            supSearch.ToDate = dtpToDate.Date;
            supSearch.CustId = Int32.Parse(ddlCustomers.SelectedValue.ToString());
            dsCustReport = new CustomerDAO().GetTransactionHistoryByCustomer(supSearch);

            Session["CustomerFullReport"] = dsCustReport;
            BindGrid();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void BindGrid()
    {
        dxgvSupplierPaymentReport.DataSource = (DataSet)Session["CustomerFullReport"];
        dxgvSupplierPaymentReport.DataBind();
    }

}
