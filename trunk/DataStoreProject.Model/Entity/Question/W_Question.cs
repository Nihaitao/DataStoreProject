using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Question
{
    public class W_Question
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 机构ID，0为系统默认
        /// </summary>
        public int System_Station_ID { get; set; }

        /// <summary>
        /// 题库ID
        /// </summary>
        public int QuestionStore_ID { get; set; }

        /// <summary>
        /// 题目类型ID
        /// </summary>
        public int QuestionType_ID { get; set; }

        /// <summary>
        /// 题目难度，1-5，5最难
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容选项，单选多选以Josn存储
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 选项个数，单选多选>0
        /// </summary>
        public int AnswerCount { get; set; }

        /// <summary>
        /// 正确答案，选项；多选答案用,隔开
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// 组合题资料ID，0代表普通题目
        /// </summary>
        public int QuestionData_ID { get; set; }


        /// <summary>
        /// 排序，用于组合题，升序，确保组合题顺序正确
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 解析
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 做题次数 （需实时更新维护）
        /// </summary>
        public int DoCount { get; set; }

        /// <summary>
        /// 正确数（需实时更新维护）
        /// </summary>
        public int CorrectCount { get; set; }

        /// <summary>
        /// 易错项
        /// </summary>
        public string EasyWrongAnswer { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 添加账号
        /// </summary>
        public int AddPerson { get; set; }
    }
}
