using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class OrderPay
    {
        public int ID { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Price { get; set; }
        public string OrderId { get; set; } 

        /// <summary>
        /// 订单明细
        /// </summary>
        public List<OrderPayDetailList> OrderPayDetailList { get; set; }
    }

    public class OrderPayDetailList
    {
        // 网课名称
        public string Name { get; set; }
        // 网课价格
        public decimal Price { get; set; }
        // 优惠价格
        public decimal PreferentialPrice { get; set; }
        //课件封面
        public string CoverPath { get; set; }
        // 总课时
        public int TotalHours { get; set; }
        //教学老师
        public string teachName { set; get; }
        //教学老师职称
        public string JobTitle { set; get; }
    }
}
