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
using LankaTiles.UserManagement;
using LankaTiles.Common;
using LankaTiles.Exception;

public partial class Main : System.Web.UI.MasterPage
{
    #region Private properties
    private User loggedUser;
    #endregion

    #region Public Properties
    public User LoggedUser 
    {
        get 
        {
            if (loggedUser != null)
            {
                return loggedUser;
            }
            else 
            {
                if (Session["LoggedUser"] != null)
                {
                    loggedUser = (User)Session["LoggedUser"];
                    return loggedUser;
                }
                else 
                {
                    Response.Redirect("UserLogin.aspx", false);
                }
                return loggedUser;
            }
        }
        set
        {
            loggedUser = value;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoggedUser"] != null)
            {
                lblLoggedUser.Text = LoggedUser.FirstName.Trim()+ " " + LoggedUser.LastName.Trim();
                ManageUserAccess();
            }
            else
            {
                Session["LoggedUser"] = null;
                Response.Redirect("UserLogin.aspx", false);
            }
            lblLoadTime.Text = DateTime.Now.ToLocalTime().ToString("hh:mm:ss tt");
        }
        catch (Exception ex)
        {
            ex.Data.Add("UILayerException", this.GetType().ToString() + Constant.Error_Seperator + "protected void Page_Load(object sender, EventArgs e)");
            if (this.LoggedUser != null && this.LoggedUser.UserName != null && this.LoggedUser.UserName != string.Empty)
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, this.LoggedUser.UserName), false);
            else
                Response.Redirect("Error.aspx?LogId=" + LankaTilesExceptions.WriteEventLogs(ex, Constant.Database_Connection_Name, "Annonimous"), false);

        }
    }

    #region public methods

    /// <summary>
    /// Manage user privileges according to user role
    /// </summary>
    public void ManageUserAccess()
    {
        try
        {
            if (LoggedUser.UserRoleID == (int)LankaTiles.Common.Structures.UserRoles.Cashier)
            {
                ulUserManagemennt.Visible = false;
                ulSupplier.Visible = false;
                ulPO.Visible = false;
                ulVouchers.Visible = false;
                ulReports.Visible = false;
                liSearchInvoice.Visible = false;
                ulChqManagement.Visible = false;
            }

            if (LoggedUser.UserRoleID == (int)LankaTiles.Common.Structures.UserRoles.InventoryUser)
            {
                ulCustomer.Visible = false;
                ulItemsManagement.Visible = false;

                //Inventory
                liRecieveGoods.Visible = false;
                liGRNSearch.Visible = false;
                liItemTransfer.Visible = false;
                liSearchTransfers.Visible = false;

                ulSales.Visible = false;
                ulVouchers.Visible = false;
                ulReports.Visible = false;
                ulChqManagement.Visible = false;
                ulUserManagemennt.Visible = false;

            }

            if (LoggedUser.UserRoleID == (int)LankaTiles.Common.Structures.UserRoles.Manager)
            {
                ulUserManagemennt.Visible = false;
                ulSupplier.Visible = false;
                ulPO.Visible = false;
                ulVouchers.Visible = false;
                ulChqManagement.Visible = false;

                //ulReports.Visible = false;
                liSearchInvoice.Visible = false;

                //sub items
                liReportSalesByItem.Visible = false;
                liReportPurchaseOrderMain.Visible = false;
                liReportPurchaseOrders.Visible = false;
                liReportPaymentsReveived.Visible = false;
                liReportvoucherexpences.Visible = false;
                liReportdaybook.Visible = false;
                liReportSalesBySalesRep.Visible = false;
                liSuppHistReport.Visible = false;
            }

            if (LoggedUser.UserRoleID==(int)LankaTiles.Common.Structures.UserRoles.Administrator)
            {
                ulSupplier.Visible = false;
                ulPO.Visible = false;
                ulInventory.Visible = false;

                //vouchers
                liAddVoucher.Visible = false;
                liSearchVoucher.Visible = false;

                ulReports.Visible = false;
                ulChqManagement.Visible = false;
                ulUserManagemennt.Visible = false;

            }

            if (LoggedUser.UserRoleID == (int)LankaTiles.Common.Structures.UserRoles.AdminAssistance)
            {

                liItemSearch.Visible = true;
                liItemTransfer.Visible = false;
                liSearchTransfers.Visible = false;

                ulReports.Visible = false;
                ulChqManagement.Visible = false;
                ulUserManagemennt.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    /// <summary>
    ///		This method binds a drop down list to a given DataSet
    /// </summary>
    /// <param name="textField">
    ///		Field from the datasource to use for the option text
    /// </param>
    /// <param name="valueField">
    ///		Field from the datasource to use for the option value 
    /// </param>
    /// <param name="dsDataSet">
    ///		DataSource
    /// </param>
    /// <param name="dropDownListID">
    ///		Name of the Drop down list
    /// </param>
    public void BindDropdown(string textField, string valueField, DataSet dsDataSet, System.Web.UI.WebControls.DropDownList dropDownListID)
    {
        try
        {
            dropDownListID.DataSource = dsDataSet;
            //set DataTextField property only if it is not null
            if (null != textField)
            {
                dropDownListID.DataTextField = textField;
            }
            //set DataValueField property only if it is not null
            if (null != valueField)
            {
                dropDownListID.DataValueField = valueField;
            }
            dropDownListID.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }

    /// <summary>
    ///		This method binds a drop down list to a given DataSet
    /// </summary>
    /// <param name="textField">
    ///		Field from the datasource to use for the option text
    /// </param>
    /// <param name="valueField">
    ///		Field from the datasource to use for the option value 
    /// </param>
    /// <param name="dsDataSet">
    ///		DataSource
    /// </param>
    /// <param name="dropDownListID">
    ///		Name of the Drop down list
    /// </param>
    public void BindCheckList(string textField, string valueField, DataSet dsDataSet, System.Web.UI.WebControls.CheckBoxList checkBoxListID)
    {
        try
        {
            checkBoxListID.DataSource = dsDataSet.Tables[0];
            //set DataTextField property only if it is not null
            if (null != textField)
            {
                checkBoxListID.DataTextField = textField;
            }
            //set DataValueField property only if it is not null
            if (null != valueField)
            {
                checkBoxListID.DataValueField = valueField;
            }
            checkBoxListID.DataBind();
        }

        catch(Exception ex)
        {
            throw ex;
        }
        finally
        {
        }
    }

    /// <summary>
    /// Clear all sessions in the system, all newly added sessions needs to be added to this method
    /// </summary>
    public void ClearSessions()
    {
 
        ///
        /// Reports sessions
        ///
        Session["SalesItemReport"] = null;
        Session["PurchaseItemReport"] = null;
        Session["InvoiceReportOption"] = null;
        Session["InvoiceReport"] = null;
        Session["Report"] = null;
        Session["VoucherExpencesReport"] = null;
        Session["DayBookReport"] = null;
        Session["SalesItemReportByRep"] = null;
        Session["PDChequeReport"] = null;
        Session["SupPayReport"] = null;
        Session["CustomerFullReport"] = null;

        ///
        /// Objects in Sessions
        ///
        Session["ObjLocation"] = null;
        Session["ObjCustomer"] = null;
        Session["ObjGatePass"] = null;        
        Session["ObjGRNPO"] = null;
        Session["ObjGRN"] = null;
        Session["ObjInv"] = null;
        Session["ObjItem"] = null;        
        Session["ObjIGroup"] = null;
        Session["ObjItemTransfer"] = null;
        Session["ObjBrand"] = null;
        Session["ObjSupplier"] = null;
        Session["ObjUser"] = null;
        Session["ObjVoucher"] = null;
        Session["ObjPurchaseOrder"] = null;
        Session["ObjPurchaseReturn"] = null;

        ///
        /// Lists
        ///
        Session["CategoryList"] = null;
        Session["VoucherPOList"] = null;
        Session["SearchItems"] = null;
        Session["DsReceivableVouchers"] = null;
        Session["GRNSearchResults"] = null;
        Session["VouchersReceivable"] = null;
        Session["PaymentsRecieved"] = null;
        Session["ReceivableInvoiceDetails"] = null;
        Session["Error"] = null;

        ///
        /// Search sessions
        ///
        Session["SearchPO"] = null;
        Session["SearchCustomers"] = null;
        Session["SearchGatePass"] = null;
        Session["SearchGroups"] = null;
        Session["SearchInvoice"] = null;
        Session["SearchTransfers"] = null;
        Session["SearchUser"] = null;
        Session["SearchVoucher"] = null;
        Session["SearchSuppliers"] = null;
        Session["SearchPurchaseReturns"] = null;
        Session["SearchChqSrchResults"] = null;

    }

    #endregion
}
