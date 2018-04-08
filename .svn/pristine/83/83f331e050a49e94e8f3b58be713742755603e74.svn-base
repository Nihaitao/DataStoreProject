using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class W_ConfigurationModel
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [PrimaryKey]
        public int System_Station_ID { set; get; }

        /// <summary>
        /// 课程有效期 机构级别开关 0关闭 1启用
        /// </summary>
        public int ValidPeriod { set; get; }

        /// <summary>
        /// 课程有效天数
        /// </summary>
        public int ComDay { set; get; }

        /// <summary>
        /// 单视频观看次数 0不限制
        /// </summary>
        public int ComVideo { set; get; }

        /// <summary>
        /// 单直播回看次数 0不限制
        /// </summary>
        public int ComLive { set; get; }

        /// <summary>
        /// 课程评论开关 0关闭 1启用
        /// </summary>
        public int CommentValid { set; get; }

        /// <summary>
        /// 课程问答开关 0关闭 1启用
        /// </summary>
        public int QuestionValid { set; get; }

        /// <summary>
        /// 问答查看权限 1购买本课程 2所有付费用户 3所有登录用户 4所有用户
        /// </summary>
        public int QuestionLookType { set; get; }

        /// <summary>
        /// 问答回复权限 1购买本课程 2所有付费用户 3所有登录用户
        /// </summary>
        public int QuestionReplyType { set; get; }

        /// <summary>
        /// 试听时长
        /// </summary>
        public decimal OverFlowTime { set; get; }

        /// <summary>
        /// 试听到时提示
        /// </summary>
        public string OverFlowInfo { set; get; }

        /// <summary>
        /// 试听视频权限 0所有用户  1注册用户
        /// </summary>
        public int OverFlowAuth { set; get; }

        /// <summary>
        /// 免费视频/资源观看权限： 0所有用户   1注册用户
        /// </summary>
        public int UserSeeAuth { set; get; }

        /// <summary>
        /// 视频观看时长添加观看记录 单位分钟 默认6分钟
        /// </summary>
        public int LookDuration { set; get; }

        /// <summary>
        /// 保利威视账号类型，0系统，1自主
        /// </summary>
        public int PolyvinfoType { set; get; }

        public int Type { set; get; }
    }
}
