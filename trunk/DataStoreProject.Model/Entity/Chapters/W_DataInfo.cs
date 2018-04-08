using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Model.Entity.Chapters
{
    public class W_DataInfo
    {

        /// <summary>
        /// 资料表
        /// </summary>
       [PrimaryKey, AutoIncrement]
        public int ID { set; get; }


        /// <summary>
        /// 资料名称
        /// </summary>
        public string Title { set; get; }

        public int BusID { get; set; }
        public int BusType { get; set;   }
        /// <summary>
        /// 资料类型 1教材  2教辅 3其他
        /// </summary>
        public int DataInfoType { set; get; }

        


        /// <summary>
        /// 资料路径
        /// </summary>
        public string Path { set; get; }
        /// <summary>
        /// 资料内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 资料大小
        /// </summary>
        public int DataSize { set; get; }
        /// <summary>
        /// 是否公开 1公开 0不公开
        /// </summary>
        public int? IsOpen { set; get; }

        /// <summary>
        /// 系统id
        /// </summary>
        public int System_Station_ID { set; get; }



        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { set; get; }



        /// <summary>
        /// 操作人
        /// </summary>
        public int AddPerson { set; get; }

        /// <summary>
        /// 禁用启用  1启用 0禁用
        /// </summary>
        public int Valid { set; get; }

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownCount { set; get; }

    }
}
