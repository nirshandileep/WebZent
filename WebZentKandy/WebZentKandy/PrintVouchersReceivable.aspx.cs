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
using LankaTiles.CustomerManagement;

public partial class PrintVouchersReceivable : System.Web.UI.Page
{
    private LankaTiles.VoucherManagement.VoucherRecievable voucherRec;

    public LankaTiles.VoucherManagement.VoucherRecievable VoucherRec
    {
        get
        {
            if (voucherRec == null)
            {
                if (Session["VouchersReceivable"] != null)
                {
                    voucherRec = (LankaTiles.VoucherManagement.VoucherRecievable)Session["VouchersReceivable"];
                }
                else
                {
                    voucherRec = new LankaTiles.VoucherManagement.VoucherRecievable();
                    Int64 voucherId = Int64.Parse(hdnPaymentID.Value.Trim());
                    voucherRec.PaymentID = voucherId;
                    voucherRec.GetVoucherByID();

                    Session["VouchersReceivable"] = voucherRec;
                }
            }
            return voucherRec;
        }
        set
        {
            voucherRec = value;
            Session["VouchersReceivable"] = value;
        }
    }

    private DataSet ReceivableInvoiceDetails
    {
        get
        {
            if (Session["ReceivableInvoiceDetails"] != null)
            {
                return (DataSet)Session["ReceivableInvoiceDetails"];
            }
            else
            {
                return null;
            }
        }
        set
        {
            Session["ReceivableInvoiceDetails"] = value;
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfEditVoucher();
                this.SetData();
            }
            if (IsCallback)
            {
                if (ReceivableInvoiceDetails != null)
                {
                    dxgvPaymentDetails.DataSource = ReceivableInvoiceDetails;
                    dxgvPaymentDetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    private void SetData()
    {
        try
        {
            Int64 voucherId;
            if (hdnPaymentID.Value != "0" && Int64.TryParse(hdnPaymentID.Value.Trim(), out voucherId))
            {
                VoucherRec.PaymentID = voucherId;

                lblPaymentCode.Text = VoucherRec.PaymentCode;
                trPaymentCode.Visible = true;

                ddlPaymentType.SelectedValue = ((int)VoucherRec.PaymentTypeId).ToString();
                lblPaymentType.Text = VoucherRec.PaymentTypeId.ToString();

                ddlCustomerCode.SelectedValue = VoucherRec.CustomerID.ToString();
                lblCustomerName.Text = ddlCustomerCode.SelectedItem.Text;
                lblPaymentDate.Text = VoucherRec.PaymentDate.ToShortDateString();
                lblPaymentAmount.Text = Decimal.Round(VoucherRec.PaymentAmount,2).ToString();

                lblCardNo.Text = VoucherRec.ChequeNo.Trim();
                lblChqDate.Text = VoucherRec.ChequeDate.ToShortDateString();
                lblCardType.Text = VoucherRec.CardType.ToString();
                lblComment.Text = VoucherRec.Comment.Trim();

                dxgvPaymentDetails.DataSource = VoucherRec.DsPaymentDetails;
                dxgvPaymentDetails.DataBind();

                dxgvPaymentDetails.Columns["DueAmount"].Visible = false;
                dxgvPaymentDetails.Columns["GrandTotal"].Visible = false;

                ddlCustomerCode.Enabled = false;
                ddlPaymentType.Enabled = false;
                btnPrint.Visible = true;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void CheckIfEditVoucher()
    {
        try
        {
            if (Request.QueryString["PaymentID"] != null && Request.QueryString["PaymentID"].Trim() != String.Empty)
            {
                hdnPaymentID.Value = Request.QueryString["PaymentID"].Trim();
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
            // Fill Customer Code
            //
            DataSet dsCustomers = new CustomerDAO().GetAllCustomers();
            if (dsCustomers == null || dsCustomers.Tables.Count == 0)
            {
                ddlCustomerCode.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                ddlCustomerCode.DataSource = dsCustomers;
                ddlCustomerCode.DataTextField = "Cus_Name";
                ddlCustomerCode.DataValueField = "CustomerID";
                ddlCustomerCode.DataBind();
                //ddlCustomerCode.Items.Add(new ListItem("--Please Select--", "-1"));
                //ddlCustomerCode.SelectedValue = "-1";
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
                hdnFromURL.Value = Request.QueryString["FromURL"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private decimal GetGridPaymentTotals()
    {
        try
        {
            Decimal count = 0;
            for (int i = 0; i < dxgvPaymentDetails.VisibleRowCount; i++)
            {
                if (dxgvPaymentDetails.Selection.IsRowSelected(i))
                {
                    if (dxgvPaymentDetails.GetRowValues(i, "Amount") != null)
                    {
                        count += Decimal.Parse(dxgvPaymentDetails.GetRowValues(i, "Amount").ToString());
                    }
                }
            }
            return count;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private DataSet GetSelectedDataset()
    {
        try
        {
            DataSet ds = VoucherRec.DsPaymentDetails.Clone();

            for (int i = 0; i < dxgvPaymentDetails.VisibleRowCount; i++)
            {
                if (dxgvPaymentDetails.Selection.IsRowSelected(i))
                {
                    if (dxgvPaymentDetails.GetRowValues(i, "Amount") != null)
                    {
                        DataRow dr = ds.Tables[0].NewRow();
                        dr.BeginEdit();
                        dr["InvoiceId"] = Int32.Parse(dxgvPaymentDetails.GetRowValues(i, "InvoiceId").ToString());
                        dr["Amount"] = Decimal.Parse(dxgvPaymentDetails.GetRowValues(i, "Amount").ToString());
                        dr["PaymentDetailID"] = Int64.Parse(dxgvPaymentDetails.GetRowValues(i, "PaymentDetailID").ToString());
                        ds.Tables[0].Rows.Add(dr);
                        dr.EndEdit();
                    }
                }
            }
            return ds;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void SetReadOnly()
    {
        ddlCardType.Enabled = false;
        ddlPaymentType.Enabled = false;
        dxgvPaymentDetails.Enabled = false;
    }
}
