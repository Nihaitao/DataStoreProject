using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class SortDisciplineModel
    {
        public int currDisciplineId { get; set; }
        public int old_prev_order { get; set; }
        public int old_next_order { get; set; }
        public int new_prev_order { get; set; }
        public int new_next_order { get; set; }
        public int currParentId { get; set; }
    }
}
