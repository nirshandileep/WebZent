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
using LankaTiles.CustomerManagement;

public partial class PrintGRNPopUp : System.Web.UI.Page
{
    private PO objGRNPO;
    private GRN objGRN;

    /// <summary>
    /// The object for Purchase Order Related GRN's
    /// </summary>
    public PO ObjGRNPO
    {
        get
        {
            if (objGRNPO == null)
            {
                if (Session["ObjGRNPO"] == null)
                {
                    objGRNPO = new PO();
                    objGRNPO.POId = Int32.Parse(hdnPOId.Value.Trim() == String.Empty ? "0" : hdnPOId.Value.Trim());
                    objGRNPO.GetPOByID();
                }
                else
                {
                    objGRNPO = (PO)(Session["ObjGRNPO"]);
                }
            }
            return objGRNPO;
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
                    objGRN.GRNId = Int32.Parse(hdnGRNId.Value.Trim() == String.Empty ? "0" : hdnGRNId.Value.Trim());

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
                this.CheckIfEditGRN();
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
    /// Check if GRNId is passed to edit
    /// </summary>
    private void CheckIfEditGRN()
    {
        try
        {
            hdnGRNId.Value = ObjGRN.GRNId.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Load the data for GRN Edit / View
    /// </summary>
    private void SetData()
    {
        try
        {
            if (ObjGRN.POId.HasValue)
            {
                hdnGRNType.Value = "1";//PO used in client side
                lblGRNType.Text = "Purchase Order";

                int poId = ObjGRN.POId.Value;
                PO po = new PO();
                po.POId = poId;
                po.GetPOByID();

                lblSupplierName.Text = po.SupplierName;
            }
            else if (ObjGRN.InvId.HasValue)//Sales return  used in client side
            {
                hdnGRNType.Value = "2";
                lblGRNType.Text = "Sales Return";
                lblTitle.Text = "GRN - Credit Note";
            }

            lblGRNNo.Text = ObjGRN.GRNId.ToString();
            hdnPOId.Value = ObjGRN.POId.ToString().Trim();
            lblPOCode.Text = ObjGRNPO.POCode;
            lblPOAmount.Text = Decimal.Round(ObjGRNPO.POAmount, 2).ToString();
            lblDate.Text = ObjGRN.Rec_Date.ToString("dd-MMM-yyyy");
            if (ObjGRN.GRNInvoice.InvoiceNo != null)
            {
                lblInvoiceNo.Text = ObjGRN.GRNInvoice.InvoiceNo.Trim();

                Customer cust = new Customer();
                cust.CustomerID = ObjGRN.GRNInvoice.CustomerID.Value;
                cust.GetCustomerByID();
                lblCustomerName.Text = cust.Cus_Name;
                lblCustomerCode.Text = cust.CustomerCode;
            }
            else
            {
                lblInvoiceNo.Text = String.Empty;
            }
            lblInvoiceTotal.Text = Decimal.Round(ObjGRN.GRNInvoice.GrandTotal, 2).ToString();

            txtCreditNote.InnerHtml = ObjGRN.CreditNote.Trim();
            lblSupplierInvNo.Text = ObjGRN.SuplierInvNo.Trim();
            lblReceivedTotal.Text = Decimal.Round(ObjGRN.TotalAmount, 2).ToString();

            gvItemList.DataSource = ObjGRN.GRNItems;
            gvItemList.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
