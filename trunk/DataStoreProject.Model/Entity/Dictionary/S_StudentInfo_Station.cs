using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Dictionary
{
    /// <summary>
    /// 学生站点关联不表实体 
    /// </summary>
    public class S_StudentInfo_Station
    {

        [PrimaryKey]
        public string StuID { get; set; }
        [PrimaryKey]
        public int System_Station_ID { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public int Education_ID { get; set; }

        /// <summary>
        /// 手机号码 加密存储，同机构唯一
        /// </summary>
        public string Phone { get; set; }

        public string Email { get; set; }


        public string QQ { get; set; }

        public string Phone2 { get; set; }

        public int NowProvince_ID { get; set; }
        public int NowCity_ID { get; set; }
        public int NowArea_ID { get; set; }

        /// <summary>
        /// 现住地址
        /// </summary>
        public string NowAddress { get; set; }



        /// <summary>
        /// 学生备注说明
        /// </summary>
        public string Remark { get; set; }
    }
}
