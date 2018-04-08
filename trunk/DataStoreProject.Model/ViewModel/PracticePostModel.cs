using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class PracticePostModel
    {
        /// <summary>
        /// 题库ID
        /// </summary>
        public int storeId { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>
        public int chapterId { get; set; }

        /// <summary>
        /// 题目类型（以逗号隔开）
        /// </summary>
        public string questionType { get; set; }

        /// <summary>
        /// 题目数量
        /// </summary>
        public int questionCount { get; set; }

        /// <summary>
        /// 题目来源（0所有，1已做，2未做，3做错）
        /// </summary>
        public int questionSource { get; set; }

        public List<Question> questionList { get; set; }
    }

    public class ExamPaperPostModel
    {
        public int[] Ids { get; set; }
        public int examPaperId { get; set; }
        public int examPaperDetailId { get; set; }
        public int[] questionIds { get; set; }
    }
}
