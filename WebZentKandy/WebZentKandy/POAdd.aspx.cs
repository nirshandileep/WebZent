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
using LankaTiles.Common;
using LankaTiles.Exception;
using LankaTiles.ItemsManagement;
using LankaTiles.SupplierManagement;
using LankaTiles.UserManagement;

public partial class POAdd : System.Web.UI.Page
{

    private PO objPurchaseOrder;

    public PO ObjPurchaseOrder
    {
        get
        {
            if (objPurchaseOrder == null)
            {
                if (Session["ObjPurchaseOrder"] == null)
                {
                    objPurchaseOrder = new PO();
                    objPurchaseOrder.POId = Int32.Parse(hdnPOId.Value.Trim() == String.Empty ? "0" : hdnPOId.Value.Trim());
                    
                    objPurchaseOrder.GetPOByID();
                    Session["ObjPurchaseOrder"] = objPurchaseOrder;
                }
                else
                {
                    objPurchaseOrder = (PO)(Session["ObjPurchaseOrder"]);
                }
            }
            return objPurchaseOrder;
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
                this.CheckIfEditPO();
                this.CheckIfPOItemSelect();
                this.SetData();
                this.AddAttributes();
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
    /// Adds client side attributes to controls
    /// mainly for Java Scripts
    /// </summary>
    private void AddAttributes()
    {
        txtQuantity.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        dxPrintPopup.ContentUrl = String.Format("~/PrintPO.aspx?POId={0}&FromURL=POAdd.aspx", hdnPOId.Value.Trim());
    }

    /// <summary>
    /// Fills the controls with the data depending on the scenario
    /// </summary>
    private void SetData()
    {
        try
        {

            if (hdnFromURL.Value.Trim() != "ProductSearch.aspx")
            {
                Master.ClearSessions();//Clear all sessions
            }

            if (hdnItemIdFromSearch.Value.Trim() != "0")
            {
                Int32 itemIdFS = Int32.Parse(hdnItemIdFromSearch.Value.Trim());
                
                Item objItem = new Item();
                objItem.ItemId = itemIdFS;
                objItem.GetItemByID();
                txtItemCode.Text = objItem.ItemCode.ToString();
                txtItemName.Text = objItem.ItemDescription.Trim();
                txtOldCost.Text = Decimal.Round(objItem.Cost, 2).ToString();
                txtSellingPrice.Text = Decimal.Round(objItem.SellingPrice, 2).ToString();
            }

            // If edit po 
            if (ObjPurchaseOrder.POId > 0 || hdnItemIdFromSearch.Value.Trim() != "0")
            {
                if (ObjPurchaseOrder.IsReceived)
                {
                    this.DisableControls();
                }

                if (ObjPurchaseOrder.POStatus == Structures.POStatus.Cancel && ObjPurchaseOrder.POId > 0)
                {
                    this.DisableControls();
                    btnAddPO.Enabled = false;
                    btnPrintPO.Enabled = false;
                }
                else if (ObjPurchaseOrder.POId > 0 && !ObjPurchaseOrder.IsReceived)
                {
                    btnCancel.Visible = true;
                }

                txtPOCode.Text = ObjPurchaseOrder.POCode.Trim();
                ddlSupplier.SelectedValue = ObjPurchaseOrder.SupId.ToString();
                if (Decimal.Round((ObjPurchaseOrder.POAmount - ObjPurchaseOrder.BalanceAmount), 2) == 0)
                {
                    txtPaidAmount.Text = String.Empty;
                }
                else
                {
                    txtPaidAmount.Text = Decimal.Round((ObjPurchaseOrder.POAmount - ObjPurchaseOrder.BalanceAmount), 2).ToString();
                }

                lblBalancePayment.Text = Decimal.Round(ObjPurchaseOrder.BalanceAmount,2).ToString();
                lblPOAmount.Text = Decimal.Round(ObjPurchaseOrder.POAmount,2).ToString();
                
                if (ObjPurchaseOrder.PODate.HasValue)
                {
                    dtpPODate.Date = ObjPurchaseOrder.PODate.Value;    
                }
                else
                {
                    dtpPODate.Value = null;
                }

                if (ObjPurchaseOrder.RequestedBy.HasValue)
                {
                    ddlRequestBy.SelectedValue = ObjPurchaseOrder.RequestedBy.ToString();    
                }
                else
                {
                    ddlRequestBy.SelectedValue = "-1";
                }

                txtCancelNote.Value = ObjPurchaseOrder.POComment;
                
                gvItemList.DataSource = ObjPurchaseOrder.DsPOItems;
                gvItemList.DataBind();
                txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            }
            this.ShowHideGrid();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    /// <summary>
    /// Check if page is loaded from a item search
    /// if item Id ezist in query string it gets filled in the ItemId query string
    /// </summary>
    private void CheckIfPOItemSelect()
    {
        try
        {
            if (Request.QueryString["ItemId"] != null && Request.QueryString["ItemId"].Trim() != String.Empty)
            {
                hdnItemIdFromSearch.Value = Request.QueryString["ItemId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    /// <summary>
    /// Check if the page is on edit mode by checking the POId passed 
    /// through the QueryString and fills the hidden field with the POId
    /// </summary>
    private void CheckIfEditPO()
    {
        try
        {
            if (Request.QueryString["POId"] != null && Request.QueryString["POId"].Trim() != String.Empty)
            {
                hdnPOId.Value = Request.QueryString["POId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
        
    }

    /// <summary>
    /// Fill the controls with detial items
    /// </summary>
    private void LoadInitialData()
    {
        try
        {
            // Read only
            txtPOCode.Text = new PODAO().GetNextPOCode();

            //
            // Suppliers
            //
            DataSet dsSuppliers = (new SupplierDAO()).GetAllSuppliers();
            if (dsSuppliers == null || dsSuppliers.Tables.Count == 0)
            {
                ddlSupplier.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("SupplierName", "SupId", dsSuppliers, ddlSupplier);
                ddlSupplier.Items.Insert(0, new ListItem("--Please Select--", "-1"));
            }

            //
            // Fill the discount dropdown
            //
            int max = Convert.ToInt32(Constant.MaximumDiscountAllowed_PO.Trim());
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
            // Fill the user name
            //
            DataSet dsUsers = new UsersDAO().GetAllUsers();
            if (dsUsers == null || dsUsers.Tables.Count == 0)
            {
                ddlRequestBy.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("FirstName", "UserId", dsUsers, ddlRequestBy);
                ddlRequestBy.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlRequestBy.SelectedValue = "-1";
            }

            dtpPODate.Date = DateTime.Now;
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

    protected void btnSavePOItem_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDiscount_SelectedIndexChanged(ddlDiscount, EventArgs.Empty);
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
                Decimal lineCost = (Decimal)qty * Decimal.Parse(txtItemCost.Text.Trim());

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

                    DataRow newRow = ObjPurchaseOrder.DsPOItems.Tables[0].NewRow();
                    newRow["ItemId"] = objItem.ItemId.ToString();
                    newRow["ItemCode"] = objItem.ItemCode;
                    newRow["ItemDescription"] = objItem.ItemDescription.Trim();
                    newRow["Qty"] = Int32.Parse(txtQuantity.Text.Trim());
                    newRow["LineCost"] = lineCost;
                    newRow["BrandName"] = objItem.BrandName.Trim();
                    newRow["POItemCost"] = txtItemCost.Text.Trim();
                    newRow["DiscountPerUnit"] = ddlDiscount.SelectedValue.Trim();
                    newRow.EndEdit();
                    ObjPurchaseOrder.DsPOItems.Tables[0].Rows.Add(newRow);
                }
                else
                {
                    
                    //Update existing
                    DataView dvUpdate = ObjPurchaseOrder.DsPOItems.Tables[0].DefaultView;

                    dvUpdate.Sort = "ItemId";
                    DataRowView[] dr = dvUpdate.FindRows(hdnItemId.Value.Trim());

                    dr[0]["ItemId"] = objItem.ItemId.ToString();
                    dr[0]["ItemCode"] = objItem.ItemCode;
                    dr[0]["ItemDescription"] = objItem.ItemDescription.Trim();
                    dr[0]["Qty"] = Int32.Parse(txtQuantity.Text.Trim());
                    dr[0]["LineCost"] = lineCost;
                    dr[0]["BrandName"] = objItem.BrandName.Trim();
                    dr[0]["POItemCost"] = txtItemCost.Text.Trim();
                    dr[0]["DiscountPerUnit"] = ddlDiscount.SelectedValue.Trim();

                    gvItemList.DataSource = ObjPurchaseOrder.DsPOItems;
                    gvItemList.DataBind();

                    txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
                    this.ShowHideGrid();
                }

                gvItemList.DataSource = ObjPurchaseOrder.DsPOItems;
                gvItemList.DataBind();


            }
            else 
            {
                lblError.Visible = true;
                lblError.Text = "Item code " + txtItemCode.Text.Trim() + " does not exist!";
                return;
            }

            Session["ObjPurchaseOrder"] = ObjPurchaseOrder;

            ClearControls();
            
            this.CalculatePOAmounts();
            this.ShowHideGrid();
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            
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

    private void ClearControls()
    {
        txtItemCode.Text = String.Empty;
        txtItemCost.Text = String.Empty;
        txtQuantity.Text = String.Empty;
        txtItemName.Text = String.Empty;
        txtOldCost.Text = String.Empty;
        txtSellingPrice.Text = String.Empty;
        ddlDiscount.SelectedIndex = 0;
        txtCostBeforeDiscount.Text = String.Empty;
        hdnItemId.Value = "0";
    }

    private bool IsItemExistInGrid()
    {
        try
        {
            DataView dvCount = ObjPurchaseOrder.DsPOItems.Tables[0].DefaultView;
            dvCount.Sort="ItemCode";
            
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

    protected void btnAddNewSupplier_Click(object sender, EventArgs e)
    {
        try
        {

            Response.Redirect("AddSupplier.aspx?" + "FromURL=POAdd.aspx", false);
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

    protected void gvItemList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            hdnItemId.Value  = gvItemList.DataKeys[e.NewEditIndex].Value.ToString();
            txtItemCode.Text = gvItemList.Rows[e.NewEditIndex].Cells[(int)Structures.PurchaseOrderGridColumnNames.ItemCode].Text.Trim();//ItemCode
            txtItemCost.Text = gvItemList.Rows[e.NewEditIndex].Cells[(int)Structures.PurchaseOrderGridColumnNames.POItemCost].Text.Trim();//ItemCost
            txtQuantity.Text = gvItemList.Rows[e.NewEditIndex].Cells[(int)Structures.PurchaseOrderGridColumnNames.Qty].Text.Trim();//Quantity
            txtItemName.Text = gvItemList.Rows[e.NewEditIndex].Cells[(int)Structures.PurchaseOrderGridColumnNames.ItemDescription].Text.Trim();//Description
            ddlDiscount.SelectedValue = gvItemList.Rows[e.NewEditIndex].Cells[(int)Structures.PurchaseOrderGridColumnNames.DiscountPerUnit].Text.Trim();//DiscountPerUnit
            if (Decimal.Parse(ddlDiscount.SelectedValue.Trim()) > 0)
            {
                txtCostBeforeDiscount.Text = (Convert.ToDecimal(txtItemCost.Text.Trim()) / (1 - Convert.ToDecimal(ddlDiscount.SelectedValue.Trim()) / 100)).ToString();

            }
            else
            {
                txtCostBeforeDiscount.Text = txtItemCost.Text;
            }
            txtSearchItem_Click(txtSearchItem, EventArgs.Empty);
            this.ShowHideGrid();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void gvItemList_RowEditing(object sender, GridViewEditEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            Int32 temp = e.RowIndex;
            DataRow[] dr = ObjPurchaseOrder.DsPOItems.Tables[0].Select("ItemId=" + gvItemList.DataKeys[temp].Value.ToString());
            dr[0].Delete();

            gvItemList.DataSource = ObjPurchaseOrder.DsPOItems;
            gvItemList.DataBind();

            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();

            Session["ObjPurchaseOrder"] = ObjPurchaseOrder;
            this.CalculatePOAmounts();
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

    private void CalculatePOAmounts()
    {
        try
        {
            Decimal total = 0;
            Decimal paid = Decimal.Parse(txtPaidAmount.Text.Trim() == String.Empty ? "0" : txtPaidAmount.Text.Trim());
            Decimal due = 0;
            foreach (GridViewRow row in gvItemList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    total += Decimal.Parse(row.Cells[6].Text.ToString().Trim() == String.Empty ? "0" : row.Cells[6].Text.ToString().Trim());    
                }
            }

            lblPOAmount.Text = total.ToString();
            due = total - paid;
            lblBalancePayment.Text = due.ToString();

            ObjPurchaseOrder.POAmount = total;
            ObjPurchaseOrder.BalanceAmount = due;

            Session["ObjPurchaseOrder"] = ObjPurchaseOrder;
            
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnAddPO_Click(object sender, EventArgs e)
    {
        try
        {
            this.CalculatePOAmounts();
            ObjPurchaseOrder.BalanceAmount = Decimal.Parse(lblBalancePayment.Text.Trim() == String.Empty ? "0" : lblBalancePayment.Text.Trim());
            ObjPurchaseOrder.POAmount = Decimal.Parse(lblPOAmount.Text.Trim() == String.Empty ? "0" : lblPOAmount.Text.Trim());
            ObjPurchaseOrder.POCode = new PODAO().GetNextPOCode();
            ObjPurchaseOrder.POCreatedDate = DateTime.Now;
            ObjPurchaseOrder.POCreatedUser = Master.LoggedUser.UserId;
            ObjPurchaseOrder.POLastModifiedBy = Master.LoggedUser.UserId;
            ObjPurchaseOrder.POLastModifiedDate = DateTime.Now;
            ObjPurchaseOrder.SupId = Int32.Parse(ddlSupplier.SelectedValue.Trim());
            ObjPurchaseOrder.PODate = dtpPODate.Date;
            ObjPurchaseOrder.POComment = txtCancelNote.Value.Trim();

            if (ddlRequestBy.SelectedValue != "-1")
            {
                ObjPurchaseOrder.RequestedBy = Int32.Parse(ddlRequestBy.SelectedValue.Trim());
            }
            else
            {
                ObjPurchaseOrder.RequestedBy = null;
            }

            if (ObjPurchaseOrder.Save())
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
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)");
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
            Item objItem = new Item();
            objItem.ItemCode = txtItemCode.Text.Trim();
            objItem.GetItemsByItemCode();
            if (objItem.ItemId > 0)
            {
                txtItemName.Text = objItem.ItemDescription.Trim();
                txtOldCost.Text = Decimal.Round(objItem.Cost, 2).ToString();
                txtSellingPrice.Text = Decimal.Round(objItem.SellingPrice, 2).ToString();
            }
            else
            {
                txtItemName.Text = String.Empty;
                ObjPurchaseOrder.SupId = Int32.Parse(ddlSupplier.SelectedValue);
                ObjPurchaseOrder.POCode = txtPOCode.Text.Trim();
                ObjPurchaseOrder.PODate = dtpPODate.Date;
                ObjPurchaseOrder.RequestedBy = Int32.Parse(ddlRequestBy.SelectedValue.Trim());
                Session["ObjPurchaseOrder"] = ObjPurchaseOrder;

                Response.Redirect("ProductSearch.aspx?FromURL=POAdd.aspx", false);
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

    private void DisableControls()
    {
        try
        {

            ddlSupplier.Enabled = false;
            btnAddNewSupplier.Enabled = false;
            txtItemCode.Enabled = false;
            btnSavePOItem.Enabled = false;
            txtItemCost.Enabled = false;
            gvItemList.Enabled = false;
            txtSearchItem.Enabled = false;
            dtpPODate.Enabled = false;
            ddlRequestBy.Enabled = false;
            btnCancel.Visible = false;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ddlDiscount_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtCostBeforeDiscount.Text.Trim() == string.Empty)
            {
                return;
            }
            Decimal discount = Convert.ToDecimal(ddlDiscount.SelectedValue.Trim());
            Decimal price = Decimal.Round(Convert.ToDecimal(txtCostBeforeDiscount.Text.Trim()), 2);
            if (discount > 0)
            {
                txtItemCost.Text = Decimal.Round((price * (1 - discount / 100)), 2).ToString();
            }
            else
            {
                txtItemCost.Text = price.ToString();
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

    /// <summary>
    /// Clear the item section controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

            this.ClearControls();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try 
        {
            ObjPurchaseOrder.POStatus = Structures.POStatus.Cancel;
            ObjPurchaseOrder.POComment = txtCancelNote.Value.Trim();
            if (new PODAO().UpdatePOStatus(ObjPurchaseOrder))
            {
                btnCancel.Enabled = false;
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                this.DisableControls();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
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
}
