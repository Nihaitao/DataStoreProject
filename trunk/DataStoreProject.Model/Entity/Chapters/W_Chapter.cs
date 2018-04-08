﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Chapters
{
    /// <summary>
    /// 章节表
    /// </summary>
    public class W_Chapter
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }
        public int CID { get; set; }//父节点ID
        public int QuestionStore_ID { get; set; }//题库ID
        public string Name { get; set; }//章节名称
        public int AddPerson { get; set; }//操作人
        public string AddTime { get; set; }//添加时间
        public int Valid { get; set; }//禁用启用  1启用 0禁用
        public int Sort { get; set; }//排序
    }
}
