using CommonFramework.Exceptions;
using CommonFramework.IBatisNet;
using DataStoreProject.Model.Entity.Question;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStoreProject.Model.ViewModel;
using DataStoreProject.Model.Entity.ExamPaper;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using NPOI.XSSF.UserModel;

namespace DataStoreProject.Business
{
    public class QuestionMapper : BaseMapper
    {
        /// <summary>
        /// 获取题型列表
        /// </summary>
        /// <returns></returns>
        public IList GetQuestionTypeList()
        {
            return QueryForList("GetQuestionTypeList", null);
        }

        /// <summary>
        /// 添加题库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddQuestionStore(W_QuestionStore model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ApiException("题库名称不能为空");
            }
            if (model.Discipline_ID == 0)
            {
                throw new ApiException("请选择科次");
            }
            return Orm.Insert(model, true) > 0;
        }

        /// <summary>
        /// 获取题库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public W_QuestionStore GetQuestionStore(int id)
        {
            W_QuestionStore model = Orm.Single<W_QuestionStore>(x => x.ID == id && x.IsDelete == 0);
            if (model == null)
                throw new ApiException("题库不存在或者已删除");
            return model;
        }

        /// <summary>
        /// 禁用题库信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DisableQuestionStore(W_QuestionStore model)
        {
            W_Question questionModel = Orm.Single<W_Question>(x => x.QuestionStore_ID == model.ID);
            if (questionModel != null)
                throw new ApiException("题库里面存在题目,不能禁用");

            W_ExamPaper examModel = Orm.Single<W_ExamPaper>(x => x.QuestionStore_ID == model.ID);
            if (examModel != null)
                throw new ApiException("题库里面存在试卷,不能禁用");
            return Orm.Update(model) > 0;
        }

        /// <summary>
        /// 修改题库信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateQuestionStore(W_QuestionStore model)
        {
            return Orm.Update(model) > 0;
        }

        /// <summary>
        /// 获取题库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetQuestionStoreById(int id)
        {
            return QueryForList("GetQuestionStoreById", new { id });
        }

        /// <summary>
        /// 获取题库列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="valid"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public IList GetQuestionStoreList(QuestionStoreRequest request, int stationId)
        {
            return QueryForList("GetQuestionStoreList", new { name = request.Name, valid = request.Valid, disciplineId = request.DisciplineId, stationId, pageIndex = (request.PageIndex - 1) * request.PageSize, pageSize = request.PageSize, pageStatus = request.PageStatus });
        }

        /// <summary>
        /// 获取题库列表总条数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public int GetTotalCount(QuestionStoreRequest request, int stationId)
        {
            return SqlMapper.QueryForObject<int>("GetQuestionStoreCount", new { name = request.Name, valid = request.Valid, disciplineId = request.DisciplineId, stationId, pageStatus = 0 });
        }


        /// <summary>
        /// 添加题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddQuestion(W_Question model)
        {
            if (string.IsNullOrEmpty(model.Title))
                throw new ApiException("题目不能为空");
            W_QuestionStore store = Orm.Single<W_QuestionStore>(x => x.ID == model.QuestionStore_ID);
            if (store.Valid == 0)
                throw new ApiException("选择的题库已被禁用，请重试");
            if (string.IsNullOrEmpty(model.Body))
            {
                model.Body = "";
            }
            return Orm.Insert(model, true) > 0;
        }

        /// <summary>
        /// 添加组合题材料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddQuestionData(W_QuestionData model)
        {
            return (int)Orm.Insert(model, true);
        }

        /// <summary>
        /// 修改题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateQuestion(W_Question model)
        {
            W_Question oldModel = Orm.Single<W_Question>(x => x.ID == model.ID);
            if (oldModel == null)
                throw new ApiException("题目不存在或者已删除");
            if (string.IsNullOrEmpty(model.Title))
                throw new ApiException("题目不能为空");
            if (model.QuestionType_ID != oldModel.QuestionType_ID)
            {
                if (SqlMapper.QueryForObject<int>("QuestionHasBeenUsed", new { id = model.ID }) > 0)
                    throw new ApiException("该题目已被使用，不能修改题型");
            }
            model.AddPerson = oldModel.AddPerson;
            model.AddTime = oldModel.AddTime;
            model.CorrectCount = oldModel.CorrectCount;
            model.DoCount = oldModel.DoCount;
            model.EasyWrongAnswer = oldModel.EasyWrongAnswer;
            model.System_Station_ID = oldModel.System_Station_ID;
            return Orm.Update(model) > 0;
        }

        /// <summary>
        /// 修改组合题材料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public bool UpdateQuestionData(int id, string content, int storeId)
        {
            W_QuestionData model = Orm.Single<W_QuestionData>(x => x.ID == id);
            if (model == null)
                throw new ApiException("题目不存在或者已删除");
            model.Content = content;

            List<W_Question> list = Orm.Select<W_Question>(x => x.QuestionData_ID == id).ToList();
            SqlMapper.BeginTransaction();
            try
            {
                if (Orm.Update(model) > 0)
                {
                    foreach (W_Question question in list)
                    {
                        if (question.QuestionStore_ID != storeId)
                        {
                            question.QuestionStore_ID = storeId;
                            Orm.Update<W_Question>(question);
                        }
                    }
                }
                SqlMapper.CommitTransaction();
            }
            catch
            {
                SqlMapper.RollBackTransaction();
                return false;
            }
            return true;

        }


        /// <summary>
        /// 删除题目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteQuestion(int id, int stationId)
        {
            W_Question model = Orm.Single<W_Question>(x => x.ID == id);
            if (model == null)
                throw new ApiException("该题目不存在或者已删除");
            if (model.System_Station_ID != stationId)
                throw new ApiException("该题目不属于当前站点，不能删除");
            if (SqlMapper.QueryForObject<int>("QuestionHasBeenUsed", new { id }) > 0)
                throw new ApiException("该题目已被使用，不能删除");
            return SqlMapper.Delete("DeleteQuestion", new { id }) > 0;
        }

        /// <summary>
        /// 删除组合题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteQuestionData(int id)
        {
            return SqlMapper.Delete("DeleteQuestionData", new { id }) > 0;
        }

        /// <summary>
        /// 获取题目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetQuestion(int id)
        {
            return QueryForList("GetQuestion", new { id });
        }

        /// <summary>
        /// 获取题目列表信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="store"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public IList GetQuestionList(QuestionRequest request, int stationId)
        {
            return QueryForList("GetQuestionList", new { title = request.Title, type = request.Type, store = request.Store, level = request.Level, stationId, pageIndex = (request.PageIndex - 1) * request.PageSize, pageSize = request.PageSize, pageStatus = request.PageStatus });
        }

        /// <summary>
        /// 获取题目列表总条数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public int GetQuestionTotalCount(QuestionRequest request, int stationId)
        {
            return SqlMapper.QueryForObject<int>("GetQuestionTotalCount", new { title = request.Title, type = request.Type, store = request.Store, level = request.Level, stationId, pageStatus = 0 });
        }

        /// <summary>
        /// 获取组合题信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetQuestionData(int id)
        {
            return QueryForList("GetQuestionData", new { id });
        }

        /// <summary>
        /// 根据组合题ID获取组合题目列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetQuestionDataById(int id)
        {
            return QueryForList("GetQuestionDataById", new { id });
        }



        /// <summary>
        /// 添加纠错
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddQuestionerrorCorrection(W_QuestionerrorCorrection model)
        {
            return Orm.Insert(model) > 0;
        }

        /// <summary>
        /// 批量处理纠错(根据题目id)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateQuestionerrorCorrection(CampusModel model)
        {
            return SqlMapper.Update("UpdateQuestionerrorCorrection", new { ID = model.ID }) > 0;
        }

        /// <summary>
        /// 根据主键ID处理纠错
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateQuestionerrorCorrectionByID(CampusModel model)
        {
            W_QuestionerrorCorrection coModel = Orm.Single<W_QuestionerrorCorrection>(x => x.ID == model.ID);
            if (coModel == null)
            {
                throw new ApiException("您要操作的数据不存在");
            }
            coModel.Valid = 1;
            return Orm.Update(coModel) > 0;
        }

        /// <summary>
        /// 删除纠错
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteQuestionerrorCorrection(W_QuestionerrorCorrection model)
        {
            W_QuestionerrorCorrection CorrectionModel = Orm.Single<W_QuestionerrorCorrection>(x => x.ID == model.ID);
            if (CorrectionModel == null)
            {
                throw new ApiException("您要操作的数据不存在");
            }

            return Orm.Delete<W_QuestionerrorCorrection>(x => x.ID == model.ID) > 0;
        }


        /// <summary>
        /// 查询纠错列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="valid"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public IList GetQuestionerrorCorrectionList(CampusModel request, int System_Station_ID)
        {
            return QueryForList("GetQuestionerrorCorrectionList", new { Title = request.Title, Valid = request.Valid, System_Station_ID = System_Station_ID, pageIndex = (request.PageIndex - 1) * request.PageSize, pageSize = request.PageSize, pageStatus = request.PageStatus });
        }

        /// <summary>
        /// 查询纠错列表总条数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="stationId"></param>
        /// <returns></returns>
        public int GetQuestionerrorCorrectionListCount(CampusModel request, int System_Station_ID)
        {
            return SqlMapper.QueryForObject<int>("GetQuestionerrorCorrectionListCount", new { Title = request.Title, System_Station_ID = System_Station_ID, Valid = request.Valid, pageStatus = 0 });
        }

        /// <summary>
        /// 查询纠错汇总列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="valid"></param>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        public IList GetErrorcorrectionsummaryList(CampusModel model)
        {
            return QueryForList("GetErrorcorrectionsummaryList", new { ID = model.ID });
        }


        public dynamic GetMyQuestionStoreList(string StuId, int System_Station_ID)
        {
            return QueryForList("GetMyQuestionStoreList", new { StuId, System_Station_ID });
        }

        /// <summary>
        /// Excel转Question
        /// </summary>
        /// <param name="intputStream"></param>
        /// <param name="FileName"></param>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public dynamic ExcelToQuestionList(System.IO.Stream intputStream, string FileName, int System_Station_ID)
        {
            IWorkbook workbook = null;
            try
            {
                if (FileName.EndsWith(".xlsx")) // 2007版本
                    workbook = new XSSFWorkbook(intputStream);
                else if (FileName.EndsWith(".xls")) // 2003版本
                    workbook = new HSSFWorkbook(intputStream);
            }
            catch (Exception ex)
            {
                throw new ApiException("上传异常，异常原因：" + ex.Message + "<br/>请尝试参考导入模板格式进行导入。");
            }
            ISheet sheet = workbook.GetSheetAt(0);
            List<Question> list = new List<Question>();
            Question question = null;
            List<W_QuestionStore> storeList = Orm.Select<W_QuestionStore>(x => x.System_Station_ID == System_Station_ID);//题库
            /*Excel模板从第四行开始
             * 第一列为题库
             * 第二列为试题类型
             * 第三列为试题难度
             * 第四列为试题内容
             * 第五列为试题答案
             * 第六至第十三列为选择题选项
             * 第十四列为答案解析
             */
            for (int i = 4; i <= sheet.LastRowNum; i++)
            {
                var row = sheet.GetRow(i);
                if (row == null)
                {
                    continue;
                };
                List<W_QuestionStore> sList = storeList.Where(x => x.Name == GetCellValue(row.GetCell(0)) && x.IsDelete == 0).ToList();
                if (sList.Count > 0)//可能存在多个题库
                {
                    try
                    {
                        foreach (var item in sList)
                        {
                            question = new Question();
                            if (sList.Count > 1)
                                question.MyAnswer = "MoreStore";
                            else
                                question.MyAnswer = "Normal";
                            question.QuestionStore_ID = item.ID;
                            question.QuestionStore_Name = item.Name;
                            question.QuestionType_ID = int.Parse(GetCellValue(row.GetCell(1)));
                            if (question.QuestionType_ID > 6 || question.QuestionType_ID < 1)
                                throw new Exception();
                            question.Level = int.Parse(GetCellValue(row.GetCell(2)));
                            question.Title = GetCellValue(row.GetCell(3));
                            question.Answer = GetCellValue(row.GetCell(4));
                            question.Mark = GetCellValue(row.GetCell(13));
                            var dictionary = new Dictionary<string, object>();
                            if (question.QuestionType_ID <= 2)
                            {
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(5))))
                                {
                                    dictionary.Add("A", GetCellValue(row.GetCell(5)));
                                    question.AnswerCount++;
                                }
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(6))))
                                {
                                    dictionary.Add("B", GetCellValue(row.GetCell(6)));
                                    question.AnswerCount++;
                                }
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(7))))
                                {
                                    dictionary.Add("C", GetCellValue(row.GetCell(7)));
                                    question.AnswerCount++;
                                }
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(8))))
                                {
                                    dictionary.Add("D", GetCellValue(row.GetCell(8)));
                                    question.AnswerCount++;
                                }
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(9))))
                                {
                                    dictionary.Add("E", GetCellValue(row.GetCell(9)));
                                    question.AnswerCount++;
                                }
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(10))))
                                {
                                    dictionary.Add("F", GetCellValue(row.GetCell(10)));
                                    question.AnswerCount++;
                                }
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(11))))
                                {
                                    dictionary.Add("G", GetCellValue(row.GetCell(11)));
                                    question.AnswerCount++;
                                }
                                if (!string.IsNullOrEmpty(GetCellValue(row.GetCell(12))))
                                {
                                    dictionary.Add("H", GetCellValue(row.GetCell(12)));
                                    question.AnswerCount++;
                                }
                                question.Body = new JavaScriptSerializer().Serialize(dictionary);
                            }
                            list.Add(question);
                        }
                    }
                    catch
                    {
                        question = new Question();
                        question.MyAnswer = "TextError";
                        list.Add(question);
                        continue;
                    }
                }
                else
                {
                    question = new Question();
                    question.MyAnswer = "NoStore";
                    list.Add(question);
                }
            }
            return list;
        }

        public string GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return "";
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();//This is a trick to get the correct value of the cell. NumericCellValue will return a numeric value no matter the cell value is a date or a number
                case CellType.String:
                    return cell.StringCellValue.Replace("'", "");
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);
                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }

        /// <summary>
        /// 导入题目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public dynamic ImportQuestion(Question model, int System_Station_ID, int accountID)
        {
            string Text = "导入成功";
            bool flg = false;
            if (Orm.Single<W_Question>(x => x.Title == model.Title && x.QuestionStore_ID == model.QuestionStore_ID && x.QuestionType_ID == model.QuestionType_ID && x.System_Station_ID == System_Station_ID) != null)
                Text = "题目已存在";
            else
            {
                W_Question question = new W_Question();
                question.AddPerson = accountID;
                question.AddTime = DateTime.Now;
                question.Answer = model.Answer;
                question.AnswerCount = model.AnswerCount;
                question.Body = model.Body == null ? "" : model.Body;
                question.Level = model.Level;
                question.Mark = model.Mark;
                question.QuestionStore_ID = model.QuestionStore_ID;
                question.QuestionType_ID = model.QuestionType_ID;
                question.Title = model.Title;
                question.System_Station_ID = System_Station_ID;
                flg = Orm.Insert<W_Question>(question) > 0;
                if (!flg)
                    Text = "导入失败";
            }
            return new { Text = Text, Result = flg };
        }
    }
}
