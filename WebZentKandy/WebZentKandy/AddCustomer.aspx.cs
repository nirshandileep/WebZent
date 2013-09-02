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
using LankaTiles.CustomerManagement;
using LankaTiles.Exception;
using LankaTiles.Common;

public partial class AddCustomer : System.Web.UI.Page
{
    private Customer objCustomer;

    public Customer ObjCustomer
    {
        get
        {
            if (objCustomer == null)
            {
                if (Session["ObjCustomer"] == null)
                {
                    String strCustomerId = hdnCustomerID.Value.Trim();
                    objCustomer = new Customer();

                    if (strCustomerId != "")
                    {
                        objCustomer.CustomerID = Convert.ToInt32(strCustomerId);
                        objCustomer.GetCustomerByID();
                        Session["ObjCustomer"] = objCustomer;
                    }
                }
                else
                {
                    objCustomer = (Customer)(Session["ObjCustomer"]);
                }
            }
            return objCustomer;
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
                this.CheckIsEditCustomer();
                this.SetData();
                this.AddAttributes();
            }

            if (hdnCustomerID.Value == "0")
            {
                Page.Title = "Add Customer";
            }
            else
            {
                Page.Title = "Edit Customer";
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

    private void SetData()
    {
        if (ObjCustomer.CustomerID > 0)
        {
            txtCustomerCode.Text = ObjCustomer.CustomerCode.Trim();
            txtCust_Name.Text = ObjCustomer.Cus_Name.Trim();
            txtCus_Adress.Text = ObjCustomer.Cus_Address.Trim();
            txtPhone.Text = ObjCustomer.Cus_Tel.Trim();
            txtContactName.Text = ObjCustomer.Cus_Contact.Trim();
            chkIsCreditAllowed.Checked = ObjCustomer.IsCreditCustomer;
            ddlStatus.SelectedValue = ObjCustomer.IsActive ? "1" : "0";
            Page.Title = "Edit Customer";
        }
        else
        {
            txtCustomerCode.Text = new CustomerDAO().GetNextCustomerCode();
            Page.Title = "Add Customer";
        }
    }

    private void AddAttributes()
    {
        txtPhone.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    private void CheckIsEditCustomer()
    {
        try
        {
            if (Request.QueryString["CustomerID"] != null && Request.QueryString["CustomerID"].Trim() != String.Empty)
            {
                hdnCustomerID.Value = Request.QueryString["CustomerID"].Trim();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ObjCustomer.CustomerCode = txtCustomerCode.Text.Trim();
            ObjCustomer.Cus_Name = txtCust_Name.Text.Trim();
            ObjCustomer.Cus_Address = txtCus_Adress.Text.Trim();
            ObjCustomer.Cus_Contact = txtContactName.Text.Trim();
            ObjCustomer.Cus_Tel = txtPhone.Text.Trim();
            ObjCustomer.IsActive = ddlStatus.SelectedValue.Trim() == "1" ? true : false;
            ObjCustomer.IsCreditCustomer = chkIsCreditAllowed.Checked;
            
            if (ObjCustomer.Save())
            {
                hdnCustomerID.Value = ObjCustomer.CustomerID.ToString();
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
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}
