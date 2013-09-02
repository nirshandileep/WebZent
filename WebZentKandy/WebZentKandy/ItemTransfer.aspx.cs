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
using LankaTiles.LocationManagement;
using LankaTiles.Common;
using LankaTiles.Exception;
using ItemPr = LankaTiles.ItemsManagement;
using LankaTiles.ItemsManagement;

public partial class ItemTransfer : System.Web.UI.Page
{

    private ItemPr.ItemTransfer objItemTransfer;

    public ItemPr.ItemTransfer ObjItemTransfer
    {
        get 
        {
            if (objItemTransfer == null)
            {
                if (Session["ObjItemTransfer"] == null)
                {
                    objItemTransfer = new ItemPr.ItemTransfer();
                    objItemTransfer.TransferId = Int32.Parse(hdnTransferID.Value.Trim() == String.Empty ? "0" : hdnTransferID.Value.Trim());

                    objItemTransfer.GetItemTransferByTransferID();
                    Session["ObjItemTransfer"] = objItemTransfer;
                }
                else
                {
                    objItemTransfer = (ItemPr.ItemTransfer)(Session["ObjItemTransfer"]);
                }
            }
            return objItemTransfer; 
        }
        set 
        { 
            objItemTransfer = value;
            Session["ObjItemTransfer"] = objItemTransfer;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["ObjIGroup"] = null;
                this.CheckFromURL();
                this.LoadInitialData();
                this.CheckIfItemSelect();
                this.CheckIsEditTransfer();
                this.SetData();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    private void SetData()
    {
        try
        {
            if (hdnFromURL.Value.Trim() != "ProductSearch.aspx")
            {
                Master.ClearSessions();//Clear all sessions
            }

            // If edit transfer
            if (ObjItemTransfer.TransferId > 0 || hdnItemIdFromSearch.Value.Trim() != "0")
            {
                if (ObjItemTransfer.ReceivedBy > 0)
                {
                    this.DisableControls();
                }
                hdnTransferID.Value = ObjItemTransfer.TransferId.ToString();
                ddlFromBranch.SelectedValue = ObjItemTransfer.BranchFrom.ToString();
                ddlToBranch.SelectedValue = ObjItemTransfer.BranchTo.ToString();
                lblDate.Text = ObjItemTransfer.TransferDate.ToShortDateString();

                gvItemList.DataSource = ObjItemTransfer.DsTransferInvoiceItems;
                gvItemList.DataBind();
                txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            }

            if (hdnItemIdFromSearch.Value.Trim() != "0")
            {
                Int32 itemIdFS = Int32.Parse(hdnItemIdFromSearch.Value.Trim());

                Item objItem = new Item();
                objItem.ItemId = itemIdFS;
                objItem.GetItemByID();
                if (ddlToBranch.SelectedValue == "-1")
                {
                    lblError.Text = "From branch has to be selected!";
                }
                objItem.GetItemsByItemIDAndBranchID(Int32.Parse(ddlFromBranch.SelectedValue.Trim()));
                txtItemCode.Text = objItem.ItemCode.ToString();
                txtItemName.Text = objItem.ItemDescription.Trim();
                txtCategory.Text = objItem.CategoryName.Trim();
                txtItemCount.Value = objItem.QuantityInHand.ToString();
            }

            this.ShowHideGrid();
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
                ddlFromBranch.Enabled = true;
                ddlToBranch.Enabled = true;
            }
            else
            {
                trNoRecords.Visible = false;
                trGrid.Visible = true;
                ddlFromBranch.Enabled = false;
                ddlToBranch.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void DisableControls()
    {
        try
        {

            ddlFromBranch.Enabled = false;
            ddlToBranch.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
            RequiredFieldValidator2.Enabled = false;
            RequiredFieldValidator4.Enabled = false;
            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator6.Enabled = false;
            RequiredFieldValidator7.Enabled = false;
            txtItemCode.ReadOnly = true;
            txtSearchItem.Enabled = false;
            txtCategory.Enabled = false;
            CompareValidator1.Enabled = false;
            btnSavePOItem.Enabled = false;
            gvItemList.Enabled = false;
            btnSaveTransfer.Enabled = false;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void CheckIsEditTransfer()
    {
        try
        {
            if (Request.QueryString["TransferId"] != null && Request.QueryString["TransferId"].Trim() != String.Empty)
            {
                hdnTransferID.Value = Request.QueryString["TransferId"].Trim();
                
                //Make the page readonly
                this.DisableControls();
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
            lblDate.Text = DateTime.Now.ToShortDateString();

            //
            // Add From Branch
            // 
            DataSet dsFromBranch = (new LocationsDAO()).GetAllBranches();
            if (dsFromBranch == null || dsFromBranch.Tables[0].Rows.Count == 0)
            {
                ddlFromBranch.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("BranchCode", "BranchId", dsFromBranch, ddlFromBranch);
                ddlFromBranch.Items.Insert(0,new ListItem("--Please Select--", "-1"));
                ddlFromBranch.Items.Insert(1, new ListItem("Main Store", "2"));
                ddlFromBranch.SelectedValue = "-1";
            }

            this.FillToBranch();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void FillToBranch()
    {
        try
        {
            // 
            // Fill the To Branch
            // 
            DataSet dsToBranch = (new LocationsDAO()).GetAllBranches();
            if (dsToBranch == null || dsToBranch.Tables[0].Rows.Count == 0)
            {
                ddlToBranch.Items.Add(new ListItem("--No Records--", "-1"));
            }
            else
            {
                Master.BindDropdown("BranchCode", "BranchId", dsToBranch, ddlToBranch);
                ddlToBranch.Items.Insert(0,new ListItem("--Please Select--", "-1"));
                ddlToBranch.Items.Insert(1, new ListItem("Main Store", "2"));
                ddlToBranch.SelectedValue = "-1";
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

    protected void txtSearchItem_Click(object sender, EventArgs e)
    {
        try
        {
            Item objItem = new Item();
            objItem.ItemCode = txtItemCode.Text.Trim();
            objItem.GetItemsByItemCode();

            if (objItem.ItemId > 0)
            {
                objItem.GetItemsByItemIDAndBranchID(Int32.Parse(ddlFromBranch.SelectedValue.Trim()));
                txtItemName.Text = objItem.ItemDescription.Trim();
                txtCategory.Text = objItem.CategoryName.ToString();
                txtItemCount.Value = objItem.QuantityInHand.ToString();
                if (!objItem.IsActive)
                {
                    lblError.Visible = true;
                    lblError.Text = String.Format(Constant.MSG_Item_Transfer_InActive, objItem.ItemCode);
                    return;
                }
                else
                {
                    lblError.Visible = false;
                    lblError.Text = String.Empty;
                }
            }
            else
            {
                ClearItemControls();
                //ObjItemTransfer.TransferId = Int16.Parse(hdnTransferID.Value.Trim() != "0" ? hdnTransferID.Value.Trim() : "0");
                ObjItemTransfer.TransferBy = hdnTransferBy.Value == "0" ? Master.LoggedUser.UserId : Int32.Parse(hdnTransferBy.Value);
                ObjItemTransfer.TransferDate = DateTime.Now;
                ObjItemTransfer.BranchFrom = Int32.Parse(ddlFromBranch.SelectedValue.Trim());
                ObjItemTransfer.BranchTo = Int32.Parse(ddlToBranch.SelectedValue.Trim());
                Session["ObjItemTransfer"] = ObjItemTransfer;

                string fromBrId = ddlFromBranch.SelectedValue.Trim();
                Response.Redirect("ProductSearch.aspx?FromURL=ItemTransfer.aspx&FromBranchId=" + fromBrId, false);
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    private void ClearItemControls()
    {
        txtItemCode.Text = string.Empty;
        txtItemName.Text = string.Empty;
        txtCategory.Text = string.Empty;
    }

    protected void btnSavePOItem_Click(object sender, EventArgs e)
    {
        try
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

                    DataRow newRow = ObjItemTransfer.DsTransferInvoiceItems.Tables[0].NewRow();
                    newRow["ItemId"] = objItem.ItemId.ToString();
                    newRow["ItemCode"] = objItem.ItemCode;
                    newRow["ItemDescription"] = objItem.ItemDescription.Trim();
                    newRow["Quantity"] = Int32.Parse(txtQuantity.Text.Trim());
                    newRow["BrandName"] = objItem.BrandName.Trim();
                    newRow.EndEdit();
                    ObjItemTransfer.TransferQty += Int32.Parse(txtQuantity.Text.Trim());
                    ObjItemTransfer.DsTransferInvoiceItems.Tables[0].Rows.Add(newRow);
                }
                else
                {

                    //Update existing
                    DataView dvUpdate = ObjItemTransfer.DsTransferInvoiceItems.Tables[0].DefaultView;

                    dvUpdate.Sort = "ItemId";
                    DataRowView[] dr = dvUpdate.FindRows(hdnItemId.Value.Trim());

                    dr[0]["ItemId"] = objItem.ItemId.ToString();
                    dr[0]["ItemCode"] = objItem.ItemCode;
                    dr[0]["ItemDescription"] = objItem.ItemDescription.Trim();
                    dr[0]["Quantity"] = Int32.Parse(txtQuantity.Text.Trim());
                    dr[0]["BrandName"] = objItem.BrandName.Trim();
                    dr[0].EndEdit();

                }

                gvItemList.DataSource = ObjItemTransfer.DsTransferInvoiceItems;
                gvItemList.DataBind();


            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Item code " + txtItemCode.Text.Trim() + " does not exist!";
                return;
            }

            Session["ObjItemTransfer"] = ObjItemTransfer;

            txtItemCode.Text = String.Empty;
            txtQuantity.Text = String.Empty;
            txtItemName.Text = String.Empty;
            txtCategory.Text = String.Empty;
            hdnItemId.Value = "0";

            this.ShowHideGrid();
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    private bool IsItemExistInGrid()
    {
        try
        {
            DataView dvCount = ObjItemTransfer.DsTransferInvoiceItems.Tables[0].DefaultView;
            dvCount.Sort = "ItemCode";

            int length = -1;
            length = dvCount.Find(txtItemCode.Text.Trim());
            if (length !=-1)
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

    protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 temp = e.RowIndex;
            DataRow[] dr = ObjItemTransfer.DsTransferInvoiceItems.Tables[0].Select("ItemId=" + gvItemList.DataKeys[temp].Value.ToString());
            dr[0].Delete();

            gvItemList.DataSource = ObjItemTransfer.DsTransferInvoiceItems;
            gvItemList.DataBind();

            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();

            Session["ObjItemTransfer"] = ObjItemTransfer;
            this.ShowHideGrid();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    protected void gvItemList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    protected void ddlFromBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.FillToBranch();
            if (ddlFromBranch.SelectedValue == "-1")
            {
                return;
            }

            ListItem li = ddlToBranch.Items.FindByValue(ddlFromBranch.SelectedValue.Trim());
            if (li != null)
            {
                ddlToBranch.Items.Remove(li);
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlFromBranch_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }

    protected void btnSaveTransfer_Click(object sender, EventArgs e)
    {
        try
        {
            ObjItemTransfer.BranchFrom = Int32.Parse(ddlFromBranch.SelectedValue);
            ObjItemTransfer.BranchTo = Int32.Parse(ddlToBranch.SelectedValue);

            if (ObjItemTransfer.BranchFrom > 0 && ObjItemTransfer.BranchTo > 0 && ObjItemTransfer.Add())
            {
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                lblError.Visible = true;
            }
            else
            {
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
                lblError.Visible = true;
            }
            
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSaveTransfer_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            }
            else
            {
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);
            }
        }
    }
}
