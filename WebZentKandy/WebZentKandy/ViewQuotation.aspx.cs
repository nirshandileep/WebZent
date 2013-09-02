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
using LankaTiles.InvoiceManagement;
using LankaTiles.Exception;
using LankaTiles.Common;
using LankaTiles.CustomerManagement;
using LankaTiles.UserManagement;

public partial class ViewQuotation : System.Web.UI.Page
{
    private Invoice objInv;

    public Invoice ObjInv
    {
        get
        {
            if (objInv == null)
            {
                if (Session["ObjInv"] == null)
                {
                    objInv = new Invoice();
                    objInv.InvoiceId = 0;

                    objInv.GetInvoiceByInvoiceID();
                    Session["ObjInv"] = objInv;
                }
                else
                {
                    objInv = (Invoice)(Session["ObjInv"]);
                }
            }
            return objInv;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.SetData();
            }
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    private void SetData()
    {
        try
        {
            if (ObjInv!=null)
            {

                lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    //ObjInv.CreatedDate.ToString("dd-MMM-yyyy");

                Customer cust = new Customer();
                cust.CustomerID = ObjInv.CustomerID.HasValue ? ObjInv.CustomerID.Value : 1;
                cust.GetCustomerByID();
                lblCustomerName.Text = cust.Cus_Name;

                User user = new User();
                user.UserId = ObjInv.CreatedUser;
                user.GetUserByID();

                lblSalesPerson.Text = user.FirstName;

                if (ObjInv.DsInvoiceDetails != null)
                {
                    gvItemList.DataSource = ObjInv.DsInvoiceDetails;
                    gvItemList.DataBind();
                }

                lblInvAmount.Text = Decimal.Round(ObjInv.GrandTotal, 2).ToString();

            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
