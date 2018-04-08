﻿using CT.Common.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataStoreProject.Business;
using System.Web.Http;
using DataStoreProject.Model.Entity.Question;
using DataStoreProject.Model.ViewModel;
using System.Net.Http;
using System.IO;

namespace DataStoreProject.Web.Controller
{
    public class QuestionController : StationBaseApiController
    {
        QuestionMapper mapper = new QuestionMapper();

        #region 题型  created by nht    2017-11-1
        /// <summary>
        /// 获取题型列表
        /// </summary>
        /// <author>nht</author>
        /// <date>2017-11-1</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionTypeList()
        {
            var dict = mapper.GetQuestionTypeList();
            return Success(dict, dict.Count);
        }
        #endregion

        #region 题库  created by nht    2017-11-1
        /// <summary>
        /// 添加题库
        /// </summary>
        /// <param name="model">题库</param>
        /// <author>nht</author>
        /// <date>2017-11-1</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddQuestionStore(W_QuestionStore model)
        {
            model.System_Station_ID = this.System_Station_ID;
            model.AddPerson = this.AccountID;
            model.AddTime = DateTime.Now;
            model.Valid = 1;//默认启用
            return Success(mapper.AddQuestionStore(model) ? "操作成功" : "操作失败");
        }
        /// <summary>
        /// 禁用题库
        /// </summary>
        /// <param name="model">题库</param>
        /// <author>nht</author>
        /// <date>2017-11-1</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic DisableQuestionStore(W_QuestionStore model)
        {
            W_QuestionStore newModel = mapper.GetQuestionStore(model.ID);
            newModel.Valid = model.Valid;
            return Success(mapper.DisableQuestionStore(newModel) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 修改、删除题库
        /// </summary>
        /// <param name="model">题库</param>
        /// <author>nht</author>
        /// <date>2017-11-1</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic ModifyQuestionStore(W_QuestionStore model)
        {
            W_QuestionStore newModel = mapper.GetQuestionStore(model.ID);
            //修改（只能修改名称）
            if (!string.IsNullOrEmpty(model.Name))
                newModel.Name = model.Name;
            //删除
            else if(model.IsDelete == 1)
                newModel.IsDelete = 1;
            return Success(mapper.UpdateQuestionStore(newModel) ? "操作成功" : "操作失败");
        }


        /// <summary>
        /// 根据题库ID获取题库
        /// </summary>
        /// <param name="id">题库id</param>
        /// <author>nht</author>
        /// <date>2017-11-1</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionStore(int id)
        {
            return Success(mapper.GetQuestionStoreById(id));
        }


        /// <summary>
        /// 根据条件查询题库列表
        /// </summary>
        /// <param name="request"></param>
        /// <author>nht</author>
        /// <date>2017-11-1</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionStoreList([FromUri]QuestionStoreRequest request)
        {
            if (!string.IsNullOrEmpty(request.Name))
                request.Name = request.Name.Trim();
            var dict = mapper.GetQuestionStoreList(request,this.System_Station_ID);
            return Success(dict, request.PageIndex == 1 ? mapper.GetTotalCount(request, this.System_Station_ID) : 0);
        }

        /// <summary>
        /// 获取我已做练习，收藏，笔记的题库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetMyQuestionStoreList()
        {
            return Success(mapper.GetMyQuestionStoreList(this.StuId, this.System_Station_ID));
        }
        #endregion

        #region 题目  created by nht    2017-11-2

        /// <summary>
        /// 添加题目信息
        /// </summary>
        /// <param name="model"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddQuestion(W_Question model)
        {
            bool flag = false;
            if (model != null) 
            {
                model.System_Station_ID = this.System_Station_ID;
                model.AddPerson = this.AccountID;
                model.AddTime = DateTime.Now;
                flag = mapper.AddQuestion(model);
            }
            return Success(flag ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 添加组合题材料信息
        /// </summary>
        /// <param name="model"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddQuestionData(W_QuestionData model) 
        {
            model.AddPerson = this.AccountID;
            model.AddTime = DateTime.Now;
            return Success(mapper.AddQuestionData(model));
        }

        /// <summary>
        /// 修改题目信息
        /// </summary>
        /// <param name="model"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateQuestion(W_Question model)
        {
            bool flag = false;
            if (model != null && model.ID > 0)
            {
                flag = mapper.UpdateQuestion(model);
            }
            return Success(flag ? "操作成功" : "操作失败");
        }
        
        /// <summary>
        /// 修改组合题材料信息
        /// </summary>
        /// <param name="model"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateQuestionData(QuestionData model)
        {
            return Success(mapper.UpdateQuestionData(model.ID, model.Content, model.QuestionStore_ID) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="model"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteQuestion(W_QuestionData model)
        {
            return Success(mapper.DeleteQuestion(model.ID,this.System_Station_ID) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 删除组合题
        /// </summary>
        /// <param name="model"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteQuestionData(W_QuestionData model)
        {
            return Success(mapper.DeleteQuestionData(model.ID) ? "操作成功" : "操作失败");            
        }

        /// <summary>
        /// 获取普通题目信息
        /// </summary>
        /// <param name="id"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestion(int id)
        {
            return Success(mapper.GetQuestion(id));
        }

        /// <summary>
        /// 获取组合题目信息
        /// </summary>
        /// <param name="id"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionData(int id)
        {
            return Success(mapper.GetQuestionData(id));
        }
        

        /// <summary>
        /// 根据组合题ID获取组合题目列表
        /// </summary>
        /// <param name="id"></param>
        /// <author>nht</author>
        /// <date>2017-11-17</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionDataById(int id)
        {
            return Success(mapper.GetQuestionDataById(id));
        }

        /// <summary>
        /// 获取题目列表
        /// </summary>
        /// <param name="request"></param>
        /// <author>nht</author>
        /// <date>2017-11-2</date>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionList([FromUri]QuestionRequest request)
        {
            if (!string.IsNullOrEmpty(request.Title))
                request.Title = request.Title.Trim();
            return Success(mapper.GetQuestionList(request, this.System_Station_ID), request.PageIndex == 1 ? mapper.GetQuestionTotalCount(request, this.System_Station_ID) : 0);
        }
        /// <summary>
        /// 实例化上传的Excel文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public dynamic ExcelToQuestionList() 
        {
            HttpPostedFile file = HttpContext.Current.Request.Files["file"];
            if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength == 0)
                return Error("文件不能为空");
            return Success(mapper.ExcelToQuestionList(file.InputStream, file.FileName, this.System_Station_ID));
        }

        [HttpPost]
        public dynamic ImportQuestion(Question model) 
        {
            return Success(mapper.ImportQuestion(model, this.System_Station_ID, this.AccountID));
        }
        #endregion

        #region 纠错 created by szf 2017-11-8

        /// <summary>
        /// 添加纠错 by szf
        /// </summary>
        /// <author>SZF</author>
        /// <date>2018-1-23</date>
        /// <param name="model">W_QuestionerrorCorrection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic AddQuestionerrorCorrection(W_QuestionerrorCorrection model)
        {
            model.AddTime = DateTime.Now;
            model.StuID = this.SafeGetStuId;
            model.Valid = 0;
            return Success(mapper.AddQuestionerrorCorrection(model) ? "操作成功" : "操作失败");
        }

        /// <summary>
        ///  批量处理纠错(根据题目id) by szf
        /// </summary>
        /// <author>SZF</author>
        /// <date>2018-1-23</date>
        /// <param name="model">W_QuestionerrorCorrection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateQuestionerrorCorrection(CampusModel model)
        {
            return Success(mapper.UpdateQuestionerrorCorrection(model) ? "操作成功" : "操作失败");
        }

        /// <summary>
        ///  根据主键ID处理纠错 by szf
        /// </summary>
        /// <author>SZF</author>
        /// <date>2018-1-23</date>
        /// <param name="model">W_QuestionerrorCorrection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic UpdateQuestionerrorCorrectionByID(CampusModel model)
        {
            return Success(mapper.UpdateQuestionerrorCorrectionByID(model) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 删除纠错 by szf
        /// </summary>
        /// <author>SZF</author>
        /// <date>2018-1-23</date>
        /// <param name="model">W_QuestionerrorCorrection</param>
        /// <returns></returns>
        [HttpPost]
        public dynamic DeleteQuestionerrorCorrection(W_QuestionerrorCorrection model)
        {
            return Success(mapper.DeleteQuestionerrorCorrection(model) ? "操作成功" : "操作失败");
        }

        /// <summary>
        /// 查询纠错列表 by szf
        /// </summary>
        /// <author>SZF</author>
        /// <date>2018-1-23</date>
        /// <param name="model">W_QuestionerrorCorrection</param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetQuestionerrorCorrectionList([FromUri]CampusModel model)
        {
            return Success(mapper.GetQuestionerrorCorrectionList(model,this.System_Station_ID), model.PageIndex == 1 ? mapper.GetQuestionerrorCorrectionListCount(model,this.System_Station_ID) : 0);
        }

        /// <summary>
        /// 查询纠错汇总列表 by szf
        /// </summary>
        /// <author>SZF</author>
        /// <date>2018-1-24</date>
        /// <param name="model">W_QuestionerrorCorrection</param>
        /// <returns></returns>
        [HttpGet]
        public dynamic GetErrorcorrectionsummaryList([FromUri]CampusModel model)
        {
            return Success(mapper.GetErrorcorrectionsummaryList(model));
        }
        #endregion


    }
}