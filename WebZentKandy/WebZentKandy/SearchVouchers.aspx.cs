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
using LankaTiles.VoucherManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class SearchVouchers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.AddAttributes();

                Master.ClearSessions();//Clear all sessions
                DataSet dsSelectAllInv = this.Search();
                dxgvVouchers.DataSource = dsSelectAllInv;
                dxgvVouchers.DataBind();
                Session["SearchVoucher"] = dsSelectAllInv;
            }
            if (IsCallback)
            {
                if (Session["SearchVoucher"] != null)
                {
                    dxgvVouchers.DataSource = (DataSet)Session["SearchVoucher"];
                    dxgvVouchers.DataBind();
                }
                else
                {
                    dxgvVouchers.DataSource = new VoucherDAO().GetAll();
                    dxgvVouchers.DataBind();
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

    private void AddAttributes()
    {
        txtChqNoFrom.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        txtChqNoTo.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["SearchVoucher"] != null)
        {
            dxgvVouchers.DataSource = (DataSet)Session["SearchVoucher"];
            dxgvVouchers.DataBind();
        }
        this.gveVoucher.WriteXlsToResponse();
    }

    protected void btnClearSearch_Click(object sender, EventArgs e)
    {
        try
        {

            txtChqNoFrom.Text = String.Empty;
            txtChqNoTo.Text = String.Empty;
            dtpFromDate.Text = String.Empty;
            dtpToDate.Text = String.Empty;
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnClearSearch_Click(object sender, EventArgs e)");
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
            DataSet dsVoucher = Search();

            dxgvVouchers.DataSource = dsVoucher;
            dxgvVouchers.DataBind();
            Session["SearchVoucher"] = dsVoucher;
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

    private DataSet Search()
    {
        try
        {
            VoucherSearch search = new VoucherSearch();
            search.ChequeNumberFrom = txtChqNoFrom.Text.Trim();
            search.ChequeNumberTo = txtChqNoTo.Text.Trim();

            if (dtpFromDate.Text.Trim() == String.Empty)
            {
                search.ChequeDateFrom = DateTime.MinValue;
            }
            else
            {
                search.ChequeDateFrom = DateTime.Parse(dtpFromDate.Value.ToString());
            }

            if (dtpToDate.Text.Trim() == String.Empty)
            {
                search.ChequeDateTo = DateTime.MinValue;
            }
            else
            {
                search.ChequeDateTo = DateTime.Parse(dtpToDate.Value.ToString());
            }

            return new VoucherDAO().SearchVoucher(search);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }
}
