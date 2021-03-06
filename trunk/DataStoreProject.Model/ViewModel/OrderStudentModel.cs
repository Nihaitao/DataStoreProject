﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class OrderStudentModel
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public int ID { get; set; }
        public string OrderNo { get; set; }
        public decimal Price { get; set; }
        public decimal PayPrice { get; set; }
        public int OrderState { get; set; }
        public int PayStatus { get; set; }
        public DateTime PayTime { get; set; }
        public int PayType { get; set; }

        /// <summary>
        /// 订单明细列表
        /// </summary>
        public List<OrderDetailList> OrderDetailList { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int PayCount { get; set; }
        public string OrderId { get; set; } //支付订单ID（用于支付）
        public int System_Station_ID { get; set; }
        public int Valid { get; set; }
    }
    /// <summary>
    /// 订单明细数据
    /// </summary>
    public class OrderDetailList
    {
        public int ID { get; set; }

        /// <summary>
        /// 网课价格
        /// </summary>
        public decimal Price { get; set; }
        // 优惠价格
        public decimal PreferentialPrice { get; set; }
        public string OrderNo { get; set; }

        /// <summary>
        /// 网课名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 总课时
        /// </summary>
        public int TotalHours { get; set; }

        /// <summary>
        /// 授课方式
        /// </summary>
        public int TeachingMethod { get; set; }

        public string CoverPath { get; set; }
        public int Valid { get; set; }
        public int Course_ID { set; get; }
        public int IsDelete { set; get; }

        //教学老师
        public string teachName { set; get; }
        //教学老师职称
        public string JobTitle { set; get; }
    }
}
