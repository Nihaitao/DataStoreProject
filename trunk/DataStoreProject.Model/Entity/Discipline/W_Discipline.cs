using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Discipline
{
    /// 学科表
    /// </summary>
    public class W_Discipline
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { set; get; }

        /// <summary>
        /// 父级ID  0为第一级
        /// </summary>
        public int CID { set; get; }
    
        /// <summary>
        /// 学科名
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 展示图
        /// </summary>
        public string Picture { set; get; }

        /// <summary>
        /// 禁用启用  1启用 0禁用
        /// </summary>
        public int Valid { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Orders { set; get; }

        /// <summary>
        /// 系统id
        /// </summary>
        public int System_Station_ID { set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { set; get; }


        /// <summary>
        /// 操作人
        /// </summary>
        public int AddPerson { set; get; }

        /// <summary>
        /// 是否置顶 0 不置顶 1 置顶
        /// </summary>
        public int IsTop { set; get; }

        /// <summary>
        /// 是否推荐 0 不推荐 1 推荐
        /// </summary>
        public int IsRecommend { set; get; }
    }
}

