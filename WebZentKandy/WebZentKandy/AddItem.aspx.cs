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
using LankaTiles.ItemsManagement.Business_Entity;
using LankaTiles.ItemsManagement.Services;
using LankaTiles.ItemsManagement;

public partial class AddItem : System.Web.UI.Page
{

    private Item objItem;

    public Item ObjItem
    {
        get
        {
            if (objItem == null)
            {
                if (Session["ObjItem"] == null)
                {
                    //string strUserID = Page.Request.QueryString["UserId"] != null ? Page.Request.QueryString["UserId"].ToString() : "";
                    String strItemId = hdnItemId.Value.Trim();
                    objItem = new Item();

                    if (strItemId != "")
                    {
                        objItem.ItemId = Convert.ToInt32(strItemId);
                        objItem.GetItemByID();
                        Session["ObjItem"] = objItem;
                    }
                }
                else
                {
                    objItem = (Item)(Session["ObjItem"]);
                }
            }
            return objItem;
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
                this.CheckIsEditItem();
                this.SetData();
                this.AddAttributes();
                this.ShowHideByRole();
                SetActiveTab();
//                LoadAlterations();
            }

            if (IsCallback)
            {
                LoadAlterations();
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

    private void ShowHideByRole()
    {
        if (Master.LoggedUser.UserRoleID == 7)
        {
            if (ObjItem.ItemId>0)
            {
                //trbrandCode.Visible = false;
                //trBrandCodeVal.Visible = false;
                //trCost.Visible = false;
                //trCostValidation.Visible = false;
                //trInvoicedQTY.Visible = false;
                //trMinSellPrice.Visible = false;
                //trMinSellPriceValidation.Visible = false;
                //trQIH.Visible = false;
                //trSelPrice.Visible = false;
                //trSelPriceVal.Visible = false;
                //trType.Visible = false;
                //trROL.Visible = false;
                //trROLVal.Visible = false;
                //trCatCode.Visible = false;
                //trCatCodeVal.Visible = false;
                //trStatus.Visible = false;
                //trStatusVal.Visible = false;                

                txtCost.Enabled = false;
                txtSelPrice.Enabled = false;
                txtMinSelPrice.Enabled = false;
                ddlBrandCode.Enabled = false;
                btnAddBrand.Enabled = false;
                ddlType.Enabled = false;
                btnAddType.Enabled = false;
                txtROL.Enabled = false;
                ddlCatCode.Enabled = false;
                btnAddNewCategory.Enabled = false;
                ddlStatus.Enabled = false;
            }
            else
            {
                txtCost.Enabled = true;
                txtSelPrice.Enabled = true;
                txtMinSelPrice.Enabled = true;
                ddlBrandCode.Enabled = true;
                btnAddBrand.Enabled = true;
                ddlType.Enabled = true;
                btnAddType.Enabled = true;
                txtROL.Enabled = true;
                ddlCatCode.Enabled = true;
                btnAddNewCategory.Enabled = true;
                ddlStatus.Enabled = true;

            }

        }
        if (Master.LoggedUser.UserRoleID == (int)Structures.UserRoles.SuperAdmin)
        {
            btnSaveAdj.Visible = true;
        }
        else
        {
            btnSaveAdj.Visible = false;
        }
    }

    private void AddAttributes()
    {
        txtROL.Attributes.Add("onkeypress", "return numbersOnly(this, event);");
    }

    public void LoadInitialData()
    {
        try
        {
            //
            // category
            //
            DataSet dsCategory = (new CategoryDAO()).GetAllCategories();
            if (dsCategory==null || dsCategory.Tables.Count == 0)
            {
                ddlCatCode.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("CategoryType", "CategoryId", dsCategory, ddlCatCode);
                ddlCatCode.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlCatCode.SelectedValue = "-1";
            }

            //
            // Brand
            //
            DataSet dsBrands = new BrandsDAO().GetAllBrands();
            if (dsBrands == null || dsBrands.Tables.Count == 0)
            {
                ddlBrandCode.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("BrandName", "BrandId", dsBrands, ddlBrandCode);
                ddlBrandCode.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlBrandCode.SelectedValue = "-1";
            }

            //
            // Status
            //
            ListItem[] status = { new ListItem("--Please Select--", "-1"), new ListItem("Active", "1"), new ListItem("InActive", "0") };
            ddlStatus.Items.AddRange(status);
            ddlStatus.SelectedValue = "1";

            //
            // Types
            //
            DataSet dsTypes = (new TypesDAO()).GetAllTypes();
            if (dsTypes == null || dsTypes.Tables.Count == 0)
            {
                ddlType.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("TypeName", "TypeId", dsTypes, ddlType);
                ddlType.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlType.SelectedValue = "-1";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SetData()
    {
        try
        {
            if (ObjItem.ItemId > 0)
            {
                txtItemCode.Text = ObjItem.ItemCode.Trim();
                txtItemDesc.Text = ObjItem.ItemDescription.Trim();
                txtCost.Text = Decimal.Round(ObjItem.Cost, 2).ToString();
                txtSelPrice.Text = Decimal.Round(ObjItem.SellingPrice, 2).ToString();
                txtMinSelPrice.Text = Decimal.Round(ObjItem.MinSellingPrice, 2).ToString();
                if (ddlCatCode.Items.FindByValue(ObjItem.CategoryId.ToString()) == null)
                {
                    ddlCatCode.Items.Add(new ListItem(ObjItem.CategoryName, ObjItem.CategoryId.ToString()));
                }
                ddlCatCode.SelectedValue = ObjItem.CategoryId.ToString();
                txtROL.Text = ObjItem.ROL.ToString();
                if (ddlBrandCode.Items.FindByValue(ObjItem.BrandId.ToString()) == null)
                {
                    ddlBrandCode.Items.Add(new ListItem(ObjItem.BrandName, ObjItem.BrandId.ToString()));
                }
                ddlBrandCode.SelectedValue = ObjItem.BrandId.ToString();
                ddlStatus.SelectedValue = ObjItem.IsActive ? "1" : "0";
                lblQIH.Text = ObjItem.QuantityInHand.ToString();
                lblInvoicedQty.Text = ObjItem.InvoicedQty.ToString();
                Page.Title = "Edit Item";

                if (ddlType.Items.FindByValue(ObjItem.IType.TypeId.ToString()) == null)
                {
                    if (ObjItem.IType.TypeId == 0)
                    {
                        ddlType.SelectedValue = "-1";
                    }
                    ddlType.Items.Add(new ListItem(ObjItem.IType.TypeName, ObjItem.IType.TypeId.ToString()));
                }
                else
                {
                    ddlType.SelectedValue = ObjItem.IType.TypeId.ToString();
                }

            }
            else
            {
                txtItemCode.Text = new ItemsDAO().GetNextItemCode();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "private void CheckFromURL()");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void CheckIsEditItem()
    {
        try
        {
            if (Request.QueryString["ItemId"] != null && Request.QueryString["ItemId"].Trim() != String.Empty)
            {
                hdnItemId.Value = Request.QueryString["ItemId"].Trim();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAddNewCategory_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddCategory.aspx" + "?FromURL=AddItem.aspx&ItemId=" + hdnItemId.Value, false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddNewCategory_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    #region Methods
    
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
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "private void CheckFromURL()");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    #endregion

    protected void btnAddBrand_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("AddItemBrand.aspx?" + "FromURL=AddItem.aspx&ItemId=" + hdnItemId.Value, false);
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddBrand_Click(object sender, EventArgs e)");
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
            ObjItem.ItemDescription = txtItemDesc.Text.Trim();
            ObjItem.ItemCode = txtItemCode.Text.Trim();
            ObjItem.Cost = Decimal.Parse(txtCost.Text.Trim());
            ObjItem.SellingPrice = Decimal.Parse(txtSelPrice.Text.Trim());
            ObjItem.MinSellingPrice = Decimal.Parse(txtMinSelPrice.Text.Trim());
            ObjItem.CategoryId = Int32.Parse(ddlCatCode.SelectedValue.Trim());
            ObjItem.ROL = Int32.Parse(txtROL.Text.Trim());
            ObjItem.BrandId = Int32.Parse(ddlBrandCode.SelectedValue.Trim());
            ObjItem.UpdatedUser = Master.LoggedUser.UserId;
            ObjItem.UpdatedDate = DateTime.Now;
            ObjItem.IsActive = ddlStatus.SelectedValue == "1" ? true : false;
            ObjItem.IType.TypeId = Int32.Parse(ddlType.SelectedValue.Trim());

            if (ObjItem.Save())
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                Session["ObjItem"] = ObjItem;
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;
            }

            ShowHideByRole();
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
            if (hdnFromURL.Value != null && hdnFromURL.Value != String.Empty)
            {
                Response.Redirect(hdnFromURL.Value.Trim(), false);
            }
            else
            {
                Response.Redirect("Home.aspx", false);
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

    //protected void lbtnAddGroupItem_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Response.Redirect("GroupItemAdd.aspx", false);
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void lbtnAddGroupItem_Click(object sender, EventArgs e)");
    //        if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
    //            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
    //        else
    //            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

    //    }
    //}

    protected void ddlBrandCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Brand tmpBrand = new Brand();
            tmpBrand.BrandId = Int32.Parse(ddlBrandCode.SelectedItem.Value.Trim() == String.Empty ? "-1" : ddlBrandCode.SelectedItem.Value.Trim());
            DataSet dsCategories = tmpBrand.GetBrandCategoriesByBrandID();

            if (dsCategories == null || dsCategories.Tables.Count == 0)
            {
                ddlCatCode.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("CategoryType", "CategoryId", dsCategories, ddlCatCode);
                ddlCatCode.Items.Add(new ListItem("--Please Select--", "-1"));
                ddlCatCode.SelectedValue = "-1";
            }
            
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void ddlBrandCode_SelectedIndexChanged(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnAddType_Click(object sender, EventArgs e)
    {
        try
        {
            ddlType.Visible = false;
            btnAddType.Visible = false;

            RequiredFieldValidator10.Enabled = true;
            txtType.Text = String.Empty;
            txtType.Visible = true;
            btnTypeSave.Visible = true;
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddType_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnTypeSave_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean save = true;
            ITypes type = new ITypes();
            type.TypeName = txtType.Text.Trim();
            if (type.Save())
            {

                //
                // Types
                //
                DataSet dsTypes = (new TypesDAO()).GetAllTypes();
                if (dsTypes == null || dsTypes.Tables.Count == 0)
                {
                    ddlType.Items.Add(new ListItem("--No Data Found--", "-1"));
                }
                else
                {
                    Master.BindDropdown("TypeName", "TypeId", dsTypes, ddlType);
                    ddlType.Items.Add(new ListItem("--Please Select--", "-1"));
                    ddlType.SelectedValue = type.TypeId.ToString();
                }

                ddlType.Visible = true;
                RequiredFieldValidator10.Enabled = false;
                txtType.Visible = false;
                btnTypeSave.Visible = false;
                btnAddType.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnTypeSave_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void tabControlItems_TabClick(object source, DevExpress.Web.ASPxTabControl.TabControlCancelEventArgs e)
    {
        SetActiveTab();
    }

    private void SetActiveTab()
    {
        if (this.tabControlItems.Tabs[0].IsActive == true)
        {
            trItemDetails.Visible = true;
            trItemStockChanges.Visible = false;
        }
        else
        {
            trItemDetails.Visible = false;
            trItemStockChanges.Visible = true;

            //
            // Set Stock adjustment detials
            //
            if (ObjItem.QuantityInHand != 0)
            {
                seQuantity.MinValue = ObjItem.QuantityInHand * -1;
            }
            seQuantity.MaxValue = 999999;

            LoadAlterations();
        }

    }

    protected void tabControlItems_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {
        SetActiveTab();
    }

    protected void btnSaveAdj_Click(object sender, EventArgs e)
    {
        try
        {
            if (seQuantity.Text == "0")
            {
                lblError.Visible = true;
                lblError.Text = "Invalid Quantity";
                return;
            }
            else
            {
                lblError.Visible = false;
                lblError.Text = "";
            }

            ItemStockAdjustment stock   = new ItemStockAdjustment();
            stock.Description           = txtDescription.Text.Trim();
            stock.Qty                   = Int32.Parse(seQuantity.Text);
            stock.ItemId                = ObjItem.ItemId;
            stock.UserId                = Master.LoggedUser.UserId;

            if (new ItemsDAO().AddItemsAdjustment(stock))
            {
                txtDescription.Text = string.Empty;
                seQuantity.Text = "0";

                LoadAlterations();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSaveAdj_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private void LoadAlterations()
    {
        try
        {
            gvItemsStockChanges.DataSource = new ItemsDAO().GetItemsAdjustmentsByItem(ObjItem);
            gvItemsStockChanges.DataBind();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
