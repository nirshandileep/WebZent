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
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class UserLogin : System.Web.UI.Page
{
    private User loginUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            if (!IsPostBack)
            {
                Session["LoggedUser"] = null;
                Session["ObjInv"] = null;
                txtUserName.Focus();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            loginUser               = new User();
            loginUser.UserName      = txtUserName.Text.Trim();
            loginUser.Password      = txtPassword.Text.Trim();

            if (loginUser.CheckLogin())
            {
                Session["LoggedUser"] = loginUser;
                Response.Redirect("Home.aspx", false);
            }
            else
            {
                lblError.Visible    = true;
                lblError.Text       = Constant.MSG_User_InvalidUserNameOrPassword;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnLogin_Click(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    private void EmptySessions()
    {
        try
        {
            Session["ObjLocation"] = null;
            Session["ObjCustomer"] = null;
            Session["ObjGatePass"] = null;
            Session["ObjInv"] = null;
            Session["ObjItem"] = null;
            Session["ObjBrand"] = null;
            Session["ObjSupplier"] = null;
            Session["ObjUser"] = null;
            Session["ObjVoucher"] = null;
            Session["ObjIGroup"] = null;
            Session["ObjItemTransfer"] = null;
            Session["ObjPurchaseOrder"] = null;
            Session["ObjGRNPO"] = null;

            Session["GRNSearchResults"] = null;
            Session["Report"] = null;
            Session["InvoiceReport"] = null;
            Session["CategoryList"] = null;
            Session["Error"] = null;
            Session["VoucherPOList"] = null;

            Session["SearchPO"] = null;
            Session["SearchItems"] = null;
            Session["SearchCustomers"] = null;
            Session["SearchGatePass"] = null;
            Session["SearchGroups"] = null;
            Session["SearchInvoice"] = null;
            Session["SearchTransfers"] = null;
            Session["SearchUser"] = null;
            Session["SearchVoucher"] = null;

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
