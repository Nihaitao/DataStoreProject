﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Campus
{
    public class W_Campus
    {

        /// <summary>
        /// 校区表
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int ID { set; get; }


        /// <summary>
        /// 校区名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 系统id
        /// </summary>
        public int System_Station_ID { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { set; get; }


        /// <summary>
        /// 操作人
        /// </summary>
        public int AddPerson { set; get; }


        /// <summary>
        /// 禁用启用  1启用 0禁用
        /// </summary>
        public int Valid { set; get; }
    }
}
