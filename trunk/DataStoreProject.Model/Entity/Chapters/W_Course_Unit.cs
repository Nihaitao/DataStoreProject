﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Chapters
{
    public class W_Course_Unit
    {

        /// <summary>
        /// 网课单元表
        /// </summary>
       [PrimaryKey, AutoIncrement]
        public int ID { set; get; }


        /// <summary>
        /// 单元名称
        /// </summary>
        public string Name { set; get; }



        /// <summary>
        /// 网课id
        /// </summary>
        public int Course_ID { set; get; }




        /// <summary>
        /// 系统id
        /// </summary>
        public int System_Station_ID { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Orders { set; get; }

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
