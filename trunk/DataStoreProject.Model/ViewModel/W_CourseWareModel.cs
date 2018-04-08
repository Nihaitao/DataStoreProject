using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class W_CourseWareModel
    {
        /// <summary>
        /// 课件表
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { set; get; }


        /// <summary>
        /// 课件名称
        /// </summary>
        public string Name { set; get; }


        /// <summary>
        /// 科次id  最后一个节点id
        /// </summary>
        public int Discipline_ID { set; get; }


        /// <summary>
        /// 学科名称
        /// </summary>
        [AutoIncrement]
        public string Discipline_Name { set; get; }

        /// <summary>
        /// 一级学科ID
        /// </summary>
        [AutoIncrement]
        public int Discipline_OneLevelID { set; get; }

        /// <summary>
        /// 二级学科ID
        /// </summary>
        [AutoIncrement]
        public int Discipline_TwoLevelID { set; get; }

        /// <summary>
        /// 三级学科ID
        /// </summary>
        [AutoIncrement]
        public int Discipline_ThreeLevelID { set; get; }


        /// <summary>
        /// 课件类型  1视频 2PPT
        /// </summary>
        public int CourseWareType_ID { set; get; }



        /// <summary>
        /// 课件类型  1视频 2PPT
        /// </summary>
        [AutoIncrement]
        public string CourseWareType_Name
        {
            get
            {
                string Name = string.Empty;
                switch (CourseWareType_ID)
                {
                    case 1: Name = "视频"; break;
                    case 2: Name = "PPT"; break;
                }
                return Name;

            }
        }


        /// <summary>
        /// 课件大小
        /// </summary>
        public long CourseSize { set; get; }

        /// <summary>
        /// 课件大小 或KB MB GB
        /// </summary>
        [AutoIncrement]
        public string CourseSizeS { set; get; }

        /// <summary>
        /// 播放地址
        /// </summary>
        public string PlayAddress { set; get; }


        /// <summary>
        /// 保利威视值
        /// </summary>
        public string PolyvVID { set; get; }


        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { set; get; }

        /// <summary>
        /// 视频时长 秒
        /// </summary>
        public int Duration { set; get; }
        /// <summary>
        /// 视频时长 格式 00:10:55
        /// </summary>
        [AutoIncrement]
        public string DurationS { set; get; }


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
        /// 操作人名称
        /// </summary>
        [AutoIncrement]
        public string AddPersonName { set; get; }
        /// <summary>
        /// 禁用启用  1启用 0禁用
        /// </summary>
        public int? Valid { set; get; }


        /// <summary>
        /// 播放次数
        /// </summary>
        public int PlayCount { set; get; }


        /// <summary>
        /// 视频状态0.上传中。1上传失败，2.转码中，3.转码失败，4.审核中，5审核通过，6已删除，7正常
        /// </summary>
        public int? Status { set; get; }


        /// <summary>
        /// 当前观看的学生
        /// </summary>
        [AutoIncrement]
        public string StuId { set; get; }


        /// <summary>
        /// 试听时长
        /// </summary>
        [AutoIncrement]
        public int OverFlowTime { set; get; }


        /// <summary>
        /// 是否购买 0未购买 1购买
        /// </summary>
        [AutoIncrement]
        public int IsPay { set; get; }

        /// <summary>
        /// 课程id
        /// </summary>
        [AutoIncrement]
        public int Course_ID { set; get; }

        /// <summary>
        /// 章节ID
        /// </summary>
        public int ChapterID { get; set; }


        /// <summary>
        ///是否公开 1免费 2试听 3收费
        /// </summary>
        [AutoIncrement]
        public int IsOpen { set; get; }


        /// <summary>
        /// 视频状态
        /// </summary>
        [AutoIncrement]
        public string Status_Name
        {
            get
            {
                string Name = string.Empty;
                switch (Status)
                {
                    case 0: Name = "上传中"; break;
                    case 1: Name = "上传失败"; break;
                    case 2: Name = "转码中"; break;
                    case 3: Name = "转码失败"; break;
                    case 4: Name = "审核中"; break;
                    case 5: Name = "审核通过"; break;
                    case 6: Name = "已删除"; break;
                    case 7: Name = "正常"; break;
                }
                return Name;

            }
        }

        public int VideoType { get; set; }

    }
}
