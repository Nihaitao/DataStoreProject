﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStoreProject.Model.Entity.ExamPaper
{
    public class W_ExamPaper
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 机构ID，0为系统默认
        /// </summary>
        public int System_Station_ID { get; set; }

        /// <summary>
        /// 题库ID
        /// </summary>
        public int QuestionStore_ID { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 试卷类型 0模拟试卷 1历年真题
        /// </summary>
        public int ExamPaperType { get; set; }

        /// <summary>
        /// 组卷方式  0自动  1手动
        /// </summary>
        public int ExamPaperDo { get; set; }

        /// <summary>
        /// 考试时长 单位分
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 合格分数
        /// </summary>
        public decimal PassScore { get; set; }

        /// <summary>
        /// 试卷测试数量 （需实时更新维护）
        /// </summary>
        public int DoCount { get; set; }

        /// <summary>
        /// 题目难度，1-5，5最难
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 省份ID
        /// </summary>
        public int Province_ID { get; set; }

        /// <summary>
        /// 是否公开 1公开 0不公开
        /// </summary>
        public int IsOpen { get; set; }

        /// <summary>
        /// 是否审核 1已审核 0未审核
        /// </summary>
        public int Valid { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 添加账号
        /// </summary>
        public int AddPerson { get; set; }
    }
}
