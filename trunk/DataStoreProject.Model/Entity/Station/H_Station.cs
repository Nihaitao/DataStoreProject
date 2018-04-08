using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Station
{
    public class H_Station
    {
        [AutoIncrement]
        [PrimaryKey]
        public int ID { set; get; }
        public string Name { set; get; }
        public int StationType { set; get; }
        public int School_ID { set; get; }
        public int Province_ID { set; get; }
        public int Valid { set; get; }
        public decimal Balance { set; get; }
        public string AccessKeyId { set; get; }
        public string AccessKeySecret { set; get; }

        public DateTime? VerificationTime { set; get; }

        /// <summary>
        /// 域名
        /// </summary>
        public string DomainName { set; get; }
        public string DomainUser { set; get; }
        public string Dominphone { set; get; }
    }
}
