﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class ChapterModel
    {
        public int ID { get; set; }
        public int CID { get; set; }//父节点ID
        public int QuestionStore_ID { get; set; }//题库ID
        public int AlreadyCounts { get; set; }//题目数量
        public int QuestionCounts { get; set; }//题目数量
        public string Name { get; set; }//章节名称
        public int AddPerson { get; set; }//操作人
        public string AddTime { get; set; }//添加时间
        public int Valid { get; set; }//禁用启用  1启用 0禁用
        public int Sort { get; set; }//排序
        public List<ChapterModel> Children { get; set; }
    }


    public class ChapterPostModel
    {
        public int ChapterID { get; set; }
        public int[] questionIds { get; set; }
    }
}
