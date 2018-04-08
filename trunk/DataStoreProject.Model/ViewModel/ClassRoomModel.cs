﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class ClassRoomModel:PageModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Valid { get; set; }
        public int ClassRoomType_ID { get; set; }
        public string CampusName { get; set; }
        public string AddPersonName { get; set; }

        public string Address { get; set; }
        public int Capacity_ID { get; set; }
        public int CampusID { get; set; }
    }
}
