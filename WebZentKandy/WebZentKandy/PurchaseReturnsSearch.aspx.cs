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
using LankaTiles.POManagement;

public partial class SearchVouchers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.AddAttributes();

                Master.ClearSessions();//Clear all sessions
                DataSet dsSelectAllInv = this.Search();
                dxgvPurchaseReturns.DataSource = dsSelectAllInv;
                dxgvPurchaseReturns.DataBind();
                Session["SearchPurchaseReturns"] = dsSelectAllInv;
            }
            if (IsCallback)
            {
                if (Session["SearchPurchaseReturns"] != null)
                {
                    dxgvPurchaseReturns.DataSource = (DataSet)Session["SearchPurchaseReturns"];
                    dxgvPurchaseReturns.DataBind();
                }
                else
                {
                    dxgvPurchaseReturns.DataSource = new VoucherDAO().GetAll();
                    dxgvPurchaseReturns.DataBind();
                }
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

    private void AddAttributes()
    {
        //txtPRNumber.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        //txtSupInvNo.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["SearchPurchaseReturns"] != null)
        {
            dxgvPurchaseReturns.DataSource = (DataSet)Session["SearchPurchaseReturns"];
            dxgvPurchaseReturns.DataBind();
        }
        this.gvePurchaseReturn.WriteXlsxToResponse();
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {

            txtPRNumber.Text = String.Empty;
            txtSupInvNo.Text = String.Empty;
            dtpFromDate.Text = String.Empty;
            dtpToDate.Text = String.Empty;
            ddlIssueStatus.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnClearSearch_Click(object sender, EventArgs e)");
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
            DataSet dsVoucher = Search();

            dxgvPurchaseReturns.DataSource = dsVoucher;
            dxgvPurchaseReturns.DataBind();
            Session["SearchPurchaseReturns"] = dsVoucher;
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

    private DataSet Search()
    {
        try
        {
            PurchaseReturnsSearch search = new PurchaseReturnsSearch();

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
            
            search.IssuedStatus = (Structures.PRRecievedStatus)Int64.Parse(ddlIssueStatus.SelectedValue.Trim());
            search.PRCode = txtPRNumber.Text.Trim();
            search.SupInvNo = txtSupInvNo.Text.Trim();

            return search.Search();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
