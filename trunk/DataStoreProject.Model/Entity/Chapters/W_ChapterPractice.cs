﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Chapters
{
    public class W_ChapterPractice
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        /// <summary>
        /// 题库ID
        /// </summary>
        public int QuestionStore_ID { get; set; }

        /// <summary>
        /// 章节练习名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
        
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StuId { get; set; }

        /// <summary>
        /// 章节练习来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 完成状态 0未完成 1已完成
        /// </summary>
        public int Status { get; set; }
    }
}
