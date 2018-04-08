﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Chapters
{
    /// <summary>
    /// 章节题目表
    /// </summary>
    public class W_Chapter_Question
    {
        /// <summary>
        /// 章节ID
        /// </summary>
        [PrimaryKey]
        public int Chapter_ID { get; set; }

        /// <summary>
        /// 题目ID
        /// </summary>
        [PrimaryKey]
        public int Question_ID { get; set; }
    }
}
