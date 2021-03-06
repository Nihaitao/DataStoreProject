﻿using CT.Common.Mvc;
using DataStoreProject.Business;
using DataStoreProject.Model.Entity;
using DataStoreProject.Model.Entity.Course_Comment;
using DataStoreProject.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DataStoreProject.Web.Controller
{
    public class CourseInterlocutionController : StationBaseApiController
    {
        CourseInterlocutionMapper mapper = new CourseInterlocutionMapper();

        /// <summary>
        /// 查询所有问答列表 by nht 2017-08-16
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetInterlocutionListByCuId([FromUri]CampusModel model)
        {
            return Success(mapper.GetInterlocutionListByCuId(model, this.System_Station_ID), model.PageIndex == 1 ? mapper.GetInterlocutionListByCuIdCount(model, this.System_Station_ID) : 0);
        }

        /// <summary>
        /// 查询我的问答列表 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetMyCourseInterlocution([FromUri]PageModel model)
        {
            return Success(mapper.GetMyCourseInterlocution(model, this.SafeGetStuId, this.System_Station_ID), model.PageIndex == 1 ? mapper.GetMyCourseInterlocutionCount(model, this.SafeGetStuId, this.System_Station_ID) : 0);
        }

        /// <summary>
        /// 删除问答 by nht 2017-08-16
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteCourseInterlocution(CampusModel model)
        {
            return Success(mapper.DeleteCourseInterlocution(model));
        }

        /// <summary>
        /// 查询问答列表 by nht 2017-08-16
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetCourseInterlocutionList(int PID)
        {
            return Success(mapper.GetCourseInterlocutionList(PID, this.System_Station_ID));
        }

        /// <summary>
        /// 查询评论列表 by nht 2017-08-16
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetCourseCommentList([FromUri]CampusModel model)
        {
            return Success(mapper.GetCourseCommentList(model, this.System_Station_ID, this.SafeGetStuId), model.PageIndex == 1 ? mapper.GetCourseCommentListCount(model, this.System_Station_ID, this.SafeGetStuId) : 0);
        }

        /// <summary>
        /// 查询我的评论列表 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetMyCourseComment([FromUri]PageModel model)
        {
            return Success(mapper.GetMyCourseComment(model, this.SafeGetStuId), model.PageIndex == 1 ? mapper.GetMyCourseCommentCount(model, this.SafeGetStuId) : 0);
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteCourseComment(CampusModel model)
        {
            return Success(mapper.DeleteCourseComment(model));
        }

        /// <summary>
        /// 启用禁用评论
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdataCommentValid(CampusModel model)
        {
            return Success(mapper.UpdataCommentValid(model,this.System_Station_ID));
        }

        /// <summary>
        /// 添加评论 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddCourseComment(W_Course_Comment model)
        {
            return Success(mapper.AddCourseComment(model, this.SafeGetStuId));
        }

        /// <summary>
        ///  //点赞&取消点赞
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateCommentThumbsup(CampusModel model)
        {
            return Success(mapper.UpdateCommentThumbsup(model,this.SafeGetStuId));
        }

        /// <summary>
        /// 添加问答
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddCourseInterlocution(W_Course_Interlocution model)
        {
            return Success(mapper.AddCourseInterlocution(model,this.SafeGetStuId));
        }

        /// <summary>
        /// 添加回复
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddContent(W_Course_Interlocution model)
        {
            return Success(mapper.AddContent(model, this.AccountID));
        }
    }
}