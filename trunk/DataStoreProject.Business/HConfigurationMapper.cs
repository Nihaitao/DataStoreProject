﻿using CommonFramework.Exceptions;
using CommonFramework.IBatisNet;
using DataStoreProject.Model.Entity.HConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Business
{
    public class HConfigurationMapper:BaseMapper
    {
        /// <summary>
        /// 获取学员设置的数据
        /// </summary>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public H_Configuration GetConfigurationByStationID(int System_Station_ID)
        {
            H_Configuration model= Orm.Single<H_Configuration>(x => x.System_Station_ID == System_Station_ID);
             if (model == null)
            {
               return SqlMapper.QueryForObject<H_Configuration>("GetConfigurationByStationID",new{System_Station_ID=System_Station_ID });
            }
            return model;
        }

        /// <summary>
        /// 添加学员设置的数据  
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool InsertConfiguration(H_Configuration Model, int System_Station_ID)
        {
            Model.System_Station_ID = System_Station_ID;
            if (Orm.Insert(Model)<=0)
            {
                throw new ApiException("添加失败");
            }
            return true;
        }

        /// <summary>
        /// 修改设置的数据  
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool UpdateConfiguration(H_Configuration Model, int System_Station_ID)
        {
           H_Configuration model= Orm.Single<H_Configuration>(x=>x.System_Station_ID==System_Station_ID);
           if (model == null) 
           {
               throw new ApiException("未找到要修改的数据");
           }
           Model.System_Station_ID = model.System_Station_ID;
           if (Orm.Update(Model) <= 0) 
           {
               throw new ApiException("修改失败");
           }
           return true;
        }

        /// <summary>
        /// 获取本站点课程设置的数据[系统配置通用] 
        /// </summary>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public W_Configuration GetWConfiguration(int System_Station_ID)
        {
            return Orm.Single<W_Configuration>(x => x.System_Station_ID == System_Station_ID);
        }

        /// <summary>
        /// 添加课程设置 
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool InsertWConfiguration(W_Configuration Model, int System_Station_ID)
        {
            Model.System_Station_ID = System_Station_ID;
            if (Orm.Insert(Model) <= 0)
            {
                throw new ApiException("添加失败");
            }
            return true;
        }

        /// <summary>
        /// 修改课程设置  
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool UpdateWConfiguration(W_Configuration Model, int System_Station_ID)
        {
            W_Configuration model = Orm.Single<W_Configuration>(x => x.System_Station_ID == System_Station_ID);
            if (model == null)
            {
                throw new ApiException("未找到要修改的数据");
            }
            if (Orm.Update(Model) <= 0)
            {
                throw new ApiException("修改失败");
            }
            return true;
        }

    }
}
