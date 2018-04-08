using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.PolyvInfo
{
    public class W_PolyvInfo_Detail
    {
        /// <summary>
        /// 机构id
        /// </summary>
        [PrimaryKey]
        public int System_Station_ID { get; set; }

        /// <summary>
        /// 保利威视配置主表id
        /// </summary>
        [PrimaryKey]
        public int PolyvInfo_ID { get; set; }

        /// <summary>
        /// cataid
        /// </summary>
        public string cataid { get; set; }

        /// <summary>
        /// 启用 禁用 1启用 0禁用
        /// </summary>
        public int Valid { get; set; }
    }
}
