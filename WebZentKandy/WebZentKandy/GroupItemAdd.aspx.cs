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

public partial class GroupItemAdd : System.Web.UI.Page
{
    private Group objIGroup;

    public Group ObjIGroup
    {
        get
        {   
            if (objIGroup == null)
            {
                if (Session["ObjIGroup"] == null)
                {
                    objIGroup = new Group();
                    objIGroup.GroupId = Int16.Parse(hdnGroupId.Value.Trim() == String.Empty ? "0" : hdnGroupId.Value.Trim());

                    objIGroup.GetGroupByID();
                    Session["ObjIGroup"] = objIGroup;
                }
                else
                {
                    objIGroup = (Group)(Session["ObjIGroup"]);
                }
            }
            return objIGroup;
        }
        set
        {
            objIGroup = value;
            Session["ObjIGroup"] = value;
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
                this.CheckIsEditIGroup();
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
            if (hdnItemIdFromSearch.Value.Trim() != "0")
            {
                Int32 itemIdFS = Int32.Parse(hdnItemIdFromSearch.Value.Trim());

                Item objItem = new Item();
                objItem.ItemId = itemIdFS;
                objItem.GetItemByID();
                txtItemCode.Text = objItem.ItemCode.ToString();
                txtItemName.Text = objItem.ItemDescription.Trim();
                txtBrand.Text = objItem.BrandName;
                txtCategory.Text = objItem.CategoryName;
                Page.Title = "Edit Group Items";
            }
            else
            {
                Page.Title = "Add Group Items";
            }

            if (Session["ObjIGroup"] != null)
            {
                Group group = (Group)Session["ObjIGroup"];
                txtGICode.Text = group.GroupCode.Trim();
                txtGroupName.Text = group.GroupName.Trim();
                txtDescription.Text = group.Description.Trim();
                ddlStatus.SelectedValue = group.IsActive == true ? "1" : "0";
                if (group.GroupItems != null && group.GroupItems.Tables[0].Rows.Count > 0)
                {

                    gvItemList.DataSource = group.GroupItems;
                    gvItemList.DataBind();

                    trNoRecords.Visible = false;
                    trGrid.Visible = true;
                }
                else
                {

                    trNoRecords.Visible = true;
                    trGrid.Visible = false;
                }
                txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
                                
            }

        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }

    private void CheckIsEditIGroup()
    {
        try
        {

            if (Request.QueryString["GroupId"] != null && Request.QueryString["GroupId"].Trim() != String.Empty)
            {
                hdnGroupId.Value = Request.QueryString["GroupId"].Trim();
                short editGroupId = 0;
                short.TryParse(hdnGroupId.Value.ToString().Trim(), out editGroupId);

                if (editGroupId > 0)
                {
                    ObjIGroup.GroupId = editGroupId;
                    if (ObjIGroup.GetGroupByID())
                    {
                        Session["ObjIGroup"] = ObjIGroup;
                    }
                }
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
            // Status
            //
            ListItem[] status = { new ListItem("--Please Select--", "-1"), new ListItem("Active", "1"), new ListItem("InActive", "0") };
            ddlStatus.Items.AddRange(status);
            ddlStatus.SelectedValue = "1";

            txtGICode.Text = new GroupsDAO().GetNextGroupsCode();
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
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "private void CheckFromURL()");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    protected void btnSearchItem_Click(object sender, EventArgs e)
    {
        
        try
        {
            Item objItem = new Item();
            objItem.ItemCode = txtItemCode.Text.Trim();
            objItem.GetItemsByItemCode();
            if (objItem.ItemId > 0)
            {
                txtItemName.Text = objItem.ItemDescription.Trim();
                txtBrand.Text = objItem.BrandName.Trim();
                txtCategory.Text = objItem.CategoryId.ToString();
            }
            else
            {
                ClearItemControls();
                ObjIGroup.GroupId = Int16.Parse(hdnGroupId.Value.Trim() != "0" ? hdnGroupId.Value.Trim() : "0");
                ObjIGroup.GroupName = txtGroupName.Text.Trim();
                ObjIGroup.ItemCount = Int16.Parse(txtLineItemsCount.Value.Trim() == String.Empty ? "0" : txtLineItemsCount.Value.Trim());
                ObjIGroup.Description = txtDescription.Text.Trim();
                ObjIGroup.IsActive = ddlStatus.SelectedItem.Value.Trim() == "0" ? false : true;
                Session["ObjIGroup"] = ObjIGroup;

                Response.Redirect("ProductSearch.aspx?FromURL=GroupItemAdd.aspx", false);
            }
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

    protected void btnAddItem_Click(object sender, EventArgs e)
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

                    DataRow newRow = ObjIGroup.GroupItems.Tables[0].NewRow();
                    newRow["ItemId"] = objItem.ItemId.ToString();
                    newRow["ItemCode"] = objItem.ItemCode;
                    newRow["ItemDescription"] = objItem.ItemDescription.Trim();
                    newRow["BrandName"] = objItem.BrandName;
                    newRow["CategoryName"] = objItem.CategoryName;
                    newRow.EndEdit();

                    ObjIGroup.GroupItems.Tables[0].Rows.Add(newRow);
                
               
                gvItemList.DataSource = ObjIGroup.GroupItems;
                gvItemList.DataBind();

                ClearItemControls();


            }
            else 
            {
                lblError.Visible = true;
                lblError.Text = "Item code " + txtItemCode.Text.Trim() + " does not exist!";
                return;
            }

            Session["ObjIGroup"] = ObjIGroup;

            txtItemCode.Text = String.Empty;
            txtItemName.Text = String.Empty;
            this.ShowHideGrid();
            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();
            
        
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

    private void ClearItemControls()
    {
        txtItemCode.Text = string.Empty;
        txtItemName.Text = string.Empty;
        txtBrand.Text = string.Empty;
        txtCategory.Text = string.Empty;
    }

    protected void gvItemList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int32 temp = e.RowIndex;
            DataRow[] dr = ObjIGroup.GroupItems.Tables[0].Select("ItemId=" + gvItemList.DataKeys[temp].Value.ToString());
            dr[0].Delete();

            gvItemList.DataSource = ObjIGroup.GroupItems;
            gvItemList.DataBind();

            txtLineItemsCount.Value = gvItemList.Rows.Count.ToString();

            Session["ObjIGroup"] = ObjIGroup;
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

    protected void btnAddGroup_Click(object sender, EventArgs e)
    {   

        try
        {
            Group tmpGroup = ObjIGroup;

            tmpGroup.GroupCode = txtGICode.Text.Trim();
            tmpGroup.GroupName = txtGroupName.Text.Trim();
            tmpGroup.Description = txtDescription.Text.Trim();
            tmpGroup.ItemCount = Int16.Parse(txtLineItemsCount.Value.Trim() == String.Empty ? "0" : txtLineItemsCount.Value.Trim());
            tmpGroup.IsActive = ddlStatus.SelectedItem.Value.Trim() == "0" ? false : true;
                        
            if (tmpGroup.Save())
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;

                ObjIGroup = tmpGroup;
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_NotSavedSeccessfully;

                ObjIGroup = null;
            }
            

        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnAddGroup_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private bool IsItemExistInGrid()
    {
        try
        {
            DataView dvCount = ObjIGroup.GroupItems.Tables[0].DefaultView;
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

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {

            Session["ObjIGroup"] = null;
            txtGICode.Text = (new GroupsDAO()).GetNextGroupsCode();
            txtGroupName.Text = String.Empty;
            txtDescription.Text = String.Empty;
            txtItemCode.Text = String.Empty;
            txtItemName.Text = String.Empty;
            txtBrand.Text = String.Empty;
            txtCategory.Text = String.Empty;
            gvItemList.DataSource = null;
            gvItemList.DataBind();
            this.ShowHideGrid();
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void btnClear_Click(object sender, EventArgs e)");
            if (Master.LoggedUser != null && Master.LoggedUser.UserName != null && Master.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, Master.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
}