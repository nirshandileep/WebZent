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
using LankaTiles.POManagement;
using LankaTiles.Exception;
using LankaTiles.Common;
using LankaTiles.SupplierManagement;
using System.Collections.Generic;
using DevExpress.Web.ASPxGridView;
using LankaTiles.CustomerManagement;
using DevExpress.Web.ASPxClasses.Internal;

public partial class PrintVoucherPopup : System.Web.UI.Page
{

    private Voucher objVoucher;

    public Voucher ObjVoucher
    {
        get
        {
            if (objVoucher == null)
            {
                if (Session["ObjVoucher"] == null)
                {
                    objVoucher = new Voucher();
                    objVoucher.VoucherID = Int32.Parse(hdnVoucherId.Value.Trim());
                    objVoucher.GetVoucherByID();

                    Session["ObjVoucher"] = objVoucher;
                }
                else
                {

                    objVoucher = (Voucher)Session["ObjVoucher"];
                }
            }
            return objVoucher;
        }
        set
        {
            objVoucher = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["ObjVoucher"] = null;
                this.CheckFromURL();
                this.GetVoucherId();
                this.LoadInitialData();
                this.SetData();
            }

            if (IsCallback)
            {
                DataSet ds;
                if (Session["VoucherPOList"] == null)
                {
                    ds = new PODAO().GetAllPayablePOs();
                    Session["VoucherPOList"] = ds;
                }
                else
                {
                    ds = (DataSet)Session["VoucherPOList"];
                }
                dxgvPODetails.DataSource = ds;
                dxgvPODetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    private void LoadInitialData()
    {
        try
        {
            if (ObjVoucher.DsVoucherDetails == null || ObjVoucher.DsVoucherDetails.Tables.Count == 0 || ObjVoucher.DsVoucherDetails.Tables[0].Rows.Count < 1)
            {


                //
                // PO
                //
                DataSet dsPO = (new PODAO()).GetAllPayablePOs();
                if (dsPO != null && dsPO.Tables.Count != 0)
                {
                    dxgvPODetails.DataSource = dsPO;
                    dxgvPODetails.DataBind();
                    Session["VoucherPOList"] = dsPO;
                }
            }
            else
            {
                //dxgvPODetails.Columns[0].Visible = false;
            }

            //
            // Suppliers
            //
            DataSet dsSuppliers = (new SupplierDAO()).GetAllSuppliers();
            if (dsSuppliers == null || dsSuppliers.Tables.Count == 0)
            {
                ddlSuppliers.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                ddlSuppliers.DataSource = dsSuppliers;
                ddlSuppliers.DataTextField = "SupplierName";
                ddlSuppliers.DataValueField = "SupId";
                ddlSuppliers.DataBind();
                //ddlSuppliers.Items.Add(new ListItem("--Please Select--", "-1"));
                //ddlSuppliers.SelectedValue = "-1";
            }

            //
            // Fill Customer Code
            //
            DataSet dsCustomers = new CustomerDAO().GetAllCustomers();
            if (dsCustomers == null || dsCustomers.Tables.Count == 0)
            {
                ddlCustomer.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                ddlCustomer.DataSource = dsCustomers;
                ddlCustomer.DataTextField = "Cus_Name";
                ddlCustomer.DataValueField = "CustomerID";
                ddlCustomer.DataBind();
                //ddlCustomer.Items.Add(new ListItem("--Please Select--", "-1"));
                //ddlCustomer.SelectedValue = "-1";
            }

            this.LoadAccountTypes();
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
            if (ObjVoucher.VoucherID > 0)
            {
                lblVoucherCode.Text = ObjVoucher.VoucherCode;
                ddlVoucherType.SelectedValue = ObjVoucher.VoucherTypeID.ToString();

                if (ObjVoucher.SupplierId.HasValue && ddlSuppliers.Items.FindByValue(ObjVoucher.SupplierId.ToString()) != null)
                {
                    ddlSuppliers.SelectedValue = ObjVoucher.SupplierId.ToString();
                    lblSupplier.Text = ddlSuppliers.SelectedItem.Text;
                }

                if (ObjVoucher.CustomerID.HasValue && ddlCustomer.Items.FindByValue(ObjVoucher.CustomerID.ToString()) != null)
                {
                    ddlCustomer.SelectedValue = ObjVoucher.CustomerID.ToString();
                    lblCustomer.Text = ddlCustomer.SelectedItem.Text;
                }

                //dxgvPODetails.Columns[0].SetColVisible(false);
                dxgvPODetails.DataSource = ObjVoucher.DsVoucherDetails;
                dxgvPODetails.DataBind();
                //dxgvPODetails.Selection.SelectAll();

                lblAmmount.Text = Math.Round(ObjVoucher.TotalAmount, 2).ToString();
                lblDescription.Text = ObjVoucher.Description.Trim();

                ddlPaymentType.SelectedValue = ObjVoucher.PaymentTypeID.ToString();
                lblPaymentType.Text = ddlPaymentType.SelectedItem.Text;

                lblBankName.Text = ObjVoucher.Bank.Trim();
                lblBranch.Text = ObjVoucher.BankBranch.Trim();
                lblChequeNo.Text = ObjVoucher.ChequeNumber.Trim();
                if (ObjVoucher.ChequeDate.HasValue)
                {
                    lblChequeDate.Text = ObjVoucher.ChequeDate.Value.ToShortDateString();
                }

                if (ObjVoucher.PaymentDate.HasValue)
                {
                    lblPaymentDate.Text = ObjVoucher.PaymentDate.Value.ToShortDateString();
                }

                if (ObjVoucher.AccountID.HasValue)
                {
                    ddlAccountTypes.SelectedValue = ObjVoucher.AccountID.Value.ToString();
                    lblAccount.Text = ddlAccountTypes.SelectedItem.Text;
                }

            }
            else
            {
                lblVoucherCode.Text = new VoucherDAO().GetNextVoucherCode();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void GetVoucherId()
    {
        try
        {
            if (Request.QueryString["VoucherID"] != null && Request.QueryString["VoucherID"].Trim() != String.Empty)
            {
                hdnVoucherId.Value = Request.QueryString["VoucherID"].Trim();
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

    private void LoadAccountTypes()
    {
        try
        {
            //
            // Fill Customer Code
            //
            DataSet dsAccountTypes = new VoucherDAO().GetAllAccountTypes();
            if (dsAccountTypes == null || dsAccountTypes.Tables.Count == 0)
            {
                ddlAccountTypes.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                ddlAccountTypes.DataSource = dsAccountTypes;
                ddlAccountTypes.DataTextField = "AccountName";
                ddlAccountTypes.DataValueField = "AccountID";
                ddlAccountTypes.DataBind();
                //ddlAccountTypes.Items.Add(new ListItem("--Please Select--", "-1"));
                //ddlAccountTypes.SelectedValue = "-1";
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
