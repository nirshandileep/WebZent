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
//using LankaTiles.GRNManagement;
using LankaTiles.Common;
using LankaTiles.Exception;
using LankaTiles.GRNManagement;
using DevExpress.Web.ASPxGridView;

public partial class GRNSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CheckFromURL();
                Master.ClearSessions();//Clear all sessions
            }
            if (IsCallback)
            {
                if (Session["GRNSearchResults"] != null)
                {
                    dxgvGRNDetails.DataSource = (DataSet)Session["GRNSearchResults"];
                    dxgvGRNDetails.DataBind();
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
            GRNSearchParameters grnsp = new GRNSearchParameters();

            grnsp.POCode = txtPOCode.Text.Trim();
            grnsp.FromDate = dtpFromDate.Value == null ? String.Empty : DateTime.Parse(dtpFromDate.Value.ToString()).ToShortDateString();
            grnsp.ToDate = dtpToDate.Value == null ? String.Empty : DateTime.Parse(dtpToDate.Value.ToString()).ToShortDateString();

            if (txtSalesReturnID.Text.Trim() == String.Empty)
            {
                grnsp.SalesReturnID = null;
            }
            else
            {
                grnsp.SalesReturnID = Int32.Parse(txtSalesReturnID.Text.Trim());
            }

            grnsp.SuplierInvNo = txtSupInvNumber.Text.Trim();

            DataSet dsGRN = (new GRNDAO()).GRNSearch(grnsp);
            Session["GRNSearchResults"] = dsGRN;
            if (dsGRN != null && dsGRN.Tables[0].Rows.Count > 0)
            {
                //gvGRN.DataSource = dsGRN;
                dxgvGRNDetails.DataSource = dsGRN;
            }
            else
            {
                //gvGRN.DataSource = null;
            }
            //gvGRN.DataBind();
            dxgvGRNDetails.DataBind();
            this.ShowHideGrid();
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
    /// Show hide the grid depending on row count
    /// </summary>
    private void ShowHideGrid()
    {
        try
        {
            //if (gvGRN.Rows.Count > 0)
            //{
            //    trGrid.Visible = true;
            //    trNoRecords.Visible = false;
            //}
            //else
            //{
            //    trGrid.Visible = false;
            //    trNoRecords.Visible = true;
            //}

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtPOCode.Text = String.Empty;
            txtSalesReturnID.Text = String.Empty;
            txtSalesReturnID.Text = String.Empty;
            Session["GRNSearchResults"] = null;
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

    protected void gvGRN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lb = new LinkButton();
            lb.ID = e.Row.Cells[0].Controls[1].ID;
            ((LinkButton)(e.Row.Cells[0].Controls[1])).Text = gvGRN.DataKeys[e.Row.RowIndex].Value.ToString();

            //lb.Text = e.Row.
        }
    }

    protected void lbGRNId_Click(object sender, EventArgs e)
    {
        try
        {
            string grnid = ((LinkButton)(sender)).CommandArgument.ToString();
            Response.Redirect("RecieveGoods.aspx?FromURL=GRNSearch.aspx&GRNId=" + grnid.Trim(), false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void lbGRNId_Click(object sender, EventArgs e)");
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
    protected void dxgvGRNDetails_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        try
        {
            int layoutIndex = -1;
            if (int.TryParse(e.Parameters, out layoutIndex))
                ApplyLayout(layoutIndex);

        }
        catch (Exception ex)
        {

            throw ex; 
        }
    }

    void ApplyLayout(int layoutIndex)
    {
        dxgvGRNDetails.BeginUpdate();
        try
        {
            dxgvGRNDetails.ClearSort();
            switch (layoutIndex)
            {
                case 0:
                    dxgvGRNDetails.GroupBy((GridViewDataColumn)dxgvGRNDetails.Columns["POCode"]);
                    break;
                
            }
        }
        finally
        {
            dxgvGRNDetails.EndUpdate();
        }
        dxgvGRNDetails.ExpandAll();
    }

    protected void btnExportData_Click(object sender, EventArgs e)
    {

        if (Session["GRNSearchResults"] != null)
        {
            dxgvGRNDetails.DataSource = (DataSet)Session["GRNSearchResults"];
            dxgvGRNDetails.DataBind();
        }
        this.gveGRNDetailsExporter.WriteXlsToResponse();

    }
}
