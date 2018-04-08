﻿using CT.Common.Mvc;
using DataStoreProject.Business;
using DataStoreProject.Model.ViewModel;
using Entites.PolyvInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace DataStoreProject.Web.Controller
{
    public class PolyvInfoController : StationBaseApiController
    {
        PolyvInfoMapper mapper = new PolyvInfoMapper();

        /// <summary>
        /// 当前站点是否设置了视频并且开启 hx 2018-01-08
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic HasSetAndOpen()
        {
            return Success(mapper.GetPolyvSetByWhere(this.System_Station_ID));
            
        }
        /// <summary>
        /// 上传大文件刷新
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic Hash()
        {
            //string Token = "Y07Q4yopIVXN83n-MPoIlirBKmrMPJu0";
            //string serviceUrl = "http://upload.polyv.net:1080/files/";
            //string writeToken = "cb1ffbac-ae05-4b4e-b310-3f92c344a2a2";
            //string secretkey = "UfmKG8jJ82";
            PolyvInfoModel model = mapper.GetPolyvSetByWhere(this.System_Station_ID);

            var ts = GenerateTimeStamp().ToString();
            var hash = EncryptToMD5(ts + model.writetoken).ToLower();
            var resp = new
            {
                ts,
                hash
            };
            return resp;
        }
        private long GenerateTimeStamp()
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            var t = (DateTime.Now.Ticks - startTime.Ticks) / 10000; //除10000调整为13位        
            return t;
        }
        private string EncryptToMD5(string str)
        {
            using (var md5 = MD5.Create())
            {
                byte[] dataToHash = Encoding.UTF8.GetBytes(str);
                byte[] dataHashed = md5.ComputeHash(dataToHash);
                return BitConverter.ToString(dataHashed).Replace("-", "");
            }
        }


        /// <summary>
        /// 读取视频配置数据 hx 2018-01-08
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetPolyvSetByWhere()
        {
            return Success(mapper.GetPolyvSetByWhere(this.System_Station_ID));
        }

        /// <summary>
        /// 读取主表数据 hx 2018-01-08
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetPolyvInfoByWhere()
        {
            return Success(mapper.GetPolyvInfoByWhere(this.System_Station_ID));
        }

        /// <summary>
        /// 保利威视配置 hx 2018-01-08
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic SavePolyvInfo(PolyvInfoModel model)
        {
            if (model == null) return Success("未传任何参数！");

            model.System_Station_ID = this.System_Station_ID;
            if (model.ID == 0)
            {
                model.AddTime = DateTime.Now;
            }
            var dict = mapper.SavePolyvInfo(model);
            return Success(dict);
        }

        /// <summary>
        /// 查询当前机构配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic GetPolyvInfoModel(PolyvInfoModel model)
        {
            return Success(mapper.GetPolyvInfoModel(this.System_Station_ID));
        }

        /// <summary>
        /// 查询配置好的基础数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetPolyvInfoList()
        {
            return Success(mapper.GetPolyvInfoList());
        }
    }
}