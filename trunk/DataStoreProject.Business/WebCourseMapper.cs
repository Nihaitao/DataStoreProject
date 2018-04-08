﻿using CommonFramework.Exceptions;
using CommonFramework.IBatisNet;
using DataStoreProject.Model.Entity.Chapters;
using DataStoreProject.Model.Entity.HConfiguration;
using DataStoreProject.Model.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStoreProject.Business
{
    public class WebCourseMapper : BaseMapper
    {
        /// <summary>
        /// 获取课程列表
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public IList GetCourseList(W_CourseModel Model, int System_Station_ID)
        {

            return QueryForList("GetCourseList", new { System_Station_ID = System_Station_ID, TeachingMethod = Model.TeachingMethod, Discipline_ID = Model.Discipline_ID, Valid = Model.Valid, IsPutaway = Model.IsPutaway, IsRecommend = Model.IsRecommend, Name = Model.Name, StuID = Model.StuID, pageIndex = (Model.PageIndex - 1) * Model.PageSize, pageSize = Model.PageSize, pageStatus = Model.PageStatus });
        }

        /// <summary>
        /// 获取课程列表总条数
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public int GetCourseListTotalCount(W_CourseModel Model, int System_Station_ID)
        {

            return SqlMapper.QueryForObject<int>("GetCourseListTotalCount", new { System_Station_ID = System_Station_ID, TeachingMethod = Model.TeachingMethod, Discipline_ID = Model.Discipline_ID, Valid = Model.Valid, IsPutaway = Model.IsPutaway, IsRecommend = Model.IsRecommend, Name = Model.Name, StuID = Model.StuID, pageStatus = 0 });
        }

        /// <summary>
        /// 禁用启用网课
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public bool UpdateCourseValid(CampusModel Model, int System_Station_ID)
        {
            W_Course model = Orm.Single<W_Course>(x => x.System_Station_ID == System_Station_ID && x.ID == Model.ID);
            if (model == null)
            {
                throw new ApiException("您要操作的数据不存在");
            }
            if (Model.Valid == 0) 
            {
                int count = SqlMapper.QueryForObject<int>("GetCourseOrderCount", new { ID = Model.ID });
                if (count > 0)
                {
                    throw new ApiException("该课程已被网校学生购买无法禁用！");
                }
            }
            model.Valid = Model.Valid;
            if (Orm.Update(model) <= 0)
            {
                throw new ApiException("修改失败");
            }
            return true;
        }

        /// <summary>
        /// 删除网课及对应的关系表
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public bool DeleteCourse(CampusModel Model, int System_Station_ID)
        {

            SqlMapper.BeginTransaction();
            try
            {
                W_Course model = Orm.Single<W_Course>(x => x.System_Station_ID == System_Station_ID && x.ID == Model.ID);
                if (model == null)
                {
                    throw new ApiException("您要操作的数据不存在");
                }
                if (model.Valid == 1)
                {
                    throw new ApiException("请先禁用，再执行删除操作");
                }

                int count = SqlMapper.QueryForObject<int>("GetCourseOrderCount", new { ID = Model.ID });
                if (count > 0)
                {
                    throw new ApiException("该课程已被网校学生购买无法删除！");
                }
                Orm.Delete<W_Course>(x => x.ID == Model.ID && x.System_Station_ID == System_Station_ID);
                Orm.Delete<W_Course_Chapters>(x => x.Course_ID == Model.ID && x.System_Station_ID == System_Station_ID);
                int isDelete = SqlMapper.QueryForObject<int>("DeleteCourse", new { ID = Model.ID,System_Station_ID=System_Station_ID });
                Orm.Delete<W_Course_Unit>(x => x.Course_ID == Model.ID && x.System_Station_ID == System_Station_ID);
                Orm.Delete<W_DataInfo>(x => x.BusID == Model.ID && x.System_Station_ID == System_Station_ID);
                SqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 上架下架网课
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public bool UpdateCourseIsPutaway(CampusModel Model, int System_Station_ID)
        {

            SqlMapper.BeginTransaction();
            try
            {
                W_Course model = Orm.Single<W_Course>(x => x.System_Station_ID == System_Station_ID && x.ID == Model.ID);
                if (model == null)
                {
                    throw new ApiException("您要操作的数据不存在");
                }

                model.IsPutaway = Model.IsPutaway;
                if (Orm.Update(model) <= 0)
                {
                    throw new ApiException("修改失败，请重新操作");
                }
                SqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 添加修改网课单元
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public dynamic SaveCourse_Unit_ClassTime(W_Course_Unit_ClassTime Model, int System_Station_ID, int AccountID)
        {
            if (Model.ID == 0)
            {
                Model.System_Station_ID = System_Station_ID;
                Model.AddTime = DateTime.Now;
                Model.Valid = 1;
                Model.AddPerson = AccountID;
                return Orm.Single<W_Course_Unit_ClassTime>(x => x.ID == (int)Orm.Insert(Model,true) && x.System_Station_ID == System_Station_ID);
            }
            else
            {
                W_Course_Unit_ClassTime list = SqlMapper.QueryForObject<W_Course_Unit_ClassTime>("GetCourseUnitClassTimeByID", new { ID = Model.ID, System_Station_ID = System_Station_ID });
                if (list == null)
                {
                    throw new ApiException("操作失败，未找到对应的数据！");
                }
                Model.AddPerson = list.AddPerson;
                Model.AddTime = list.AddTime;
                Model.System_Station_ID = list.System_Station_ID;
                if (Orm.Update(Model) <= 0)
                {
                    throw new ApiException("修改失败");
                }
                return Model;
            }
        }

        /// <summary>
        /// 根据网课id 查找对应数据
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public dynamic GetCourseByID(int ID, int System_Station_ID, string StuId)
        {
            W_CourseModel model = SqlMapper.QueryForObject<W_CourseModel>("GetCourseByID", new { ID = ID, System_Station_ID = System_Station_ID, StuId = StuId });
            if (model != null && model.Discipline_ID > 0)
            {
                //获取一级 二级 三级学科ID
                DisciplineIDModel DisciplineModel = new DisciplineMapper().GetDisciplineIDS(model.Discipline_ID);
                if (DisciplineModel != null)
                {
                    model.Discipline_OneLevelID = DisciplineModel.Discipline_OneLevelID;//一级学科ID
                    model.Discipline_TwoLevelID = DisciplineModel.Discipline_TwoLevelID;//二级学科ID
                    model.Discipline_ThreeLevelID = DisciplineModel.Discipline_ThreeLevelID;//三级学科ID
                }
            }
            return model;
        }

        /// <summary>
        /// 根据对应条件查询资料数据
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public dynamic GetDataInfoList(DataInfoModel model, int System_Station_ID)
        {
            return QueryForList("GetCourseList", new { DataInfoType = model.DataInfoType, IsOpen = model.IsOpen, Course_ID = model.Course_ID, CourseChapters_ID = model.CourseChapters_ID, CourseUnit_ID = model.CourseUnit_ID, CourseUnitClassTime_ID = model.CourseUnitClassTime_ID, pageIndex = (model.PageIndex - 1) * model.PageSize, pageSize = model.PageSize, pageStatus = model.PageStatus });
        }

        /// <summary>
        /// 根据对应条件查询资料数据总条数
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public dynamic GetDataInfoListTotalCount(DataInfoModel model, int System_Station_ID)
        {
            return QueryForList("GetDataInfoListTotalCount", new { DataInfoType = model.DataInfoType, IsOpen = model.IsOpen, Course_ID = model.Course_ID, CourseChapters_ID = model.CourseChapters_ID, CourseUnit_ID = model.CourseUnit_ID, CourseUnitClassTime_ID = model.CourseUnitClassTime_ID, pageStatus = 0 });
        }

        /// <summary>
        /// 添加修改 网课 
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public dynamic SaveCourse(W_Course model, int System_Station_ID, int AccountID)
        {
            if (GetExsitWebCourse(model, System_Station_ID) > 0)
            {
                throw new ApiException("该网课已存在！");
            }
            if (model.ID > 0)
            {
                W_CourseModel Model = GetCourseByID(model.ID, System_Station_ID, "");
                if (Model == null)
                {
                    throw new ApiException("操作失败，未找到对应的数据！");
                }
                model.AddPerson = Model.AddPerson;
                model.AddTime = Model.AddTime;
                model.System_Station_ID = Model.System_Station_ID;
                model.Valid = Model.Valid;

                if (Orm.Update(model) <= 0)
                {
                    throw new ApiException("修改失败");
                }
                return model;
            }
            else
            {
                model.System_Station_ID = System_Station_ID;
                model.Valid = 1;
                model.AddTime = DateTime.Now;
                model.AddPerson = AccountID;
                return Orm.Single<W_Course>(x => x.ID == (int)Orm.Insert(model, true) && x.System_Station_ID == System_Station_ID);
            }
        }
        

        /// <summary>
        /// 查询网课是否存在
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int GetExsitWebCourse(W_Course Model, int System_Station_ID)
        {
            return SqlMapper.QueryForObject<int>("GetExsitWebCourse", new { Name = Model.Name, System_Station_ID = System_Station_ID, ID = Model.ID });
        }

        /// <summary>
        /// 查询网课对应的单元数据  包含单元下课次
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public List<Course_UnitModel> GetCourse_UnitNodeList(W_Course_Unit Model, int System_Station_ID)
        {
            List<Course_UnitModel> UnitList = SqlMapper.QueryForList<Course_UnitModel>("GetCourse_UnitList", new { Valid = Model.Valid, System_Station_ID = System_Station_ID, Course_ID = Model.Course_ID }).ToList();
            Model.System_Station_ID = System_Station_ID;

            if (UnitList != null && UnitList.Count > 0)
            {
                //单元课次
                List<W_Course_Unit_ClassTime> UnitClassTimeList = new List<W_Course_Unit_ClassTime>();
                string UnitIDS = string.Join(",", UnitList.Select(x => x.ID));
                //对应单元集合
                UnitClassTimeList = GetCourseUnitClassTimeList(Model, UnitIDS);
                if (UnitClassTimeList != null && UnitClassTimeList.Count > 0)
                {  //给单元赋 对应的课次
                    UnitList.ForEach(F =>
                    {
                        F.UnitClassTimeList = UnitClassTimeList.FindAll(X => X.Unit_ID == F.ID);
                    });
                }
            }
            else 
            {
                throw new ApiException("未找到对应单元数据");
            }

            return UnitList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="UnitIDS"></param>
        /// <returns></returns>
        public List<W_Course_Unit_ClassTime> GetCourseUnitClassTimeList(W_Course_Unit Model, string UnitIDS)
        {
            List<W_Course_Unit_ClassTime> model = SqlMapper.QueryForList<W_Course_Unit_ClassTime>("GetCourseUnitClassTimeList", new { UnitIDS = UnitIDS, ID = Model.ID, System_Station_ID = Model.System_Station_ID, Valid = Model.Valid }).ToList();
            return model;
        }


        /// <summary>
        /// 删除单元及对应的关系表
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic DeleteCourseUnit(W_Course_Unit Model, int System_Station_ID)
        {

            SqlMapper.BeginTransaction();
            try
            {
                W_Course_Unit model = Orm.Single<W_Course_Unit>(x => x.ID == Model.ID && x.System_Station_ID == System_Station_ID);
                if (model == null)
                {
                    throw new ApiException("找不到对应数据，请重试");
                }
                Orm.Delete<W_Course_Unit_ClassTime>(x => x.Unit_ID == Model.ID);
                Orm.Delete<W_Course_Unit>(x => x.ID == Model.ID);
                Orm.Delete<W_DataInfo>(x => x.BusID == Model.ID && x.BusType == 2);

                SqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw ex;
            }

        }


        /// <summary>
        /// 添加修改网课单元
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic SaveCourse_Unit(W_Course_Unit Model, int System_Station_ID, int AccountID)
        {
            if (Model.ID > 0)
            {
                W_Course_Unit model = Orm.Single<W_Course_Unit>(x => x.ID == Model.ID && x.System_Station_ID == System_Station_ID);
                if (model == null)
                {
                    throw new ApiException("找不到对应数据，请重试");
                }
                Model.System_Station_ID = model.System_Station_ID;
                Model.AddPerson = model.AddPerson;
                Model.AddTime = model.AddTime;
                Model.Valid = model.Valid;
                Model.Orders = model.Orders;
                if (Orm.Update(Model) <= 0)
                {
                    throw new ApiException("修改失败");
                }
                Course_UnitModel m = SqlMapper.QueryForObject<Course_UnitModel>("SelectCourse_Unit", new  {ID=model.ID,System_Station_ID=System_Station_ID });
                if (m != null)
                {
                    //单元课次
                    List<W_Course_Unit_ClassTime> UnitClassTimeList = new List<W_Course_Unit_ClassTime>();
                    //对应单元集合
                    UnitClassTimeList = GetCourseUnitClassTimeList(Model, m.ID.ToString());
                    if (UnitClassTimeList != null && UnitClassTimeList.Count > 0)
                    { 
                         m.UnitClassTimeList = UnitClassTimeList.FindAll(X => X.Unit_ID == m.ID);
                    }
                }
                return m;
            }
            else
            {
                Model.AddPerson = AccountID;
                Model.System_Station_ID = System_Station_ID;
                Model.AddTime = DateTime.Now;
                Model.Valid = 1;
                int Orders = SqlMapper.QueryForObject<int>("GetMaxCourseUnit", new { Course_ID=Model.Course_ID,System_Station_ID=System_Station_ID });
                Model.Orders = Orders + 1;
                int modelid = (int)Orm.Insert(Model, true);
                if (modelid <= 0) 
                {
                    throw new ApiException("添加失败");
                }
                return Orm.Single<W_Course_Unit>(x => x.ID == modelid && x.System_Station_ID == System_Station_ID);
            }
        }

        /// <summary>
        /// 删除单元及对应的关系表
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic DeleteCourseUnitClassTime(W_Course_Unit_ClassTime Model, int System_Station_ID)
        {

            SqlMapper.BeginTransaction();
            try
            {
                W_Course_Unit_ClassTime model = Orm.Single<W_Course_Unit_ClassTime>(x => x.ID == Model.ID && x.System_Station_ID == System_Station_ID);
                if (model == null)
                {
                    throw new ApiException("未找到对应数据，请查看再试");
                }
                Orm.Delete<W_Course_Unit_ClassTime>(x => x.ID == Model.ID && x.System_Station_ID==System_Station_ID);
                Orm.Delete<W_DataInfo>(x => x.BusID == Model.ID && x.System_Station_ID==System_Station_ID);

                SqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw ex;
            }

        }

        /// <summary>
        /// 添加修改网课章节
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic SaveCourse_Chapters(W_Course_Chapters Model, int System_Station_ID, int AccountID)
        {
            if (Model.Name == null) 
            {
                throw new ApiException("名称不能为空");
            }
            if (Model.ID > 0)
            {
                W_Course_Chapters model = Orm.Single<W_Course_Chapters>(x => x.ID == Model.ID && x.System_Station_ID == System_Station_ID);
                if (model == null)
                {
                    throw new ApiException("未找到对应数据");
                }
                Model.AddPerson = model.AddPerson;
                Model.AddTime = model.AddTime;
                Model.System_Station_ID = model.System_Station_ID;
                Model.Valid = model.Valid;
                Model.Orders = model.Orders;
                if (Orm.Update(Model) <= 0)
                {
                    throw new ApiException("修改失败，请重试");
                }

                return  GetCourse_ChaptersNodeList(model, "", System_Station_ID);
            }
            else
            {
                Model.System_Station_ID = System_Station_ID;
                Model.AddPerson = AccountID;
                Model.AddTime = DateTime.Now;
                Model.Valid = 1;
                Model.Orders = SqlMapper.QueryForObject<int>("GetMaxchapterOrders", new { System_Station_ID = System_Station_ID, CID = Model.CID }) + 1;
                return  Orm.Single<W_Course_Chapters>(x => x.ID == (int)Orm.Insert(Model, true) && x.System_Station_ID == System_Station_ID);
            }
        }

        /// <summary>
        /// 根据网课id查找对应的章节  分子父节点
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic GetCourse_ChaptersNodeList(W_Course_Chapters model, string StuId, int System_Station_ID)
        {
            List<Course_ChaptersModel> List = SqlMapper.QueryForList<Course_ChaptersModel>("GetCourse_ChaptersList", new { System_Station_ID = System_Station_ID, StuId = StuId,Course_ID = model.Course_ID, CID = model.CID, Valid = model.Valid }).ToList();
            List<Course_ChaptersModel> NodeList = new List<Course_ChaptersModel>();
            if (List != null && List.Count > 0)
            {

                NodeList = List.FindAll(X => X.CID == 0);
                NodeList.ForEach(x =>
                {
                    x.ChildNodeList = List.FindAll(F => F.CID == x.ID);
                });
            }
            return NodeList;
        }


        /// <summary>
        /// 删除网课章节及对应的关系表
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic DeleteCourseChapters(W_Course_Chapters Model, int System_Station_ID)
        {

            SqlMapper.BeginTransaction();
            try
            {
                W_Course_Chapters model = Orm.Single<W_Course_Chapters>(x => x.ID == Model.ID && x.System_Station_ID == System_Station_ID);
                if (model == null)
                {
                    throw new ApiException("未找到对应数据，请查看再试");
                }
                Orm.Delete<W_Course_Chapters>(x => x.ID == Model.ID && x.System_Station_ID == System_Station_ID);
                Orm.Delete<W_DataInfo>(x => x.BusID == Model.ID && x.BusType == 1);

                SqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw ex;
            }
        }

        /// <summary>
        /// 删除网课章节及对应的关系表
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic GetCourseUnitClassTimeByID(int ID, int System_Station_ID)
        {
            return SqlMapper.QueryForObject<W_Course_Unit_ClassTime>("GetCourseUnitClassTimeByID", new { ID = ID, System_Station_ID = System_Station_ID });
        }

        public dynamic GetCourseLookInfo(string StuId, int System_Station_ID)
        {
            return QueryForList("GetCourseLookInfo", new { StuId, System_Station_ID });
        }


        /// <summary>
        /// 拖拽排序（拖拽完就保存）
        /// </summary>
        /// <param name="new_prev_order">拖拽后上节点排序值</param>
        /// <param name="currDisciplineId">当前被拖拽ID</param>
        /// <param name="currParentId">当前被拖拽ID的父ID</param>
        /// <returns></returns>
        public bool MoveChapter(SortDisciplineModel model, int System_Station_ID)
        {
            SqlMapper.BeginTransaction();
            try
            {
                W_Course_Chapters ChapterModel = Orm.Single<W_Course_Chapters>(x => x.ID == model.currDisciplineId && x.System_Station_ID == System_Station_ID);
                if (ChapterModel == null)
                {
                    throw new ApiException("请刷新重试，您要操作的数据不存在");
                }

                if (model.currParentId == 0)
                {
                    SqlMapper.Update("MoveChapter", new { new_prev_order = model.new_prev_order, currParentId = 0, currDisciplineId = model.currDisciplineId, System_Station_ID = System_Station_ID });
                }
                else if (model.currParentId > 0)
                {
                    SqlMapper.Update("MoveChapter", new { currParentId = model.currParentId, new_prev_order = model.new_prev_order, currDisciplineId = model.currDisciplineId, System_Station_ID = System_Station_ID });
                }
                SqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw ex;
            }

        }

        /// <summary>
        /// 添加录播的观看视频
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic SaveCourse_Look(W_Course_Look_DetailModel Model, string StuId, int System_Station_ID)
        {
            SqlMapper.BeginTransaction();
            try
            {
                if (Model.CourseChapters_ID <= 0)
                {
                    throw new ApiException("章节ID不能小于0");
                }
                if (Model.LookType <0)
                {
                    throw new ApiException("观看类型LookType不能为空！");
                }
                Model.EndTime = DateTime.Now;
                Model.AddTime = DateTime.Now;
                W_Course_Look_Detail OldModel = SqlMapper.QueryForObject<W_Course_Look_Detail>("GetCourse_Look_Detail", new { StuId = StuId, CourseChapters_ID = Model.CourseChapters_ID });
                if (OldModel != null)
                {
                    OldModel.EndTime = OldModel.EndTime.AddSeconds(double.Parse(Model.LookTime.ToString()));

                    if (SqlMapper.Update("UpdateCourse_Look_Detail", new { ID = OldModel.ID, EndTime = OldModel.EndTime }) <= 0) 
                    {
                        throw new ApiException("更新最后播放时间出错");
                    }

                    CourseLookTimeModel LookTimeModel = SqlMapper.QueryForObject<CourseLookTimeModel>("GetCourseLookTimeModel", new { stuid =StuId, CourseChapters_ID = Model.CourseChapters_ID });
                    int TotalSeconds = 0;//已播放的秒数
                    int Duration = 0;//视频的时长
                    if (LookTimeModel != null)
                    {
                        TotalSeconds = LookTimeModel.TotalSeconds;
                        Duration = LookTimeModel.Duration;
                    }
                    else
                    {
                        throw new ApiException("未找到用户的播放记录");
                    }

                    //未获取到视频播放时长
                    if (Duration <= 0)
                    {
                        throw new ApiException("视频的时长为0");
                    }

                    //默认观看6分钟添加记录
                    int LookDuration = 6;
                    //查询配置信息 视频播放时长 
                    W_Configuration ConfigurationModel =new HConfigurationMapper().GetWConfiguration(System_Station_ID);

                    if (ConfigurationModel != null)
                        LookDuration = ConfigurationModel.LookDuration;

                    //判断学生观看视频记录时间是否大于设置的时长 或者 视频时长小于设置的时长的时候判断观看时长和视频时长相差不到10秒的时候执行 
                    if (TotalSeconds >= LookDuration * 60 || (Duration < LookDuration * 60 && (Duration - TotalSeconds) <= 10))
                    {
                        int counts = SqlMapper.QueryForObject<int>("GetW_Course_LookCounts", new { stuid = StuId, CourseChapters_ID = Model.CourseChapters_ID });
                        if (counts == 0)
                        {
                            W_Course_Look CourseLook = new W_Course_Look();
                            CourseLook.CourseChapters_ID = Model.CourseChapters_ID;
                            CourseLook.IP = Model.IP;
                            CourseLook.LookTime = DateTime.Now;
                            CourseLook.LookType = Model.LookType;
                            CourseLook.StuId = StuId;
                            Orm.Insert<W_Course_Look>(CourseLook);
                        }
                    }

                }
                else
                {
                    W_Course_Look_Detail model = new W_Course_Look_Detail();
                    model.AddTime = Model.AddTime;
                    model.CourseChapters_ID = Model.CourseChapters_ID;
                    model.EndTime = Model.EndTime;
                    model.IP = Model.IP;
                    model.LookType = Model.LookType;
                    model.StuId = StuId;
                    Orm.Insert(model);
                }
                SqlMapper.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw ex;
            }
           
        }

        /// <summary>
        /// 根据网课章节id 查找对应数据
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic GetCourseChaptersByID(int ID, int System_Station_ID)
        {
            return SqlMapper.QueryForObject<W_Course_ChaptersModel>("GetCourseChaptersByID", new { ID = ID, System_Station_ID = System_Station_ID });
        }

        /// <summary>
        /// 根据网课单元id 查找对应数据
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public dynamic GetCourseUnitByID(int ID, int System_Station_ID)
        {
            return SqlMapper.QueryForObject<W_Course_UnitModel>("GetCourseUnitByID", new { ID = ID, System_Station_ID = System_Station_ID });
        }
    }
}
