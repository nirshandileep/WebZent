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
using LankaTiles.UserManagement;
using LankaTiles.Exception;
using LankaTiles.Common;
using LankaTiles.LocationManagement;


public partial class SearchUsers : System.Web.UI.Page
{

    //private User searchUser;

    //public User SearchUser
    //{
    //    get
    //    {
    //        if (searchUser == null)
    //        {
    //            if (Session["SearchUser"] == null)
    //            {
                    
    //                searchUser = new User();
    //                if (strUserID != "0")
    //                {
    //                    searchUser.UserId = Convert.ToInt32(strUserID);
    //                    searchUser.GetUserByUserId();
    //                    Session["SearchUser"] = searchUser;
    //                }
    //            }
    //            else
    //            {
    //                searchUser = (User)(Session["SearchUser"]);
    //            }
    //        }
    //        return searchUser;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                this.LoadInitialData();
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

    public void LoadInitialData()
    {

        try
        {
            // User Roles
            DataSet dsRoles = (new UsersDAO()).GetAllUserRoles();
            if (dsRoles == null || dsRoles.Tables.Count == 0)
            {
                ddlUserRole.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("RoleName", "RoleId", dsRoles, ddlUserRole);
                ddlUserRole.Items.Add(new ListItem("All", "-1"));
                ddlUserRole.SelectedValue = "-1";
            }

            // Branches
            DataSet dsBranches = (new LocationsDAO()).GetAllBranches();
            if (dsBranches == null || dsBranches.Tables.Count == 0)
            {
                ddlBranches.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("BranchCode", "BranchId", dsBranches, ddlBranches);
                ddlBranches.Items.Add(new ListItem("All", "-1"));
                ddlBranches.SelectedValue = "-1";
            }

            // Remove Main Store
            if (ddlBranches.Items.FindByValue(Constant.MainStoreId) != null)
            {
                ddlBranches.Items.Remove(ddlBranches.Items.FindByValue(Constant.MainStoreId));
            }

            // Status
            ListItem[] status = { new ListItem("All","-1"), new ListItem("Active", "1"), new ListItem("Inactive", "0") };
            ddlStatus.Items.AddRange(status);

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void Search()
    {
        try
        {
            User searchUser = new User();
            searchUser.BranchID =  ddlBranches.SelectedValue != "-1" ? Int32.Parse(ddlBranches.SelectedValue.Trim()): 0;
            searchUser.FirstName = txtFirstName.Text.Trim();
            //searchUser.IsActive = Boolean.Parse();
            searchUser.LastName = txtLastName.Text.Trim();
            searchUser.UserName = txtUserName.Text.Trim();
            searchUser.SearchOption = Int32.Parse(ddlStatus.SelectedValue.Trim());
            searchUser.UserRoleID = ddlUserRole.SelectedValue != "-1" ? short.Parse(ddlUserRole.SelectedValue.Trim()): short.Parse("0");

            DataSet searchData = searchUser.SearchUsers();
            if (searchData!=null && searchData.Tables[0].Rows.Count > 0)
            {
                trGridView.Visible = true;
                trNoRecords.Visible = false;
                gvUserList.DataSource = searchData;
                gvUserList.DataBind();
            }
            else
            {
                trGridView.Visible = false;
                trNoRecords.Visible = true;
            }
            
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

            this.Search();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnClear_Click(object sender, EventArgs e)");
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
            this.Search();    
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


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strUserid = ((ImageButton)(sender)).CommandArgument.ToString();
            Response.Redirect("AddUser.aspx?UserId=" + strUserid + "&FromURL=SearchUsers.aspx", false);
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

    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strUserid = ((ImageButton)(sender)).CommandArgument.ToString();
            // todo : Inactivate User
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnRemove_Click(object sender, ImageClickEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }


    protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                Int32 userid = Convert.ToInt32(gvUserList.DataKeys[e.Row.RowIndex].Values["UserId"].ToString());
                Boolean isActive = Convert.ToBoolean(e.Row.Cells[5].Text.Trim());
                
                //if (!isActive)
                //{
                //    btnDel.Enabled = false;
                //}

                //if (userid == loggedUser.UserId)
                //{
                //    btnDel.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }


}
