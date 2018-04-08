using DataStoreProject.Model.Entity.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    //老师模型
    public class TeacherModel : T_TeacherInfo
    {
        /// <summary>
        /// 禁用启用  1启用 0禁用
        /// </summary>
        public int Valid { get; set; }

        /// <summary>
        /// 老师详细ID
        /// </summary>
        public int TeacherDetail_ID { get; set; }

        /// <summary>
        /// 老师学科关系集合  多个学科用“,”隔开
        /// </summary>
        public string Teacher_DisciplineIds { get; set; }

        public int System_Station_ID { get; set; }

        /// <summary>
        /// 老师所教学科集合
        /// </summary>
        public List<T_TeacherInfo_Detail_DisciplineModel> TeacherDisciplines { get; set; }

    }
}
