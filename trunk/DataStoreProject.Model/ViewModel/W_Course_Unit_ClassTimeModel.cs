﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class W_Course_Unit_ClassTimeModel
    {
        /// <summary>
        /// 网课单元表
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { set; get; }


        /// <summary>
        /// 单元ID
        /// </summary>
        public int Unit_ID { set; get; }


        /// <summary>
        /// 单元名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BegInTime { set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { set; get; }


        /// <summary>
        /// 教师明细id
        /// </summary>
        public int TeacherDetail_ID { set; get; }



        /// <summary>
        /// 教室ID
        /// </summary>
        public int ClassRoom_ID { set; get; }


        /// <summary>
        /// 教室名称
        /// </summary>
        [AutoIncrement]
        public string ClassRoom_Name { set; get; }


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
        /// 操作人名称
        /// </summary>
        [AutoIncrement]
        public string AddPersonName { set; get; }

        /// <summary>
        /// 禁用启用  1启用 0禁用
        /// </summary>
        public int Valid { set; get; }

    }
}
