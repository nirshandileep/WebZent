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
using LankaTiles.ItemsManagement;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class SearchGroupItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["SearchGroups"] = null;
                this.FillDataToGrid();
                this.CheckFromURL();
                this.SetUIForURLParameters();
            }
            if (IsCallback)
            {
                this.FillDataToGrid();
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

    private void SetUIForURLParameters()
    {
        try
        {

            if (hdnFromURL.Value != "AddInvoice.aspx")
            {
                dxgvGroupItems.Columns[0].Visible = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void FillDataToGrid()
    {
        if (Session["SearchGroups"] != null)
        {
            dxgvGroupItems.DataSource = (DataSet)Session["SearchGroups"];
            dxgvGroupItems.DataBind();
        }
        else
        {
            DataSet dsSelectAllGroups = new GroupsDAO().GetAllGroups();
            dxgvGroupItems.DataSource = dsSelectAllGroups;
            dxgvGroupItems.DataBind();
            Session["SearchGroups"] = dsSelectAllGroups;
        }
    }

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

            throw ex;
        }
    }

    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        if (Session["SearchGroups"] != null)
        {
            dxgvGroupItems.DataSource = (DataSet)Session["SearchGroups"];
            dxgvGroupItems.DataBind();
        }
        this.gveGroupItems.WriteXlsToResponse();
    }

    protected void dxgvGroupItems_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
    }
}
