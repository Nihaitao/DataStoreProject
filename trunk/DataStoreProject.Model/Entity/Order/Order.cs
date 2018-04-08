using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Order
{
    public class W_Order
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string StuId { get; set; }
        public string OrderNo { get; set; }
        public int System_Station_ID { get; set; }
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 支付状态 0未支付  1支付成功
        /// </summary>
        public int PayStatus { get; set; }

        /// <summary>
        /// 支付方式 0线下支付  1线上支付
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayPrice { get; set; }

        /// <summary>
        /// 票号
        /// </summary>
        public string TicketNumber { get; set; }

        /// <summary>
        /// 订单状态 0正常 1不正常
        /// </summary>
        public int OrderState { get; set; }

        /// <summary>
        /// 订单支付ID（用于支付）
        /// </summary>
        public string OrderId { get; set; }

    }

    public class W_Order_Detail
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// 订单主表ID
        /// </summary>
        public int CID { get; set; }

        /// <summary>
        /// 网课ID
        /// </summary>
        public int Course_ID { get; set; }

        /// <summary>
        /// 商品价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 0未删除 1已删除
        /// </summary>
        public int IsDelete { get; set; }

    }
}
