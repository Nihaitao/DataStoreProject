using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class W_CollectionModel : PageModel
    {
        public int BusType { get; set; }//业务类型，0 试题 1 试卷
        public int BusID { get; set; }//业务ID
        public string StuID { get; set; }//学生主表ID
        public string AddTime { get; set; }//收藏时间

        public int QuestionStore_ID { get; set; }
        public int QuestionType_ID { get; set; }
    }
}
