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
using LankaTiles.InvoiceManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class SearchGatePass : System.Web.UI.Page
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
                dxgvGPSearch.DataSource = (DataSet)Session["SearchGatePass"];
                dxgvGPSearch.DataBind();
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["SearchGatePass"] = null;
            txtGPCode.Text = String.Empty;
            txtInvoiceNumber.Text = String.Empty;
            dtpToDate.Text = String.Empty;
            dtpFromDate.Text = String.Empty;
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCancel_Click(object sender, EventArgs e)");
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
            GetPassSearch gps = new GetPassSearch();
            if (dtpFromDate.Text == String.Empty)
            {
                gps.CreatedDateFrom = String.Empty;
            }
            else
            {
                gps.CreatedDateFrom = dtpFromDate.Date.ToShortDateString();
            }

            if (dtpToDate.Text == String.Empty)
            {
                gps.CreatedDateTo = String.Empty;
            }
            else
            {
                gps.CreatedDateTo = dtpToDate.Date.ToShortDateString();
            }

            gps.GPCode = txtGPCode.Text.Trim();
            GetPassDAO gpsearch = new GetPassDAO();
            DataSet ds = new DataSet();
            ds = gpsearch.GetPassSearch(gps);
            dxgvGPSearch.DataSource = ds;
            dxgvGPSearch.DataBind();
            Session["SearchGatePass"] = ds;
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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (Session["SearchGatePass"] != null)
        {
            dxgvGPSearch.DataSource = (DataSet)Session["SearchGatePass"];
            dxgvGPSearch.DataBind();
        }
        this.vgeGatePass.WriteXlsToResponse();
    }
}
