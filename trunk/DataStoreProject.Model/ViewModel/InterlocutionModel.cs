using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class InterlocutionModel
    {
        public int ID { set; get; }
        public int PID { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>
        public int ChapterId { get; set; }

        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 客服ID
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// 客服名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { set; get; }

        /// <summary>
        /// 最近回复
        /// </summary>
        public NewInterlocution NewInterlocution { set; get; }
    }

    public class NewInterlocution
    {
        public int PID { set; get; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentId { set; get; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { set; get; }

        /// <summary>
        /// 客服ID
        /// </summary>
        public int AccountID { get; set; }

        /// <summary>
        /// 客服名称
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { set; get; }

    }
}
