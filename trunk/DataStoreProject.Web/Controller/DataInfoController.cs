﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonFramework;
using CommonFramework.Exceptions;
using CommonFramework.IBatisNet.SqlMap;
using CommonFramework.Mvc;
using CommonFramework.Mvc.Attribute;
using CT.Common.Mvc;
using DataStoreProject.Business;
using DataStoreProject.Model.Entity.DataInfo;
using DataStoreProject.Model.ViewModel;
namespace DataStoreProject.Web.Controller
{
    public class DataInfoController : StationBaseApiController
    {
        /// <summary>
        /// 获取全部资料列表 BY SZF
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetDatainfoList([FromUri]DataInfoModel1 model)
        {
            DataInfoMapper mapper = new DataInfoMapper();

            var dict = mapper.GetDatainfoList(model, this.System_Station_ID);
            return Success(dict, model.PageIndex == 1 ? mapper.selectDatainfoCount(model, this.System_Station_ID) : 0);
        }
        /// <summary>
        /// 添加资料
        /// </summary>
        /// <param name="model">W_DataInfo</param>
        /// <returns></returns>
        [HttpPost]

        public dynamic AddDatainfo(DataInfoModel1 model)
        {
            W_DataInfo newModel = new W_DataInfo();
            if (model != null)
            {
                if (model.BusType == 0)
                {
                    newModel.BusType = 0;
                    newModel.BusID = model.Course_ID;

                }
                else if (model.BusType == 1) 
                {
                    newModel.BusType = 1;
                    newModel.BusID = model.CourseChapters_ID;
                }
                else if (model.BusType == 2) 
                {
                    newModel.BusType = 2;
                    newModel.BusID = model.CourseUnit_ID;
                }
                newModel.Title = model.Title;
                newModel.Path = model.Path;
                newModel.DownCount = model.DownCount;
                newModel.DataSize = model.DataSize;
                newModel.DataInfoType = model.DataInfoType;
                newModel.Content = model.Content;
                newModel.AddPerson = this.AccountID;
                newModel.AddTime = DateTime.Now.ToString();
                newModel.System_Station_ID = this.System_Station_ID;
                DataInfoMapper mapper = new DataInfoMapper();
                var dict = mapper.AddDataInfo(newModel);
                if (dict)
                    return Success("操作成功");
                else
                    return Success("操作失败");
            }
            else
            {
                return Success("操作失败");
            }
        }
        /// <summary>
        /// 修改资料 by szf
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateDatainfo(W_DataInfo model)
        {
            if (model != null)
            {
                model.System_Station_ID = this.System_Station_ID;

                DataInfoMapper mapper = new DataInfoMapper();

                if (mapper.UpdateDataInfo(model))
                    return Success("操作成功");
                else
                    return Success("操作失败");
            }
            else
            {
                return Success("操作失败");
            }
        }

        /// <summary>
        /// 删除资料 by szf
        /// </summary>
        /// <param name="ID">主键id</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteDatainfo(W_DataInfo model)
        {
            DataInfoMapper mapper = new DataInfoMapper();

            if (mapper.DeleteDataInfo(model.ID))
                return Success("操作成功");
            else
                return Success("操作失败");
        }
        /// <summary>
        /// 下载资料并且加一次下载数 by szf 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetDownInfo(int ID)
        {
            DataInfoMapper mapper = new DataInfoMapper();
            var dict = mapper.GetDownInfo(ID);
            dict.DownCount = dict.DownCount + 1;
            if (mapper.UpdateDataInfo(dict))
            {
                return Success(dict);
            }
            else
            {
                return Success("下载失败");
            }
        }
    }
}