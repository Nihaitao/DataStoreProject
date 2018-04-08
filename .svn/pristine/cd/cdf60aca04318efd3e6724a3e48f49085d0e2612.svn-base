using DataStoreProject.Model.Entity.Chapters;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class WebCourseModel
    {
    }


    public class Course_ChaptersModel : W_Course_Chapters
    {

        /// <summary>
        /// 是否观看  1已观看 0未观看
        /// </summary>
        public int IsLook { get; set; }
        public string CourseWare_Name { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<Course_ChaptersModel> ChildNodeList { get; set; }

    }



    public class Course_UnitModel : W_Course_Unit
    {

        /// <summary>
        /// 单元课次
        /// </summary>
        public List<W_Course_Unit_ClassTime> UnitClassTimeList { get; set; }

    }



    public class DataInfoModel : PageModel
    {
        /// <summary>
        /// 资料表
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { set; get; }


        /// <summary>
        /// 资料名称
        /// </summary>
        public string Title { set; get; }

        public int BusID { get; set; }
        /// <summary>
        /// 资料类型 1教材  2教辅 3其他
        /// </summary>
        public int DataInfoType { set; get; }

        /// <summary>
        ///资料类型 1教材  2教辅 3其他
        /// </summary>
        [AutoIncrement]
        public string DataInfoType_Name
        {
            get
            {
                string Name = string.Empty;
                switch (DataInfoType)
                {
                    case 1: Name = "教材"; break;
                    case 2: Name = "教辅"; break;
                    case 3: Name = "其他"; break;
                }
                return Name;

            }
        }


        /// <summary>
        /// 资料路径
        /// </summary>
        public string Path { set; get; }
        /// <summary>
        /// 资料内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 资料大小
        /// </summary>
        public int DataSize { set; get; }
        /// <summary>
        /// 是否公开 1公开 0不公开
        /// </summary>
        public int? IsOpen { set; get; }

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
        public int Valid { set; get; }


        /// <summary>
        /// 网课主表ID
        /// </summary>
        public int Course_ID { set; get; }
        /// <summary>
        /// 面授直播单元ID
        /// </summary>
        public int CourseUnit_ID { set; get; }


        /// <summary>
        /// 面授直播课次ID
        /// </summary>
        public int CourseUnitClassTime_ID { set; get; }



        /// <summary>
        /// 录播课件ID
        /// </summary>
        public int CourseChapters_ID { set; get; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownCount { set; get; }



    }



    //public class CourseWareModel : W_CourseWare
    //{
    //    public int StartRow { get; set; }

    //    public int PageSize { get; set; }


    //}


    public class CourseModel : W_Course
    {
        public int StartRow { get; set; }

        public int PageSize { get; set; }

        public int IsShiT { get; set; }

    }



    public class CourseLookTimeModel
    {
        /// <summary>
        /// 播放总秒数 秒
        /// </summary>
        public int TotalSeconds { get; set; }

        /// <summary>
        /// 视频时长 秒
        /// </summary>
        public int Duration { get; set; }


    }

}
