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
using LankaTiles.ChequeManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class SearchChequeBooks : System.Web.UI.Page
{
    private DataSet dsResults;

    private DataSet ReportData 
    {
        get
        {
            if (dsResults == null)
            {
                if (Session["SearchChqSrchResults"] == null)
                {
                    dsResults = new ChequeBookDAO().GetAllChequeBooks();
                    Session["SearchChqSrchResults"] = dsResults;
                }
                else
                {
                    dsResults = (DataSet)Session["SearchChqSrchResults"];
                }
            }
            return dsResults;
        }
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();

                dxgvChequeBooks.DataSource = ReportData;
                dxgvChequeBooks.DataBind();
            }

            if (IsCallback)
            {
                dxgvChequeBooks.DataSource = ReportData;
                dxgvChequeBooks.DataBind();
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


    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ReportData != null)
            {
                dxgvChequeBooks.DataSource = this.ReportData;
                dxgvChequeBooks.DataBind();
            }
            this.gveChequeReport.WriteXlsxToResponse();
            gveChequeReport.Dispose();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnExport_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
}
