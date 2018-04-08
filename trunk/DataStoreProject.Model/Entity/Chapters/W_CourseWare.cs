using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Chapters
{
    public class W_CourseWare
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Discipline_ID { get; set; }
        public int CourseWareType_ID { get; set; }
        public long CourseSize { get; set; }
        public string PlayAddress { get; set; }
        public string PolyvVID { get; set; }
        public string Tag { get; set; }
        public int Duration { get; set; }
        public int System_Station_ID { get; set; }
        public DateTime AddTime { get; set; }
        public int AddPerson { get; set; }
        public int Valid { get; set; }
        public int PlayCount { get; set; }
        public int Status { get; set; }

    }
}
