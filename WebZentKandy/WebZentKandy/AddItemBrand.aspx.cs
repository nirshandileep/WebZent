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

public partial class AddItemBrand : System.Web.UI.Page
{

    private DataSet categoryList;
    private Brand objBrand;

    public DataSet CategoryList
    {
        get 
        {
            if (categoryList == null)
            {
                if (Session["CategoryList"] == null)
                {
                    categoryList = new DataSet();
                    categoryList = new CategoryDAO().GetAllCategories();
                    Session["CategoryList"] = categoryList;
                }
                else
                {
                    categoryList = (DataSet)Session["CategoryList"];
                }
            }
            return categoryList; 
        }
        set 
        {
            categoryList = value;
        }
    }

    public Brand ObjBrand
    {
        get
        {
            if (objBrand == null)
            {
                if (Session["ObjBrand"] == null)
                {
                    //string strUserID = Page.Request.QueryString["UserId"] != null ? Page.Request.QueryString["UserId"].ToString() : "";
                    String strItemId = hdnBrandId.Value.Trim();
                    objBrand = new Brand();

                    if (strItemId != "")
                    {
                        objBrand.BrandId = Convert.ToInt32(strItemId);
                        objBrand.GetBrandByID();
                        Session["ObjBrand"] = objBrand;
                    }
                }
                else
                {
                    objBrand = (Brand)(Session["ObjBrand"]);
                }
            }
            return objBrand;
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
                this.CheckIsEditBrand();
                this.SetData();
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

    #region Methods

    public void LoadInitialData()
    {
        try
        {

            Master.BindCheckList("CategoryType", "CategoryId", this.CategoryList, cblCategoryType);
            if (cblCategoryType.Items.Count == 0)
            {
                cblCategoryType.Items.Add(new ListItem("--No Records--", "-1"));
            }
        }
        catch
        {
            
            throw;
        }
     }

    private void SetData()
    {
        try
        {

            if (ObjBrand.BrandId > 0)
            {
                txtBrandName.Text = ObjBrand.BrandName.Trim();

                DataView dv = ObjBrand.DsCategories.Tables[0].DefaultView;
                dv.Sort = "CategoryId";
                

                foreach (ListItem li in cblCategoryType.Items)
                {
                    if (dv.Find(li.Value) != -1)
                    {
                        li.Selected = true;
                    }
                }
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
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void CheckIsEditBrand()
    {
        try
        {
            if (Request.QueryString["BrandId"] != null && Request.QueryString["BrandId"].Trim() != String.Empty)
            {
                hdnBrandId.Value = Request.QueryString["BrandId"].Trim();
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion Methods

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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Brand objBrand = new Brand();
            objBrand.BrandId = Int32.Parse(hdnBrandId.Value);
            objBrand.GetBrandByID();

            objBrand.BrandName = txtBrandName.Text.Trim();

            DataSet dsCatg = objBrand.DsCategories.Copy();

            DataView dvCategories = dsCatg.Tables[0].DefaultView;
            dvCategories.Sort = "CategoryId";

            for (int i = 0; i < cblCategoryType.Items.Count; i++)
            {
                int found = -1;
                found = dvCategories.Find(cblCategoryType.Items[i].Value);

                if (cblCategoryType.Items[i].Selected && found == -1)
                {
                    DataRow newRow = dvCategories.Table.NewRow();
                    newRow["CategoryId"] = Convert.ToInt32(cblCategoryType.Items[i].Value.ToString());
                    newRow["CategoryType"] = cblCategoryType.Items[i].Text;
                    newRow.EndEdit();
                    dvCategories.Table.Rows.Add(newRow);
                }
                else if (cblCategoryType.Items[i].Selected == false && found != -1)
                {
                    dvCategories.Delete(found);
                }
            }

            objBrand.DsCategories = null;
            objBrand.DsCategories = dsCatg;

            if ( objBrand.Save())
            {
                lblError.Visible = true;
                lblError.Text = Constant.MSG_Save_SavedSeccessfully;
                hdnBrandId.Value = objBrand.BrandId.ToString();
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
}
