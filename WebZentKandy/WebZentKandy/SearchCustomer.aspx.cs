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
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class SearchCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                this.CheckFromURL();
                this.Search();
            }
            this.ShowHideControlsByUser();
            if (IsCallback)
            {
                dxgvCustomers.DataSource = (DataSet)Session["SearchCustomers"];
                dxgvCustomers.DataBind();
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

    private void ShowHideControlsByUser()
    {
        try
        {
            short userrole = Master.LoggedUser.UserRoleID;

            switch (userrole)
            {
                case 1://SuperUser
                    dxgvCustomers.Columns[0].Visible = true;
                    btnUpdate.Visible = true;
                    break;
                default:
                    dxgvCustomers.Columns[0].Visible = false;
                    btnUpdate.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void Search()
    {
        try
        {
            DataSet searchCus = new CustomerDAO().GetAllCustomers();
            dxgvCustomers.DataSource = searchCus;
            Session["SearchCustomers"] = searchCus;
            dxgvCustomers.DataBind();
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

    protected void btnExportToReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["SearchCustomers"] != null)
            {
                dxgvCustomers.DataSource = (DataSet)Session["SearchCustomers"];
                dxgvCustomers.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnExportToReport_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
        this.gveCustomerExport.WriteXlsToResponse();
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
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    protected void dxgvCustomers_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            DataRow[] dr = ((DataSet)Session["SearchCustomers"]).Tables[0].Select("CustomerID="+e.Keys[0].ToString());
            if (dr.Length>0)
            {
                dr[0]["Cus_CreditTotal"] = e.NewValues["Cus_CreditTotal"];
                e.Cancel = true;
                dxgvCustomers.CancelEdit();
                dxgvCustomers.DataBind();    
            }
            
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (Session["SearchCustomers"] != null)
            {
                ds = (DataSet)Session["SearchCustomers"];
            }

            if (new CustomerDAO().UpdateAllCustomers(ds))
            {
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
            }
            else
            {
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnUpdate_Click(object sender, EventArgs e)");
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

    protected void dxgvCustomers_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        try
        {
            int cusid = Int32.Parse(e.Keys[0].ToString().Trim());
            if (cusid == 1)
            {
                e.RowError = "Editing Cash User is not allowed!";
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvCustomers_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)");
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
