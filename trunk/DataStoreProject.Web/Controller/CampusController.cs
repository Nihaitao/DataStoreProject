using CT.Common.Mvc;
using DataStoreProject.Business;
using DataStoreProject.Model.Entity.Campus;
using DataStoreProject.Model.Entity.Discipline;
using DataStoreProject.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DataStoreProject.Web.Controller
{
    public class CampusController : StationBaseApiController
    {

        CampusMapper mapper = new CampusMapper();
        /// <summary>
        /// 根据对应条件查询校区数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetCampusList([FromUri]CampusModel Campusmodel)
        {
            return Success(mapper.GetCampusList(Campusmodel, this.System_Station_ID), Campusmodel.PageIndex == 1 ? mapper.GetCampusListTotalCount(Campusmodel,this.System_Station_ID) : 0);
        }
        /// <summary>
        /// 根据校区id查找对应数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetCampusByID(int ID)
        {
            return Success(mapper.GetCampusByID(ID,this.System_Station_ID));
        }

        /// <summary>
        /// 启用禁用校区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateValid(CampusModel Model)
        {
            return Success(mapper.UpdateValid(Model, this.System_Station_ID));
        }

         /// <summary>
        /// 删除校区
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteCampus(CampusModel Model)
        {
            return Success(mapper.DeleteCampus(Model, this.System_Station_ID));
        }

         /// <summary>
        /// 根据对应条件查询教室数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetClassRoomList([FromUri]ClassRoomModel model)
        {
            return Success(mapper.GetClassRoomList(model, this.System_Station_ID), model.PageIndex == 1 ? mapper.GetClassRoomListTotalCount(model, this.System_Station_ID) : 0);
        }

        /// <summary>
        /// 启用禁用教室
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateClassRoomValid(ClassRoomModel Model)
        {
            return Success(mapper.UpdateClassRoomValid(Model,this.System_Station_ID));
        }

        /// <summary>
        /// 删除教室
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteClassRoom(ClassRoomModel model)
        {
            return Success(mapper.DeleteClassRoom(model, this.System_Station_ID));
        }

        
        /// <summary>
        /// 根据校区教室id查找对应数据  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetClassRoomByID(int ID)
        {
            return Success(mapper.GetClassRoomByID(ID, this.System_Station_ID));
        }

        /// <summary>
        /// 添加修改校区教室
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic SaveClassRoom(W_ClassRoom Model)
        {
            return Success(mapper.SaveClassRoom(Model, this.System_Station_ID,this.AccountID));
        }

        /// <summary>
        /// 添加修改校区
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic SaveCampus(W_Campus Model)
        {
            return Success(mapper.SaveCampus(Model,this.AccountID, this.System_Station_ID));
        }
    }
}