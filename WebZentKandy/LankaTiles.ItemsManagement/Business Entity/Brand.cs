using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LankaTiles.Exception;
using LankaTiles.Common;

namespace LankaTiles.ItemsManagement
{
    [Serializable]
    public class Brand
    {
        #region Private Variables

        private Int32 brandId;
        private String brandName;
        private DataSet dsCategories;

        #endregion 

        #region Public Properties

        public Int32 BrandId
        {
            get
            {
                return brandId;
            }
            set
            {
                brandId = value;
            }
        }

        public String BrandName
        {
            get 
            { 
                return brandName; 
            }
            set 
            {
                brandName = value; 
            }
        }

        public DataSet DsCategories
        {
            get
            {
                return dsCategories;
            }
            set
            {
                dsCategories = value;
            }
        }

        #endregion

        #region Save

        public bool Save()
        {
            try
             {
                if (this.BrandId > 0)
                {
                    return (new BrandsDAO()).UpdateBrands(this);
                }
                else
                {
                    return (new BrandsDAO()).AddBrands(this);
                }
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool IsCategoryExists(Category objCategory)");
                throw ex;
            }
        }

        #endregion

        #region Get Brand By ID

        public bool GetBrandByID()
        {
            try
            {
                return (new BrandsDAO()).GetBrandByID(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool IsCategoryExists(Category objCategory)");
                throw ex;
            }
        }

        #endregion

        #region Get Brand Categories By BrandID

        public DataSet GetBrandCategoriesByBrandID()
        {
            try
            {
                return (new BrandsDAO()).GetBrandCategoriesByBrandID(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool IsCategoryExists(Category objCategory)");
                return null;
                throw ex;
            }
        }

        #endregion

        #region Search Brands

        public DataSet SearchBrands()
        {
            try
            {
                return (new BrandsDAO()).SearchBrands(this);
            }
            catch (System.Exception ex)
            {
                ex.Data.Add("BusinessLayerException", GetType().ToString() + Constant.Error_Seperator + "public bool IsCategoryExists(Category objCategory)");
                return null;
                throw ex;
            }
        }

        #endregion
    }
}
