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
using LankaTiles.InvoiceManagement;
using LankaTiles.ItemsManagement;

public partial class AddGatePass : System.Web.UI.Page
{
    private GetPass objGatePass;

    public GetPass ObjGatePass
    {
        get 
        {
            if (objGatePass == null)
            {
                if (Session["ObjGatePass"] == null)
                {
                    objGatePass = new GetPass();
                    objGatePass.GPId = Int32.Parse(hdnGatePassId.Value.Trim());
                    objGatePass.GetGetPassByID();
                }
                else
                {
                    objGatePass = (GetPass)Session["ObjGatePass"];

                }
            }
            return objGatePass; 
        }
        set { objGatePass = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Master.ClearSessions();//Clear all sessions
                this.LoadInitialData();
                this.CheckFromURL();
                this.CheckIfEditGatePass();
                this.SetUIForURLParams();
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

    private void CheckIfEditGatePass()
    {
        try
        {
            if (Request.QueryString["GPId"] != null && Request.QueryString["GPId"].Trim() != String.Empty)
            {
                hdnGatePassId.Value = Request.QueryString["GPId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void AddAttributes()
    {
        dxPrintPopup.ContentUrl = "~/PrintGatePass.aspx?FromURL=AddGatePass.aspx&GPId=" + hdnGatePassId.Value.Trim();
        btnPrint.OnClientClick = "return ShowPrintWindow()";
    }

    private void SetData()
    {
        try
        {
            if (ObjGatePass.GPId > 0)
            {
                txtGatepassCode.Text = ObjGatePass.GPCode;
                ddlInvoiceNumber.Enabled = false;
                ddlInvoiceNumber.Items.Add(new ListItem(ObjGatePass.InvoiceNo, ObjGatePass.InvoiceId.ToString()));
                ddlInvoiceNumber.SelectedValue = ObjGatePass.InvoiceId.ToString();
                txtInvAmmount.Text = Decimal.Round(ObjGatePass.InvoiceAmmount,2).ToString();

                gvItemList.DataSource = ObjGatePass.DsGatePassDetails;
                gvItemList.DataBind();

                if (gvItemList.Rows.Count > 0)
                {
                    trNoRecords.Visible = false;
                }
                else
                {
                    trNoRecords.Visible = true;
                }

                btnPrint.Visible = true;
            }
            else
            {
                txtGatepassCode.Text = new GetPassDAO().GetNextGetPassCode().Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void SetUIForURLParams()
    {
        
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

    private void LoadInitialData()
    {
        try
        {
            //
            // Invoice
            //
            DataSet dsInvoice = (new InvoiceDAO()).GetAllPaidPartiallyDeliveredInvoices();
            if (dsInvoice == null || dsInvoice.Tables.Count == 0)
            {
                ddlInvoiceNumber.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("InvoiceNo", "InvoiceId", dsInvoice, ddlInvoiceNumber);
                ddlInvoiceNumber.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlInvoiceNumber.SelectedValue = "-1";
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void ddlInvoiceNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlInvoiceNumber.SelectedValue == "-1")
            {
                return;
            }
            else
            {
                //
                // PendingItems
                //
                Invoice inv = new Invoice();
                inv.InvoiceId = Int32.Parse(ddlInvoiceNumber.SelectedValue.Trim());
                DataSet dsItems = (new InvoiceDAO()).GetItemsTobeIssuedByInvoiceID(inv);
                
                if (dsItems == null || dsItems.Tables.Count == 0)
                {
                    ddlItemCode.Items.Add(new ListItem("--No Records--", "-1"));
                }
                else
                {
                    Master.BindDropdown("ItemCode", "Id", dsItems, ddlItemCode);
                    ddlItemCode.Items.Add(new ListItem("--Please Select--", "-1"));
                    ddlItemCode.SelectedValue = "-1";
                }

                inv.GetInvoiceByInvoiceID();
                txtInvAmmount.Text = Decimal.Round(inv.GrandTotal, 2).ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlInvoiceNumber_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void ddlItemCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlItemCode.SelectedValue.Trim() != String.Empty && ddlItemCode.SelectedValue != "-1")
            {
                Invoice inv     = new Invoice();
                inv.InvoiceId   = Int32.Parse(ddlInvoiceNumber.SelectedValue.Trim());

                Item item       = new Item();
                item.ItemId     = Int32.Parse(ddlItemCode.SelectedValue.Trim());

                if (new InvoiceDAO().GetItemTobeIssuedDetailsByInvoiceAndItem(inv, item))
                {
                    txtRemainingQty.Text    = item.QuantityToBeIssued.ToString();
                    txtItemName.Text        = item.ItemDescription.Trim();
                    hdnInvDetId.Value       = inv.InvoiceDetails.InvDetailId.ToString();
                    hdnQIH.Text             = item.QuantityInHand.ToString();
                }
            }
            else
            {
                this.ClearItemDetails();
                return;
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

    private void ClearItemDetails()
    {
        txtRemainingQty.Text = String.Empty;
        txtItemName.Text = String.Empty;
        hdnInvDetId.Value = "0";
        txtQuantity.Text = String.Empty;
        ddlItemCode.SelectedValue = "-1";
    }

    protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 temp = e.RowIndex;
            if (ddlInvoiceNumber.SelectedValue != "-1")
            {
                DataRow[] dr = ObjGatePass.DsGatePassDetails.Tables[0].Select("InvDetID=" + gvItemList.DataKeys[temp]["InvDetID"].ToString());
                dr[0].Delete();

                ddlInvoiceNumber_SelectedIndexChanged(ddlInvoiceNumber, EventArgs.Empty);//Fill all the pending Item codes
            }

            gvItemList.DataSource = ObjGatePass.DsGatePassDetails;
            gvItemList.DataBind();

            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            Session["ObjGatePass"] = ObjGatePass;

            this.ClearItemDetails();
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

    protected void btnAddGatePass_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvItemList.Rows.Count > 0)
            {
                ObjGatePass.GPCode = txtGatepassCode.Text.Trim();
                ObjGatePass.InvoiceId = Int32.Parse(ddlInvoiceNumber.SelectedValue.Trim());
                ObjGatePass.CreatedBy = Master.LoggedUser.UserId;
                ObjGatePass.CreatedDate = DateTime.Now;
                ObjGatePass.ModifiedBy = Master.LoggedUser.UserId;
                ObjGatePass.ModifiedDate = DateTime.Now;
                if (ObjGatePass.Save())
                {
                    hdnGatePassId.Value = ObjGatePass.GPId.ToString();
                    btnPrint.Visible = true;
                    lblError.Visible = true;
                    lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                    this.AddAttributes();
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                }
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddGatePass_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Visible = false;
            if (ddlItemCode.SelectedValue != String.Empty && ddlItemCode.SelectedValue != "-1")
            {
                //todo: Check if item is already in grid
                //if (!this.IsItemInGrid(Int32.Parse(ddlItemCode.SelectedValue.Trim())))
                //{

                //Check if the QIH in main store is sufficient to issue the QTY
                Item item = new Item();
                item.ItemId = Int32.Parse(ddlItemCode.SelectedValue.Trim());
                //new ItemsDAO().GetItemsByItemIDAndBranchID(item, Int32.Parse(Constant.MainStoreId.Trim()));
                //if (item.QuantityInHand < Int32.Parse(txtQuantity.Text.Trim()))
                //{
                //    lblError.Visible = true;
                //    lblError.Text = string.Format(Constant.MSG_Item_InsufficientInMainStore, txtQuantity.Text.Trim(), item.QuantityInHand.ToString(), (Int32.Parse(txtQuantity.Text.Trim()) - item.QuantityInHand).ToString());
                //    return;
                //}

                if (hdnGatePassId.Value == "0")
                {
                    DataRow drNewRow        = ObjGatePass.DsGatePassDetails.Tables[0].NewRow();
                    drNewRow["GPId"]        = 0;
                    drNewRow["InvDetID"]    = Int64.Parse(hdnInvDetId.Value.Trim());
                    Int32 qty               = Int32.Parse(txtQuantity.Text.Trim());
                    drNewRow["Qty"]         = qty;

                    //drNewRow["Id"]          = Int32.Parse(ddlItemCode.SelectedValue.Trim());
                    drNewRow["ItemCode"]    = ddlItemCode.SelectedItem.Text.Trim();
                    drNewRow["ItemDescription"] = txtItemName.Text.Trim();

                    //Decimal itemCost        = Decimal.Parse(hdnItemValue.Value.Trim());
                    //drNewRow["ItemValue"]   = itemCost;
                    drNewRow.EndEdit();
                    ObjGatePass.DsGatePassDetails.Tables[0].Rows.Add(drNewRow);
                    Session["ObjGatePass"] = ObjGatePass;
                    gvItemList.DataSource = ObjGatePass.DsGatePassDetails;
                    gvItemList.DataBind();
                }
                else
                {
                    #region no editing for the moment
                    //if (hdnGRNDetailsId.Value != "0")
                    //{
                    //    DataView dv = ObjGRN.GRNItems.Tables[0].DefaultView;
                    //    dv.Sort = "GRNDetailsId";

                    //    DataRowView[] drv = dv.FindRows(Int32.Parse(hdnGRNDetailsId.Value.Trim()));
                    //    drv[0]["ReceivedQty"] = Int32.Parse(txtQuantity.Text.Trim());

                    //    gvItemList.DataSource = ObjGRN.GRNItems;
                    //    gvItemList.DataBind();
                    //}
                    //else if (hdnItemId.Value.Trim() != "0" && hdnGRNDetailsId.Value.Trim() == "0")//GRN Not yet saved
                    //{
                    //    DataView dv = ObjGRN.GRNItems.Tables[0].DefaultView;
                    //    dv.Sort = "ItemId";

                    //    DataRowView[] drv = dv.FindRows(Int32.Parse(hdnGRNDetailsId.Value.Trim()));
                    //    drv[0]["ReceivedQty"] = Int32.Parse(txtQuantity.Text.Trim());

                    //    gvItemList.DataSource = ObjGRN.GRNItems;
                    //    gvItemList.DataBind();
                    //}
                    #endregion
                }
            }
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            this.ClearItemDetails();
            this.ShowHideGrid();
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

    private void ShowHideGrid()
    {
        try
        {

            if (gvItemList.Rows.Count == 0)
            {
                trGrid.Visible = false;
                trNoRecords.Visible = true;
                ddlInvoiceNumber.Enabled = true;
            }
            else
            {
                trGrid.Visible = true;
                trNoRecords.Visible = false;
                ddlInvoiceNumber.Enabled = false;
            }
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void gvItemList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ListItem li = new ListItem();
                if (ddlInvoiceNumber.SelectedValue != "-1")//PO
                {
                    li = ddlItemCode.Items.FindByValue(gvItemList.DataKeys[e.Row.RowIndex][1].ToString());
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
            //Response.Redirect("PrintGRNPopUp.aspx?FromURL=AddInvoice.aspx&GPId=" + hdnGatePassId.Value.Trim(), false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnPrint_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
}