using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LankaTiles.Common;
using LankaTiles.Exception;
using LankaTiles.LocationManagement;

public partial class AddBranch : System.Web.UI.Page
{

    private Location objLocation;

    public Location ObjLocation
    {
        get
        {
            if (objLocation == null)
            {
                if (Session["ObjLocation"] == null)
                {
                    //string strUserID = Page.Request.QueryString["UserId"] != null ? Page.Request.QueryString["UserId"].ToString() : "";
                    String strBranchID = hdnBranchId.Value.Trim();
                    objLocation = new Location();

                    if (strBranchID != "0")
                    {
                        objLocation.BranchId = Convert.ToInt32(strBranchID);
                        //objLocation = objLocation.GetBranchDetails();
                        if (objLocation.GetLocationByID())
                        {
                            Session["ObjLocation"] = objLocation;
                        }
                    }
                }
                else
                {
                    objLocation = (Location)(Session["ObjLocation"]);
                }
            }
            return objLocation;
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
                this.CheckIfBranchEdit();
                this.SetData();
                this.AddAttributes();
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
        txtTelPhone.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    public void SetData()
    {
        try
        {

            if (hdnBranchId.Value!="0")
            {
                txtBranchCode.Text      = ObjLocation.BranchCode;
                txtBranchName.Text      = ObjLocation.BranchName;
                txtAddress1.Text        = ObjLocation.Address1;
                txtAddress2.Text        = ObjLocation.Address2;
                txtTelPhone.Text        = ObjLocation.Telephone;
                txtContact.Text         = ObjLocation.ContactName;
                ddlStatus.SelectedValue = ObjLocation.IsActive == true ? "1" : "0";
                txtInvCode.Text = ObjLocation.InvPrefix;
                txtInvCode.ReadOnly     = true;

            }
            else
            {
                txtInvCode.ReadOnly     = false;
            }

        }
        catch
        {
            
            throw;
        }
 
    }

    private void CheckIfBranchEdit()
    {
        try
        {
            if (Request.QueryString["BranchID"] != null && Request.QueryString["BranchID"].Trim() != String.Empty)
            {
                hdnBranchId.Value = Request.QueryString["BranchID"].Trim();
                Page.Title = "Edit Branch";
            }
            else
            {
                Session["ObjLocation"] = null;
            }
        }
        catch
        {
            throw;
        }
    }

    public void LoadInitialData()
    {
        try
        {
            ListItem[] status = { new ListItem("Active", "1"), new ListItem("InActive", "0") };
            ddlStatus.Items.AddRange(status);
        }
        catch
        {
            throw;
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
        catch
        {
            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ObjLocation.BranchId = Int32.Parse(hdnBranchId.Value);
            ObjLocation.BranchCode = txtBranchCode.Text.Trim();
            ObjLocation.BranchName = txtBranchName.Text.Trim();
            ObjLocation.Address1 = txtAddress1.Text.Trim();
            ObjLocation.Address2 = txtAddress2.Text.Trim();
            ObjLocation.ContactName = txtContact.Text.Trim();
            ObjLocation.Telephone = txtTelPhone.Text.Trim();
            ObjLocation.IsActive = ddlStatus.SelectedValue == "1" ? true : false;
            ObjLocation.InvPrefix = txtInvCode.Text.ToUpper().Trim();

            if (!(new LocationsDAO()).IsBranchCodeExists(txtBranchCode.Text.Trim()))
            {
                if(objLocation.Save())
                {
                    lblError.Visible = true;
                    lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                    hdnBranchId.Value = objLocation.BranchId.ToString();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Location_CodeExists;
            }

            /// if branch code exists
            /// save data
            /// set page properties to edit mode
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
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCancel_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
}
