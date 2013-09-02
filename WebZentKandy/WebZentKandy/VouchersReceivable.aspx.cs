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
using LankaTiles.Exception;
using LankaTiles.Common;
using LankaTiles.CustomerManagement;

public partial class VouchersReceivable : System.Web.UI.Page
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
                Master.ClearSessions();//Clear all sessions
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfEditVoucher();
                this.SetData();
                this.AddAttributes();
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
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private void AddAttributes()
    {
        txtChequeNumber.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        dxPrintInvoice.ContentUrl = String.Format("~/PrintVouchersReceivable.aspx?PaymentID={0}&FromURL=VouchersReceivable.aspx", hdnPaymentID.Value.Trim());
    }

    private void SetData()
    {
        try
        {
            Int64 voucherId;
            if (hdnPaymentID.Value != "0" && Int64.TryParse(hdnPaymentID.Value.Trim(),out voucherId))
            {
                VoucherRec.PaymentID = voucherId;
                
                lblPaymentCode.Text = VoucherRec.PaymentCode;
                trPaymentCode.Visible = true;

                ddlCustomerCode.SelectedValue = VoucherRec.CustomerID.ToString();
                ddlPaymentType.SelectedValue = ((int)VoucherRec.PaymentTypeId).ToString();
                dtpPaymentDate.Date = VoucherRec.PaymentDate;
                txtPaymentAmount.Text = VoucherRec.PaymentAmount.ToString();
                
                txtChequeNumber.Text = VoucherRec.ChequeNo.Trim();
                dtpChequeDate.Date = VoucherRec.ChequeDate;
                ddlCardType.SelectedValue = ((int)VoucherRec.CardType).ToString();
                txtComment.Text = VoucherRec.Comment.Trim();

                dxgvPaymentDetails.DataSource = VoucherRec.DsPaymentDetails;
                dxgvPaymentDetails.DataBind();

                dxgvPaymentDetails.Columns["#"].Visible = false;
                dxgvPaymentDetails.Columns["DueAmount"].Visible=false;
                dxgvPaymentDetails.Columns["GrandTotal"].Visible = false;

                btnCalculate.Visible = false;
                btnSave.Enabled = false;
                ddlCustomerCode.Enabled = false;
                ddlPaymentType.Enabled = false;
                dtpPaymentDate.Enabled = false;
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
                Master.BindDropdown("Cus_Name", "CustomerID", dsCustomers, ddlCustomerCode);
                ddlCustomerCode.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlCustomerCode.SelectedValue = "-1";
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

    protected void ddlCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Int32 customerId;
            if (Int32.TryParse(ddlCustomerCode.SelectedItem.Value.ToString(), out customerId))
            {
                DataSet ds = new VoucherRecievableDAO().GetReceivableInvoiceDetailsByCustomerID(customerId);
                this.ReceivableInvoiceDetails = ds;
                dxgvPaymentDetails.DataSource = ds;
                dxgvPaymentDetails.DataBind();
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlCustomerName_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    protected void dxgvPaymentDetails_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            DataRow[] dr = ReceivableInvoiceDetails.Tables[0].Select("InvoiceId=" + e.Keys[0].ToString());
            if (dr.Length > 0)
            {

                decimal newValue;
                decimal oldValue;

                ///
                /// If old value was not numeric
                ///
                if (!decimal.TryParse(e.OldValues["Amount"].ToString(), out oldValue))
                {
                    return;
                }

                if (decimal.TryParse(e.NewValues["Amount"].ToString(), out newValue))
                {
                    if (oldValue < newValue)
                    {
                        return;
                    }

                    dr[0]["Amount"] = e.NewValues["Amount"];
                }
                else
                {
                    return;
                }

                e.Cancel = true;
                dxgvPaymentDetails.CancelEdit();
                dxgvPaymentDetails.DataBind();

            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            lblError.Text = string.Empty;

            if (dxgvPaymentDetails.Selection.Count == 0)
            {
                lblError.Visible = true;
                lblError.Text = "Atleast one invoice must be selected";
                return;
            }
            txtPaymentAmount.Text = this.GetGridPaymentTotals().ToString();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCalculate_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            VoucherRec.PaymentAmount = Decimal.Parse(txtPaymentAmount.Text.Trim());

            if (VoucherRec.PaymentAmount != this.GetGridPaymentTotals())
            {
                lblError.Text = "Please calculate before saving, calculation mismatch";
                lblError.Visible = true;
                return;
            }

            #region Set Payment Type

            if ((int)Structures.PaymentTypes.CASH == Int32.Parse(ddlPaymentType.SelectedValue.Trim()))
            {
                VoucherRec.PaymentTypeId = Structures.PaymentTypes.CASH;
            }
            else if((int)Structures.PaymentTypes.CHEQUE == Int32.Parse(ddlPaymentType.SelectedValue.Trim()))
            {
                VoucherRec.PaymentTypeId = Structures.PaymentTypes.CHEQUE;
            }
            else if ((int)Structures.PaymentTypes.CREDIT_CARD == Int32.Parse(ddlPaymentType.SelectedValue.Trim()))
            {
                VoucherRec.PaymentTypeId = Structures.PaymentTypes.CREDIT_CARD;
            }

            #endregion

            if (VoucherRec.PaymentTypeId == Structures.PaymentTypes.CREDIT_CARD)
            {
                #region CC region

                VoucherRec.ChequeNo = txtChequeNumber.Text.Trim();//Set credit card number

                if ((int)Structures.CardTypes.AMERICAN_EXPRESS == Int32.Parse(ddlCardType.SelectedValue.Trim()))
                {
                    VoucherRec.CardType = Structures.CardTypes.AMERICAN_EXPRESS;
                }
                else if ((int)Structures.CardTypes.MASTER == Int32.Parse(ddlCardType.SelectedValue.Trim()))
                {
                    VoucherRec.CardType = Structures.CardTypes.MASTER;
                }
                else if ((int)Structures.CardTypes.VISA == Int32.Parse(ddlCardType.SelectedValue.Trim()))
                {
                    VoucherRec.CardType = Structures.CardTypes.VISA;
                }

                #endregion
            }
            else if (VoucherRec.PaymentTypeId == Structures.PaymentTypes.CHEQUE)
            {
                #region Cheque region

                VoucherRec.ChequeDate = dtpChequeDate.Date;
                VoucherRec.ChequeNo = txtChequeNumber.Text.Trim();//Set cheque number

                #endregion
            }

            VoucherRec.PaymentDate = dtpPaymentDate.Date;
            VoucherRec.Comment = txtComment.Text.Trim();
            VoucherRec.CreatedBy = Master.LoggedUser.UserId;
            VoucherRec.CustomerID = Int32.Parse(ddlCustomerCode.SelectedValue.Trim());
            VoucherRec.ModifiedBy = Master.LoggedUser.UserId;

            if (VoucherRec.PaymentID == 0)
            {
                VoucherRec.DsPaymentDetails = this.GetSelectedDataset();
            }

            if (VoucherRec.Save())
            {
                trPaymentCode.Visible = true;
                lblPaymentCode.Text = VoucherRec.PaymentCode;

                this.SetReadOnly();
                hdnPaymentID.Value = VoucherRec.PaymentID.ToString();
                VoucherRec.GetVoucherByID();
                dxgvPaymentDetails.DataSource = VoucherRec.DsPaymentDetails;
                dxgvPaymentDetails.DataBind();

                btnPrint.Visible = true;

                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                lblError.Visible = true;
                this.AddAttributes();
            }
            else 
            {
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                lblError.Visible = true;
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
        btnSave.Enabled = false;
        btnCalculate.Enabled = false;
        ddlCardType.Enabled = false;
        dtpPaymentDate.Enabled = false;
        ddlPaymentType.Enabled = false;

        txtChequeNumber.Enabled = false;
        txtComment.Enabled = false;
        dtpChequeDate.Enabled = false;
        dxgvPaymentDetails.Enabled = false;

    }
}
