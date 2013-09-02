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
using LankaTiles.InvoiceManagement;
using LankaTiles.Exception;
using LankaTiles.Common;

public partial class PrintGatePass : System.Web.UI.Page
{
    private GetPass objGatePass;

    public GetPass ObjGatePass
    {
        get
        {
            if (objGatePass == null)
            {
                if (Session["ObjGatePass"] == null)
                {
                    objGatePass = new GetPass();
                    objGatePass.GPId = Int32.Parse(hdnGatePassId.Value.Trim());
                    objGatePass.GetGetPassByID();
                }
                else
                {
                    objGatePass = (GetPass)Session["ObjGatePass"];

                }
            }
            return objGatePass;
        }
        set { objGatePass = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["ObjGatePass"] = null;
                this.CheckFromURL();
                this.CheckIfEditGatePass();
                this.SetData();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    private void CheckIfEditGatePass()
    {
        try
        {
            if (Request.QueryString["GPId"] != null && Request.QueryString["GPId"].Trim() != String.Empty)
            {
                hdnGatePassId.Value = Request.QueryString["GPId"].Trim();
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
            if (ObjGatePass.GPId > 0)
            {
                lblGatepassCode.Text = ObjGatePass.GPCode;
                lblInvoiceNumber.Text = ObjGatePass.InvoiceNo;
                lblInvoiceAmount.Text = Decimal.Round(ObjGatePass.InvoiceAmmount, 2).ToString();

                gvItemList.DataSource = ObjGatePass.DsGatePassDetails;
                gvItemList.DataBind();
            }
        }
        catch (Exception ex)
        {

            throw ex;
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
                //hdnFromURL.Value = Request.QueryString["FromURL"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
