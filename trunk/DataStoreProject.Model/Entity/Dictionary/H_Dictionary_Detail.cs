using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Dictionary
{
    /// <summary>
    /// 字典详细表
    /// </summary>
    public class H_Dictionary_Detail
    {
        /// <summary>
        /// 字典主表ID
        /// </summary>
        [PrimaryKey]
        public int Dictionary_ID { set; get; }

        /// <summary>
        /// 机构ID
        /// </summary>
        [PrimaryKey]
        public int System_Station_ID { set; get; }

        /// <summary>
        /// 可自定义显示名称
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 状态  1启用 0禁用
        /// </summary>
        public int Valid { set; get; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDelete { set; get; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortID { set; get; }

        /// <summary>
        /// 是否为系统字段
        /// </summary>
        [AutoIncrement]
        public int IsSystemField { set; get; }
    }
}
