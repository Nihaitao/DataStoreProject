using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace Entites.Resource.Question
{
    public class W_QuestionType
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 题目类型名称
        /// </summary>
        public string Name { get; set; } 
    }
}
