using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Dictionary
{
    /// <summary>
    /// 学生主表实体 zoujiawei by 2017-10-24
    /// </summary>
    public class S_StudentInfo
    {
        [PrimaryKey]
        public string StuID { get; set; }

        /// <summary>
        /// 如果身份证号码为空以手机号码存储
        /// </summary>
        public string CardNumber { get; set; }

        public string Phone { get; set; }


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

    }
}
