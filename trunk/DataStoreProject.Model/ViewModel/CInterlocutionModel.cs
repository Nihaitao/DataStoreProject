using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class CInterlocutionModel
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
        /// 最新评论内容
        /// </summary>
        public string CommentContent { set; get; }

        /// <summary>
        /// 最新评论时间
        /// </summary>
        public string AddCommentTime { set; get; }

        /// <summary>
        /// 最近回复
        /// </summary>
        public CNewInterlocution NewInterlocution { set; get; }
    }

    public class CNewInterlocution
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
        /// 最新评论内容
        /// </summary>
        public string ReplyContent { set; get; }

        /// <summary>
        /// 最新回复时间
        /// </summary>
        public string AddReplyTime { set; get; }

    }
}
