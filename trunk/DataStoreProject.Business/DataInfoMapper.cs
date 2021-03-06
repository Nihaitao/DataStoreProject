﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonFramework.Exceptions;
using CommonFramework.IBatisNet;
using DataStoreProject.Model.Entity.DataInfo;
using DataStoreProject.Model.ViewModel;

namespace DataStoreProject.Business
{
    public class DataInfoMapper : BaseMapper
    {
        /// <summary>
        /// 获取全部资料列表 BY SZF
        /// </summary>
        /// <param name="model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public IList GetDatainfoList(DataInfoModel1 model, int System_Station_ID)
        {
            return QueryForList("selectDatainfoList", new { Course_ID = model.Course_ID, System_Station_ID, pageIndex = (model.PageIndex - 1) * model.PageSize, pageSize = model.PageSize, pageStatus = model.PageStatus });
        }

        /// <summary>
        /// 获取全部资料列表总条数 BY SZF
        /// </summary>
        /// <param name="model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public int selectDatainfoCount(DataInfoModel1 model, int System_Station_ID)
        {
            return SqlMapper.QueryForObject<int>("selectDatainfoCount", new {Course_ID=model.Course_ID, System_Station_ID, pageStatus = 0 });
        }

        public bool AddDataInfo(W_DataInfo model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ApiException("标题不能为空");
                //当出现异常后，可以直接抛ApiException，拦截器将会拦截异常，并格式化输出
            }
            return Orm.Insert(model) > 0;
        }

        public bool UpdateDataInfo(W_DataInfo test)
        {
            if (test == null)
            {
                throw new ApiException("对象错误");
                //当出现异常后，可以直接抛ApiException，拦截器将会拦截异常，并格式化输出
            }
            return Orm.Update(test) > 0;
        }
        public bool DeleteDataInfo(int ID)
        {
            return Orm.Delete<W_DataInfo>(x => x.ID == ID)>0;
        }
        public W_DataInfo GetDownInfo(int id)
        {
            return Orm.Single<W_DataInfo>(x => x.ID == id);
        }

    }
}