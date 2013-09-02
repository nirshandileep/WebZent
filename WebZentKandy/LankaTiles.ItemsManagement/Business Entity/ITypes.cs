using System;
using System.Collections.Generic;
using System.Text;
using LankaTiles.ItemsManagement.Services;

namespace LankaTiles.ItemsManagement.Business_Entity
{
    [Serializable]
    public class ITypes
    {
        private int _typeId;
        private string _typeName;

        public int TypeId
        {
            get { return _typeId; }
            set { _typeId = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        #region Constructor

        public ITypes()
        {
            TypeId = 0;
        }

        #endregion

        /// <summary>
        /// This method will be used to save the Items
        /// Currently it is only the Insert done
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            if ((new TypesDAO()).AddType(this))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
