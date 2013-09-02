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
using LankaTiles.ItemsManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class AddCategory : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                this.CheckFromURL();
                this.CheckIsEditCategory();
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

    #region Methods
    
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

    public void CheckIsEditCategory()
    {
        try
        {
            if (Request.QueryString["CategoryId"] != null && Request.QueryString["CategoryId"].Trim() != String.Empty)
            {
                hdnCategory.Value = Request.QueryString["CategoryId"].Trim();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region Events

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Category objCategory        = new Category();
            objCategory.CategoryId      = Int32.Parse(hdnCategory.Value.Trim());
            objCategory.CategoryType    = txtCatType.Text.Trim();
            objCategory.CategoryDesc    = txtDescription.Text.Trim();

            if (!(new CategoryDAO()).IsCategoryExists(txtCatType.Text.Trim()))
            {
                if (objCategory.Save())
                {
                    lblError.Visible    = true;
                    lblError.Text       = Constant.MSG_Save_SavedSeccessfully;
                }
            }
            else
            {
                lblError.Visible        = true;
                lblError.Text           = Constant.Error_Category_Exists;
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
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCancel_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    #endregion Events

    
}
