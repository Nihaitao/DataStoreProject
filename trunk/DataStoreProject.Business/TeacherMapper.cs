﻿using CommonFramework.Exceptions;
using CommonFramework.IBatisNet;
using DataStoreProject.Model.Entity.Teacher;
using DataStoreProject.Model.ViewModel;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataStoreProject.Business
{
    public class TeacherMapper : BaseMapper
    {
        #region 老师相关操作 hx add 2018-01-03

        /// <summary>
        /// 添加老师
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool TeacherInsert(TeacherModel model)
        {
            bool add = false;

            if (model.System_Station_ID == 0)
            {
                throw new ApiException("机构ID不存在！");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ApiException("姓名不能为空！");
            }

            if (string.IsNullOrEmpty(model.CardNo))
            {
                //throw new ApiException("身份证不能为空！");
            }
            else
            {
                string CardNumRegexStr = "(^\\d{15}$)|(^\\d{18}$)|(^\\d{17}(\\d|X|x)$)";
                if (!Regex.IsMatch(model.CardNo, CardNumRegexStr))
                {
                    throw new ApiException("身份证格式不正确");
                }

                //查询老师身份证是否存在
                TeacherModel teacherModel = SqlMapper.QueryForObject<TeacherModel>("GetTeacherByWhere", new { CardNo = model.CardNo, Phone = "", System_Station_ID = model.System_Station_ID, TeacherDetail_ID = 0 });
                if (teacherModel != null)
                {
                    throw new ApiException("身份证为" + model.CardNo + "的老师已存在，姓名为" + teacherModel.Name);
                }
            }

            //查询老师手机号码是否存在
            TeacherModel PhoneModel = SqlMapper.QueryForObject<TeacherModel>("GetTeacherByWhere", new { CardNo = "", Phone = model.Phone, System_Station_ID = model.System_Station_ID, TeacherDetail_ID = 0 });
            if (PhoneModel != null)
            {
                throw new ApiException("电话号码为" + model.Phone + "的老师已存在，姓名为" + PhoneModel.Name);
            }

            try
            {
                SqlMapper.BeginTransaction();//开启事务

                //根据身份证查询老师主表
                T_TeacherInfo teacher = null;
                if (!string.IsNullOrEmpty(model.CardNo))
                   teacher = SqlMapper.QueryForObject<T_TeacherInfo>("GetTeacherByCardOrPhone", new { CardNo = model.CardNo, Phone = "" });
                //根据手机号码查询老师主表
                T_TeacherInfo teacher2 = SqlMapper.QueryForObject<T_TeacherInfo>("GetTeacherByCardOrPhone", new { CardNo = "", Phone = model.Phone });

                if (teacher == null && teacher2 == null) //主表没有，插入主表
                {
                    T_TeacherInfo teacher3 = new T_TeacherInfo();
                    teacher3.Name = model.Name;
                    teacher3.Sex = model.Sex;
                    teacher3.CardNo = model.CardNo;
                    teacher3.JobTitle = model.JobTitle;
                    teacher3.HeadImage = model.HeadImage;
                    teacher3.Phone = model.Phone;
                    teacher3.Email = model.Email;
                    teacher3.Birthday = model.Birthday;
                    teacher3.Address = model.Address;
                    teacher3.Education_ID = model.Education_ID;
                    teacher3.GraduateSchool = model.GraduateSchool;
                    teacher3.Introduction = model.Introduction;
                    teacher3.AddPerson = model.AddPerson;
                    teacher3.AddTime = model.AddTime;

                    int id = (int)Orm.Insert(teacher3, true);

                    if (id > 0)
                    {
                        //添加主表成功，添加子表
                        model.ID = id;
                        add = InsertTeacherDetail(model);
                    }
                    else
                    {
                        throw new ApiException("添加老师主表失败！");
                    }
                }
                else //主表有，插入子表
                {
                    if (teacher != null && teacher2 != null)
                    {
                        //如果是同一主表
                        if (teacher.ID == teacher2.ID)
                        {
                            model.ID = teacher.ID;
                        }
                        else
                        {
                            //不是同一主表，以身份证为主表
                            model.ID = teacher.ID;
                            model.CardNo = ""; //不更新身份证
                            model.Phone = ""; //不更新号码                            
                        }
                    }
                    else if (teacher != null && teacher2 == null)
                    {
                        model.ID = teacher.ID;
                    }
                    else if (teacher == null && teacher2 != null)
                    {
                        model.ID = teacher2.ID;
                    }

                    int updateTeacher = SqlMapper.Update("TeacherUpdate", new
                    {
                        Name = model.Name,
                        Sex = model.Sex,
                        CardNo = model.CardNo,
                        JobTitle = model.JobTitle,
                        HeadImage = model.HeadImage,
                        Phone = model.Phone,
                        Email = model.Email,
                        Birthday = model.Birthday,
                        Address = model.Address,
                        Education_ID = model.Education_ID,
                        GraduateSchool = model.GraduateSchool,
                        Introduction = model.Introduction,
                        ID = model.ID,
                        //System_Station_ID = model.System_Station_ID
                    });
                    if (updateTeacher > 0) //修改主表
                    {
                        //添加子表
                        add = InsertTeacherDetail(model);
                    }
                }

                SqlMapper.CommitTransaction();
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw new ApiException(ex.Message);
            }
            finally
            {
                //SqlMapper.CloseConnection();
            }

            return add;
        }

        /// <summary>
        /// 添加老师子表、学科关系
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertTeacherDetail(TeacherModel model)
        {
            bool add = false;

            T_TeacherInfo_Detail detail = new T_TeacherInfo_Detail();
            detail.Teacher_ID = model.ID;
            detail.System_Station_ID = model.System_Station_ID;
            detail.AddTime = model.AddTime;
            detail.AddPerson = model.AddPerson;
            detail.Valid = model.Valid;

            int id2 = (int)Orm.Insert(detail, true);

            if (id2 > 0)
            {
                //添加子表成功，添加老师学科关系
                if (model.Teacher_DisciplineIds != null && model.Teacher_DisciplineIds.Length > 0)
                {
                    string[] dics = model.Teacher_DisciplineIds.Split(',');
                    if (dics.Length > 0)
                    {
                        T_TeacherInfo_Detail_Discipline teacherD_Dis = null;
                        foreach (string disciplineId in dics)
                        {
                            if (string.IsNullOrEmpty(disciplineId))
                                continue;

                            teacherD_Dis = new T_TeacherInfo_Detail_Discipline();
                            teacherD_Dis.TeacherDetail_ID = id2;
                            teacherD_Dis.System_Station_ID = model.System_Station_ID;
                            teacherD_Dis.Discipline_ID = int.Parse(disciplineId);
                            teacherD_Dis.AddTime = model.AddTime;
                            teacherD_Dis.AddPerson = model.AddPerson;

                            //添加老师所教学科
                            Orm.Insert(teacherD_Dis);
                        }
                    }
                }

                add = true;
            }
            else
            {
                throw new ApiException("添加老师详细表失败！");
            }

            return add;
        }

        /// <summary>
        /// 修改老师
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool TeacherUpdate(TeacherModel model)
        {
            if (model.System_Station_ID == 0)
            {
                throw new ApiException("机构ID不存在！");
            }

            if (model.TeacherDetail_ID == 0)
            {
                throw new ApiException("TeacherDetail_ID不能为空！");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ApiException("姓名不能为空！");
            }

            if (string.IsNullOrEmpty(model.CardNo))
            {
                //throw new ApiException("身份证不能为空！");
            }
            else
            {
                string CardNumRegexStr = "(^\\d{15}$)|(^\\d{18}$)|(^\\d{17}(\\d|X|x)$)";
                if (!Regex.IsMatch(model.CardNo, CardNumRegexStr))
                {
                    throw new ApiException("身份证格式不正确！");
                }
            }

            //根据老师详细ID查询老师
            TeacherModel teacherModel = SqlMapper.QueryForObject<TeacherModel>("GetTeacherByWhere", new { CardNo = "", Phone = "", System_Station_ID = model.System_Station_ID, TeacherDetail_ID = model.TeacherDetail_ID });
            if (teacherModel == null)
            {
                throw new ApiException("老师不存在，TeacherDetail_ID是否正确！");
            }

            //如果身份证有变更
            if ((!string.IsNullOrEmpty(model.CardNo)) && model.CardNo != teacherModel.CardNo)
            {
                //判断身份证是否已使用
                T_TeacherInfo teacher = SqlMapper.QueryForObject<T_TeacherInfo>("GetTeacherByCardOrPhone", new { CardNo = model.CardNo,Phone = "" });
                if (teacher != null)
                {
                    throw new ApiException("身份证已存在，姓名为：" + teacher.Name);
                }
            }

            //如果手机号有变更
            if (model.Phone != teacherModel.Phone)
            {
                //判断身份证是否已使用
                T_TeacherInfo teacher = SqlMapper.QueryForObject<T_TeacherInfo>("GetTeacherByCardOrPhone", new { CardNo = "", Phone = model.Phone });
                if (teacher != null)
                {
                    throw new ApiException("手机号已存在，姓名为：" + teacher.Name);
                }
            }

            bool update = false;

            try
            {
                SqlMapper.BeginTransaction();//开启事务

                int updateTeacher = SqlMapper.Update("TeacherUpdate", new
                {
                    Name = model.Name,
                    Sex = model.Sex,
                    CardNo = model.CardNo,
                    JobTitle = model.JobTitle,
                    HeadImage = model.HeadImage,
                    Phone = model.Phone,
                    Email = model.Email,
                    Birthday = model.Birthday,
                    Address = model.Address,
                    Education_ID = model.Education_ID,
                    GraduateSchool = model.GraduateSchool,
                    Introduction = model.Introduction,
                    ID = model.ID,
                    System_Station_ID = model.System_Station_ID
                });

                if (updateTeacher > 0)
                {
                    //删除老师学科关系
                    SqlMapper.Delete("DeleteTeacherDiscipline", new { TeacherDetail_ID = model.TeacherDetail_ID, System_Station_ID = model.System_Station_ID });

                    //添加老师学科关系
                    if (model.Teacher_DisciplineIds != null && model.Teacher_DisciplineIds.Length > 0)
                    {
                        string[] dics = model.Teacher_DisciplineIds.Split(',');
                        if (dics.Length > 0)
                        {
                            T_TeacherInfo_Detail_Discipline teacherD_Dis = null;
                            foreach (string disciplineId in dics)
                            {
                                if (string.IsNullOrEmpty(disciplineId))
                                    continue;

                                teacherD_Dis = new T_TeacherInfo_Detail_Discipline();
                                teacherD_Dis.TeacherDetail_ID = model.TeacherDetail_ID;
                                teacherD_Dis.System_Station_ID = model.System_Station_ID;
                                teacherD_Dis.Discipline_ID = int.Parse(disciplineId);
                                teacherD_Dis.AddTime = model.AddTime;
                                teacherD_Dis.AddPerson = model.AddPerson;

                                //添加老师所教学科
                                Orm.Insert(teacherD_Dis);
                            }
                        }
                    }

                    update = true;
                }
                else
                {
                    throw new ApiException("修改老师信息失败！");
                }

                SqlMapper.CommitTransaction();
            }
            catch (Exception ex)
            {
                SqlMapper.RollBackTransaction();
                throw new ApiException(ex.Message);
            }
            finally
            {
                //SqlMapper.CloseConnection();
            }

            return update;
        }

        /// <summary>
        /// 根据详细ID 查询老师
        /// </summary>
        /// <returns></returns>
        public TeacherModel GetTeacherByID(int TeacherDetail_ID, int System_Station_ID)
        {
            if (System_Station_ID == 0)
            {
                throw new ApiException("机构ID不存在！");
            }

            if (TeacherDetail_ID == 0)
            {
                throw new ApiException("参数TeacherDetail_ID为0！");
            }

            TeacherModel model = SqlMapper.QueryForObject<TeacherModel>("GetTeacherByWhere", new { CardNo = "", Phone = "", System_Station_ID = System_Station_ID, TeacherDetail_ID = TeacherDetail_ID });
            if (model == null)
            {
                throw new ApiException("未找到该老师，参数TeacherDetail_ID是否正确！");
            }

            //老师所教授的学科
            model.TeacherDisciplines = GetTeacherDetailDiscipline(TeacherDetail_ID, System_Station_ID);

            return model;
        }

        public List<T_TeacherInfo_Detail_DisciplineModel> GetTeacherDetailDiscipline(int TeacherDetail_ID, int System_Station_ID)
        {
            List<T_TeacherInfo_Detail_DisciplineModel> list = SqlMapper.QueryForList<T_TeacherInfo_Detail_DisciplineModel>("GetTeacherDetailDiscipline", new { TeacherDetail_ID = TeacherDetail_ID, System_Station_ID = System_Station_ID }).ToList();
            if (list != null && list.Count > 0)
            {
                list.ForEach(T =>
                {
                    //获取一级 二级 三级学科ID
                    DisciplineIDModel DisciplineModel = new DisciplineMapper().GetDisciplineIDS(T.Discipline_ID);
                    if (DisciplineModel != null)
                    {
                        T.Discipline_OneLevelID = DisciplineModel.Discipline_OneLevelID;//一级学科ID
                        T.Discipline_TwoLevelID = DisciplineModel.Discipline_TwoLevelID;//二级学科ID
                        T.Discipline_ThreeLevelID = DisciplineModel.Discipline_ThreeLevelID;//三级学科ID
                    }

                });
            }
            return list;
        }

        /// <summary>
        /// 删除老师
        /// </summary>
        /// <param name="System_Station_ID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool RemoveTeacher(TeacherModel model)
        {
            if (model.System_Station_ID == 0)
            {
                throw new ApiException("机构ID不存在！");
            }

            if (model.TeacherDetail_ID == 0)
            {
                throw new ApiException("参数TeacherDetail_ID为0！");
            }

            //根据老师详细ID查询老师
            TeacherModel teacherModel = SqlMapper.QueryForObject<TeacherModel>("GetTeacherByWhere", new { CardNo = "", Phone = "", System_Station_ID = model.System_Station_ID, TeacherDetail_ID = model.TeacherDetail_ID });
            if (teacherModel == null)
            {
                throw new ApiException("老师不存在，TeacherDetail_ID是否正确！");
            }

            //判断网课是否启用了老师，启用了禁止删除
            int count = SqlMapper.QueryForObject<int>("ExistCourseByTeacherID", new { TeacherDetail_ID = model.TeacherDetail_ID, System_Station_ID = model.System_Station_ID });
            if (count > 0)
            {
                throw new ApiException("当前老师被网课应用,无法删除！");
            }

            if (SqlMapper.Delete("DeleteTeacher", new { System_Station_ID = model.System_Station_ID, TeacherDetail_ID = model.TeacherDetail_ID }) > 0)
            {
                //删除老师所教学科
                SqlMapper.Delete("DeleteTeacherDiscipline", new { TeacherDetail_ID = model.TeacherDetail_ID, System_Station_ID = model.System_Station_ID });

                return true;
            }

            return false;
        }

        /// <summary>
        /// 老师状态：启用/禁用
        /// </summary>
        /// <param name="System_Station_ID"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool SetTeacherValid(TeacherModel model)
        {
            if (model.System_Station_ID == 0)
            {
                throw new ApiException("机构ID不存在！");
            }

            if (model.TeacherDetail_ID == 0)
            {
                throw new ApiException("参数TeacherDetail_ID为0！");
            }

            //根据老师详细ID查询老师
            TeacherModel teacherModel = SqlMapper.QueryForObject<TeacherModel>("GetTeacherByWhere", new { CardNo = "", Phone = "", System_Station_ID = model.System_Station_ID, TeacherDetail_ID = model.TeacherDetail_ID });
            if (teacherModel == null)
            {
                throw new ApiException("老师不存在，TeacherDetail_ID是否正确！");
            }

            if (SqlMapper.Update("TeacherValid", new { Valid = model.Valid, System_Station_ID = model.System_Station_ID, TeacherDetail_ID = model.TeacherDetail_ID }) > 0)
                return true;

            return false;
        }

        /// <summary>
        /// 获取机构所有老师列表
        /// </summary>
        /// <returns></returns>
        public IList GetAllTeachers(PageModel model, string Name, int? Valid, int Discipline_ID, int System_Station_ID)
        {
            if (System_Station_ID == 0)
            {
                throw new ApiException("机构ID不存在！");
            }

            IList list = SqlMapper.QueryForList("GetAllTeachers", new
            {
                System_Station_ID = System_Station_ID,
                Name = Name,
                Valid = Valid,
                Discipline_ID = Discipline_ID,
                pageIndex = (model.PageIndex - 1) * model.PageSize,
                pageSize = model.PageSize,
                pageStatus = model.PageStatus
            });

            return list;
        }

        /// <summary>
        /// 机构老师总数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public int GetAllTeachers_TotalCount(PageModel model, string Name, int? Valid, int Discipline_ID, int System_Station_ID)
        {
            return SqlMapper.QueryForObject<int>("GetAllTeachers_TotalCount", new
            {
                System_Station_ID = System_Station_ID,
                Name = Name,
                Valid = Valid,
                Discipline_ID = Discipline_ID,
                pageStatus = 0
            });
        }
        #endregion
    }
}
