using DataStoreProject.Model.Entity.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class T_TeacherInfo_Detail_DisciplineModel : T_TeacherInfo_Detail_Discipline
    {

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
