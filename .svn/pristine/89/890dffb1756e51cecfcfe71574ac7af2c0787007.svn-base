using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataStoreProject.Model.Entity.ExamPaper
{
    public class W_DoExamResult
    {

        /// <summary>
        /// 主键ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StuId { get; set; }

        /// <summary>
        /// 记录类型，0在线考试  1章节练习
        /// </summary>
        public int BusType { get; set; }

        /// <summary>
        /// 业务外键ID，BusType=0存储试卷ID，BusType=1存储章节练习ID
        /// </summary>
        public int BusID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 交卷状态 0未交卷 1已交卷
        /// </summary>
        public int Valid { get; set; }

        /// <summary>
        /// 是否已评阅  1评阅
        /// </summary>
        public int IsReview { get; set; }
    }
}
