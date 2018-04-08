using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Teacher
{
    /// <summary>
    /// 老师子表
    /// </summary>
    public class T_TeacherInfo_Detail
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { set; get; }

        /// <summary>
        /// 老师主表id
        /// </summary>
        public int Teacher_ID { set; get; }

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
