using Entites.PolyvInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.ViewModel
{
    public class PolyvInfoModel : W_PolyvInfo
    {

        public int PolyvInfo_ID { set; get; }


        /// <summary>
        /// 目录
        /// </summary>
        public string cataid { set; get; }

        /// <summary>
        /// 禁用 启用 0禁用 1启用
        /// </summary>
        public int Valid { set; get; }


        /// <summary>
        /// 【保利威视账号类型，0系统，1自主】
        /// </summary>
        public int PolyvSource { set; get; }

    }
    public class PlayModelData
    {
        public string error { get; set; }

        public List<PlayModel> data { get; set; }
    }

    public class PlayModel
    {
        public string[] hls { get; set; }
        public string duration { set; get; }

        public string source_filesize { set; get; }

        public string PlayAddress { set; get; }
    }
    public class JsonModel
    {

        public int code { get; set; }

        public string status { get; set; }

        public string message { get; set; }

        public DataModel data { get; set; }

    }
    public class DataModel
    {
        public string catatree { get; set; }
        public string cataid { get; set; }

    }

    public class JsonModel2
    {
        public int code { get; set; }

        public string status { get; set; }

        public string message { get; set; }

    }
}
