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
using LankaTiles.GRNManagement;
using LankaTiles.POManagement;
using LankaTiles.Exception;
using LankaTiles.Common;
using LankaTiles.ItemsManagement;
using LankaTiles.InvoiceManagement;

public partial class RecieveGoods : System.Web.UI.Page
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
                Master.ClearSessions();//Clear all sessions
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfEditPR();
                this.SetData();
                this.AddAttributes();
                this.SetUIByUserRole();
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

    private void SetUIByUserRole()
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    private void AddAttributes()
    {
        //txtQuantity.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        txtGrnId.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        dxPrintPR.ContentUrl = String.Format("~/PrintPurchaseReturns.aspx?PRId={0}&FromURL=PurchaseReturns.aspx", hdnPRId.Value.Trim());
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
                    txtGrnId.Text = PurchaseReturn.GRNId.ToString();
                    dtpReturnDate.Date = PurchaseReturn.ReturnDate;
                    lblPOCode.Text = PurchaseReturn.POCode;
                    lblSupInvNo.Text = PurchaseReturn.SuplierInvNo;
                    lblInvoiceTotal.Text = Decimal.Round(purchaseReturn.TotalReturns, 2).ToString();
                    lblSupplierName.Text = PurchaseReturn.SupplierName;

                    gvItemList.DataSource = PurchaseReturn.DsReturnDetails;
                    gvItemList.DataBind();

                }

                txtCreditNote.Text = PurchaseReturn.Comment.Trim();

                this.ShowHideGrid();
                btnPrintVoucher.Visible = true;
                if (true)//(Master.LoggedUser.UserRoleID > 2)
                {
                    this.ReadOnlyMode();
                }
            }
            else
            {
                //lblDate.Text = DateTime.Now.ToString(Constant.Format_Date);
                dtpReturnDate.Date = DateTime.Now;
                btnPrintVoucher.Visible = false;
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

    private void ReadOnlyMode()
    {
        try
        {

            btnConfirm.Enabled = false;
            dtpReturnDate.Enabled = false;
            btnAddItem.Enabled = false;
            gvItemList.Enabled = false;
            txtCreditNote.Enabled = false;
            btnSave.Enabled = false;
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

    private void FillPOCode()
    {
        //
        // Load Pending PO's
        //
        DataSet dsPO = (new PODAO()).GetAllPartialyReceivedPO();
        if (dsPO == null || dsPO.Tables.Count == 0 || dsPO.Tables[0].Rows.Count == 0)
        {
            //ddlPOCode.Items.Add(new ListItem("--No Records--", "-1"));
        }
        else
        {
            //Master.BindDropdown("POCode", "POId", dsPO, ddlPOCode);
            //ddlPOCode.Items.Add(new ListItem("--Please Select--", "-1"));
            //ddlPOCode.SelectedValue = "-1";
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

    protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.ClearItemDetails();

            if (ddlItemCode.SelectedValue.Trim() != "-1" && ddlItemCode.SelectedValue.Trim() != "")
            {
                Int32 itemid = Int32.Parse(ddlItemCode.SelectedValue.Trim());

                ///
                /// Get grn details for 
                ///
                DataSet dsItems = new PurchaseReturnsDAO().GetPRItemsByGRNDetailID(Int64.Parse(ddlItemCode.SelectedValue.Trim()));
                if (dsItems != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataView dvItems = dsItems.Tables[0].DefaultView;

                    dvItems.Sort = "GRNDetailsId";
                    DataRowView[] dr = dvItems.FindRows(itemid.ToString());
                    if (dr.Length > 0)
                    {
                        hdnItemId.Value = dr[0]["ItemId"].ToString();
                        txtItemName.Text = dr[0]["ItemDescription"].ToString();
                        txtMaxRecievable.Text = dr[0]["TotalRemaining"].ToString();
                        
                        dxseQty.MaxValue = decimal.Parse(dr[0]["TotalRemaining"].ToString());
                        rvQty.MaximumValue = dxseQty.MaxValue.ToString();

                        txtCost.Text = Decimal.Round(Convert.ToDecimal(dr[0]["POItemCost"].ToString()), 2).ToString();
                    }
                }

            }
            else
            {
                ClearItemDetails();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// Clear All Item Details
    /// </summary>
    private void ClearItemDetails()
    {
        dxseQty.MinValue = 0;
        txtMaxRecievable.Text = "0";
        txtItemName.Text = String.Empty;
        dxseQty.Text = "0";
        hdnItemId.Value = "0";
        txtCost.Text = string.Empty;
        txtTotalCost.Text = string.Empty;
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlItemCode.Items.Count > 1 && ddlItemCode.SelectedValue != "-1")
            {
                //todo: Check if item is already in grid
                if (!this.IsItemInGrid(Int32.Parse(ddlItemCode.SelectedValue.Trim()), "GRNDetailsId"))
                {
                    if (hdnPRId.Value == "0")
                    {
                        DataRow drNewRow = PurchaseReturn.DsReturnDetails.Tables[0].NewRow();
                        //drNewRow["PRDetailId"] = DBNull.Value;
                        drNewRow["GRNDetailsId"] = Int32.Parse(ddlItemCode.SelectedValue.Trim());

                        Int32 qty = Int32.Parse(dxseQty.Text.Trim());
                        drNewRow["Qty"] = qty;
                        drNewRow["ItemCode"] = ddlItemCode.SelectedItem.Text.Trim();
                        drNewRow["UnitCost"] = Decimal.Parse(txtCost.Text.Trim());
                        drNewRow["ItemDescription"] = txtItemName.Text.Trim();
                        drNewRow["PRId"] = Int32.Parse(hdnPRId.Value.Trim());

                        Decimal itemCost = Decimal.Parse(txtCost.Text.Trim()) * (Decimal)qty;
                        drNewRow["TotalCost"] = itemCost;
                        drNewRow.EndEdit();
                        PurchaseReturn.DsReturnDetails.Tables[0].Rows.Add(drNewRow);
                        gvItemList.DataSource = PurchaseReturn.DsReturnDetails;
                        gvItemList.DataBind();

                    }
                    else
                    {
                        if (hdnPRDetailsId.Value != "0")
                        {
                            DataView dv = PurchaseReturn.DsReturnDetails.Tables[0].DefaultView;
                            dv.Sort = "GRNDetailsId";

                            DataRowView[] drv = dv.FindRows(Int32.Parse(hdnPRDetailsId.Value.Trim()));
                            drv[0]["Qty"] = Int32.Parse(dxseQty.Text.Trim());

                            Decimal itemCost = Decimal.Parse(txtCost.Text.Trim()) * Decimal.Parse(dxseQty.Text.Trim());
                            drv[0]["TotalCost"] = decimal.Round(itemCost, 2);

                            gvItemList.DataSource = PurchaseReturn.DsReturnDetails;
                            gvItemList.DataBind();
                        }
                    }
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = "The Item Already Exists In Grid";
                }

                Session["ObjPurchaseReturn"] = PurchaseReturn;
            }
            this.ClearItemControls();
            this.ShowHideGrid();
            this.CalculateTotal();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddItem_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    
    private void CalculateTotal()
    {
        try
        {

            Decimal Total = 0;
            Int32 Qty = 0;
            Decimal ItemCost = 0;
            Decimal lineTotal = 0;
            foreach (GridViewRow row in gvItemList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Qty = Int32.Parse(row.Cells[2].Text.ToString().Trim() == String.Empty ? "0" : row.Cells[2].Text.ToString().Trim());
                    ItemCost = Decimal.Parse(row.Cells[3].Text.ToString().Trim() == String.Empty ? "0" : row.Cells[3].Text.ToString().Trim());
                    lineTotal = (Decimal)Qty * ItemCost;
                    Total += lineTotal;
                }
            }

            lblInvoiceTotal.Text = Total.ToString();
            PurchaseReturn.TotalReturns = Total;
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    /// <summary>
    /// Returns true it item exists in gvItemList
    /// </summary>
    /// <param name="p"></param>
    /// <returns>True when item exists, False if item does not exist</returns>
    private bool IsItemInGrid(int p, string colName)
    {
        try
        {
            DataView dvItems = new DataView();
            dvItems = (DataView)gvItemList.DataSource;
            if (dvItems != null)
            {
                dvItems.Sort = colName;

                Int32 rowId = -1;
                rowId = dvItems.Find(p);
                if (rowId != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

    private void ClearItemControls()
    {
        try
        {
            ddlItemCode.SelectedValue = "-1";
            txtItemName.Text = String.Empty;
            dxseQty.Text = String.Empty;
            txtMaxRecievable.Text = String.Empty;
            txtCost.Text = String.Empty;
            txtTotalCost.Text = string.Empty;
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
                trGrid.Visible = false;
                trNoRecords.Visible = true;
                //ddlPOCode.Enabled = true;
            }
            else
            {
                trGrid.Visible = true;
                trNoRecords.Visible = false;
               //ddlPOCode.Enabled = false;
            }
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 temp = e.RowIndex;
            DataRow[] dr = PurchaseReturn.DsReturnDetails.Tables[0].Select("GRNDetailsId=" + gvItemList.DataKeys[temp]["GRNDetailsId"].ToString());
            dr[0].Delete();

            //DataRow[] dr = ObjGRN.GRNItems.Tables[0].Select("Id=" + gvItemList.DataKeys[temp]["Id"].ToString());
            //dr[0].Delete();
            btnConfirm_Click(btnConfirm, EventArgs.Empty);//Fill 
            gvItemList.DataSource = PurchaseReturn.DsReturnDetails;
            gvItemList.DataBind();
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            Session["ObjPurchaseReturn"] = PurchaseReturn;
            this.ShowHideGrid();
            this.CalculateTotal();
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

    protected void gvItemList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

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

    //protected void btnAddGRN_Click(object sender, EventArgs e)
    //{
    //    try
    //    {

            
    //        ObjGRN.Rec_By = Master.LoggedUser.UserId;
    //        ObjGRN.Rec_Date = dtpGRNDate.Date;
            
    //        if (rblGRNType.SelectedValue == "1")//PO
    //        {
    //            ObjGRN.POId = Int32.Parse(ddlPOCode.SelectedValue.Trim() != "-1" ? ddlPOCode.SelectedValue.Trim() : "0");
    //            ObjGRN.InvId = null;
    //            ObjGRN.CreditNote = String.Empty;
    //            ObjGRN.SuplierInvNo = txtSupplierInvNo.Text.Trim();
    //            if (hdnOldInvNumber.Value.Trim() != txtSupplierInvNo.Text.Trim())
    //            {
    //                if (true == new GRNDAO().IsSupplierInvNoExist(ObjGRN))
    //                {
    //                    lblError.Text = "Supplier Invoice Number Already exists!!";
    //                    lblError.Visible = true;
    //                    return;
    //                }    
    //            }
                
    //        }
    //        else if (rblGRNType.SelectedValue == "2")//Sales return
    //        {
    //            ObjGRN.POId = null;
    //            ObjGRN.InvId = Int32.Parse(hdnInvoiceId.Value.Trim());
    //            ObjGRN.CreditNote = txtCreditNote.Text.Trim();
    //            ObjGRN.SuplierInvNo = String.Empty;
    //        }

    //        if (ObjGRN.Save())
    //        {
    //            lblError.Visible = true;
    //            lblError.Text = Constant.MSG_Save_SavedSeccessfully;
    //            hdnPRId.Value = ObjGRN.GRNId.ToString();
    //            btnPrint.Visible = true;
    //            btnSave.Enabled = false;
    //            btnSave.CssClass = "show_success";
    //        }
    //        else
    //        {
    //            btnPrint.Visible = false;
    //            lblError.Visible = true;
    //            lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddGRN_Click(object sender, EventArgs e)");
    //        if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
    //            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
    //        else
    //            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

    //    }
    //}

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            Session["ObjGRN"] = null;
            Int64 GRNId = 0;
            if (Int64.TryParse(txtGrnId.Text.Trim(), out GRNId))
            {
                ObjGRN.GRNId = GRNId;
                ObjGRN.GetGRNByID();
                if (ObjGRN.POId.HasValue)
                {

                    lblGRNError.Visible = false;
                    lblGRNError.Text = String.Empty;

                    PO purchaseorder = new PO();
                    purchaseorder.POId = ObjGRN.POId.Value;
                    purchaseorder.GetPOByID();
                    
                    lblSupInvNo.Text = ObjGRN.SuplierInvNo;
                    lblPOCode.Text = purchaseorder.POCode;
                    lblSupplierName.Text = purchaseorder.SupplierName;

                    ddlItemCode.DataSource = new PurchaseReturnsDAO().GetItemsToReturnByGRNId(GRNId);
                    ddlItemCode.DataTextField = "ItemCode";
                    ddlItemCode.DataValueField = "GRNDetailsId";
                    ddlItemCode.DataBind();
                    ddlItemCode.Items.Add(new ListItem("--Please Select--", "-1"));
                    ddlItemCode.SelectedValue = "-1";

                }
                else
                {
                    lblGRNError.Visible = true;
                    lblGRNError.Text = "Incorrect PO GRN Id";
                }
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnConfirm_Click(object sender, EventArgs e)");
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
                ListItem li = new ListItem();
                li = ddlItemCode.Items.FindByValue(gvItemList.DataKeys[e.Row.RowIndex]["GRNDetailsId"].ToString());

                if (li != null)
                {
                    ddlItemCode.Items.Remove(li);
                }
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void gvItemList_RowDataBound(object sender, GridViewRowEventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }    
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            ClientScript.RegisterStartupScript(this.GetType(), "Invoice", "<script language='javascript'>LoadGRNPrintPopup('FromURL=RecieveGoods.aspx?GRNId=" + hdnPRId.Value.Trim() + "');</script>");
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            PurchaseReturn.Comment = txtCreditNote.Text.Trim();
            PurchaseReturn.CreatedBy = Master.LoggedUser.UserId;

            if (txtGrnId.Text.Trim() != string.Empty)
            {
                PurchaseReturn.GRNId = Int64.Parse(txtGrnId.Text.Trim());
            }

            PurchaseReturn.ModifiedBy = Master.LoggedUser.UserId;
            PurchaseReturn.ReturnDate = dtpReturnDate.Date;
            this.CalculateTotal();//to set return total

            if (PurchaseReturn.Save())
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;

                lblPRCode.Text = PurchaseReturn.PRCode;
                hdnPRId.Value = PurchaseReturn.PRId.ToString();
                btnPrintVoucher.Visible = true;

                AddAttributes();

                this.ReadOnlyMode();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
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

    protected void dxseQty_NumberChanged(object sender, EventArgs e)
    {
        try
        {
            decimal qty = 0;
            decimal cost = 0;
            decimal totalcost = 0;

            if (decimal.TryParse(dxseQty.Text.Trim(), out qty) &&
                decimal.TryParse(txtCost.Text.Trim(), out cost))
            {
                totalcost = qty * cost;
                txtTotalCost.Text = decimal.Round(totalcost, 2).ToString();
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
