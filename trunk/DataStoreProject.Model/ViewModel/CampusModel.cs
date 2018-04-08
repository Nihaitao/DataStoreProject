﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    /// <summary>
    /// 可以用来做修改状态接收参数使用(通用实体)
    /// </summary>
    public class CampusModel:PageModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Valid { get; set; }
        public int IsPutaway { get; set; }
        public int Course_ID { get; set; }
        public int Discipline_ID { get; set; }
        public string StuId { get; set; }
        public string Ids { get; set; }
        public int orderDetailId { get; set; }
        public int IsRecommend { get; set; }
        public int PayStatus { get; set; }

        public int TeachingMethod { get; set; }
        public int topNum { get; set; }

        public int Comment_ID { get; set; }

        public string Title { get; set; }
    }
}
