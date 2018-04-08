using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class W_Course_Look_DetailModel
    {

        /// <summary>
        /// 视频播放记录表
        /// </summary>
        [AutoIncrement]
        [PrimaryKey]
        public int ID { get; set; }


        /// <summary>
        /// 学生
        /// </summary>
        public string StuId { get; set; }



        /// <summary>
        /// 章节id
        /// </summary>
        public int CourseChapters_ID { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }


        /// <summary>
        /// 观看结束时间  
        /// </summary>
        public DateTime EndTime { get; set; }



        /// <summary>
        /// IP
        /// </summary>
        public string IP { get; set; }

        public decimal LookTime { get; set; }

        /// <summary>
        /// 观看类型  0 PC端 ，1微信端
        /// </summary>
        public int LookType { get; set; }

    }
}
