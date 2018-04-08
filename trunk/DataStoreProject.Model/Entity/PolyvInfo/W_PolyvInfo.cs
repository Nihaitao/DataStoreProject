﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.PolyvInfo
{
    public class W_PolyvInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }


        /// <summary>
        /// 机构id
        /// </summary>
        public int System_Station_ID { get; set; }


        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// userid
        /// </summary>
        public string userid { get; set; }


        /// <summary>
        /// writetoken
        /// </summary>
        public string writetoken { get; set; }


        /// <summary>
        /// readtoken
        /// </summary>
        public string readtoken { get; set; }

        /// <summary>
        /// secretkey
        /// </summary>
        public string secretkey { get; set; }


        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }


        ///// <summary>
        ///// 【保利威视账号类型，0系统，1自主】
        ///// </summary>
        //[AutoIncrement]
        //public int PolyvSource { set; get; }

    }
}
