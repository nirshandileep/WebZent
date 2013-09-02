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
using LankaTiles.LocationManagement;

public partial class BranchSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                this.Search();
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

    private void Search()
    {
        try
        {
            Location loc = new Location();
            loc.BranchCode = "";
            loc.BranchName = "";
            loc.IsActive = true;
            DataSet dsBranch = new LocationsDAO().SearchLocation(loc);

            //DataSet dsBranch = new LocationsDAO().GetAllBranches();

            if (dsBranch != null && dsBranch.Tables.Count > 0)
            {

                gvBranches.DataSource = dsBranch;
                gvBranches.DataBind();
                trGrid.Visible = true;
                trNoRecords.Visible = false;
            }
            else
            {

                trNoRecords.Visible = true;
                trGrid.Visible = false;
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strBranchid = ((ImageButton)(sender)).CommandArgument.ToString();
            if (strBranchid == Constant.MainStoreId)
            {
                return;//do not edit main store details
            }
            Response.Redirect("AddBranch.aspx?BranchID=" + strBranchid + "&FromURL=BranchSearch.aspx", false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnEdit_Click(object sender, ImageClickEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
    protected void gvBranches_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }
}
