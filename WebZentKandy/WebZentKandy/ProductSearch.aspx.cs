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
using LankaTiles.ItemsManagement;
using LankaTiles.Common;
using LankaTiles.Exception;
using LankaTiles.LocationManagement;
using LankaTiles.SupplierManagement;
using DevExpress.Web.ASPxGridView;
using System.Drawing;
using DevExpress.Web.ASPxEditors;
using LankaTiles.InvoiceManagement;

public partial class ProductSearch : System.Web.UI.Page
{

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                Session["SearchItems"] = null;
                Session["ShowRoomIndex"] = null;
                this.LoadInitialData();
                this.CheckFromURL();
                this.ShowHideControlsByUser();
                this.SetUIForURLParams();
                this.Search();
                Session["ShowRoomIndex"] = ddlBranch.SelectedIndex.ToString();
            }
            
            if (IsCallback)
            {
                if (Session["ShowRoomIndex"].ToString() != ddlBranch.SelectedIndex.ToString())
                {
                    Session["ShowRoomIndex"] = ddlBranch.SelectedIndex.ToString();
                    this.Search();
                }

                if (Session["SearchItems"] != null)
                {
                    dxgvItemList.DataSource = Session["SearchItems"];
                    dxgvItemList.DataBind();
                }
                this.LoadInvoiceItems();
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

    #endregion

    #region Methods

    private void LoadInvoiceItems()
    {
        try
        {
            if (Session["ObjInv"] != null)
            {
                Invoice invoice = new Invoice();
                invoice = (Invoice)Session["ObjInv"];

                gvInvoiceItems.DataSource = invoice.DsInvoiceDetails.Tables[0];
                gvInvoiceItems.DataBind();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void ShowHideControlsByUser()
    {
        try
        {
            short userrole = Master.LoggedUser.UserRoleID;

            switch (userrole)
            {
                case 1://SuperUser
                    dxgvItemList.Columns[0].Visible = true;//Use
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.Cost.ToString()].Visible = true;//Cost
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.MinSellingPrice.ToString()].Visible = true;//MS Price
                    btnUpdate.Visible = true;
                    break;
                case 7:
                    dxgvItemList.Columns[0].Visible = true;//Use
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TotalValue.ToString()].Visible = false;//total cost of items
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.InvoicedQty.ToString()].Visible = false;//Inv Quantity
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.MinSellingPrice.ToString()].Visible = false;//MS Price
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.Cost.ToString()].Visible = false;//Cost
                    break;
                default:
                    dxgvItemList.Columns[0].Visible = false;//Use
                    dxgvItemList.Columns["I-Code"].Visible = false;//Edit Item Link
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.Cost.ToString()].Visible = false;//Cost
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.MinSellingPrice.ToString()].Visible = false;//MS Price
                    //dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.QuantityInHand.ToString()].Visible = false;//QIH
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.InvoicedQty.ToString()].Visible = false;//Inv Quantity
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TotalValue.ToString()].Visible = false;//total cost of items
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueQIH.ToString()].Visible = false;//True QTY
                    dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueStockValue.ToString()].Visible = false;//True Stock value

                    btnUpdate.Visible = false;
                    btnExport.Visible = false;
                    break;
            }

            if (ddlBranch.SelectedIndex == ddlBranch.Items.FindByValue(-1).Index)
            {
                dxgvItemList.Columns[0].Visible = false;
                //btnUpdate.Visible = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    private void SetUIForURLParams()
    {
        try
        {

            if (Request.QueryString["FromBranchId"] != null && Request.QueryString["FromBranchId"].Trim() != String.Empty)
            {
                hdnBranchId.Value = Request.QueryString["FromBranchId"].Trim();
            }

            //
            // Disable status if from invoice page
            //
            if (hdnFromURL.Value.Trim() == "AddInvoice.aspx")
            {
                ddlStatus.SelectedValue = "1";
                ddlStatus.Enabled = false;
                this.LoadInvoiceItems();
                btnItemsInInvoice.Visible = true;
            }

            if (hdnFromURL.Value.Trim() == "ItemTransfer.aspx")
            {
                // Add the Main Store to the Dropdown
                if (ddlBranch.Items.FindByValue(Constant.MainStoreId) == null)
                {
                    ddlBranch.Items.Add(new ListEditItem("Main Store", Constant.MainStoreId.ToString()));
                }

                //Set detault filter values for the grid
                //(dxgvItemList.Columns["QIH"] as GridViewDataColumn).Settings.AutoFilterCondition = AutoFilterCondition.Greater;
                dxgvItemList.FilterExpression = Constant.FE_ItemSearch_From_ItemTransfers;

            }
            else
            {
                if (Master.LoggedUser.UserRoleID != (Int16)Structures.UserRoles.SuperAdmin)
                {
                    // Remove the Main Store from the Dropdown
                    if (ddlBranch.Items.FindByValue(Constant.MainStoreId) != null)
                    {
                        ddlBranch.Items.Remove(ddlBranch.Items.FindByValue(Constant.MainStoreId));
                    }
                }
                else
                {
                    ddlBranch.Visible = true;
                }
                
            }

            //Set the from branch in the dropdown
            if (ddlBranch.Items.FindByValue(Int32.Parse(hdnBranchId.Value.Trim())) != null)
            {
                ddlBranch.SelectedIndex = ddlBranch.Items.FindByValue(Int32.Parse(hdnBranchId.Value.Trim())).Index;
                ddlBranch.Enabled = false;
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

    /// <summary>
    /// Fill the default details for the controls
    /// </summary>
    public void LoadInitialData()
    {
        try
        {

            //
            // category
            //
            DataSet dsCategory = (new CategoryDAO()).GetAllCategories();
            if (dsCategory == null || dsCategory.Tables.Count == 0)
            {
                ddlCategory.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("CategoryType", "CategoryId", dsCategory, ddlCategory);
                ddlCategory.Items.Add(new ListItem("All", "-1"));
                ddlCategory.SelectedValue = "-1";
            }

            //
            // Add Branch
            // 
            DataSet dsRoles = (new LocationsDAO()).GetAllBranches();
            if (dsRoles == null || dsRoles.Tables.Count == 0)
            {
                ddlBranch.Items.Add(new ListEditItem("--No Records--", "-1"));
            }
            else
            {
                //Master.BindDropdown("BranchCode", "BranchId", dsRoles, ddlBranch);
                ddlBranch.DataSource = dsRoles;
                ddlBranch.TextField = "BranchCode";
                ddlBranch.ValueField = "BranchId";
                ddlBranch.DataBind();

                ddlBranch.Items.Add(new ListEditItem("All", "-1"));
                ddlBranch.SelectedIndex = ddlBranch.Items.FindByValue(-1).Index;
                //ddlBranch.SelectedItem.Value = -1;
            }

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
                Master.BindDropdown("Sup_Code", "SupId", dsSuppliers, ddlSupplier);
                ddlSupplier.Items.Add(new ListItem("All", "-1"));
                ddlSupplier.SelectedValue = "-1";
            }


            //
            // Brand
            //
            DataSet dsBrands = new BrandsDAO().GetAllBrands();
            if (dsBrands == null || dsBrands.Tables.Count == 0)
            {
                ddlBrands.Items.Add(new ListItem("--No Data Found--", "-1"));
            }
            else
            {
                Master.BindDropdown("BrandName", "BrandId", dsBrands, ddlBrands);
                ddlBrands.Items.Add(new ListItem("All", "-1"));
                ddlBrands.SelectedValue = "-1";
            }

            //
            // Status
            //
            ListItem[] status = { new ListItem("All", "-1"), new ListItem("Active", "1"), new ListItem("InActive", "0") };
            ddlStatus.Items.AddRange(status);

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "public void LoadInitialData()");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    #region Search Items

    /// <summary>
    /// Search the products using the values provided in the search controls
    /// </summary>
    public void Search()
    {
        try
        {
            this.ShowHideGrids();

            ItemsSearch searchItem = new ItemsSearch();
            searchItem.ItemCode = txtItemCode.Text.Trim();
            searchItem.ItemDescription = txtItemDescription.Text.Trim();
            decimal selPrice = 0;
            Decimal.TryParse(txtPrice.Text.Trim(), out selPrice);
            searchItem.SellingPrice = selPrice;
            searchItem.Option = ddlStatus.SelectedValue != "-1" ? Int32.Parse(ddlStatus.SelectedValue.Trim()):-1;
            searchItem.SupId = ddlSupplier.SelectedValue != "-1" ? Int32.Parse(ddlSupplier.SelectedValue.Trim()): 0;
            searchItem.CategoryId = ddlCategory.SelectedValue != "-1" ? Int32.Parse(ddlCategory.SelectedValue.Trim()):0;
            Int64 qih = 0;
            Int64.TryParse(txtQuantityInHand.Text.Trim(), out qih);
            searchItem.QuantityInHand = qih;
            int rol = 0;
            int.TryParse(txtROL.Text.Trim(), out rol);
            searchItem.ROL = rol;
            searchItem.BranchID = ddlBranch.SelectedIndex != -1 ? Int32.Parse(ddlBranch.SelectedItem.Value.ToString().Trim()) : 0;
            searchItem.BrandID = ddlBrands.SelectedValue != "-1" ? Int32.Parse(ddlBrands.SelectedValue.Trim()): 0;

            DataSet dsProd = searchItem.Search();
            Session["SearchItems"] = dsProd;

            if (dsProd != null && dsProd.Tables.Count > 0 && dsProd.Tables[0] != null && dsProd.Tables[0].Rows.Count > 0)
            {
                dxgvItemList.DataSource = dsProd;
                dxgvItemList.DataBind();
                trItemList.Visible = true;
            }
            else
            {

                dxgvItemList.DataSource = null;
                dxgvItemList.DataBind();
            }

        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }
    #endregion

    private void ShowHideGrids()
    {
        try
        {
            if (hdnFromURL.Value.Trim() == "GroupItemAdd.aspx" || hdnFromURL.Value.Trim() == "ItemTransfer.aspx")
            {
                dxgvItemList.Visible = true;
                dxgvItemList.Columns[0].Visible = false;//Edit
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.Cost.ToString()].Visible = false;//Cost
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.MinSellingPrice.ToString()].Visible = false;//Min Selling Price
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.ROL.ToString()].Visible = false;//ROL
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.InvoicedQty.ToString()].Visible = false;//Invoiced QTY

                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueQIH.ToString()].Visible = false;//True QTY
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueStockValue.ToString()].Visible = false;//True Stock value
                btnExport.Visible = false;
            }
            else if (hdnFromURL.Value.Trim() == "AddInvoice.aspx")
            {
                dxgvItemList.Visible = true;
                dxgvItemList.Columns[0].Visible = false;//Edit
                dxgvItemList.Columns["I-Code"].Visible = false;//Edit Item
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.Cost.ToString()].Visible = false;//Cost
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.MinSellingPrice.ToString()].Visible = false;//Min Selling Price
                //dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.QuantityInHand.ToString()].Visible = false;//QIH
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.InvoicedQty.ToString()].Visible = false;//Invoiced QTY
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueQIH.ToString()].Visible = false;//True QTY
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueStockValue.ToString()].Visible = false;//True Stock value
                btnExport.Visible = false;
            }
            else if (hdnFromURL.Value.Trim() == "POAdd.aspx")
            {
                dxgvItemList.Visible = true;
                dxgvItemList.Columns[0].Visible = false;//Edit
                dxgvItemList.Columns["I-Code"].Visible = false;//Invoice Select
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.Cost.ToString()].Visible = false;//Cost
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.MinSellingPrice.ToString()].Visible = false;//Min Selling Price
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.ROL.ToString()].Visible = false;//ROL
                //dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.QuantityInHand.ToString()].Visible = false;//QIH
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.InvoicedQty.ToString()].Visible = false;//Invoiced QTY
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueStockValue.ToString()].Visible = false;//True Stock value

                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueQIH.ToString()].Visible = false;//True QTY
                dxgvItemList.Columns[Structures.ItemSearchGridColumnNames.TrueStockValue.ToString()].Visible = false;//True Stock value
                btnExport.Visible = false;
            }
            else
            {
                dxgvItemList.Visible = true;
                if (Master.LoggedUser.UserRoleID != 1)
                {
                    if (Master.LoggedUser.UserRoleID == 7)//Admin Assistance
                    {
                        dxgvItemList.Columns["I-Code"].Visible = true;
                    }
                    else
                    {
                        dxgvItemList.Columns["I-Code"].Visible = false;
                    }
                    dxgvItemList.Columns[0].Visible = false;
                }
                else
                {
                    dxgvItemList.Columns[0].Visible = true;
                    dxgvItemList.Columns["I-Code"].Visible = true;
                }
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    #endregion

    #region Events

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.Search();
            this.ShowHideControlsByUser();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnSearch_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if (Session["SearchItems"] != null)
        {
            dxgvItemList.DataSource = (DataSet)Session["SearchItems"];
            dxgvItemList.DataBind();
        }
        this.gveItemList.WriteXlsToResponse();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if (Session["SearchItems"] != null)
            {
                ds = (DataSet)Session["SearchItems"];
            }

            if (new ItemsDAO().UpdateItemsInBulk(ds))
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
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnUpdate_Click(object sender, EventArgs e)");
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

    #region Grid events

    /// <summary>
    /// Validate the line item update process
    /// Update will not be done if new value is less than the existing value
    /// Status update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dxgvItemList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        try
        {
            DataRow[] dr = ((DataSet)Session["SearchItems"]).Tables[0].Select("ItemId=" + e.Keys[0].ToString());
            if (dr.Length > 0)
            {

                int newValue;
                int oldValue;

                ///
                /// If old value was not numeric
                ///
                if (!Int32.TryParse(e.OldValues["QuantityInHand"].ToString(), out oldValue))
                {
                    return;
                }

                if (Int32.TryParse(e.NewValues["QuantityInHand"].ToString(), out newValue))
                {

                    dr[0]["QuantityInHand"] = e.NewValues["QuantityInHand"];

                    e.Cancel = true;
                    dxgvItemList.CancelEdit();
                    dxgvItemList.DataBind();
                }
                else
                {
                    return;
                }

                ///
                /// IsActive
                ///
                dr[0]["IsActive"] = e.NewValues["IsActive"];

                e.Cancel = true;
                dxgvItemList.CancelEdit();
                dxgvItemList.DataBind();

            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvItemList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)");
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

    protected void dxgvItemList_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        try
        {
            int newValue;
            int oldValue;

            ///
            /// If old value was not numeric
            ///
            if (!Int32.TryParse(e.OldValues["QuantityInHand"].ToString(), out oldValue))
            {
                e.RowError = "Error updating!";
            }

            if (Int32.TryParse(e.NewValues["QuantityInHand"].ToString(), out newValue))
            {

                ///
                /// check if stock was reduced
                ///
                if (oldValue > newValue)
                {
                    e.RowError = "Cannot reduce quantity using this method";
                }

                ///
                /// check if new value entered is negative
                ///
                if (newValue < 0)
                {
                    e.RowError = "Cannot enter negative values";
                }
            }
            else
            {
                e.RowError = "Invalid new Quaitity";
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvItemList_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)");
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

    protected void dxgvItemList_Init(object sender, EventArgs e)
    {
        try
        {
            this.CheckFromURL();
            string fromURL = hdnFromURL.Value.Trim();

            if (fromURL == String.Empty)
            {
                return;
            }
            else
            {
                GridViewDataHyperLinkColumn colLink         = new GridViewDataHyperLinkColumn();
                colLink.Caption         = "Use";
                colLink.FieldName       = "ItemId";                           //Column parameter, "Id" is URL link ("<a href="3">Edit</a> ")
                colLink.PropertiesHyperLinkEdit.Text        = "Use";          // Display text
                colLink.PropertiesHyperLinkEdit.TextField   = "ItemCode";     //Display content of column "Content", alternative to PropertiesHyperLinkEdit.Text
                colLink.Settings.FilterMode             = ColumnFilterMode.DisplayText;
                colLink.Settings.AutoFilterCondition    = AutoFilterCondition.Contains;

                if (fromURL == "GroupItemAdd.aspx")
                {
                    colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = Constant.URL_Navigate_From_GroupItemAdd;//The URL is replaced {0} to "Id" to especific Row ("/Default.aspx?=3")
                }
                else if (fromURL == "ItemTransfer.aspx")
                {
                    colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = Constant.URL_Navigate_From_ItemTransfer;
                }
                else if (fromURL == "AddInvoice.aspx")
                {
                    colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = Constant.URL_Navigate_From_AddInvoice;
                }
                else if (fromURL == "POAdd.aspx")
                {
                    colLink.PropertiesHyperLinkEdit.NavigateUrlFormatString = Constant.URL_Navigate_From_POAdd;
                }

                colLink.Visible     = true;
                colLink.VisibleIndex = 0;// dxgvItemList.Columns.Count;
                colLink.Width       = 25;
                colLink.PropertiesHyperLinkEdit.Style.BackColor = Color.AntiqueWhite;
                colLink.PropertiesHyperLinkEdit.Style.ForeColor = Color.Black;
                dxgvItemList.Columns.Add(colLink);                     //Add column to ASPxGrid
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvItemList_Init(object sender, EventArgs e)");
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

    protected void dxgvItemList_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        try
        {
            if (e.Column.Caption=="Description")
            {
                (e.Editor as ASPxTextBox).Height = Unit.Pixel(24);
                (e.Editor as ASPxTextBox).Font.Size = FontUnit.Small;
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void dxgvItemList_AutoFilterCellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)");
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

    #endregion

    #endregion

    /// <summary>
    /// Search after branch dropdown select change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.Search();
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
