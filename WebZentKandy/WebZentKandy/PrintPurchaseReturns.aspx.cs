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
using LankaTiles.POManagement;
using LankaTiles.GRNManagement;
using LankaTiles.Exception;
using LankaTiles.Common;

public partial class PrintPurchaseReturns : System.Web.UI.Page
{

    private GRN objGRN;
    private PurchaseReturns purchaseReturn;

    public PurchaseReturns PurchaseReturn
    {
        get
        {
            if (purchaseReturn == null)
            {
                if (Session["ObjPurchaseReturn"] == null)
                {
                    purchaseReturn = new PurchaseReturns();
                    purchaseReturn.PRId = Int32.Parse(hdnPRId.Value.Trim() == String.Empty ? "0" : hdnPRId.Value.Trim());
                    purchaseReturn.GetPurchaseReturnsByPRNId();
                }
                else
                {
                    purchaseReturn = (PurchaseReturns)(Session["ObjPurchaseReturn"]);
                }
            }
            return purchaseReturn;
        }
    }

    /// <summary>
    /// GRN Object used for all transactions within the page
    /// </summary>
    public GRN ObjGRN
    {
        get
        {
            if (objGRN == null)
            {
                if (Session["ObjGRN"] == null)
                {
                    objGRN = new GRN();
                    objGRN.GRNId = Int32.Parse(hdnPRId.Value.Trim() == String.Empty ? "0" : hdnPRId.Value.Trim());

                    objGRN.GetGRNByID();
                    Session["ObjGRN"] = objGRN;
                }
                else
                {
                    objGRN = (GRN)(Session["ObjGRN"]);
                }
            }
            return objGRN;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["ObjPurchaseReturn"] = null;
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfEditPR();
                this.SetData();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    /// <summary>
    /// Load the data for GRN Edit / View
    /// </summary>
    private void SetData()
    {
        try
        {

            if (hdnPRId.Value != "0")
            {
                if (PurchaseReturn.PRId > 0)
                {

                    lblPRCode.Text = PurchaseReturn.PRCode;
                    lblGRNId.Text = PurchaseReturn.GRNId.ToString();
                    lblDate.Text = PurchaseReturn.ReturnDate.ToShortDateString();
                    lblPOCode.Text = PurchaseReturn.POCode;
                    lblSupInvNo.Text = PurchaseReturn.SuplierInvNo;
                    lblSupplierName.Text = PurchaseReturn.SupplierName;
                    lblInvoiceTotal.Text = Decimal.Round(purchaseReturn.TotalReturns, 2).ToString();
                    
                    gvItemList.DataSource = PurchaseReturn.DsReturnDetails;
                    gvItemList.DataBind();
                }
                lblReturnNote.Text = PurchaseReturn.Comment.Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Check if GRNId is passed to edit
    /// </summary>
    private void CheckIfEditPR()
    {
        try
        {
            if (Request.QueryString["PRId"] != null && Request.QueryString["PRId"].Trim() != String.Empty)
            {
                hdnPRId.Value = Request.QueryString["PRId"].Trim();
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
            // CLEAR SESSIONS
            Session["ObjGRN"] = null;
            Session["ObjGRNPO"] = null;
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
                hdnFromURL.Value = Request.QueryString["FromURL"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
