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

public partial class ReportItemHistory : System.Web.UI.Page
{
    private DataSet dsItemHist;

    public DataSet DsItemHist
    {
        get
        {
            if (dsItemHist == null)
            {
                if (Session["DsItemHist"] == null)
                {

                }
            }
            return dsItemHist; 
        }
        set 
        { 
            dsItemHist = value; 
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["DsItemHist"] = null;
            DataSet dsItems = new ItemsDAO().GetAllItems();
            Master.BindDropdown("ItemDescription", "ItemId", dsItems, ddlItems);
            ddlItems.DataBind();
            ddlItems.Items.Add(new ListItem("", "0"));
            ddlItems.SelectedValue = "0";
        }

        if (IsCallback)
        {
            gvItemsTransactionHistory.DataSource = (DataSet)Session["DsItemHist"];
            gvItemsTransactionHistory.DataBind();
        }
    }


    protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        int itemId = 0;
        itemId = int.Parse(ddlItems.SelectedValue);

        Session["DsItemHist"] = new ItemsDAO().GetItemHistoryByItemId(itemId);

        gvItemsTransactionHistory.DataSource = (DataSet)Session["DsItemHist"];
        gvItemsTransactionHistory.DataBind();

    }
}
