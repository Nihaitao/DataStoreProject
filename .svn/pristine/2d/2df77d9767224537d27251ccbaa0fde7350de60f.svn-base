using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Dictionary
{
    /// <summary>
    /// 系统字典表
    /// </summary>
    public class H_Dictionary
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }

        /// <summary>
        /// 机构ID，0代表系统默认
        /// </summary>
        public int System_Station_ID { get; set; }

        /// <summary>
        /// 标识的列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 对应的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 1 系统默认字段，不允许修改
        /// </summary>
        public int Editable { get; set; }

        /// <summary>
        /// 状态  0启用 1禁用
        /// </summary>
        public int Valid { get; set; }

        /// <summary>
        /// 是否删除(0-否 1-是)
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortID { get; set; }
    }
}
