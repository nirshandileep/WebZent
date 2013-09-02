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
using LankaTiles.SupplierManagement;

public partial class AddSupplier : System.Web.UI.Page
{

    private Supplier objSupplier;

    public Supplier ObjSupplier
    {
        get
        {
            if (objSupplier == null)
            {
                if (Session["ObjSupplier"] == null)
                {
                    String strSupplier = hdnSupplierId.Value.Trim();
                    objSupplier = new Supplier();

                    if (strSupplier != "0")
                    {
                        objSupplier.SupId = Convert.ToInt32(strSupplier);
                        objSupplier.GetSupplierByID();
                        Session["ObjSupplier"] = objSupplier;
                    }
                }
                else
                {
                    objSupplier = (Supplier)(Session["ObjSupplier"]);
                }
            }
            return objSupplier;
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
                this.CheckIsEditSupplier();
                this.SetData();
                this.AddAttributes();
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

    private void AddAttributes()
    {
        txtPhone.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    private void CheckIsEditSupplier()
    {
        try
        {

            if (Request.QueryString["SupplierId"] != null && Request.QueryString["SupplierId"].Trim() != String.Empty)
            {
                hdnSupplierId.Value = Request.QueryString["SupplierId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void SetData()
    {
        try
        {
            if (ObjSupplier.SupId > 0)
            {
                txtSupplierCode.Text = ObjSupplier.Sup_Code.Trim();
                txtSupplierName.Text = ObjSupplier.SupplierName.Trim();
                txtPhone.Text = ObjSupplier.SupplierPhone.Trim();
                txtContactPerson.Text = ObjSupplier.SupplierContact.Trim();
                txtSupplierAddress.Text = ObjSupplier.SupplierAddress.Trim();
                ddlStatus.SelectedValue = ObjSupplier.IsActive ? "1" : "0";
            }
            else
            {
                txtSupplierCode.Text = new SupplierDAO().GetNextSupplierCode();
            }
        }
        catch (Exception ex)
        {

            throw ex;
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

    private void LoadInitialData()
    {
        try
        {

            //
            // Status
            //
            ListItem[] status = { new ListItem("--Please Select--", "-1"), new ListItem("Active", "1"), new ListItem("InActive", "0") };
            ddlStatus.Items.AddRange(status);
            ddlStatus.SelectedValue = "1";
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            ObjSupplier.CreatedDate = DateTime.Now;
            ObjSupplier.CreatedUser = Master.LoggedUser.UserId;
            ObjSupplier.IsActive = ddlStatus.SelectedValue == "1" ? true : false;
            ObjSupplier.ModifiedBy = Master.LoggedUser.UserId;
            ObjSupplier.ModifiedDate = DateTime.Now;
            ObjSupplier.Sup_Code = txtSupplierCode.Text.Trim();
            ObjSupplier.SupplierAddress = txtSupplierAddress.Text.Trim();
            ObjSupplier.SupplierContact = txtContactPerson.Text.Trim();
            ObjSupplier.SupplierName = txtSupplierName.Text.Trim();
            ObjSupplier.SupplierPhone = txtPhone.Text.Trim();
            
            if (ObjSupplier.Save())
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSave_Click(object sender, EventArgs e)");
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

    protected void btnCancel_Click1(object sender, EventArgs e)
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

            throw ex;
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
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }
}
