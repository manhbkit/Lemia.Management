
using Lemia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.IO
{
    public class BusinessLayer
    {
        string _className = "BusinessLayer";
        DataLayer _dl = new DataLayer();
        #region Category

        public List<Category> Categories_GetAll()
        {
            try
            {
                DataLayer dl = new DataLayer();
                return dl.Categories_GetAll().ToList<Category>();
            }
            catch (Exception ex)
            {
               Common.Utils.WriteLog(ex.Message, this._className);
            }
            return null;
        }

        public int Categories_Ins(string cateName,  string description, int cateId)
        {
            try
            {
                return _dl.Categories_Ins(cateName,description,cateId);
            }
            catch (Exception ex)
            {
                Common.Utils.WriteLog("Error::Categories_Ins::" + ex.Message, this._className);
            }
            return -1;
        }
        #endregion

       
    }
}
