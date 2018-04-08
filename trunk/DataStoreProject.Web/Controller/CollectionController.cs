﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CommonFramework;
using CommonFramework.Exceptions;
using CommonFramework.IBatisNet.SqlMap;
using CommonFramework.Mvc;
using CommonFramework.Mvc.Attribute;
using CT.Common.Mvc;
using DataStoreProject.Business;
using DataStoreProject.Model.Entity.DataInfo;
using DataStoreProject.Model.ViewModel;
using DataStoreProject.Model.Entity.Question;
namespace DataStoreProject.Web.Controller
{
    public class CollectionController : StationBaseApiController
    {
        CollectionMapper mapper = new CollectionMapper();
        /// <summary>
        /// 根据业务类型,学生主表ID获取对应收藏 （业务类型，0 试题 1 试卷） by SZF
        /// </summary>
        /// <param name="BusType">业务类型</param>
        /// <param name="StuID">学生ID</param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetCollectionList([FromUri]W_CollectionModel model)
        {
            return Success(mapper.GetCollectionList(model, this.SafeGetStuId, this.System_Station_ID), model.PageIndex == 1 ? mapper.GetCollectionListCount(model, this.SafeGetStuId, this.System_Station_ID) : 0);
        }


        /// <summary>
        /// 根据业务类型,学生主表ID获取对应笔记 
        /// </summary>
        /// <param name="BusType">业务类型</param>
        /// <param name="StuID">学生ID</param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetNoteList([FromUri]W_CollectionModel model)
        {
            return Success(mapper.GetNoteList(model, this.SafeGetStuId, this.System_Station_ID), model.PageIndex == 1 ? mapper.GetNoteListCount(model, this.SafeGetStuId, this.System_Station_ID) : 0);
        }

        /// <summary>
        /// 根据题目ID获取笔记列表by szf
        /// </summary>
        /// <author>SZF</author>
        /// <date>2017-11-8</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionNoteList(int Question_ID)
        {
            return Success(mapper.GetQuestionNoteList(this.SafeGetStuId, Question_ID));
        }

        /// <summary>
        /// 添加笔记 by szf
        /// <author>SZF</author>
        /// <date>2017-11-8</date>
        /// <param name="model">W_QuestionNote</param>
        /// <returns></returns>
        [HttpPost]

        public dynamic AddQuestionNote(W_QuestionNote model)
        {
            model.AddTime = DateTime.Now.ToString();
            model.StuID = this.SafeGetStuId;
            return Success(mapper.AddQuestionNote(model) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 添加收藏/取消收藏
        /// </summary>
        /// <param name="model">W_Collection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddCollection(W_Collection model)
        {
            if (model != null)
            {
                model.AddTime = DateTime.Now.ToString();
                model.StuID = this.SafeGetStuId;
                var dict = mapper.AddCollection(model);
                if (dict)
                    return Success("操作成功");
                else
                    return Success("操作失败");
            }
            else
            {
                return Success("操作失败");
            }
        }

        /// <summary>
        /// 删除题目下面所有的笔记
        /// </summary>
        /// <param name="model">W_Collection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteAllNote(W_QuestionNote model)
        {
            return Success(mapper.DeleteAllNote(model,this.SafeGetStuId));
        }

        /// <summary>
        /// 根据ID删除单条笔记
        /// </summary>
        /// <param name="model">W_Collection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteNoteByID(W_QuestionNote model)
        {
            return Success(mapper.DeleteNoteByID(model, this.SafeGetStuId));
        }

        /// <summary>
        /// 修改笔记
        /// </summary>
        /// <param name="model">W_Collection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateNoteByID(W_QuestionNote model)
        {
            return Success(mapper.UpdateNoteByID(model, this.SafeGetStuId));
        }
    }
}