using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Question
{
    public class W_QuestionNote
    {
        /// <summary>
        /// 笔记表
        /// </summary>
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 题目ID
        /// </summary>
        public int Question_ID { get; set; }

        /// <summary>
        ///学生主表ID
        /// </summary>
        public string StuID { get; set; }

        /// <summary>
        /// 笔记
        /// </summary>
        public string Remart { get; set; }
        
        /// <summary>
        /// 状态 0 可用 1 已删除
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { get; set; }
    }
}
