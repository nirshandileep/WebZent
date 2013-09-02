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
using LankaTiles.LocationManagement;
using LankaTiles.ChequeManagement;
using DevExpress.Web.ASPxEditors;

public partial class AddVoucher : System.Web.UI.Page
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
                Master.ClearSessions();//Clear all sessions
                this.CheckFromURL();
                this.GetVoucherId();
                this.LoadInitialData();
                this.SetData();
                this.AddAttributes();
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
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private void AddAttributes()
    {
        txtChequeNumber.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        txtAmmount.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        ddlVoucherType.Attributes.Add("onchange", "return OnVoucherTypeSelectChange();");
        dxPrintVoucher.ContentUrl = String.Format("~/PrintVoucherPopup.aspx?VoucherID={0}&FromURL=AddVoucher.aspx", hdnVoucherId.Value.Trim());
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
                dxgvPODetails.Columns[0].Visible = false;
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
                Master.BindDropdown("SupplierName", "SupId", dsSuppliers, ddlSuppliers);
                ddlSuppliers.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlSuppliers.SelectedValue = "-1";
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
                Master.BindDropdown("Cus_Name", "CustomerID", dsCustomers, ddlCustomer);
                ddlCustomer.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlCustomer.SelectedValue = "-1";
            }

            //
            // Add Branch
            // 
            DataSet dsRoles = (new LocationsDAO()).GetAllBranches();
            if (dsRoles == null || dsRoles.Tables.Count == 0)
            {
                ddlBranch.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("BranchCode", "BranchId", dsRoles, ddlBranch);
                ddlBranch.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlBranch.SelectedValue = "-1";
            }

            //
            // Add Branch
            // 
            DataSet dsCheques = (new ChequeBookDAO()).GetAllAvailableCheques();
            if (dsRoles == null || dsRoles.Tables.Count == 0)
            {
                ddlChequeNumbers.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("ChequeNo", "ChqId", dsCheques, ddlChequeNumbers);
                ddlChequeNumbers.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlChequeNumbers.SelectedValue = "-1";
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
                ddlChequeNumbers.Visible = false;
                txtChequeNumber.Visible = true;

                txtVoucherCode.Text = ObjVoucher.VoucherCode;
                ddlVoucherType.SelectedValue = ObjVoucher.VoucherTypeID.ToString();

                if (ObjVoucher.SupplierId.HasValue && ddlSuppliers.Items.FindByValue(ObjVoucher.SupplierId.ToString()) != null)
                {
                    ddlSuppliers.SelectedValue = ObjVoucher.SupplierId.ToString();
                }

                if (ObjVoucher.CustomerID.HasValue && ddlCustomer.Items.FindByValue(ObjVoucher.CustomerID.ToString()) != null)
                {
                    ddlCustomer.SelectedValue = ObjVoucher.CustomerID.ToString();
                }

                if (ObjVoucher.BranchId.HasValue)
                {
                    ddlBranch.SelectedValue = ObjVoucher.BranchId.Value.ToString();
                }

                //dxgvPODetails.Columns[0].SetColVisible(false);
                dxgvPODetails.DataSource = ObjVoucher.DsVoucherDetails;
                dxgvPODetails.DataBind();
                dxgvPODetails.Selection.SelectAll();

                txtAmmount.Text = Math.Round(ObjVoucher.TotalAmount, 2).ToString();
                txtDescription.Text = ObjVoucher.Description.Trim();
                ddlPaymentType.SelectedValue = ObjVoucher.PaymentTypeID.ToString();
                ddlBankName.SelectedValue = ObjVoucher.Bank.Trim();
                ddlBranchLocation.SelectedValue = ObjVoucher.BankBranch.Trim();
                txtChequeNumber.Text = ObjVoucher.ChequeNumber.Trim();
                if (ObjVoucher.ChequeDate.HasValue)
                {
                    dtpChequeDate.Date = ObjVoucher.ChequeDate.Value;    
                }

                if (ObjVoucher.PaymentDate.HasValue)
                {
                    dtpPaymentDate.Date = ObjVoucher.PaymentDate.Value;
                }

                if (ObjVoucher.AccountID.HasValue)
                {
                    ddlAccountTypes.SelectedValue = ObjVoucher.AccountID.Value.ToString();
                }

                if (ObjVoucher.BranchId.HasValue)
                {
                    ddlBranch.SelectedValue = ObjVoucher.BranchId.ToString();
                }

                this.ReadOnlyMode();
            }
            else
            {
                txtVoucherCode.Text = new VoucherDAO().GetNextVoucherCode();

                ddlChequeNumbers.Visible = true;
                txtChequeNumber.Visible = false;
            }

            if (hdnVoucherId.Value != "0")
            {
                btnPrintVoucher.Visible = true;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void ReadOnlyMode()
    {
        try
        {
            txtVoucherCode.Enabled = false;
            ddlVoucherType.Enabled = false;
            ddlSuppliers.Enabled = false;
            ddlCustomer.Enabled = false;
            dxgvPODetails.Enabled = false;
            txtAmmount.Enabled = false;
            txtDescription.Enabled = false;
            ddlPaymentType.Enabled = false;
            ddlBankName.Enabled = false;
            ddlBranchLocation.Enabled = false;
            txtChequeNumber.Enabled = false;
            dtpChequeDate.Enabled = false;
            btnCalculate.Enabled = false;
            btnSave.Enabled = false;
            dtpPaymentDate.Enabled = false;
            ddlAccountTypes.Enabled = false;
            btnAddAccount.Visible = false;
            ddlChequeNumbers.Enabled = false;
            ddlBranch.Enabled = false;
            RequiredFieldValidator4.Enabled = false;
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

    protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVoucherType.SelectedValue=="1")//PO
            {
                btnCalculate.Visible = true;
            }
            else if (ddlVoucherType.SelectedValue == "2")//Supplier
            {
                btnCalculate.Visible = false;
                txtAmmount.ReadOnly = false;
            }
            else//other
            {
                btnCalculate.Visible = false;
                txtAmmount.ReadOnly = false;
            }

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlVoucherType_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void ddlPOCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddlPOCode.SelectedValue.Trim()!="-1")
            //{
            //    PO po = new PO();
            //    po.POId = Int32.Parse(ddlPOCode.SelectedValue.Trim());
            //    po.GetPOByID();
            //    txtAmmount.Text = Decimal.Round(po.POAmount,2).ToString();
            //    txtAmmount.ReadOnly = true;
            //}
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlPOCode_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void Grid_HtmlCommandCellPrepared(object sender, ASPxGridViewTableCommandCellEventArgs e)
    {
        if (hdnVoucherId.Value == "0")
        {
            return;
        }

        if (e.CommandCellType == GridViewTableCommandCellType.Data)
        {
            if (e.KeyValue.ToString() == "0")
                foreach (Control control in e.Cell.Controls)
                {
                    InternalWebControl checkBox = control as InternalWebControl;
                    if (checkBox != null)
                    {
                        checkBox.GetType();
                        //((checkBox.GetType())).Checked = true;
                        checkBox.Enabled = false;
                    }

                }
        }
    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPaymentType.SelectedValue.Trim() != "-1")
            {

            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Decimal total = 0;

            //
            // Validate Ammount
            //
            if (!Decimal.TryParse(txtAmmount.Text.Trim(), out total))
            {
                lblError.Visible = true;
                lblError.Text = "Error in total!";
                return;
            }

            int paymentType;
            
            //
            // Validate payment type
            //
            if (!int.TryParse(ddlPaymentType.SelectedValue.Trim(),out paymentType))
            {
                lblError.Visible = true;
                lblError.Text = "Select Payment Type!";
                return;
            }
           
            if (ddlVoucherType.SelectedValue == "1")//PO
            {
                #region if po selected
                //One PO to allow multiple 
                if (dxgvPODetails.Selection.Count == 1)
                {
                    if (total > this.CalculateGridAmmounts())
                    {
                        lblError.Visible = true;
                        lblError.Text = "Cannot pay more than GRN Due!";
                        return;
                    }
                    else
                    {
                        for (int i = 0; i < dxgvPODetails.VisibleRowCount; i++)
                        {
                            if (dxgvPODetails.Selection.IsRowSelected(i))
                            {
                                //DataRow dr = dsVouchers.Tables[0].NewRow();
                                DataRow dr = ObjVoucher.DsVoucherDetails.Tables[0].NewRow();
                                dr.BeginEdit();

                                if (dxgvPODetails.GetRowValues(i, "GRNTotal") != null)
                                {
                                    dr["POId"] = dxgvPODetails.GetRowValues(i, "POId").ToString();
                                    dr["GRNId"] = dxgvPODetails.GetRowValues(i, "GRNId").ToString();//new
                                    dr["Amount"] = Decimal.Parse(txtAmmount.Text.Trim());
                                    dr.EndEdit();
                                    ObjVoucher.DsVoucherDetails.Tables[0].Rows.Add(dr);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (dxgvPODetails.Selection.Count > 1)
                {
                    if (total != this.CalculateGridAmmounts())
                    {
                        lblError.Visible = true;
                        lblError.Text = "Total of selected GRN's should be equal to voucher total!";
                        return;
                    }
                    else
                    {
                        Decimal temp = 0;
                        //Decimal count = 0;
                        String supplierName = "";
                        Int16 selectedRowCount = 0;

                        for (int i = 0; i < dxgvPODetails.VisibleRowCount; i++)
                        {
                            if (dxgvPODetails.Selection.IsRowSelected(i))
                            {
                                selectedRowCount++;
                                if (i == 0 && ObjVoucher.DsVoucherDetails.Tables[0].Rows.Count > 0)
                                {
                                    ObjVoucher.DsVoucherDetails.Tables[0].Rows.Clear();
                                }
                                DataRow dr = ObjVoucher.DsVoucherDetails.Tables[0].NewRow();
                                dr.BeginEdit();
                                if (dxgvPODetails.GetRowValues(i, "GRNTotal") != null)
                                {
                                    if (selectedRowCount > 1)
                                    {
                                        if (supplierName != dxgvPODetails.GetRowValues(i, "SupplierName").ToString().Trim())
                                        {
                                            lblError.Text = "Cannot pay to different suppliers through single voucher!";
                                            lblError.Visible = true;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        supplierName = dxgvPODetails.GetRowValues(i, "SupplierName").ToString().Trim();
                                    }
                                    dr["POId"] = dxgvPODetails.GetRowValues(i, "POId").ToString();
                                    dr["GRNId"] = dxgvPODetails.GetRowValues(i, "GRNId").ToString();//new
                                    temp = Decimal.Parse(dxgvPODetails.GetRowValues(i, "GRNTotal").ToString());
                                    dr["Amount"] = temp;
                                    //count += temp;
                                }
                                dr.EndEdit();
                                ObjVoucher.DsVoucherDetails.Tables[0].Rows.Add(dr);
                            }
                        }
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "Atleast 1 PO should be selected!";
                    return;
                }

                ObjVoucher.VoucherTypeID = (Int32)Structures.VoucherTypes.PURCHASE_ORDERS;
                #endregion
            }
            else if (ddlVoucherType.SelectedValue == "2")//Supplier
            {
                #region if supplier selected
                if (ddlSuppliers.SelectedValue != "-1")
                {
                    ObjVoucher.SupplierId = Int32.Parse(ddlSuppliers.SelectedValue.Trim());
                }
                else
                {
                    return;
                }

                ObjVoucher.VoucherTypeID = (Int32)Structures.VoucherTypes.SUPPLIERS;
                #endregion
            }
            else if (ddlVoucherType.SelectedValue == "3")//customers
            {
                #region if customer selected
                if (ddlCustomer.SelectedValue.Trim() != "-1" && 
                    ddlCustomer.SelectedValue.Trim() != string.Empty)
                {
                    ObjVoucher.CustomerID = Int32.Parse(ddlCustomer.SelectedValue.Trim());
                }
                else
                {
                    return;
                }
                ObjVoucher.VoucherTypeID = (Int32)Structures.VoucherTypes.CUSTOMERS;
                #endregion
            }
            else if (ddlVoucherType.SelectedValue == "0")//other
            {
                ObjVoucher.SupplierId = null;
                if (Int32.Parse(ddlAccountTypes.SelectedValue.Trim()) > 0)
                {
                    ObjVoucher.AccountID = Int32.Parse(ddlAccountTypes.SelectedValue.Trim());
                }
                ObjVoucher.VoucherTypeID = (Int32)Structures.VoucherTypes.OTHER;
            }

            ObjVoucher.VoucherCode = txtVoucherCode.Text.Trim();
            ObjVoucher.TotalAmount = total;
            ObjVoucher.Description = txtDescription.Text.Trim();
            ObjVoucher.CreatedBy = Master.LoggedUser.UserId;
            ObjVoucher.CreatedDate = DateTime.Now;
            ObjVoucher.PaymentTypeID = paymentType;

            if (dtpPaymentDate.Date == null && dtpPaymentDate.Text.Trim() == String.Empty)
            {
                ObjVoucher.PaymentDate = null;
            }
            else
            {
                ObjVoucher.PaymentDate = DateTime.Parse(dtpPaymentDate.Value.ToString());
            }

            if (ddlBranch.SelectedValue != "-1")
            {
                ObjVoucher.BranchId = Int32.Parse(ddlBranch.SelectedValue.Trim());
            }
            else
            {
                ObjVoucher.BranchId = null;
            }

            if (ddlPaymentType.SelectedValue.Trim() != "1")//Cheque & CC
            {
                if (ddlChequeNumbers.SelectedValue == "-1")
                {
                    lblError.Visible = true;
                    lblError.Text = "Please select a valid Cheque number";
                    return;
                }
                ObjVoucher.ChequeNumber = ddlChequeNumbers.SelectedItem.Text; //txtChequeNumber.Text.Trim();
                ObjVoucher.ChqId = Int32.Parse(ddlChequeNumbers.SelectedValue.ToString());
                ObjVoucher.Bank = ddlBankName.SelectedValue.Trim();
                ObjVoucher.BankBranch = ddlBranchLocation.SelectedValue.Trim();
                if (dtpChequeDate.Date == null || dtpChequeDate.Text.Trim() == String.Empty)
                {
                    ObjVoucher.ChequeDate = null;
                }
                else
                {
                    ObjVoucher.ChequeDate = DateTime.Parse(dtpChequeDate.Value.ToString());
                }
            }
            else//Cash
            {
                ObjVoucher.ChequeNumber = String.Empty;
                ObjVoucher.ChequeDate = null;
                ObjVoucher.Bank = String.Empty;
                ObjVoucher.BankBranch = String.Empty;
            }

            if (ObjVoucher.Add())
            {
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                lblError.Visible = true;
                btnSave.Enabled = false;
                hdnVoucherId.Value = ObjVoucher.VoucherID.ToString();
                this.AddAttributes();
            }
            else
            {
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                lblError.Visible = true;
            }

            if (hdnVoucherId.Value != "0")
            {
                btnPrintVoucher.Visible = true;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx", false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCancel_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void dxgvPODetails_SelectionChanged(object sender, EventArgs e)
    {
        try
        {
            //Do not count if saved previously
            if (hdnVoucherId.Value != "0")
            {
                return;
            }

            Decimal count = 0;
            for (int i = 0; i < dxgvPODetails.VisibleRowCount; i++)
            {
                if (dxgvPODetails.Selection.IsRowSelected(i))
                {
                    if (dxgvPODetails.GetRowValues(i, "GRNTotal") != null)
                    {
                        count += Decimal.Parse(dxgvPODetails.GetRowValues(i, "GRNTotal").ToString());
                    }
                }
            }
            txtAmmount.Text = count.ToString();
            if (dxgvPODetails.Selection.Count == 1)
            {
                txtAmmount.ReadOnly = false;
            }
            else if (dxgvPODetails.Selection.Count > 1)
            {
                txtAmmount.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvPODetails_SelectionChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void dxgvPODetails_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs e)
    {
        try
        {
            

            
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvPODetails_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAfterPerformCallbackEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    #region not needed
    List<object> selectedValues;

    private void GetSelectedValues()
    {
        List<string> fieldNames = new List<string>();
        foreach (GridViewColumn column in dxgvPODetails.Columns)
            if (column is GridViewDataColumn)
                fieldNames.Add(((GridViewDataColumn)column).FieldName);
        selectedValues = dxgvPODetails.GetSelectedFieldValues(fieldNames.ToArray());
    }

    private void PrintSelectedValues()
    {
        if (selectedValues == null) return;
        string result = "";
        foreach (object[] item in selectedValues)
        {
            foreach (object value in item)
            {
                result += string.Format("{0}&nbsp;&nbsp;&nbsp;&nbsp;", value);
            }
            result += "</br>";
        }
        //Literal1.Text = result;
    }
    #endregion

    protected void dxgvPODetails_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //string country = e.Parameters.ToString();
        //grid.Selection.UnselectAll();
        for (int i = 0; i < dxgvPODetails.VisibleRowCount; i++)
        {
            if (dxgvPODetails.GetRowValues(i, "BalanceAmount") != null)
            {

            }
        }
    }
    protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSuppliers.SelectedValue != "-1")
            {
                Supplier sup = new Supplier();
                sup.SupId = Int32.Parse(ddlSuppliers.SelectedValue.Trim());
                sup.GetSupplierByID();
                txtAmmount.Text = Decimal.Round(sup.CreditAmmount, 2).ToString();
                btnCalculate.Visible = false;
            }
            else
            {
                txtAmmount.Text = String.Empty;
                btnCalculate.Visible = false;
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
            Decimal count;
            count = CalculateGridAmmounts();
            
            txtAmmount.Text = count.ToString();
            if (dxgvPODetails.Selection.Count == 1)
            {
                txtAmmount.ReadOnly = false;
            }
            else if (dxgvPODetails.Selection.Count > 1)
            {
                txtAmmount.ReadOnly = true;
            }
        }
        catch (Exception ex)
        { 

            throw ex;
        }
    }

    /// <summary>
    /// Calculates the selected items and set total ammount
    /// </summary>
    private Decimal CalculateGridAmmounts()
    {
        try
        {
            Decimal count = 0;
            for (int i = 0; i < dxgvPODetails.VisibleRowCount; i++)
            {
                if (dxgvPODetails.Selection.IsRowSelected(i))
                {
                    if (dxgvPODetails.GetRowValues(i, "GRNTotal") != null)
                    {
                        count += Decimal.Parse(dxgvPODetails.GetRowValues(i, "GRNTotal").ToString());
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

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Customer cus = new Customer();
            if (ddlCustomer.Items.Count > 0 && ddlCustomer.SelectedValue.Trim() != "-1")
            {
                if (ddlCustomer.SelectedValue.Trim() != "1")//cash customer
                {
                    cus.CustomerID = Int32.Parse(ddlCustomer.SelectedValue.Trim());
                    cus.GetCustomerByID();
                    Decimal creditSum = Decimal.Round(cus.Cus_CreditTotal, 2);
                    if (creditSum < 0)//has a debit total
                    {
                        creditSum = Decimal.Negate(creditSum);
                        txtAmmount.Text = creditSum.ToString();
                        txtAmmount.ReadOnly = true;
                    }
                    else
                    {
                        txtAmmount.Text = "Cannot Pay";
                        txtAmmount.ReadOnly = true;
                    }
                }
                else
                {
                    txtAmmount.ReadOnly = false;
                    txtAmmount.Text = String.Empty;
                }
            }
            else
            {
                txtAmmount.ReadOnly = false;
            }

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
        }
    }

    protected void btnAddAccount_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlAccountTypes.Visible)
            {
                ddlAccountTypes.Visible = false;
                rfvAccountReq.Enabled = true;
                txtAccountType.Visible = true;
                txtAccountType.Text = string.Empty;
                CustomValidator3.Enabled = false;
                btnAddAccount.Text = "Save";
            }
            else
            {
                if (new VoucherDAO().AddAccountTypes(txtAccountType.Text.Trim()))//save
                {
                    ddlAccountTypes.Visible = true;
                    rfvAccountReq.Enabled = false;
                    txtAccountType.Visible = false;
                    txtAccountType.Text = string.Empty;
                    CustomValidator3.Enabled = true;
                    btnAddAccount.Text = "Add New"; 
                    this.LoadAccountTypes();
                }
                else
                {
                    this.LoadAccountTypes();
                }
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
                Master.BindDropdown("AccountName", "AccountID", dsAccountTypes, ddlAccountTypes);
                ddlAccountTypes.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlAccountTypes.SelectedValue = "-1";
            }
            
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    protected void dxgvPODetails_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            DataSet dsVoucherDetails = null;
            DataView dvVoucherDetails = null;
            if (Session["VoucherPOList"] != null)
            {
                dsVoucherDetails = (DataSet)Session["VoucherPOList"];
                dvVoucherDetails = dsVoucherDetails.Tables[0].DefaultView;
                int rowid = -1;
                dvVoucherDetails.Sort = "GRNId";
                rowid = dvVoucherDetails.Find(e.Keys[0].ToString());

                if (rowid > -1)
                {
                    decimal newValue;
                    decimal oldValue;

                    ///
                    /// If old value was not numeric
                    ///
                    if (!decimal.TryParse(e.OldValues["GRNTotal"].ToString(), out oldValue))
                    {
                        return;
                    }

                    if (decimal.TryParse(e.NewValues["GRNTotal"].ToString(), out newValue))
                    {
                        if (oldValue < newValue)
                        {
                            return;
                        }

                        dvVoucherDetails[rowid]["GRNTotal"] = e.NewValues["GRNTotal"];
                        Session["VoucherPOList"] = dsVoucherDetails;
                    }
                    else
                    {
                        return;
                    }

                    e.Cancel = true;
                    dxgvPODetails.CancelEdit();
                    dxgvPODetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ddlChequeNumbers_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //Set the bank and branch details
            ddlBankName.SelectedValue = "";
            ddlBranchLocation.SelectedValue = "";

            Int32 chqId = Int32.Parse(ddlChequeNumbers.SelectedValue.ToString());
            Cheques chq = new Cheques();
            chq.ChqId = chqId;
            chq.GetChequeById();

            ddlBankName.SelectedValue = chq.Bank;
            ddlBranchLocation.SelectedValue = chq.BankBranch;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
