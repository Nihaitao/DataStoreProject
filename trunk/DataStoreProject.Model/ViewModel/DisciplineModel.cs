using DataStoreProject.Model.Entity.Chapters;
using DataStoreProject.Model.Entity.Discipline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class DisciplineModel : W_Discipline
    {
        public int StartRow { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<DisciplineModel> ChildNodeList { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<DisciplineModel> ChildNodeList2 { get; set; }

        /// <summary>
        /// 课程
        /// </summary>
        public List<W_CourseModel> CourseList { get; set; }

    }



    public class DisciplineIDModel
    {
        /// <summary>
        ///主键id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public int CID { get; set; }

        /// <summary>
        /// 一级学科ID
        /// </summary>
        public int Discipline_OneLevelID { set; get; }

        /// <summary>
        /// 二级学科ID
        /// </summary>
        public int Discipline_TwoLevelID { set; get; }

        /// <summary>
        /// 三级学科ID
        /// </summary>
        public int Discipline_ThreeLevelID { set; get; }


    }
}
