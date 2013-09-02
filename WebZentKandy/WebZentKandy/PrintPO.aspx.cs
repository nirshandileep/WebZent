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
using LankaTiles.POManagement;
using LankaTiles.SupplierManagement;
using LankaTiles.UserManagement;

public partial class PrintPO : System.Web.UI.Page
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
                this.CheckIfEditPO();
                this.SetData();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }
    
    private void CheckIfEditPO()
    {
        try
        {
            if (Request.QueryString["POId"] != null && Request.QueryString["POId"].Trim() != String.Empty)
            {
                hdnPOId.Value = Request.QueryString["POId"].Trim();
                lblPOCode.Text = Request.QueryString["POId"].Trim();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }

    private void SetData()
    {
        try
        {
            // If edit po 
            if (ObjPurchaseOrder.POId > 0)
            {
                lblPOCode.Text = ObjPurchaseOrder.POCode.Trim();
                
                Supplier supplier = new Supplier();
                supplier.SupId = ObjPurchaseOrder.SupId;
                if (supplier.GetSupplierByID())
                {
                    lblSupplier.Text = supplier.SupplierName;
                }

                if (Decimal.Round((ObjPurchaseOrder.POAmount - ObjPurchaseOrder.BalanceAmount), 2) == 0)
                {
                    lblTotalPaid.Text = String.Empty;
                }
                else
                {
                    lblTotalPaid.Text = Decimal.Round((ObjPurchaseOrder.POAmount - ObjPurchaseOrder.BalanceAmount), 2).ToString();
                }

                lblBalancePayment.Text = Decimal.Round(ObjPurchaseOrder.BalanceAmount, 2).ToString();
                lblPOAmount.Text = Decimal.Round(ObjPurchaseOrder.POAmount, 2).ToString();

                if (ObjPurchaseOrder.RequestedBy.HasValue)
                {
                    User user = new User();
                    user.UserId = ObjPurchaseOrder.RequestedBy.Value;
                    new UsersDAO().GetUserByID(user);
                    lblRequestedBy.Text = user.FirstName;
                }
                

                gvItemList.DataSource = ObjPurchaseOrder.DsPOItems;
                gvItemList.DataBind();
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void btnAddNewSupplier_Click(object sender, EventArgs e)
    {

    }
}
