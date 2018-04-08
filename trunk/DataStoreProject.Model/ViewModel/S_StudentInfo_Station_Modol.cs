using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    /// <summary>
    /// 学生基本信息业务实体
    /// </summary>
    public class S_StudentInfo_Station_Modol
    {
        [PrimaryKey]
        public string StuID { get; set; }

        /// <summary>
        /// 如果身份证号码为空以手机号码存储
        /// </summary>
        public string CardNumber { get; set; }


        public string Phone { get; set; }

        public string Code { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 身份证地址
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 签发机关
        /// </summary>
        public string Police { get; set; }

        /// <summary>
        /// 有效期开始时间
        /// </summary>
        public string EffDate { get; set; }

        /// <summary>
        /// 有效期结束时间
        /// </summary>
        public string ExpDate { get; set; }

        /// <summary>
        /// 身份证头像
        /// </summary>
        public string HeadImg { get; set; }


        public int Education_ID { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Phone2 { get; set; }
        public int NowProvince_ID { get; set; }
        public int NowCity_ID { get; set; }
        public int NowArea_ID { get; set; }
        public string NowAddress { get; set; }

        public string Remark { get; set; }
        /// <summary>
        /// 注册类型  1注册名身份证注册  2手机号注册
        /// </summary>
        public int RegisterType { get; set; }
    }
}
