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
                Master.ClearSessions();//Clear all sessions
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfEditGRN();
                //this.CheckIfPOItemSelect();
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
            if (Master.LoggedUser.UserRoleID < 3)
            {
                trCostPrice.Visible = true;
            }
            else
            {
                trCostPrice.Visible = false;
            }
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    private void AddAttributes()
    {
        txtQuantity.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
        //txtInvoiceNumber.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    /// <summary>
    /// Load the data for GRN Edit / View
    /// </summary>
    private void SetData()
    {
        try
        {

            if (hdnGRNId.Value != "0")
            {
                if (ObjGRN.POId.HasValue && ObjGRN.POId > 0)
                {
                    rblGRNType.SelectedValue = "1";
                    rblGRNType_SelectedIndexChanged(rblGRNType, EventArgs.Empty);
                    hdnPOId.Value = ObjGRN.POId.ToString();

                    if (ddlPOCode.Items.FindByValue(hdnPOId.Value.Trim()) != null)
                    {
                        ddlPOCode.SelectedValue = hdnPOId.Value.Trim();
                    }
                    else
                    {
                        ddlPOCode.Items.Add(new ListItem(ObjGRNPO.POCode.Trim(), ObjGRN.POId.ToString().Trim()));
                        ddlPOCode.SelectedValue = ObjGRN.POId.ToString();
                    }

                    ddlPOCode_SelectedIndexChanged(ddlPOCode, EventArgs.Empty);//Fill items by Selected POCode
                    txtPOAmmount.Text = ObjGRNPO.POAmount.ToString() == String.Empty ? "0" : Decimal.Round(ObjGRNPO.POAmount).ToString();
                    txtSupplierInvNo.Text = ObjGRN.SuplierInvNo.Trim();
                    hdnOldInvNumber.Value = ObjGRN.SuplierInvNo.Trim();
                }
                else if (ObjGRN.InvId.HasValue && ObjGRN.InvId > 0)
                {
                    rblGRNType.SelectedValue = "2";
                    hdnInvoiceId.Value = ObjGRN.InvId.ToString();
                    txtInvoiceNumber.Text = ObjGRN.GRNInvoice.InvoiceNo.Trim();
                    lblInvoiceTotal.Text = Decimal.Round(ObjGRN.GRNInvoice.GrandTotal, 2).ToString();
                    txtCreditNote.Text = ObjGRN.CreditNote.Trim();
                    frvInvoiceOnly.Enabled = false;
                }
                lblGRNNo.Text = ObjGRN.GRNId.ToString();
                txtReceivedTotal.Text = Decimal.Round(ObjGRN.TotalAmount, 2).ToString();
                //lblDate.Text = ObjGRN.Rec_Date.ToString(Constant.Format_Date);
                dtpGRNDate.Date = ObjGRN.Rec_Date;
                btnPrint.Visible = true;

                gvItemList.DataSource = ObjGRN.GRNItems;
                gvItemList.DataBind();

                this.ShowHideGrid();

                if (true)//(Master.LoggedUser.UserRoleID > 2)
                {
                    this.ReadOnlyMode();
                }
            }
            else
            {
                //lblDate.Text = DateTime.Now.ToString(Constant.Format_Date);
                dtpGRNDate.Date = DateTime.Now;
                btnPrint.Visible = false;
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
    private void CheckIfEditGRN()
    {
        try
        {
            if (Request.QueryString["GRNId"] != null && Request.QueryString["GRNId"].Trim() != String.Empty)
            {
                hdnGRNId.Value = Request.QueryString["GRNId"].Trim();
                Page.Title = "View GRN";
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

            rblGRNType.Enabled = false;
            ddlPOCode.Enabled = false;
            txtInvoiceNumber.Enabled = false;
            btnConfirm.Enabled = false;
            txtSupplierInvNo.Enabled = false;

            ddlItemCode.Enabled = false;
            txtQuantity.Enabled = false; 
            btnAddItem.Enabled = false;

            gvItemList.Enabled = false;
            btnAddGRN.Enabled = false;

            dtpGRNDate.Enabled = false;
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
            trSalesReturn.Visible = false;
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
            ddlPOCode.Items.Add(new ListItem("--No Records--", "-1"));
        }
        else
        {
            Master.BindDropdown("POCode", "POId", dsPO, ddlPOCode);
            ddlPOCode.Items.Add(new ListItem("--Please Select--", "-1"));
            ddlPOCode.SelectedValue = "-1";
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
            if (ddlItemCode.SelectedValue.Trim() != "-1" && ddlItemCode.SelectedValue.Trim() != "")
            {
                if (rblGRNType.SelectedItem.Value.Trim() == "1")//PO
                {
                    Item tmpItem = new Item();
                    tmpItem.ItemId = Int32.Parse(ddlItemCode.SelectedValue.Trim());
                    tmpItem.GetItemByID();
                    txtItemName.Text = tmpItem.ItemDescription.Trim();

                    DataSet dsItems = ObjGRNPO.GetAllPartialyReceivedPOItemsByPOID();
                    if (dsItems != null && dsItems.Tables[0].Rows.Count > 0)
                    {
                        DataView dvItems = dsItems.Tables[0].DefaultView;

                        dvItems.Sort = "ItemId";
                        DataRowView[] dr = dvItems.FindRows(tmpItem.ItemId.ToString());
                        if (dr.Length > 0)
                        {
                            hdnItemId.Value = dr[0]["ItemId"].ToString();
                            txtItemName.Text = dr[0]["ItemDescription"].ToString();
                            txtMaxRecievable.Text = dr[0]["TotalRemaining"].ToString();
                            hdnItemValue.Value = dr[0]["POItemCost"].ToString();
                            txtCost.Text = Decimal.Round(Convert.ToDecimal(dr[0]["POItemCost"].ToString()), 2).ToString();
                        }
                    }
                }
                else if (rblGRNType.SelectedItem.Value.Trim() == "2")//Invoice
                {
                    DataSet ds = new DataSet();
                    ds = new InvoiceDAO().GetInvoiceDetailsByInvoiceIDForReturns(ObjGRN.GRNInvoice);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        DataView dvItems = ds.Tables[0].DefaultView;
                        dvItems.Sort = "Id";
                        DataRowView[] dr = dvItems.FindRows(ddlItemCode.SelectedValue.Trim());
                        if (dr.Length>0)
                        {
                            hdnItemId.Value = dr[0]["Id"].ToString();
                            txtItemName.Text = dr[0]["ItemDescription"].ToString();
                            txtMaxRecievable.Text = dr[0]["ReturnQty"].ToString();//IssuedQTY - 30-09-2012 changed from ReturnQty to IssuedQTY
                            hdnItemValue.Value = dr[0]["Price"].ToString();
                            txtCost.Text = Math.Round(Convert.ToDecimal(dr[0]["Price"].ToString()), 2).ToString();
                        }
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
        txtMaxRecievable.Text = "0";
        txtItemName.Text = String.Empty;
        txtQuantity.Text = "0";
        hdnItemId.Value = "0";
        txtCost.Text = string.Empty;
    }

    protected void rblGRNType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            rblGRNType.Enabled = false;
            ///1 - Purchase Order
            ///2 - Sales Return
            if (rblGRNType.SelectedValue == "1")
            {
                txtSupplierInvNo.ReadOnly = false;
                ddlPOCode.Enabled = true;
                rfvPOCode.Enabled = true;
                txtInvoiceNumber.Enabled = false;
                rfvInvoiceNo.Enabled = false;
                btnConfirm.Enabled = false;
                txtInvoiceNumber.Text = String.Empty;
                this.FillPOCode();
                ddlItemCode.Items.Clear();
                ddlItemCode.Items.Insert(0, (new ListItem("--No Records--", "-1")));
                txtCreditNote.Text = String.Empty;
                trSalesReturn.Visible = false;
                RequiredFieldValidator2.Enabled = false;
            }
            else if (rblGRNType.SelectedValue == "2")
            {
                txtSupplierInvNo.ReadOnly = true;
                ddlPOCode.Enabled = false;
                rfvPOCode.Enabled = false;
                txtInvoiceNumber.Enabled = true;
                rfvInvoiceNo.Enabled = true;
                btnConfirm.Enabled = true;
                ddlPOCode.SelectedValue = "-1";
                ddlItemCode.Items.Clear();
                ddlItemCode.Items.Insert(0, (new ListItem("--No Records--", "-1")));
                trSalesReturn.Visible = true;
                RequiredFieldValidator2.Enabled = true;
                rfvInvoiceNumber.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (rblGRNType.SelectedValue == "1")//PO
            {
                if (ddlItemCode.SelectedValue != "-1")
                {
                    //todo: Check if item is already in grid
                    if (!this.IsItemInGrid(Int32.Parse(ddlItemCode.SelectedValue.Trim()), "ItemId"))
                    {
                        if (hdnGRNId.Value == "0")
                        {
                            DataRow drNewRow = ObjGRN.GRNItems.Tables[0].NewRow();
                            drNewRow["Id"] = DBNull.Value;
                            drNewRow["ItemId"] = Int32.Parse(ddlItemCode.SelectedValue.Trim());
                            drNewRow["ItemCode"] = ddlItemCode.SelectedItem.Text.Trim();
                            drNewRow["GRNDetailsId"] = 0;
                            drNewRow["ItemDescription"] = txtItemName.Text.Trim();
                            
                            Int32 qty = Int32.Parse(txtQuantity.Text.Trim());
                            drNewRow["ReceivedQty"] = qty;

                            Decimal itemCost = Decimal.Parse(hdnItemValue.Value.Trim());
                            drNewRow["ItemValue"] = itemCost;
                            drNewRow.EndEdit();
                            ObjGRN.GRNItems.Tables[0].Rows.Add(drNewRow);
                            gvItemList.DataSource = ObjGRN.GRNItems;
                            gvItemList.DataBind();
                        }
                        else
                        {
                            if (hdnGRNDetailsId.Value != "0")
                            {
                                DataView dv = ObjGRN.GRNItems.Tables[0].DefaultView;
                                dv.Sort = "GRNDetailsId";

                                DataRowView[] drv = dv.FindRows(Int32.Parse(hdnGRNDetailsId.Value.Trim()));
                                drv[0]["ReceivedQty"] = Int32.Parse(txtQuantity.Text.Trim());

                                gvItemList.DataSource = ObjGRN.GRNItems;
                                gvItemList.DataBind();
                            }
                            else if (hdnItemId.Value.Trim() != "0" && hdnGRNDetailsId.Value.Trim() == "0")//GRN Not yet saved
                            {
                                DataView dv = ObjGRN.GRNItems.Tables[0].DefaultView;
                                dv.Sort = "ItemId";

                                DataRowView[] drv = dv.FindRows(Int32.Parse(hdnGRNDetailsId.Value.Trim()));
                                drv[0]["ReceivedQty"] = Int32.Parse(txtQuantity.Text.Trim());

                                gvItemList.DataSource = ObjGRN.GRNItems;
                                gvItemList.DataBind();
                            }

                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "The Item Already Exists In Grid";
                    }
                }
            }
            else if (rblGRNType.SelectedValue == "2")//Sales return
            {
                if (ddlItemCode.SelectedValue != "-1")
                {
                    //todo: Check if item is already in grid
                    if (!this.IsItemInGrid(Int32.Parse(ddlItemCode.SelectedValue.Trim()),"Id"))
                    {
                        if (hdnGRNId.Value == "0")
                        {
                            DataRow drNewRow = ObjGRN.GRNItems.Tables[0].NewRow();
                            drNewRow["Id"] = Int32.Parse(ddlItemCode.SelectedValue.Trim());
                            drNewRow["ItemId"] = DBNull.Value;
                            drNewRow["ItemCode"] = ddlItemCode.SelectedItem.Text.Trim();
                            drNewRow["GRNDetailsId"] = 0;
                            drNewRow["ItemDescription"] = txtItemName.Text.Trim();

                            Int32 qty = Int32.Parse(txtQuantity.Text.Trim());
                            drNewRow["ReceivedQty"] = qty;

                            Decimal itemCost = Decimal.Parse(hdnItemValue.Value.Trim());
                            drNewRow["ItemValue"] = itemCost;
                            drNewRow.EndEdit();
                            ObjGRN.GRNItems.Tables[0].Rows.Add(drNewRow);
                            gvItemList.DataSource = ObjGRN.GRNItems;
                            gvItemList.DataBind();
                        }
                        else
                        {
                            if (hdnGRNDetailsId.Value != "0")
                            {
                                DataView dv = ObjGRN.GRNItems.Tables[0].DefaultView;
                                dv.Sort = "GRNDetailsId";

                                DataRowView[] drv = dv.FindRows(Int32.Parse(hdnGRNDetailsId.Value.Trim()));
                                drv[0]["ReceivedQty"] = Int32.Parse(txtQuantity.Text.Trim());

                                gvItemList.DataSource = ObjGRN.GRNItems;
                                gvItemList.DataBind();
                            }
                            else if (hdnItemId.Value.Trim() != "0" && hdnGRNDetailsId.Value.Trim() == "0")//GRN Not yet saved
                            {
                                DataView dv = ObjGRN.GRNItems.Tables[0].DefaultView;
                                dv.Sort = "ItemId";

                                DataRowView[] drv = dv.FindRows(Int32.Parse(hdnGRNDetailsId.Value.Trim()));
                                drv[0]["ReceivedQty"] = Int32.Parse(txtQuantity.Text.Trim());

                                gvItemList.DataSource = ObjGRN.GRNItems;
                                gvItemList.DataBind();
                            }

                        }
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "The Item Already Exists In Grid";
                    }
                }
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

            txtReceivedTotal.Text = Decimal.Round(Total, 2).ToString();

            ObjGRN.TotalAmount = Total;
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
            txtQuantity.Text = String.Empty;
            txtMaxRecievable.Text = String.Empty;
            txtCost.Text = String.Empty;
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
                ddlPOCode.Enabled = true;
            }
            else
            {
                trGrid.Visible = true;
                trNoRecords.Visible = false;
                ddlPOCode.Enabled = false;
            }
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ddlPOCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPOCode.SelectedValue.Trim() != "-1")
            {
                //
                // Load Pending PO's
                //
                hdnPOId.Value = ddlPOCode.SelectedValue.Trim();
                ObjGRNPO.POId = Int32.Parse(ddlPOCode.SelectedValue.Trim());
                //hdnPOId.Value = ObjGRNPO.POId.ToString();

                DataSet dsPOItems = ObjGRNPO.GetAllPartialyReceivedPOItemsByPOID();
                if (dsPOItems == null || dsPOItems.Tables.Count == 0)
                {
                    ddlPOCode.Items.Add(new ListItem("--No Records--", "-1"));
                }
                else
                {
                    Master.BindDropdown("ItemDescription", "ItemId", dsPOItems, ddlItemCode);
                    ddlItemCode.Items.Add(new ListItem("--Please Select--", "-1"));
                    ddlItemCode.SelectedValue = "-1";
                    txtPOAmmount.Text = Decimal.Round(ObjGRNPO.POAmount, 2).ToString();
                    txtSupplierName.Text = ObjGRNPO.SupplierName;
                }
            }
            else
            {
                this.ClearItemDetails();
            }
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

    protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 temp = e.RowIndex;
            if (rblGRNType.SelectedValue=="1")//PO
            {
                DataRow[] dr = ObjGRN.GRNItems.Tables[0].Select("ItemId=" + gvItemList.DataKeys[temp]["ItemId"].ToString());
                dr[0].Delete();

                ddlPOCode_SelectedIndexChanged(ddlPOCode, EventArgs.Empty);//Fill all the pending Item codes
            }
            else
            {
                DataRow[] dr = ObjGRN.GRNItems.Tables[0].Select("Id=" + gvItemList.DataKeys[temp]["Id"].ToString());
                dr[0].Delete();

                btnConfirm_Click(btnConfirm, EventArgs.Empty);//Fill 
            }

            gvItemList.DataSource = ObjGRN.GRNItems;
            gvItemList.DataBind();

            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();

            Session["ObjGRN"] = ObjGRN;
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

    protected void btnAddGRN_Click(object sender, EventArgs e)
    {
        try
        {

            
            ObjGRN.Rec_By = Master.LoggedUser.UserId;
            ObjGRN.Rec_Date = dtpGRNDate.Date;
            
            if (rblGRNType.SelectedValue == "1")//PO
            {
                ObjGRN.POId = Int32.Parse(ddlPOCode.SelectedValue.Trim() != "-1" ? ddlPOCode.SelectedValue.Trim() : "0");
                ObjGRN.InvId = null;
                ObjGRN.CreditNote = String.Empty;
                ObjGRN.SuplierInvNo = txtSupplierInvNo.Text.Trim();
                if (hdnOldInvNumber.Value.Trim() != txtSupplierInvNo.Text.Trim())
                {
                    if (true == new GRNDAO().IsSupplierInvNoExist(ObjGRN))
                    {
                        lblError.Text = "Supplier Invoice Number Already exists!!";
                        lblError.Visible = true;
                        return;
                    }    
                }
                
            }
            else if (rblGRNType.SelectedValue == "2")//Sales return
            {
                ObjGRN.POId = null;
                ObjGRN.InvId = Int32.Parse(hdnInvoiceId.Value.Trim());
                ObjGRN.CreditNote = txtCreditNote.Text.Trim();
                ObjGRN.SuplierInvNo = String.Empty;
            }

            if (ObjGRN.Save())
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                hdnGRNId.Value = ObjGRN.GRNId.ToString();
                lblGRNNo.Text = ObjGRN.GRNId.ToString();
                btnPrint.Visible = true;
                btnAddGRN.Enabled = false;
                btnAddGRN.CssClass = "show_success";
            }
            else
            {
                btnPrint.Visible = false;
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddGRN_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            ObjGRN.GRNInvoice.InvoiceNo = txtInvoiceNumber.Text.Trim().ToUpper();
            if (!ObjGRN.GRNInvoice.GetInvoiceByInvoiceNumber())
            {
                lblInvError.Visible = true;
                lblInvError.Text = "Incorrect Invoice Number!";
                return;
            }
            else
            {
                if (ObjGRN.GRNInvoice.Status == Structures.InvoiceStatus.Cancelled)
                {
                    lblInvError.Text = "This is a cancelled Invoice!";
                    return;
                }
                txtInvoiceNumber.ReadOnly = true;
                lblInvError.Visible = false;
                hdnInvoiceId.Value = ObjGRN.GRNInvoice.InvoiceId.ToString();
            }

            lblInvoiceTotal.Text = Decimal.Round(ObjGRN.GRNInvoice.GrandTotal, 2).ToString();

            //
            // Fill Item code dropdown
            //
            DataSet dsInvitems = ObjGRN.GRNInvoice.DsInvoiceDetails;
            DataView dv = dsInvitems.Tables[0].DefaultView;
            dv.Sort = "ItemId";
            dv.RowFilter = "(ItemId > 0 OR ItemId is NOT NULL) and (IssuedQTY is NOT NULL OR IssuedQTY>0)";

            //dsInvitems.Merge(dv.Table);

            if (dsInvitems == null || dsInvitems.Tables.Count == 0)
            {
                ddlPOCode.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                //Master.BindDropdown("ItemCode", "Id", dsInvitems, ddlItemCode);//Id = InvoiceDetail Id
                ddlItemCode.DataSource = dv;
                ddlItemCode.DataTextField = "ItemCode";
                ddlItemCode.DataValueField = "Id";
                ddlItemCode.DataBind();

                ddlItemCode.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlItemCode.SelectedValue = "-1";
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
            if (e.Row.RowType==DataControlRowType.DataRow)
            {
                ListItem li = new ListItem();
                if (rblGRNType.SelectedValue == "1")//PO
                {
                    li = ddlItemCode.Items.FindByValue(gvItemList.DataKeys[e.Row.RowIndex]["ItemId"].ToString());
                }
                else if (rblGRNType.SelectedValue == "2")
                {
                     li = ddlItemCode.Items.FindByValue(gvItemList.DataKeys[e.Row.RowIndex]["Id"].ToString());
                }
                
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

            ClientScript.RegisterStartupScript(this.GetType(), "Invoice", "<script language='javascript'>LoadGRNPrintPopup('FromURL=RecieveGoods.aspx?GRNId=" + hdnGRNId.Value.Trim() + "');</script>");
        }
        catch (Exception)
        {
            
            throw;
        }
    }
}
