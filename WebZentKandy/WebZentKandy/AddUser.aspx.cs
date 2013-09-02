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
using LankaTiles.UserManagement;
using LankaTiles.LocationManagement;

public partial class AddUser : System.Web.UI.Page
{
    private User objUser;

    public User ObjUser
    {
        get 
        {
            if (objUser == null)
            {
                if (Session["ObjUser"] == null)
                {
                    //string strUserID = Page.Request.QueryString["UserId"] != null ? Page.Request.QueryString["UserId"].ToString() : "";
                    String strUserID = hdnUserId.Value.Trim();
                    objUser = new User();

                    if (strUserID != "0")
                    {
                        objUser.UserId = Convert.ToInt32(strUserID);
                        if (objUser.GetUserByID())
                        {
                            Session["ObjUser"] = objUser;
                        }
                    }
                }
                else
                {
                    objUser = (User)(Session["ObjUser"]);
                }
            }
            return objUser;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfEditUser();
                this.SetData();
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

    private void CheckIfEditUser()
    {
        try
        {
            if (Request.QueryString["UserId"] != null && Request.QueryString["UserId"].Trim() != String.Empty)
            {
                hdnUserId.Value = Request.QueryString["UserId"].Trim();
                Page.Title = "Edit User";
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    public void LoadInitialData()
    {
        try
        {
            DataSet ds = (new UsersDAO()).GetAllUserRoles();
            Master.BindDropdown("RoleName", "RoleId", ds, ddlUserRole);
            
            ListItem[] status = { new ListItem("Active", "1"), new ListItem("InActive", "0") };
            ddlStatus.Items.AddRange(status);
            
            DataSet dsRoles = (new LocationsDAO()).GetAllBranches();
            Master.BindDropdown("BranchCode", "BranchId", dsRoles, ddlBranches);
            if (ddlBranches.Items.FindByValue(Constant.MainStoreId) != null)
            {
                ddlBranches.Items.Remove(ddlBranches.Items.FindByValue(Constant.MainStoreId));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ObjUser.UserName = txtUserName.Text.Trim();
            ObjUser.FirstName = txtFirstName.Text.Trim();
            ObjUser.LastName = txtLastName.Text.Trim();
            ObjUser.Password = txtPassword.Text.Trim();
            ObjUser.IsActive = ddlStatus.SelectedValue.Trim() == "1" ? true : false;
            ObjUser.UserRoleID = short.Parse(ddlUserRole.SelectedValue.Trim() == String.Empty ? Structures.UserRoles.Cashier.ToString() : ddlUserRole.SelectedValue.Trim());
            ObjUser.BranchID = Int32.Parse(ddlBranches.SelectedValue.Trim());

            if (!(new UsersDAO()).IsUserNameExists(txtUserName.Text.Trim()))
            {
                if (ObjUser.Save())
                {
                    lblSuccess.Visible = true;
                    lblSuccess.Text = Constant.MSG_Save_SavedSeccessfully;
                    hdnUserId.Value = ObjUser.UserId.ToString();
                    Page.Title = "Edit User";
                }
                else
                {
                    lblSuccess.Visible = true;
                    lblSuccess.Text = Constant.MSG_Save_NotSavedSeccessfully;
                }
            }
            else
            {
                lblSuccess.Visible = true;
                lblSuccess.Text = Constant.MSG_User_UserNameExists;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSave_Click(object sender, EventArgs e)");
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
            if (hdnFromURL.Value != null && hdnFromURL.Value != String.Empty)
            {
                Response.Redirect(hdnFromURL.Value.Trim(), false);
            }
            else
            {
                Response.Redirect("Home.aspx", false);
            }
        }
        catch(Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCancel_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        
        }
    }

    #region Methods
    
    /// <summary>
    /// Fill FromURL to go back
    /// </summary>
    private void CheckFromURL()
    {
        if (Request.QueryString["FromURL"] != null && Request.QueryString["FromURL"].Trim() != String.Empty)
        {
            hdnFromURL.Value = Request.QueryString["FromURL"].Trim();
        }
    }

    private void SetData()
    {
        try
        {
            if (ObjUser.UserId > 0)
            {
                txtFirstName.Text = ObjUser.FirstName.Trim();
                txtLastName.Text = ObjUser.LastName.Trim();
                ddlUserRole.SelectedValue = ObjUser.UserRoleID.ToString();
                ddlStatus.SelectedValue = ObjUser.IsActive ? "1" : "0";
                ddlBranches.SelectedValue = ObjUser.BranchID.ToString();
                txtUserName.Text = ObjUser.UserName.Trim();
                txtPassword.Text = ObjUser.Password;
                txtConfirmPassword.Text = ObjUser.Password;

                if (Master.LoggedUser.UserId.ToString()==hdnUserId.Value.Trim())
                {
                    ddlUserRole.Enabled = false;
                    ddlStatus.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

}
