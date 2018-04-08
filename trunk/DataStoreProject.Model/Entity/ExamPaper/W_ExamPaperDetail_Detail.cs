using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStoreProject.Model.Entity.ExamPaper
{
    public class W_ExamPaperDetail_Detail
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public int ExamPaper_ID { get; set; }

        /// <summary>
        /// 试卷题型ID
        /// </summary>
        public int ExamPaper_Detail_ID { get; set; }

        /// <summary>
        /// 题目ID
        /// </summary>
        public int Question_ID { get; set; }
    }
}
