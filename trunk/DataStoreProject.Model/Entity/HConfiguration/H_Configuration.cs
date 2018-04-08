using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.HConfiguration
{
    public partial class H_Configuration
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        [PrimaryKey]
        public int System_Station_ID { set; get; }


        /// <summary>
        /// 注册开关 0关闭 1开启
        /// </summary>
        public int RegisterValid { set; get; }

        /// <summary>
        /// 注册方式 0手机号码注册，1用户名注册
        /// </summary>
        public int RegisterType { set; get; }

        /// <summary>
        /// 注册协议开关  0关闭  1启用
        /// </summary>
        public int RegisterProtocolValid { set; get; }

        /// <summary>
        /// 协议内容
        /// </summary>
        public string RegisterProtocolContent { set; get; }

        /// <summary>
        /// 同时登录 0关闭 1开启
        /// </summary>
        public int MultiOnline { set; get; }

        /// <summary>
        /// 第三方登录 0关闭 1开启
        /// </summary>
        public int ThirdLoginValid { set; get; }

        /// <summary>
        /// 微信登录功能 0关闭 1启用
        /// </summary>
        public int WxValid { set; get; }

        /// <summary>
        /// 微信开放平台AppID
        /// </summary>
        public string WxAppID { set; get; }
        /// <summary>
        /// 微信开放平台AppSecret
        /// </summary>
        public string WxAppSecret { set; get; }

        /// <summary>
        /// QQ登录功能 0关闭 1启用
        /// </summary>
        public int QQValid { set; get; }

        /// <summary>
        /// QQ开放平台AppID
        /// </summary>
        public string QQAppID { set; get; }

        /// <summary>
        /// 开放平台AppSecret
        /// </summary>
        public string QQAppKey { set; get; }

        /// <summary>
        /// 独立登录、注册页面 0关闭 1启用
        /// </summary>
        public int LoginPageConfig { set; get; }

    }
}
