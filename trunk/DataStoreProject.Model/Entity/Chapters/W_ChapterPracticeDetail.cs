using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Chapters
{
    public class W_ChapterPractice_Detail
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        /// <summary>
        /// 章节练习ID
        /// </summary>
        public int ChapterPractice_ID { get; set; }
        
        /// <summary>
        /// 题目ID
        /// </summary>
        public int Question_ID { get; set; }
    }
}
