using CT.Common.Mvc;
using DataStoreProject.Business;
using DataStoreProject.Model.Entity.Dictionary;
using DataStoreProject.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DataStoreProject.Web.Controller
{
    public class HDictionaryController : StationBaseApiController
    {
        HDictionaryMapper mapper = new HDictionaryMapper();
        /// <summary>
        /// 获取字典列表 通过列名
        /// </summary>
        /// <param name="System_Station_ID">机构ID</param>
        /// <param name="Valid">状态</param>
        /// <param name="ColumnName">列名</param>
        /// <returns>List<H_Dictionary></returns>
        [HttpGet]
        public dynamic GetHDictionaryList(int Valid, string ColumnName)
        {
            return Success(mapper.GetHDictionaryList(this.System_Station_ID, Valid, ColumnName));
        }

       

    }
}