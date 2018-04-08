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
using DataStoreProject.Model.Entity.Chapters;
using DataStoreProject.Model.ViewModel;
namespace DataStoreProject.Web.Controller
{
    public class ChapterController : StationBaseApiController
    {
        ChapterMapper mapper = new ChapterMapper();

        /// <summary>
        /// 获取全部章节列表根据题库ID by SZF
        /// </summary>
        /// <param name="ID">题库ID</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public dynamic GetChapterList(int QuestionStore_ID)
        {

            var model = mapper.GetChapterList(QuestionStore_ID);
            return Success(model);
        }
        /// <summary>
        /// 添加章节列表 by SZF
        /// </summary>
        /// <param name="model">W_Chapter</param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public dynamic AddChapter(W_Chapter model)
        {
            if (model != null)
            {
                model.AddPerson = this.AccountID;
                model.AddTime = DateTime.Now.ToString();
                ChapterMapper mapper = new ChapterMapper();
                var dict = mapper.AddChapter(model);
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
        /// 修改章节列表 by SZF
        /// </summary>
        /// <param name="model">W_Chapter</param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public dynamic UpdateChapter(W_Chapter model)
        {
            if (model != null)
            {
                if (mapper.UpdateChapter(model))
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
        /// 根据ID删除章节列表
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteChapter(W_Chapter model)
        {

            return Success(mapper.DeleteChapter(model));

        }
        /// <summary>
        /// 根据ID和Valid启用状态修改状态
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="Valid">启用禁用</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic Enabledisable(W_Chapter model)
        {
            return Success(mapper.Enabledisable(model) ? "操作成功" : "操作失败");
        }




        #region 章节题目 created by nht  2017-11-08       注意：所有章节题目不包括组合题 （表w_question  字段QuestionData_ID = 0 表示非组合题）

        /// <summary>
        /// 章节选择题目
        /// </summary>
        /// <author>nht</author>
        /// <date>2017-11-08</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic CheckChapterQuestion(PracticePostModel post)
        {
            return Success(mapper.CheckChapterQuestion(post.chapterId, post.questionList) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 查询已有的题目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetChapterQuestionList([FromUri]ChapterQuestions request)
        {
            return Success(mapper.GetChapterQuestionList(request), request.PageIndex == 1 ? mapper.GetChapterQuestionTotalCount(request) : 0);
        }

        /// <summary>
        /// 查询未选的题目列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetNonChapterQuestionList([FromUri]ChapterQuestions request)
        {
            return Success(mapper.GetNonChapterQuestionList(request, this.System_Station_ID), request.PageIndex == 1 ? mapper.GetNonChapterQuestionTotalCount(request, this.System_Station_ID) : 0);
        }

        /// <summary>
        /// 给章节添加题目 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddChapterQuestions(ChapterPostModel model)
        {
            return Success(mapper.AddChapterQuestions(model) ? "操作成功" : "操作失败");
        }


        /// <summary>
        /// 删除多个章节题目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteChapterQuestion(ChapterPostModel request)
        {
            return Success(mapper.DeleteChapterQuestion(request) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 获取章节信息以及章节题目数量
        /// </summary>
        /// <param name="storeId">题库ID</param>
        /// <author>nht</author>
        /// <date>2017-11-09</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetChapterQuestion(int storeId)
        {
            return Success(mapper.GetChapterQuestion(storeId, this.SafeGetStuId));
        }

        /// <summary>
        /// 根据章节ID获取学生章节做题情况
        /// </summary>
        /// <param name="chapterId">章节ID</param>
        /// <author>nht</author>
        /// <date>2017-11-09</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetStudentChapterPractice(int chapterId)
        {
            return Success(mapper.GetStudentChapterPractice(chapterId, this.SafeGetStuId));
        }

        /// <summary>
        /// 创建章节练习
        /// </summary>
        /// <param name="storeId">题库ID</param>
        /// <param name="chapterId">章节ID</param>
        /// <param name="questionType">题目类型（多个以逗号隔开）</param>
        /// <param name="questionCount">题目数量</param>
        /// <param name="questionSource">题目来源（0所有，1已做，2未做，3做错）</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic CreateChapterPractice(PracticePostModel post)
        {
            W_ChapterPractice model = new W_ChapterPractice();
            model.QuestionStore_ID = post.storeId;
            model.AddTime = DateTime.Now;
            model.StuId = this.SafeGetStuId;
            return Success(mapper.CreateChapterPractice(model, post.chapterId, post.questionType, post.questionCount, post.questionSource));
        }

        /// <summary>
        /// 继续做题
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public dynamic ContinueChapterPractice(ChapterModel post) 
        {
            return Success(mapper.ContinueChapterPractice(post.ID, this.StuId));
        }

        /// <summary>
        /// 交卷
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public dynamic SubmitPractice(PracticeModel model)
        {
            return Success(mapper.SubmitPractice(model, this.SafeGetStuId) ? "交卷成功" : "交卷失败");
        }

        /// <summary>
        /// 显示做题结果
        /// </summary>
        /// <param name="busType">业务类型</param>
        /// <param name="busId">业务ID</param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetPracticeResult(int busId, int resultId)
        {
            return Success(mapper.GetPracticeResult(busId, resultId, this.StuId));
        }

        [HttpGet]
        public dynamic GetSafeGetStuId()
        {
            return this.SafeGetStuId;
        }

        [HttpGet]
        public dynamic GetStuId()
        {
            return this.StuId;
        }

        [HttpGet]
        public dynamic GetSystemID()
        {
            return this.System_Station_ID;
        }

        /// <summary>
        /// 获取我的题目数目（收藏，笔记，错题，做题记录）
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetMyQuestionInfo(int ID)
        {
            return Success(mapper.GetMyQuestionInfo(ID, this.StuId));
        }

        /// <summary>
        /// 获取章节练习名称信息
        /// </summary>
        /// <param name="chapterId"></param>
        /// <param name="storeId"></param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetPraticeInfo(int chapterId, int storeId)
        {
            return Success(mapper.GetPraticeInfo(chapterId, storeId));
        }

        /// <summary>
        /// 获取学生做题记录
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetStudentPraticeList([FromUri]W_CollectionModel page)
        {
            return Success(mapper.GetStudentPraticeList(this.SafeGetStuId, page), page.PageIndex == 1 ? mapper.GetStudentPraticeCount(this.SafeGetStuId, page.QuestionStore_ID) : 0);
        }

        #endregion
    }
}