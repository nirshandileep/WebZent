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
using SupSearch = LankaTiles.SupplierManagement;
using LankaTiles.Common;
using LankaTiles.Exception;
using LankaTiles.SupplierManagement;

public partial class SupplierSearch : System.Web.UI.Page
{

    private SupSearch.SupplierSearch objSrchSupplier;

    public SupSearch.SupplierSearch ObjSrchSupplier
    {
        get
        {
            if (objSrchSupplier == null)
            {
                objSrchSupplier = new SupSearch.SupplierSearch();
            }
            return objSrchSupplier;
        }
        set 
        {
            objSrchSupplier = value;
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
                this.SearchSupplier();
            }
            this.ShowHideControlsByUser();
            if (IsCallback)
            {
                if (Session["SearchSuppliers"] != null)
                {
                    dxgvSupplierList.DataSource = (DataSet)Session["SearchSuppliers"];
                    dxgvSupplierList.DataBind();
                }
                else
                {
                    this.SearchSupplier();
                }
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

    private void ShowHideControlsByUser()
    {
        try
        {
            short userrole = Master.LoggedUser.UserRoleID;

            switch (userrole)
            {
                case 1://SuperUser
                    dxgvSupplierList.Columns[0].Visible = true;
                    btnUpdate.Visible = true;
                    break;
                default:
                    dxgvSupplierList.Columns[0].Visible = false;
                    btnUpdate.Visible = false;
                    break;
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
            ListItem[] status = { new ListItem("All", "-1"), new ListItem("Active", "1"), new ListItem("InActive", "0") };
            ddlStatus.Items.AddRange(status);
        }
        catch(Exception ex)
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.SearchSupplier();
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

    /// <summary>
    /// Search suppliers with the given search criteria
    /// </summary>
    private void SearchSupplier()
    {
        try
        {
            ObjSrchSupplier.Sup_Code = txtSupplierCode.Text.Trim();
            ObjSrchSupplier.SupplierName = txtSupplierName.Text.Trim();
            ObjSrchSupplier.Option = Int32.Parse(ddlStatus.SelectedValue.Trim());
            DataSet dsSuppliers = ObjSrchSupplier.Search();
            if (dsSuppliers != null && dsSuppliers.Tables.Count > 0 && dsSuppliers.Tables[0].Rows.Count > 0)
            {

                dxgvSupplierList.DataSource = dsSuppliers;
                dxgvSupplierList.DataBind();
            }

            Session["SearchSuppliers"] = dsSuppliers;
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
            txtSupplierCode.Text = String.Empty;
            txtSupplierName.Text = String.Empty;
            txtContactPerson.Text = String.Empty;
            ddlStatus.SelectedValue = "-1";
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

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string strSupId = ((ImageButton)(sender)).CommandArgument.ToString();
            Response.Redirect("AddSupplier.aspx?SupplierId=" + strSupId + "&FromURL=SupplierSearch.aspx", false);
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

    protected void gvUserList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (Session["SearchSuppliers"] != null)
        {
            dxgvSupplierList.DataSource = (DataSet)Session["SearchSuppliers"];
            dxgvSupplierList.DataBind();
        }
        this.gveSupplierList.WriteXlsToResponse();
    }

    protected void dxgvSupplierList_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        try
        {
            //todo: validate here
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvSupplierList_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void dxgvSupplierList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            DataRow[] dr = ((DataSet)Session["SearchSuppliers"]).Tables[0].Select("SupId=" + e.Keys[0].ToString());
            if (dr.Length > 0)
            {

                decimal newValue;
                decimal oldValue;

                ///
                /// If old value was not numeric
                ///
                if (!Decimal.TryParse(e.OldValues["CreditAmmount"].ToString(), out oldValue))
                {
                    return;
                }

                if (Decimal.TryParse(e.NewValues["CreditAmmount"].ToString(), out newValue))
                {

                    dr[0]["CreditAmmount"] = e.NewValues["CreditAmmount"];

                    e.Cancel = true;
                    dxgvSupplierList.CancelEdit();
                    dxgvSupplierList.DataBind();
                }
                else
                {
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvSupplierList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)");
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
            if (Session["SearchSuppliers"] != null)
            {
                ds = (DataSet)Session["SearchSuppliers"];
            }

            if (new SupplierDAO().UpdateAllSuppliers(ds))
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
}
