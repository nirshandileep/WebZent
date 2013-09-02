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
using LankaTiles.ItemsManagement;
using LankaTiles.Exception;
using LankaTiles.Common;
using LankaTiles.CustomerManagement;
using LankaTiles.LocationManagement;
using LankaTiles.UserManagement;
using LankaTiles.GRNManagement;

public partial class AddInvoice : System.Web.UI.Page
{

    private Invoice objInv;

    public Invoice ObjInv
    {
        get
        {
            if (objInv == null)
            {
                if (Session["ObjInv"] == null)
                {
                    objInv = new Invoice();
                    objInv.InvoiceId = Int32.Parse(hdnInvId.Value.Trim() == String.Empty ? "0" : hdnInvId.Value.Trim());

                    if (objInv.InvoiceId > 0)
                    {
                        chkIsHidden.Enabled = false;
                    }
                    objInv.GetInvoiceByInvoiceID();
                    Session["ObjInv"] = objInv;
                }
                else
                {
                    objInv = (Invoice)(Session["ObjInv"]);
                }
            }
            return objInv;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["SearchItems"] = null;
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfEditInv();
                this.CheckIfItemSelect();
                this.SetData();
                this.AddAttributes();
                this.ShowHideControlsByRole();
                if (ObjInv.InvoiceId == 0)
                {
                    btnQuotation.Visible = true;
                }
                else
                {
                    btnQuotation.Visible = false;
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

    /// <summary>
    /// Manage user rights by user role
    /// </summary>
    private void ShowHideControlsByRole()
    {
        try
        {

            if (Master.LoggedUser.UserRoleID == (int)Structures.UserRoles.Cashier)
            {
                btnCancel.Visible = false;
                trRemarks.Visible = false;
            }
            else
            {
                btnCancel.Visible = true;
                trRemarks.Visible = true;
            }

            switch (Master.LoggedUser.UserRoleID.ToString())
            {
                case "1":
                    if (ObjInv.InvoiceId>0)
                    {
                        btnSaveRep.Visible = true;
                        btnInvByChange.Visible = true;
                    }
                    break;
                case "7":
                    if (ObjInv.InvoiceId > 0)
                    {
                        btnSaveRep.Visible = true;
                        btnInvByChange.Visible = true;
                    }
                    break;
                default:
                    break;
            }

        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    /// <summary>
    /// Adds client side attributes to controls
    /// </summary>
    private void AddAttributes()
    {
        txtQuantity.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        txtChequeNumber.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        dxPrintInvoice.ContentUrl = String.Format("~/InvoicePrintPopup.aspx?InvoiceId={0}&FromURL=AddInvoice.aspx", hdnInvId.Value.Trim());
    }

    private void SetData()
    {
        try
        {
            if (hdnFromURL.Value.Trim() != "ProductSearch.aspx" && hdnFromURL.Value.Trim() != "SearchGroupItems.aspx")
            {
                Master.ClearSessions();//Clear all sessions
                ObjInv.BranchId = Master.LoggedUser.BranchID;
                txtInvNo.Text = new InvoiceDAO().GetNextCodeForInvoice(ObjInv);
            }

            //
            // Single Item from Search
            //
            if (hdnItemIdFromSearch.Value.Trim() != "0")
            {
                Int32 itemIdFS = Int32.Parse(hdnItemIdFromSearch.Value.Trim());

                Item objItem = new Item();
                objItem.ItemId = itemIdFS;
                objItem.GetItemByID();
                rblItemType.SelectedValue = "1";
                txtItemCode.Text = objItem.ItemCode.ToString();
                txtItemName.Text = objItem.ItemDescription.Trim();
                txtItemSelPrice.Text = Decimal.Round(objItem.SellingPrice, 2).ToString();
                hdnPriceBeforeDiscount.Value = txtItemSelPrice.Text.Trim();
                txtMinSellingPrice.Value = Decimal.Round(objItem.Cost, 2).ToString();
                txtAvailableQty.Value = (objItem.QuantityInHand - objItem.InvoicedQty).ToString();

                ddlCreditOption.SelectedValue = ObjInv.CreditOption.ToString();
                CreditOptionSelectChange();
            }

            //
            // Group Item from Search
            //
            if (hdnGroupIdFromSearch.Value.Trim() != "0")
            {
                Int32 itemIdFS = Int32.Parse(hdnGroupIdFromSearch.Value.Trim());

                Group objGItem = new Group();
                objGItem.GroupId = short.Parse(itemIdFS.ToString());
                objGItem.GetGroupByID();
                rblItemType.SelectedValue = "2";
                txtItemCode.Text = objGItem.GroupCode.Trim();
                txtItemName.Text = objGItem.GroupName.Trim();
                txtItemSelPrice.Text = Decimal.Round(objGItem.SellingPrice, 2).ToString();
                txtMinSellingPrice.Value = Decimal.Round(objGItem.Cost, 2).ToString();
                txtAvailableQty.Value = objGItem.QuantityInHand.ToString();

                ddlCreditOption.SelectedValue = ObjInv.CreditOption.ToString();
                CreditOptionSelectChange();
            }

            if (ObjInv.InvoiceId > 0 || hdnItemIdFromSearch.Value.Trim() != "0" || hdnGroupIdFromSearch.Value.Trim() != "0")
            {

                if (ObjInv.IsIssued || ObjInv.Status == Structures.InvoiceStatus.Cancelled)
                {
                    this.SetInvoiceReadOnly();
                }

                if (ObjInv.IsVoucherPaymentMade)
                {
                    btnCheckGRNId.Enabled = false;
                    txtPaidAmount.Enabled = false;
                    ddlPaymentType.Enabled = false;
                    trCreditOption.Visible = false;
                    trCreditOptionBel.Visible = false;
                }

                txtInvNo.Text = ObjInv.InvoiceNo;
                if (ObjInv.GRNId.HasValue)
                {
                    txtGRNId.Text = ObjInv.GRNId.ToString();
                    hdnGRNId.Value = ObjInv.GRNId.ToString();
                    txtCustomerCredBal.Text = ObjInv.CustDebitTotal.ToString();
                }

                ///
                /// Recall grn details for the validator if Invoice id is zero
                ///
                if (ObjInv.InvoiceId == 0)
                {
                    if (txtGRNId.Text.Trim() != string.Empty)
                    {
                        GRN receiveGoods = new GRN();
                        receiveGoods.GRNId = ObjInv.GRNId.Value;
                        Int32 custId = ObjInv.CustomerID.HasValue ? ObjInv.CustomerID.Value : 1;

                        if (new GRNDAO().GetGRNByIDAndCustId(receiveGoods, custId))
                        {
                            rvCustomerBal.MaximumValue = decimal.Round((receiveGoods.TotalAmount - receiveGoods.TotalPaid), 2).ToString();
                        }
                    }
                }

                if (ObjInv.I_in.HasValue && ObjInv.I_in == true)
                {
                    chkIsHidden.Checked = true;
                }
                else
                {
                    chkIsHidden.Checked = false;
                }
                //lblDate.Text = ObjInv.CreatedDate.ToShortDateString();
                dpDate.Date = ObjInv.CreatedDate;

                ddlPaymentType.SelectedValue = ObjInv.PaymentType;
                ddlCustomerCode.SelectedValue = ObjInv.CustomerID.HasValue ? ObjInv.CustomerID.ToString() : "1";
                ddlCardType.SelectedValue = Convert.ToInt16(ObjInv.CardType).ToString();

                //Fill customer data
                this.CustomerSelectChange();

                if (ObjInv.InvoiceId > 0)
                {
                    ddlInvoicedBy.SelectedValue = ObjInv.CreatedUser.ToString();
                    btnCheckGRNId.Enabled = false;
                    txtGRNId.Enabled = false;
                    rvCustomerBal.Enabled = false;

                    trCreditOption.Visible = false;
                    trCreditOptionBel.Visible = false;

                    dpDate.Enabled = false;
                }
                else
                {
                    ddlInvoicedBy.SelectedValue = Master.LoggedUser.UserId.ToString();
                }

                if (ObjInv.DsInvoiceDetails != null)
                {
                    gvItemList.DataSource = ObjInv.DsInvoiceDetails;
                    gvItemList.DataBind();
                }

                if (Decimal.Round((ObjInv.GrandTotal - ObjInv.DueAmount), 2) == 0)
                {
                    txtPaidAmount.Text = String.Empty;
                }
                else
                {
                    txtPaidAmount.Text = Decimal.Round((ObjInv.GrandTotal - ObjInv.DueAmount), 2).ToString();
                }

                lblBalancePayment.Text = Decimal.Round(ObjInv.DueAmount, 2).ToString();
                lblInvAmount.Text = ObjInv.GrandTotal.ToString();
                txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();

                txtChequeNumber.Text = ObjInv.ChequeNumber.ToString();
                if (ObjInv.ChequeDate != DateTime.MinValue)
                {
                    dxdcChequeDate.Value = ObjInv.ChequeDate;
                }
                
                if (ObjInv.InvoiceId > 0)
                {
                    tdPrintInvoice.Visible = true;
                    trRemarks.Visible = true;
                }
                else
                {
                    tdPrintInvoice.Visible = false;
                    trRemarks.Visible = false;
                }

                txtCancelNote.Value = ObjInv.Remarks.Trim();
            }

            if (ObjInv.InvoiceId == 0)
            {
                ddlInvoicedBy.SelectedValue = Master.LoggedUser.UserId.ToString();
            }

            this.ShowHideGrid();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Make all controls to read only mode
    /// </summary>
    private void SetInvoiceReadOnly()
    {
        try
        {
            ddlPaymentType.Enabled = false;
            ddlCustomerCode.Enabled = false;
            txtCustomerCredBal.ReadOnly = true;
            chkUseCredit.Enabled = false;
            txtChequeNumber.ReadOnly = true;
            dxdcChequeDate.Enabled = false;
            txtPaidAmount.ReadOnly = true;

            txtItemCode.ReadOnly = true;
            txtSearchItem.Enabled = false;
            txtQuantity.ReadOnly = true;
            btnSavePOItem.Enabled = false;
            txtTransferNote.Disabled = true;

            if (ObjInv.Status == Structures.InvoiceStatus.Cancelled)
            {
                gvItemList.Enabled = false;
            }

            txtCancelNote.Disabled = true;
            btnCancel.Enabled = false;
            btnQuotation.Visible = false;

            if (ObjInv.DueAmount <= ObjInv.GrandTotal)//need to recheck
            {
                txtPaidAmount.ReadOnly = false;
            }

            ddlInvoicedBy.Enabled = false;
            btnSaveRep.Enabled = false;
            btnInvByChange.Enabled = false;

            btnCheckGRNId.Enabled = false;

            trCreditOption.Visible = false;
            trCreditOptionBel.Visible = false;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    private void ShowHideGrid()
    {
        try
        {
            if (gvItemList.Rows.Count == 0)
            {
                trNoRecords.Visible = true;
                trGrid.Visible = false;
            }
            else
            {
                trNoRecords.Visible = false;
                trGrid.Visible = true;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void CheckIfItemSelect()
    {
        try
        {
            if (Request.QueryString["ItemId"] != null && Request.QueryString["ItemId"].Trim() != String.Empty)
            {
                hdnItemIdFromSearch.Value = Request.QueryString["ItemId"].Trim();
            }
            if (Request.QueryString["GroupId"] != null && Request.QueryString["GroupId"].Trim() != String.Empty)
            {
                hdnGroupIdFromSearch.Value = Request.QueryString["GroupId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void CheckIfEditInv()
    {
        try
        {
            if (Request.QueryString["InvoiceId"] != null && Request.QueryString["InvoiceId"].Trim() != String.Empty)
            {
                hdnInvId.Value = Request.QueryString["InvoiceId"].Trim();
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
            //lblDate.Text = DateTime.Now.ToShortDateString();
            dpDate.Date = DateTime.Now;

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

            //
            // Fill the discount dropdown
            //
            int max = Convert.ToInt32(Constant.MaximumDiscountAllowed_Invoice.Trim());
            decimal increment = Convert.ToDecimal(Constant.Increment_Seed.Trim());
            if (ddlDiscount.Items.Count == 0)
            {
                for (decimal i = 0.00M; i < max; )
                {
                    ddlDiscount.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    i = i + increment;
                }
            }

            //
            // If currently logged user is not admin cannot update paid amounts
            //
            if (hdnInvId.Value != "0")
            {
                if (Master.LoggedUser.UserRoleID > 1)
                {
                    txtPaidAmount.ReadOnly = true;
                }
            }

            //
            // Fill the user name
            //
            DataSet dsUsers = new UsersDAO().GetAllUsers();
            if (dsUsers == null || dsUsers.Tables.Count == 0)
            {
                ddlInvoicedBy.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("FirstName", "UserId", dsUsers, ddlInvoicedBy);
            }
        }
        catch (Exception ex)
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

    protected void chkIsHidden_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ObjInv.BranchId = Master.LoggedUser.BranchID;
            if (chkIsHidden.Checked)
            {
                txtInvNo.Text = new InvoiceDAO().GetNextCodeForHdnInvoice(ObjInv);
            }
            else
            {
                txtInvNo.Text = new InvoiceDAO().GetNextCodeForInvoice(ObjInv);
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void chkIsHidden_CheckedChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPaymentType.SelectedValue != "1")//Payment type cheque or credit card
            {
                if (ddlCustomerCode.Items.FindByValue("1") != null)
                {
                    ddlCustomerCode.Items.Remove(ddlCustomerCode.Items.FindByValue("1"));
                }
            }

            if (ddlPaymentType.SelectedValue != "2")//Payment type Cheque
            {
                dxdcChequeDate.Value = null;
                txtChequeNumber.Text = String.Empty;
            }

            if (ddlPaymentType.SelectedValue == "1")//Payment type Cash
            {
                if (ddlCustomerCode.Items.FindByValue("1") == null)
                {
                    ddlCustomerCode.Items.Add(new ListItem("Cash", "1"));
                }
            }
            else if (ddlPaymentType.SelectedValue == "2")//Payment type Cheque
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

    protected void txtSearchItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblItemType.SelectedValue == "1")//Single item
            {
                this.SearchItems();
            }
            else if (rblItemType.SelectedValue == "2")//Group Item
            {
                this.SearchGroupItems();
            }

            ddlDiscount.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void txtSearchItem_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Search group item by group item code
    /// </summary>
    private void SearchGroupItems()
    {
        try
        {
            Group objGItem = new Group();
            objGItem.GroupCode = txtItemCode.Text.Trim();
            objGItem.GetGroupByCode();

            if (objGItem.GroupId > 0)
            {

                //Check If Item Is Active
                if (!objGItem.IsActive)
                {
                    lblError.Visible = true;
                    lblError.Text = "The Group Item is not an active!";
                    return;
                }
                else
                {
                    lblError.Visible = false;
                }

                txtItemName.Text = objGItem.GroupName.Trim();
                txtMinSellingPrice.Value = Decimal.Round(objGItem.Cost, 2).ToString();
                txtItemSelPrice.Text = Decimal.Round(objGItem.SellingPrice, 2).ToString();
                hdnPriceBeforeDiscount.Value = txtItemSelPrice.Text.Trim();
            }
            else
            {
                txtItemName.Text = String.Empty;
                ObjInv.InvoiceNo = txtInvNo.Text.Trim();
                ObjInv.I_in = chkIsHidden.Checked;
                ObjInv.PaymentType = ddlPaymentType.SelectedValue.Trim();
                ObjInv.CreatedDate = DateTime.Now;
                ObjInv.CreatedUser = Master.LoggedUser.UserId;
                ObjInv.ChequeNumber = txtChequeNumber.Text.Trim();
                ObjInv.ChequeDate = dxdcChequeDate.Date;
                ObjInv.Remarks = txtCancelNote.Value.Trim();
                ObjInv.CreatedUser = Int32.Parse(ddlInvoicedBy.SelectedValue.Trim());
                ObjInv.CreditOption = Int32.Parse(ddlCreditOption.SelectedValue.Trim());

                Session["ObjInv"] = ObjInv;

                Response.Redirect("SearchGroupItems.aspx?FromURL=AddInvoice.aspx", false);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Search item by item code
    /// </summary>
    private void SearchItems()
    {
        try
        {

            Item objItem = new Item();
            objItem.ItemCode = txtItemCode.Text.Trim();
            objItem.GetItemsByItemCode();

            if (objItem.ItemId > 0)
            {
                //Check If Item Is Active
                if (!objItem.IsActive)
                {
                    lblError.Visible = true;
                    lblError.Text = "Item is not an active item!";
                    return;
                }
                else
                {
                    lblError.Visible = false;
                }

                txtItemName.Text = objItem.ItemDescription.Trim();
                txtItemSelPrice.Text = Decimal.Round(objItem.SellingPrice, 2).ToString();
                txtMinSellingPrice.Value = Decimal.Round(objItem.Cost, 2).ToString();
                txtAvailableQty.Value = (objItem.QuantityInHand - objItem.InvoicedQty).ToString();
                hdnPriceBeforeDiscount.Value = txtItemSelPrice.Text.Trim();
            }
            else
            {
                txtItemName.Text = String.Empty;
                ObjInv.InvoiceNo = txtInvNo.Text.Trim();
                ObjInv.I_in = chkIsHidden.Checked;
                ObjInv.CustomerID = Int32.Parse(ddlCustomerCode.SelectedValue.Trim());
                ObjInv.PaymentType = ddlPaymentType.SelectedValue.Trim();
                ObjInv.CreatedDate = DateTime.Now;
                ObjInv.CreatedUser = Master.LoggedUser.UserId;
                ObjInv.ChequeNumber = txtChequeNumber.Text.Trim();
                ObjInv.ChequeDate = dxdcChequeDate.Date;
                ObjInv.Remarks = txtCancelNote.Value.Trim();
                ObjInv.CreatedUser = Int32.Parse(ddlInvoicedBy.SelectedValue.Trim());
                ObjInv.CreditOption = Int32.Parse(ddlCreditOption.SelectedValue.Trim());

                if (hdnGRNId.Value != "0" && hdnGRNId.Value != string.Empty)
                {
                    ObjInv.GRNId = long.Parse(hdnGRNId.Value.Trim());
                    ObjInv.CustDebitTotal = Decimal.Parse(txtCustomerCredBal.Text.Trim() == string.Empty ? "0" : txtCustomerCredBal.Text.Trim());
                }
                else
                {
                    ObjInv.GRNId = null;
                    ObjInv.CustDebitTotal = 0;
                }
                //ObjInv.DsInvoiceDetails = (DataSet)gvItemList.DataSource;

                Session["ObjInv"] = ObjInv;
                Response.Redirect("ProductSearch.aspx?FromURL=AddInvoice.aspx", false);
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Delete detail item from grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            try
            {
                Int32 temp = e.RowIndex;
                String itemId = gvItemList.DataKeys[temp]["ItemId"].ToString();
                String GroupId = gvItemList.DataKeys[temp]["GroupId"].ToString();
                DataRow[] dr = null;

                if (itemId!=string.Empty)
                {
                    dr = ObjInv.DsInvoiceDetails.Tables[0].Select("ItemId=" + gvItemList.DataKeys[temp]["ItemId"].ToString());    
                }
                else if (GroupId!=string.Empty)
                {
                    dr = ObjInv.DsInvoiceDetails.Tables[0].Select("GroupId=" + gvItemList.DataKeys[temp]["GroupId"].ToString());    
                }
                
                dr[0].Delete();

                gvItemList.DataSource = ObjInv.DsInvoiceDetails;
                gvItemList.DataBind();

                txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();

                Session["ObjInv"] = ObjInv;
                this.CalculateInvAmounts();
                this.ShowHideGrid();
            }
            catch (Exception ex)
            {
                ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)");
                if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                    Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
                else
                    Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Add item to details section
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSavePOItem_Click(object sender, EventArgs e)
    {
        try
        {

            if (rblItemType.SelectedValue == "1")
            {
                this.InsertItemToGrid();
            }
            else if (rblItemType.SelectedValue == "2")
            {
                this.InsertGroupItemToGrid();
            }

            txtItemCode.Text = String.Empty;
            txtQuantity.Text = String.Empty;
            txtItemName.Text = String.Empty;
            txtItemSelPrice.Text = String.Empty;
            hdnItemId.Value = "0";
            ddlDiscount.SelectedIndex = 0;

            this.CalculateInvAmounts();
            this.ShowHideGrid();
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            //ddlCustomerCode_SelectedIndexChanged(ddlCustomerCode, EventArgs.Empty);

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSavePOItem_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Adds the item to the details grid if item already not exists
    /// </summary>
    private void InsertItemToGrid()
    {
        Item objItem = new Item();
        objItem.ItemCode = txtItemCode.Text.Trim();
        objItem.GetItemsByItemCode();

        //Check If Item Is Active
        if (!objItem.IsActive)
        {
            lblError.Visible = true;
            lblError.Text = "Item is not an active item!";
            return;
        }
        else
        {
            lblError.Visible = false;
        }

        if (objItem.ItemId > 0)
        {
            Int32 qty = Int32.Parse(txtQuantity.Text.Trim());
            Decimal lineCost = (Decimal)qty * Decimal.Parse(txtItemSelPrice.Text.Trim());

            if (hdnItemId.Value == "0")
            {
                //Add new Item
                if (this.IsItemExistInGrid())
                {
                    lblError.Text = String.Format(Constant.MSG_PO_ItemAlreadyExistInPO, txtItemCode.Text.Trim());
                    lblError.Visible = true;
                    return;
                }
                else
                {
                    lblError.Visible = false;
                    lblError.Text = String.Empty;
                }

                DataRow newRow = ObjInv.DsInvoiceDetails.Tables[0].NewRow();
                newRow["ItemId"] = objItem.ItemId.ToString();
                newRow["ItemCode"] = objItem.ItemCode;
                newRow["ItemDescription"] = objItem.ItemDescription.Trim();
                newRow["Quantity"] = Int32.Parse(txtQuantity.Text.Trim());
                newRow["Price"] = txtItemSelPrice.Text.Trim();
                newRow["TotalPrice"] = lineCost;
                newRow["GroupId"] = DBNull.Value;
                newRow["DiscountPerUnit"] = Decimal.Round(Convert.ToDecimal(ddlDiscount.SelectedValue.Trim()), 0);

                newRow.EndEdit();
                ObjInv.DsInvoiceDetails.Tables[0].Rows.Add(newRow);
            }
            else
            {

                //Update existing
                DataView dvUpdate = ObjInv.DsInvoiceDetails.Tables[0].DefaultView;

                dvUpdate.Sort = "ItemId";
                DataRowView[] dr = dvUpdate.FindRows(hdnItemId.Value.Trim());

                dr[0]["ItemId"] = objItem.ItemId.ToString();
                dr[0]["ItemCode"] = objItem.ItemCode;
                dr[0]["ItemDescription"] = objItem.ItemDescription.Trim();
                dr[0]["Quantity"] = Int32.Parse(txtQuantity.Text.Trim());
                dr[0]["Price"] = txtItemSelPrice.Text.Trim();
                dr[0]["TotalPrice"] = lineCost;

                gvItemList.DataSource = ObjInv.DsInvoiceDetails;
                gvItemList.DataBind();

                txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
                this.ShowHideGrid();
            }

            gvItemList.DataSource = ObjInv.DsInvoiceDetails;
            gvItemList.DataBind();

        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Item code " + txtItemCode.Text.Trim() + " does not exist!";
            return;
        }

        Session["ObjInv"] = ObjInv;
        return;
    }

    /// <summary>
    /// Saves searched group item details to line item grid
    /// </summary>
    private void InsertGroupItemToGrid()
    {
        try
        {
            Group objGroupItem = new Group();
            objGroupItem.GroupCode = txtItemCode.Text.Trim();
            objGroupItem.GetGroupByCode();

            //Check If Item Is Active
            if (!objGroupItem.IsActive)
            {
                lblError.Visible = true;
                lblError.Text = "Item is not an active item!";
                return;
            }
            else
            {
                lblError.Visible = false;
            }

            //if (objGroupItem.IsBillable)
            //{
            //    lblError.Visible = false;
            //}
            //else
            //{
            //    lblError.Visible = true;
            //    lblError.Text = "Quantity is of related items in the group are insufficient to continue the purchase!.";
            //    return;
            //}

            if (objGroupItem.GroupId > 0)
            {
                Int32 qty = Int32.Parse(txtQuantity.Text.Trim());
                Decimal lineCost = (Decimal)qty * Decimal.Parse(txtItemSelPrice.Text.Trim());

                if (hdnItemId.Value == "0")//Not an item edit
                {
                    //Add new Item
                    if (this.IsItemExistInGrid())
                    {
                        lblError.Text = String.Format(Constant.MSG_PO_ItemAlreadyExistInPO, txtItemCode.Text.Trim());
                        lblError.Visible = true;
                        return;
                    }
                    else
                    {
                        lblError.Visible = false;
                        lblError.Text = String.Empty;
                    }

                    DataRow newRow = ObjInv.DsInvoiceDetails.Tables[0].NewRow();
                    newRow["ItemId"] = DBNull.Value;
                    newRow["ItemCode"] = objGroupItem.GroupCode.Trim();
                    newRow["ItemDescription"] = objGroupItem.GroupName.Trim();
                    newRow["Quantity"] = Int32.Parse(txtQuantity.Text.Trim());
                    newRow["Price"] = txtItemSelPrice.Text.Trim();
                    newRow["TotalPrice"] = lineCost;
                    newRow["GroupId"] = objGroupItem.GroupId.ToString();
                    newRow["DiscountPerUnit"] = Decimal.Round(Convert.ToDecimal(ddlDiscount.SelectedValue.Trim()), 0);

                    newRow.EndEdit();
                    ObjInv.DsInvoiceDetails.Tables[0].Rows.Add(newRow);
                }
                else
                {

                    //Update existing
                    DataView dvUpdate = ObjInv.DsInvoiceDetails.Tables[0].DefaultView;

                    dvUpdate.Sort = "ItemId";
                    DataRowView[] dr = dvUpdate.FindRows(hdnItemId.Value.Trim());

                    dr[0]["ItemId"] = DBNull.Value;
                    dr[0]["ItemCode"] = objGroupItem.GroupCode.Trim();
                    dr[0]["ItemDescription"] = objGroupItem.GroupName.Trim();
                    dr[0]["Quantity"] = Int32.Parse(txtQuantity.Text.Trim());
                    dr[0]["Price"] = txtItemSelPrice.Text.Trim();
                    dr[0]["TotalPrice"] = lineCost;
                    //dr[0]["GroupId"] = objGroupItem.GroupId.ToString();

                    gvItemList.DataSource = ObjInv.DsInvoiceDetails;
                    gvItemList.DataBind();

                    txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
                    this.ShowHideGrid();
                }

                gvItemList.DataSource = ObjInv.DsInvoiceDetails;
                gvItemList.DataBind();

            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Item code " + txtItemCode.Text.Trim() + " does not exist!";
                return;
            }

            Session["ObjInv"] = ObjInv;
            return;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Calculate invoice ammounts
    /// </summary>
    private void CalculateInvAmounts()
    {
        try
        {
            Decimal cb = 0;
            Decimal total = 0;
            Decimal paid = Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim());
            Decimal due = 0;

            if (txtCustomerCredBal.Text.Trim() != String.Empty)
            {
                cb = Decimal.Parse(txtCustomerCredBal.Text.Trim());
                if (chkUseCredit.Checked)
                {
                    paid += cb;
                }
            }

            foreach (GridViewRow row in gvItemList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    total += Decimal.Parse(row.Cells[5].Text.ToString().Trim() == String.Empty ? "0" : row.Cells[5].Text.ToString().Trim());
                }
            }

            lblInvAmount.Text = total.ToString();
            due = total - paid;
            lblBalancePayment.Text = due.ToString();

            ObjInv.GrandTotal = total;
            ObjInv.DueAmount = due;
            

            Session["ObjInv"] = ObjInv;

        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Check if item has already exists in the details grid
    /// </summary>
    /// <returns>True is exist</returns>
    private bool IsItemExistInGrid()
    {
        try
        {
            DataView dvCount = ObjInv.DsInvoiceDetails.Tables[0].DefaultView;
            dvCount.Sort = "ItemCode";

            int length = -1;
            length = dvCount.Find(txtItemCode.Text.Trim());
            if (length != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private bool ValidateCustomerAndAmmountToSave()
    {
        bool success = true;
        try
        {

            Decimal invAmountLimit = Decimal.Parse(Constant.Invoice_Max_Amount_Without_Customer);
            string[] CustomerList = Constant.Invoice_Omitted_Customers_Above_Limit.Split(',');
            int customerSelected = int.Parse(ddlCustomerCode.SelectedValue);

            if (ObjInv.GrandTotal >= invAmountLimit)
            {
                for (int cust = 0; cust < CustomerList.GetLength(0); cust++)
                {
                    if (customerSelected.ToString() == CustomerList[cust].Trim())
                    {
                        success = false;
                        lblError.Text = "Cannot save invoice to selected customer!";
                        lblError.Visible = true;
                    }
                }
            }
            
            //customerList = Constant.Invoice_Omitted_Customers_Above_Limit.
            
        }
        catch (Exception ex)
        {

            throw ex;
        }
        return success;
    }

    /// <summary>
    /// Add or Update invoice details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            //Validate customer and amount, save if only true
            if (ValidateCustomerAndAmmountToSave() == false)
            {
                return;
            }
            lblError.Text = "No Transfer Generated";
            txtTransferNote.Value = "No Transfer Generated";

            ObjInv.InvoiceNo = txtInvNo.Text.Trim();
            ObjInv.CustomerID = Int32.Parse(ddlCustomerCode.SelectedValue.Trim());
            ObjInv.GrandTotal = Decimal.Parse(lblInvAmount.Text.Trim());
            ObjInv.BranchId = Master.LoggedUser.BranchID;
            ObjInv.PaymentType = ddlPaymentType.SelectedValue;
            ObjInv.PaidAmount = Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim());
            ObjInv.Date = dpDate.Date;

            decimal customerCreditTotal = 0;
            customerCreditTotal = decimal.Parse(txtCustomerCredBal.Text.Trim() != string.Empty ? txtCustomerCredBal.Text.Trim() : "0");

            //if (ddlCreditOption.SelectedValue != "-1")
            //{
            if (ddlCreditOption.SelectedValue == "1" && hdnInvId.Value == "0" && customerCreditTotal < ObjInv.PaidAmount)
            {
                lblError.Text = "Cannot pay more than credit ammount of the customer, please only use less than or equal to customer credit amount";
                lblError.Visible = true;
                return;
            }

            switch (ddlCreditOption.SelectedValue)
            {
                case "1"://Customer credit amount
                    ObjInv.CustDebitTotal = ObjInv.PaidAmount;//Decimal.Parse(txtCustomerCredBal.Text.Trim() != String.Empty ? txtCustomerCredBal.Text.Trim() : "0");
                    ObjInv.DueAmount = ObjInv.GrandTotal - ObjInv.PaidAmount;// (Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim()));
                    break;
                case "2"://GRN Id
                    if (hdnGRNId.Value != "0")
                    {
                        ObjInv.GRNId = Int64.Parse(hdnGRNId.Value.Trim());
                        ObjInv.CustDebitTotal = ObjInv.PaidAmount;// Decimal.Parse(txtCustomerCredBal.Text.Trim() != String.Empty ? txtCustomerCredBal.Text.Trim() : "0");
                        ObjInv.DueAmount = ObjInv.GrandTotal - (Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim()));
                    }
                    else
                    {
                        ObjInv.GRNId = null;
                        ObjInv.CustDebitTotal = 0;
                        ObjInv.DueAmount = ObjInv.GrandTotal - Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim());
                    }

                    break;
                default:
                    ObjInv.GRNId = null;
                    ObjInv.CustDebitTotal = 0;
                    ObjInv.DueAmount = ObjInv.GrandTotal - Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim());
                    break;
            }
            //}
            //else
            //{
            //    ObjInv.GRNId = null;
            //    ObjInv.CustDebitTotal = 0;
            //    ObjInv.DueAmount = ObjInv.GrandTotal - Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim());
            //}

            if (ObjInv.GrandTotal < ObjInv.CustDebitTotal)
            {
                lblError.Text = "Customer return total cannot be greater than Invoice total!";
                lblError.Visible = true;
                return;
            }

            if (ObjInv.CustDebitTotal > ObjInv.PaidAmount)
            {
                lblError.Text += " Customer return total cannot be than paid ammount!";
                lblError.Visible = true;
                return;
            }

            if (ObjInv.DueAmount > 0)
            {
                ObjInv.IsPaid = false;
            }
            else
            {
                ObjInv.IsPaid = true;
            }

            ObjInv.ChequeNumber = txtChequeNumber.Text.Trim();
            if (dxdcChequeDate.Date.ToString() == String.Empty)
            {
                ObjInv.ChequeDate = DateTime.MinValue;
            }
            else
            {
                ObjInv.ChequeDate = dxdcChequeDate.Date;
            }
            ObjInv.Remarks = txtCancelNote.Value.Trim();

            ObjInv.I_in = chkIsHidden.Checked;

            ObjInv.CreatedUser = Master.LoggedUser.UserId;
            ObjInv.ModifiedUser = Master.LoggedUser.UserId;

            if (ddlCardType.SelectedValue == "1")
            {
                ObjInv.CardType = Structures.CardTypes.MASTER;
            }
            else if (ddlCardType.SelectedValue == "2")
            {
                ObjInv.CardType = Structures.CardTypes.VISA;
            }
            else if (ddlCardType.SelectedValue == "3")
            {
                ObjInv.CardType = Structures.CardTypes.AMERICAN_EXPRESS;
            }

            if (ObjInv.Save())
            {
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                lblError.Visible = true;

                hdnInvId.Value = ObjInv.InvoiceId.ToString();
                txtInvNo.Text = ObjInv.InvoiceNo.Trim();
                chkIsHidden.Enabled = false;
                tdPrintInvoice.Visible = true;

                if (Master.LoggedUser.UserRoleID > 1)
                {
                    txtPaidAmount.ReadOnly = true;
                }

                this.AddAttributes();
                this.CalculateInvAmounts();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                hdnInvId.Value = "0";
                chkIsHidden.Enabled = false;
            }

            this.ShowHideControlsByRole();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddInvoice_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Generates the transfer details for the invoice
    /// </summary>
    /// <returns></returns>
    private bool GenerateTransferNote()
    {
        try
        {
            bool result = true;
            foreach (DataRow dr in ObjInv.DsInvoiceDetails.Tables[0].Rows)
            {
                if (dr["ItemId"] == null)//If Group item
                {
                    continue;
                }
                Int32 itemId = Int32.Parse(dr["ItemId"].ToString().Trim());
                
                Item item = new Item();
                item.ItemId = itemId;
                item.GetItemsByItemIDAndBranchID(Int32.Parse(Constant.MainStoreId));//Gets stock for main store only
                Int32 itemCount = Int32.Parse(dr["Quantity"].ToString().Trim());
                Int32 TransferQty = (Int32)item.QuantityInHand - (item.InvoicedQty + itemCount);
                if (TransferQty >= 0)
                {
                    continue;
                }
                else
                {
                    TransferQty = TransferQty * -1;// Make TransferQty possitive value
                }

                LocationStocks locStock = new LocationStocks();
                locStock.ItemId = itemId;

                DataSet dsItemSummary = new LocationsDAO().GetLocationStocksByItem(locStock);

                foreach (DataRow drIS in dsItemSummary.Tables[0].Rows)
                {
                    Int32 branchId = Int32.Parse(drIS["BranchId"].ToString().Trim());
                    if (branchId.ToString()==Constant.MainStoreId)
                    {
                        continue;
                    }
                    Int32 Stock = Int32.Parse(drIS["QuantityInHand"].ToString().Trim());
                    if (Stock - TransferQty >= 0)
                    {
                        txtTransferNote.Value += String.Format(Constant.MSG_Invoice_Transfer_Details, item.ItemCode, drIS["BranchCode"].ToString().Trim(), TransferQty.ToString());
                        break;
                    }
                    else
                    {
                        txtTransferNote.Value += String.Format(Constant.MSG_Invoice_Transfer_Details, item.ItemCode, drIS["BranchCode"].ToString().Trim(), Stock.ToString());
                        TransferQty = TransferQty - Stock;
                        continue;
                    }
                }

                if (TransferQty > 0)
                {
                    result = false;
                    break;
                }
                else
                {
                    result = true;
                }
            }
            return result;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Invoice print
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrintInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("InvoicePrintPopup.aspx?FromURL=AddInvoice.aspx&InvoiceId=" + hdnInvId.Value.Trim(), false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnPrintInvoice_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Invoice Quotation print
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnQuotation_Click(object sender, EventArgs e)
    {
        try
        {

            ObjInv.CustomerID = Int32.Parse(ddlCustomerCode.SelectedValue.Trim());
            ObjInv.BranchId = Master.LoggedUser.BranchID;
            ObjInv.PaymentType = ddlPaymentType.SelectedValue;
            ObjInv.DueAmount = ObjInv.GrandTotal - Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim());

            if (ObjInv.DueAmount > 0)
            {
                ObjInv.IsPaid = false;
            }
            else
            {
                ObjInv.IsPaid = true;
            }

            ObjInv.I_in = chkIsHidden.Checked;
            ObjInv.CreatedUser = Master.LoggedUser.UserId;
            ObjInv.CreatedDate = DateTime.Now;

            ClientScript.RegisterStartupScript(this.GetType(), "Invoice", "<script language='javascript'>LoadInvoiceQuotation('FromURL=AddInvoice.aspx?InvoiceId=" + hdnInvId.Value.Trim() + "');</script>");
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ddlCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    if (ddlCustomerCode.SelectedValue != "1")
        //    {
        //        Customer cus = new Customer();
        //        cus.CustomerID = Int32.Parse(ddlCustomerCode.SelectedValue.Trim());
        //        cus.GetCustomerByID();
        //        if (cus.Cus_CreditTotal < 0)
        //        {
        //            chkUseCredit.Enabled = true;
        //        }
        //        else
        //        {
        //            chkUseCredit.Enabled = false;
        //        }
        //        txtCustomerCredBal.Text = Decimal.Round(cus.Cus_CreditTotal, 2).ToString();
                
        //    }
        //    else
        //    {
        //        txtCustomerCredBal.Text = "0";
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlCustomerCode_SelectedIndexChanged(object sender, EventArgs e)");
        //    if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
        //        Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
        //    else
        //        Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        //}
    }

    /// <summary>
    /// Cancel invoice
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ObjInv.Status == Structures.InvoiceStatus.Created || ObjInv.Status == Structures.InvoiceStatus.Printed ||
                ObjInv.Status == Structures.InvoiceStatus.Pending)
            {

                ObjInv.Remarks = txtCancelNote.Value.Trim();
                ObjInv.Status = Structures.InvoiceStatus.Cancelled;
                if (ObjInv.CancelInvoice())
                {
                    lblError.Visible = true;
                    lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Cannot cancel Invoice in current status";
            }

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

    protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try 
        {
            if (hdnPriceBeforeDiscount.Value.Trim() == string.Empty)
            {
                return;
            }
            Decimal discount = Convert.ToDecimal(ddlDiscount.SelectedValue.Trim());
            Decimal price = Decimal.Round(Convert.ToDecimal(hdnPriceBeforeDiscount.Value.Trim()),2);
            if (discount > 0)
            {
                txtItemSelPrice.Text = Decimal.Round((price * (1 - discount / 100)), 2).ToString();
            }
            else
            {
                txtItemSelPrice.Text = price.ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void gvItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((System.Data.DataRowView)(e.Row.DataItem)).Row["IssuedQTY"].ToString().Trim() != String.Empty)
                {
                    e.Row.ControlStyle.BackColor = System.Drawing.Color.LightSlateGray;
                    e.Row.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    /// <summary>
    /// Change the invoice created by after the invoice is created
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveRep_Click(object sender, EventArgs e)
    {
        try
        {
            if (ObjInv.InvoiceId > 0)
            {
                ObjInv.CreatedUser = Int32.Parse(ddlInvoicedBy.SelectedValue.Trim());
                new InvoiceDAO().UpdateSalesRep(ObjInv);    
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSaveRep_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnCheckGRNId_Click(object sender, EventArgs e)
    {
        try
        {
            //if (hdnGRNId.Value == "0")
            //{
                if (txtGRNId.Text.Trim() != string.Empty)
                {
                    lblGRNError.Visible = false;

                    GRN receiveGoods = new GRN();
                    receiveGoods.GRNId = Int64.Parse(txtGRNId.Text.Trim());
                    
                    Int32 custId = 0;
                    custId = Int32.Parse(ddlCustomerCode.SelectedValue.Trim());

                    if(new GRNDAO().GetGRNByIDAndCustId(receiveGoods,custId))
                    {
                        txtCustomerCredBal.Text = decimal.Round((receiveGoods.TotalAmount - receiveGoods.TotalPaid),2).ToString();
                        hdnGRNId.Value = receiveGoods.GRNId.ToString();
                        ObjInv.GRNId = receiveGoods.GRNId;
                        txtPaidAmount.Text = decimal.Round((receiveGoods.TotalAmount - receiveGoods.TotalPaid), 2).ToString();

                        ddlCreditOption.SelectedValue = "2";//GRN
                        ddlCreditOption.Enabled = false;

                        rvCustomerBal.Enabled = true;
                        rvCustomerBal.MaximumValue = decimal.Round((receiveGoods.TotalAmount - receiveGoods.TotalPaid), 2).ToString();
                    }
                    else
                    {
                        lblGRNError.Text = "Incorrect GRNId";
                        lblGRNError.Visible = true;
                        rvCustomerBal.Enabled = false;
                        ddlCreditOption.Enabled = true;
                    }
                }
            //}
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnCheckGRNId_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void ddlCustomerCode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            CustomerSelectChange();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlCustomerCode_SelectedIndexChanged1(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Populate customer details by customer
    /// </summary>
    private void CustomerSelectChange()
    {
        try
        {
            if (ddlCustomerCode.SelectedValue != "-1")
            {
                Customer cust = new Customer();
                cust.CustomerID = int.Parse(ddlCustomerCode.SelectedValue);
                cust.GetCustomerByID();

                txtCus_Adress.Text = cust.Cus_Address;
                txtGRNIds.Text = cust.GRNIds;

                if (hdnInvId.Value == "0")
                {
                    //txtCustomerCredBal.Text = Decimal.Round(cust.Cus_CreditTotal, 2).ToString();
                }
            }
            else
            {
                txtCus_Adress.Text = "";
                txtCustomerCredBal.Text = "0";
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ddlCreditOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CreditOptionSelectChange();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlCreditOption_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Credit option select change method
    /// </summary>
    private void CreditOptionSelectChange()
    {
        try
        {
            hdnGRNId.Value = "0";
            txtGRNId.Text = "";
            btnCheckGRNId.Enabled = false;
            txtCustomerCredBal.Text = "0";
            rvCustomerBal.MinimumValue = "0";
            rvCustomerBal.Enabled = true;

            switch (ddlCreditOption.SelectedValue.ToString())
            {
                case "-1"://Select Option
                    break;
                case "1"://Customer
                    Customer cust = new Customer();
                    cust.CustomerID = int.Parse(ddlCustomerCode.SelectedValue);
                    cust.GetCustomerByID();
                    rvCustomerBal.Enabled = false;
                    if (hdnInvId.Value == "0")
                    {
                        if (cust.Cus_CreditTotal < 0)
                        {
                            //rvCustomerBal.MinimumValue = (cust.Cus_CreditTotal).ToString();

                            //decimal.Round((receiveGoods.TotalAmount - receiveGoods.TotalPaid), 2).ToString();
                        }
                        txtCustomerCredBal.Text = Decimal.Round(cust.Cus_CreditTotal, 2).ToString();
                    }

                    break;
                case "2"://GRN
                    btnCheckGRNId.Enabled = true;
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
