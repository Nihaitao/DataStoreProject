using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace DataStoreProject.Model.Entity.Question
{
    public class W_QuestionStore
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        /// <summary>
        /// 机构ID，0为系统默认
        /// </summary>
        public int System_Station_ID { get; set; }

        /// <summary>
        /// 科次ID
        /// </summary>
        public int Discipline_ID { get; set; }  

        /// <summary>
        /// 题库名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态 1启用  0禁用
        /// </summary>
        public int Valid { get; set; }

        /// <summary>
        /// 是否删除 1删除
        /// </summary>
        public int IsDelete { get; set; }

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
