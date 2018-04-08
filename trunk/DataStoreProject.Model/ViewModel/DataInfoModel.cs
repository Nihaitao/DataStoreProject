﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    /// <summary>
    /// 资料表
    /// </summary>
    public class DataInfoModel1:PageModel
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        public int Course_ID { get; set; }

        public int CourseUnit_ID { get; set; }

        public int CourseChapters_ID { get; set; }
        public int System_Station_ID { get; set; }//机构ID
        public string Title { get; set; }//资料标题
        public int BusType { get; set; }//业务类型，0课程  1网课点播Chapter_ID，2网课直播面授unit_classtime_ID
        public int BusID { get; set; }//业务外键ID
        public int DataInfoType { get; set; }//资料类型 1教材  2教辅 3其他
        public string Path { get; set; }//资料路径
        public string Content { get; set; }//资料内容
        public int DataSize { get; set; }//资料大小  单位  k
        public int IsOpen { get; set; }//是否公开 1公开 0不公开
        public int DownCount { get; set; }//下载次数
        public string AddTime { get; set; }//添加时间
        public int AddPerson { get; set; }//添加人
    }
}
