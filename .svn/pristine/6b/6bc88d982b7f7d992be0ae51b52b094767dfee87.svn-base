using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Course_Comment
{
    public class W_Course_Interlocution
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public int Course_ID { get; set; }


        /// <summary>
        /// 章节ID
        /// </summary>
        public int Chapter_ID { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 客服ID
        /// </summary>
        public int Account_ID { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string Student_ID { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
