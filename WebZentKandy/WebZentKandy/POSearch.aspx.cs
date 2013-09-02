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
using LankaTiles.SupplierManagement;
using DevExpress.Web.ASPxGridView;

public partial class POSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CheckFromURL();
                this.LoadInitialData();
                Master.ClearSessions();//Clear all sessions
            }
            if (IsCallback)
            {
                dxgvPOSearch.DataSource = (DataSet)Session["SearchPO"];
                dxgvPOSearch.DataBind();
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

    private void LoadInitialData()
    {
        try
        {

            ///
            /// Load Suppliers
            ///
            DataSet dsSuppliers = (new SupplierDAO()).GetAllSuppliers();
            if (dsSuppliers == null || dsSuppliers.Tables.Count == 0)
            {
                ddlSupplier.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("Sup_Code", "SupId", dsSuppliers, ddlSupplier);
                ddlSupplier.Items.Add(new ListItem("All", "-1"));
                ddlSupplier.SelectedValue = "-1";
            }

            ///
            /// Load Payment Due Option
            ///
            ListItem[] liPaymentOptions = { new ListItem("Partialy Paid", "1"), new ListItem("Completed", "0"), new ListItem("All", "-1") };
            ddlPaymentDue.Items.AddRange(liPaymentOptions);
            ddlPaymentDue.SelectedValue = "-1";

            ///
            /// Load Recieved Status Option
            ///
            ListItem[] liRecievedOption = { new ListItem("Partialy Recieved", "0"), new ListItem("Totally Recieved", "1"), new ListItem("All", "-1") };
            ddlPORecievedOption.Items.AddRange(liRecievedOption);
            ddlPORecievedOption.SelectedValue = "-1";
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Fill FromURL to go back
    /// </summary>
    private void CheckFromURL()
    {
        try
        {
            if (Request.QueryString["FromURL"] != null && Request.QueryString["FromURL"].Trim() != String.Empty)
            {
                hdnFromURL.Value = Request.QueryString["FromURL"].Trim();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "private void CheckFromURL()");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private void Search()
    {
        try
        {
            //DataSet searchPO = new PODAO().GetAllPO();
            //gvPO.DataSource = searchPO;
            //gvPO.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string poid = ((LinkButton)(sender)).CommandArgument.ToString();
            Response.Redirect("POAdd.aspx?FromURL=POSearch.aspx&POId=" + poid.Trim(), false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void LinkButton1_Click(object sender, EventArgs e)");
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
            POSearchParameters pos = new POSearchParameters();
            pos.POCode = txtPOCode.Text.Trim();
            pos.DueAmountOption = Int32.Parse(ddlPaymentDue.SelectedValue.Trim());
            pos.POAmount = Decimal.Parse(txtPOAmmount.Text.Trim() != String.Empty ? txtPOAmmount.Text.Trim() : "0");
            pos.SupId = Int32.Parse(ddlSupplier.SelectedValue.Trim());
            pos.TotRcvdOption = Int32.Parse(ddlPORecievedOption.SelectedValue.Trim());
            pos.FromDate = dtpFromDate.Value == null ? String.Empty : DateTime.Parse(dtpFromDate.Value.ToString()).ToShortDateString();
            pos.ToDate = dtpToDate.Value == null ? String.Empty : DateTime.Parse(dtpToDate.Value.ToString()).ToShortDateString();
            DataSet dsPOs = (new PODAO()).SearchPO(pos);
            Session["SearchPO"] = dsPOs;

            if (dsPOs != null && dsPOs.Tables[0].Rows.Count > 0)
            {
                dxgvPOSearch.DataSource = dsPOs;
                dxgvPOSearch.DataBind();
            }
            else
            {
                dxgvPOSearch.DataSource = dsPOs;
                dxgvPOSearch.DataBind();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtPOCode.Text = String.Empty;
            txtPOAmmount.Text = String.Empty;
            ddlSupplier.SelectedValue = "-1";
            ddlPaymentDue.SelectedValue = "-1";
            ddlPORecievedOption.SelectedValue = "-1";
            //Set date selectors to todays date
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCancel_Click(object sender, EventArgs e)");
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
        if (Session["SearchPO"] != null)
        {
            dxgvPOSearch.DataSource = (DataSet)Session["SearchPO"];
            dxgvPOSearch.DataBind();
        }
        this.vgePOSearch.WriteXlsxToResponse();
    }

    protected void dxgvPOSearch_BeforePerformDataSelect(object sender, EventArgs e)
    {
        
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
