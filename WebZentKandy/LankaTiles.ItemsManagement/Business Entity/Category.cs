using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace LankaTiles.ItemsManagement
{
    [Serializable]
    public class Category
    {
        #region private variables

        private Int32 categoryId;
        private String categoryType;
        private String categoryDesc;

        #endregion

        #region Public Variables

        public Int32 CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        public String CategoryType
        {
            get { return categoryType; }
            set { categoryType = value; }
        }

        public String CategoryDesc
        {
            get { return categoryDesc; }
            set { categoryDesc = value; }
        }

        #endregion

        #region constructor

        public Category()
        {
            this.categoryId = 0;
            this.categoryType = String.Empty;
            this.categoryDesc = String.Empty;
        }

        #endregion

        #region Save

        /// <summary>
        /// Insert and update to be handled here
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                if (this.CategoryId > 0)
                {
                    return (new CategoryDAO()).UpdateCategory(this);
                }
                else
                {
                    return (new CategoryDAO()).AddCategory(this);
                }
            }
            catch(System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Inactivate the Category
        /// </summary>
        public bool Delete()
        {
            try
            {
                return (new CategoryDAO()).DeleteCategory(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Get Categories By ID

        public bool GetCategoriesByID()
        {
            try
            {
                return new CategoryDAO().GetCategoriesByID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return false;
            }
        }

        #endregion

        #region Get Brands By Category ID

        public DataSet GetBrandsByCategoryID()
        {
            try
            {
                return new CategoryDAO().GetBrandsByCategoryID(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null;
            }
        }

        #endregion

        #region Search Categories

        public DataSet SearchCategories()
        {
            try
            {
                return new CategoryDAO().SearchCategories(this);
            }
            catch (System.Exception ex)
            {
                throw ex;
                return null                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                ;
            }
        }

        #endregion
    }
}
