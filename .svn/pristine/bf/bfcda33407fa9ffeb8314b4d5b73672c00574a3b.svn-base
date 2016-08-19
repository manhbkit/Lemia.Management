
using Lemia.Entities;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lemia.IO
{
    public class DataLayer
    {
        #region Category
        public int Categories_Ins(string cateName, string description, int cateId)
        {
            object obj = SqlHelper.ExecuteScalar(ConnectionString.Local_Crawler_ConnectionString, "Categories_Ins",
              cateName, description, cateId);
            if (obj != null)
            {
                return int.Parse(obj.ToString());
            }
            return -1;
        }
        public IDataReader Categories_GetAll()
        {
            try
            {
                return (IDataReader)SqlHelper.ExecuteReader(ConnectionString.Local_Crawler_ConnectionString, "Categories_GetAll");
            }
            catch (Exception ex)
            {
            }
            return null;
        }

       
        #endregion

       
    }
}
