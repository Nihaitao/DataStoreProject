using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Campus
{
    public class W_ClassRoom
    {
        /// <summary>
        /// 教室表
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int ID { set; get; }


        /// <summary>
        /// 校区ID
        public int CampusID { set; get; }


        /// <summary>
        /// 教室名称
        /// </summary>
        public string Name { set; get; }


        /// <summary>
        /// 教室类型  1普通教室  2机房
        /// </summary>
        public int ClassRoomType_ID { set; get; }


        /// <summary>
        /// 容量  1. 1-49人  2.50-99人 3.100-199人 4.200-299人  5.300人以上
        /// </summary>
        public int Capacity_ID { set; get; }


        /// <summary>
        /// 地址
        /// </summary>
        public string Address { set; get; }



        /// <summary>
        /// 公交线路
        /// </summary>
        public string BusRoute { set; get; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }



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
        /// 禁用启用  1启用 0禁用
        /// </summary>
        public int Valid { set; get; }

    }
}
