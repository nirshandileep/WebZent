using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using LankaTiles.Common;    //After the reference is added
using LankaTiles.Exception;
using System.Data;

namespace LankaTiles.LocationManagement
{
    [Serializable]
    public class Location
    {
        #region Private Properties
        private int branchId;
        private String branchCode;
        private String branchName;
        private String address1;
        private String address2;
        private String telephone;
        private String contactName;
        private String invPrefix;
        private bool isActive;
        #endregion

        #region Public properties

        public int BranchId
        {
            get { return branchId; }
            set { branchId = value; }
        }

        public String BranchCode
        {
            get { return branchCode; }
            set { branchCode = value; }
        }

        public String BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }

        public String Address1
        {
            get { return address1; }
            set { address1 = value; }
        }

        public String Address2
        {
            get { return address2; }
            set { address2 = value; }
        }

        public String Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        public String ContactName
        {
            get { return contactName; }
            set { contactName = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public String InvPrefix
        {
            get { return invPrefix; }
            set { invPrefix = value; }
        }

        #endregion

        #region Save
        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 if save fails, else returns locationId</returns>
        public bool Save()
        {
            try
            {
                if (this.BranchId > 0)
                {
                    return (new LocationsDAO()).UpdateLocation(this);
                }
                else
                {
                    return (new LocationsDAO()).AddLocation(this);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Delete
        public bool Delete()
        {
            try
            {
                return (new LocationsDAO()).DeleteLocation(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Get Location By ID

        public bool GetLocationByID()
        {
            try
            {
                return (new LocationsDAO()).GetLocationByID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

    }
}
