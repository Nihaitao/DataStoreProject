﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class OrderMyCourseList
    {
        /// <summary>
        /// 教师名称
        /// </summary>
        public string TeacherName { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 授课方式 1录播 2直播 3面授
        /// </summary>
        public int TeachingMethod { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课程封面图
        /// </summary>
        public string CoverPath { get; set; }

        /// <summary>
        /// 讲ID
        /// </summary>

        public int CourseChaptersID { get; set; }

        /// <summary>
        /// 讲名称
        /// </summary>
        public string CourseChaptersName { get; set; }

        /// <summary>
        /// 章ID
        /// </summary>
        public int CourseChaptersIDParent { get; set; }

        /// <summary>
        /// 章名称
        /// </summary>
        public string CourseChaptersNameParent { get; set; }

        /// <summary>
        /// 学习时长
        /// </summary>
        public int StudyLength { get; set; }
        public string JobTitle { get; set; }

        public int TotalHours { get; set; }
    }

    public class MyCourseInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int TotalHours { get; set; }
        public int LookHours { get; set; }
        public int LearnTimes { get; set; }
        public string CoverPath { get; set; }
        public string Teacher { get; set; }
        public string JobTitle { get; set; }
        public int WareId { get; set; }
        public string WareName { get; set; }
        public int ChapterId { get; set; }
        public string ChapterName { get; set; }
    }


    public class LearnWareNow
    {
        public int ID { get; set; }
        public int ChapterId { get; set; }
        public int WareId { get; set; }
        public string WareName { get; set; }
    }
}
