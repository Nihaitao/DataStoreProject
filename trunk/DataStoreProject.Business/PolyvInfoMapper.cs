﻿using CommonFramework.Exceptions;
using CommonFramework.IBatisNet;
using DataStoreProject.Model.Entity.PolyvInfo;
using DataStoreProject.Model.Entity.Station;
using DataStoreProject.Model.ViewModel;
using Entites.PolyvInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace DataStoreProject.Business
{
    public class PolyvInfoMapper : BaseMapper
    {
        /// <summary>
        /// 读取视频配置数据 hx add 20171205
        /// </summary>
        /// <param name="System_Station_ID"></param>
        /// <returns></returns>
        public PolyvInfoModel GetPolyvSetByWhere(int System_Station_ID)
        {
            PolyvInfoModel model = SqlMapper.QueryForObject<PolyvInfoModel>("GetPolyvSetByWhere", new { System_Station_ID = System_Station_ID });
            if (model == null)
            {
                throw new ApiException("资源上传目录未设置，请先设置！");
            }
            else
            {
                if (model.Valid == 0)
                {
                    throw new ApiException("资源上传设置未开启，请先开启！");
                }
            }
            return model;
        }

        /// <summary>
        /// 根据条件查询主表
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public W_PolyvInfo GetPolyvInfoByWhere(int System_Station_ID)
        {
            return SqlMapper.QueryForObject<W_PolyvInfo>("GetPolyvInfoByWhere", new { System_Station_ID = System_Station_ID });
        }



        public string doGet(string Url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ce)
            {
                throw ce;
            }
        }


        private long GenerateTimeStamp()
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            var t = (DateTime.Now.Ticks - startTime.Ticks) / 10000; //除10000调整为13位        
            return t;
        }

        private string EncryptToSHA1(string str)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] dataToHash = Encoding.UTF8.GetBytes(str);
                byte[] dataHashed = sha1.ComputeHash(dataToHash);
                return BitConverter.ToString(dataHashed).Replace("-", "");
            }
            ;
        }

        public string EncryptToMD5(string str)
        {
            using (var md5 = MD5.Create())
            {
                byte[] dataToHash = Encoding.UTF8.GetBytes(str);
                byte[] dataHashed = md5.ComputeHash(dataToHash);
                return BitConverter.ToString(dataHashed).Replace("-", "");
            }
        }

        /// <summary>
        /// 保利威视配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool SavePolyvInfo(PolyvInfoModel model)
        {
            if (model.System_Station_ID == 0)
            {
                throw new ApiException("机构ID不存在！");
            }

            //自主类型  参数判断
            if (model.PolyvSource == 1)
            {
                if (string.IsNullOrEmpty(model.userid))
                {
                    throw new ApiException("userid不能为空！");
                }

                if (string.IsNullOrEmpty(model.writetoken))
                {
                    throw new ApiException("writetoken不能为空！");
                }

                if (string.IsNullOrEmpty(model.readtoken))
                {
                    throw new ApiException("readtoken不能为空！");
                }

                if (string.IsNullOrEmpty(model.secretkey))
                {
                    throw new ApiException("secretkey不能为空！");
                }

                string refMessage = "";
                //测试接口是否可用、参数是否正确
                if (!CheckParameter(model.userid, model.writetoken, model.readtoken, model.secretkey, ref refMessage))
                {
                    throw new ApiException(refMessage);
                }
            }

            H_Station station = Orm.Single<H_Station>(x => x.ID == model.System_Station_ID);
            string StationName = station != null ? station.Name : "";
            if (model.PolyvSource == 1)
                model.Name = StationName; //自主的就读机构的名称
            string Message = string.Empty;

            //是否第一次设置
            W_PolyvInfo_Detail polyvDetail = Orm.Single<W_PolyvInfo_Detail>(x => x.System_Station_ID == model.System_Station_ID);
            if (polyvDetail == null) //第一次设置
            {
                if (model.PolyvSource == 0) //系统默认
                {
                    #region
                    W_PolyvInfo Model = Orm.Single<W_PolyvInfo>(x => x.System_Station_ID == 0); //读主表默认
                    if (model == null)
                        throw new ApiException("未找到对应的默认配置数据！");

                    //调用接口创建目录
                    string cataid = CreateDirectory(Model, StationName, out Message);
                    //创建目录失败
                    if (string.IsNullOrEmpty(cataid))
                    {
                        throw new ApiException("创建目录失败！" + Message);
                    }
                    else
                    {
                        //添加详细
                        return SavePolyvDetail(true, model.System_Station_ID, Model.ID, cataid, model.Valid, model.PolyvSource);
                    }
                    #endregion
                }
                else if (model.PolyvSource == 1) //自主类型
                {
                    #region
                    W_PolyvInfo Model = Orm.Single<W_PolyvInfo>(x => x.System_Station_ID == model.System_Station_ID); //读主表配置
                    if (Model == null) //主表为空
                    {
                        W_PolyvInfo polyv = new W_PolyvInfo();
                        polyv.System_Station_ID = model.System_Station_ID;
                        polyv.Name = model.Name;
                        polyv.userid = model.userid;
                        polyv.writetoken = model.writetoken;
                        polyv.readtoken = model.readtoken;
                        polyv.secretkey = model.secretkey;
                        polyv.AddTime = DateTime.Now;
                        int polyv_id = (int)Orm.Insert(polyv, true);
                        if (polyv_id > 0)
                        {
                            //调用接口创建目录
                            string cataid = CreateDirectory(polyv, StationName, out Message);

                            //创建目录失败
                            if (string.IsNullOrEmpty(cataid))
                            {
                                throw new ApiException("创建目录失败！" + Message);
                            }
                            else
                            {
                                //添加详细
                                return SavePolyvDetail(true, polyv.System_Station_ID, polyv_id, cataid, model.Valid, model.PolyvSource);
                            }
                        }
                        else
                        {
                            throw new ApiException("设置不成功，设置主表添加异常！");
                        }
                    }
                    else
                    {
                        //调用接口创建目录
                        string cataid = CreateDirectory(model, StationName, out Message);

                        //创建目录失败
                        if (string.IsNullOrEmpty(cataid))
                        {
                            throw new ApiException("创建目录失败！" + Message);
                        }
                        else
                        {
                            //直接修改明细
                            return SavePolyvDetail(true, model.System_Station_ID, Model.ID, cataid, model.Valid, model.PolyvSource);
                        }
                    }
                    #endregion
                }
                else
                {
                    throw new ApiException("设置不成功，账号类型不正确！");
                }
            }
            else //不是第一次设置
            {
                if (model.PolyvSource == 0) //系统默认
                {
                    #region
                    W_PolyvInfo Model = Orm.Single<W_PolyvInfo>(x => x.System_Station_ID == 0); //读主表默认
                    if (model == null)
                        throw new ApiException("未找到对应的默认配置数据！");

                    //读详细表
                    W_PolyvInfo_Detail D_Model = Orm.Single<W_PolyvInfo_Detail>(x => x.System_Station_ID == model.System_Station_ID);
                    if (D_Model != null)
                    {
                        //不创建目录 直接修改详细
                        return SavePolyvDetail(false, D_Model.System_Station_ID, Model.ID, D_Model.cataid, model.Valid, model.PolyvSource);
                    }
                    else
                    {
                        throw new ApiException("未找到对应的明细配置数据！");
                    }
                    #endregion
                }
                else if (model.PolyvSource == 1) //自主类型
                {
                    #region
                    W_PolyvInfo Model = Orm.Single<W_PolyvInfo>(x => x.System_Station_ID == model.System_Station_ID); //读主表配置
                    if (Model == null) //主表为空
                    {
                        W_PolyvInfo polyv = new W_PolyvInfo();
                        polyv.System_Station_ID = model.System_Station_ID;
                        polyv.Name = model.Name;
                        polyv.userid = model.userid;
                        polyv.writetoken = model.writetoken;
                        polyv.readtoken = model.readtoken;
                        polyv.secretkey = model.secretkey;
                        polyv.AddTime = DateTime.Now;
                        int polyv_id = (int)Orm.Insert(polyv, true);

                        if (polyv_id > 0)
                        {
                            //调用接口创建目录
                            string cataid = CreateDirectory(polyv, StationName, out Message);

                            //创建目录失败
                            if (string.IsNullOrEmpty(cataid))
                            {
                                throw new ApiException("创建目录失败！" + Message);
                            }
                            else
                            {
                                //修改详细
                                return SavePolyvDetail(false, polyv.System_Station_ID, polyv_id, cataid, model.Valid, model.PolyvSource);
                            }
                        }
                        else
                        {
                            throw new ApiException("设置不成功，设置主表添加异常！");
                        }
                    }
                    else //主表不为空，修改
                    {
                        //数据是否修改
                        bool HasChange = false;
                        if (model.userid != Model.userid)
                            HasChange = true;
                        if (model.writetoken != Model.writetoken)
                            HasChange = true;
                        if (model.readtoken != Model.readtoken)
                            HasChange = true;
                        if (model.secretkey != Model.secretkey)
                            HasChange = true;

                        W_PolyvInfo_Detail D_Model = Orm.Single<W_PolyvInfo_Detail>(x => x.System_Station_ID == model.System_Station_ID);//读详细表

                        if (HasChange) //如果数据有变更
                        {
                            //调用接口创建目录
                            string cataid = CreateDirectory(model, StationName, out Message);

                            //创建目录失败
                            if (string.IsNullOrEmpty(cataid))
                            {
                                throw new ApiException("创建目录失败！" + Message);
                            }
                            else
                            {
                                //修改主表
                                if (SqlMapper.Update("UpdatePolyvInfo", new { Name = model.Name, userid = model.userid, writetoken = model.writetoken, readtoken = model.readtoken, secretkey = model.secretkey, System_Station_ID = model.System_Station_ID, ID = Model.ID }) > 0)
                                {
                                    //修改详细
                                    return SavePolyvDetail(false, D_Model.System_Station_ID, Model.ID, cataid, model.Valid, model.PolyvSource);
                                }
                                else
                                {
                                    throw new ApiException("主表修改失败！" + Message);
                                }
                            }
                        }
                        else
                        {
                            //直接修改明细
                            return SavePolyvDetail(false, D_Model.System_Station_ID, Model.ID, D_Model.cataid, model.Valid, model.PolyvSource);
                        }
                    }
                    #endregion
                }
                else
                {
                    throw new ApiException("设置不成功，账号类型不正确！");
                }
            }
        }

        /// <summary>
        /// 检查接口参数是否可用 hx add 20171206
        /// </summary>
        /// <param name="userid">POLYV用户ID</param>
        /// <param name="writetoken">用户的写密钥</param>
        /// <param name="readtoken">用户的读密钥</param>
        /// <param name="secretkey"></param>
        /// <returns></returns>
        public bool CheckParameter(string userid, string writetoken, string readtoken, string secretkey, ref string refMessage)
        {
            //①检查 userid、secretkey是否正确
            string url = string.Format("http://api.polyv.net/v2/data/{0}/viewlog", userid);
            string ts = ConvertDateTimeToInt(DateTime.Now).ToString().Substring(0, 13);
            string str = string.Format("day=20160711&ptime={0}&userid={1}{2}", ts, userid, secretkey);
            string Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "sha1").ToUpper();
            string postData = string.Format("day=20160711&ptime={0}&sign={1}", ts, Sign);
            string result = ResponseToString(doPost(url, postData));
            JavaScriptSerializer jss = new JavaScriptSerializer();
            JsonModel2 Json = jss.Deserialize<JsonModel2>(result);
            if (Json.code != 200)
            {
                refMessage = ReturnMessage(Json.message);
                return false;
            }

            return true;
        }

        public string ReturnMessage(string message)
        {
            if (message.IndexOf("userid") > 0)
            {
                return "设置不成功，" + message + "<br /> userid 不正确，请检查 userid 值!";
            }
            else if (message.IndexOf("sign") > 0)
            {
                return "设置不成功，" + message + "<br /> sign 不正确，请检查 secretkey 值 !";
            }
            else
            {
                return "设置不成功，未知错误!";
            }

            //②检查 writetoken 是否正确

            //③检查 readtoken 是否正确
        }

        // <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        public string ResponseToString(WebResponse response)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                string ContentType = response.ContentType.Trim();
                if (ContentType.IndexOf("utf-8") != -1)
                    encoding = Encoding.UTF8;
                else if (ContentType.IndexOf("utf-7") != -1)
                    encoding = Encoding.UTF7;
                else if (ContentType.IndexOf("unicode") != -1)
                    encoding = Encoding.Unicode;
                else if (ContentType.IndexOf("text/plain") != -1)
                    encoding = Encoding.UTF8;
                StreamReader stream = new StreamReader(response.GetResponseStream(), encoding);
                string code = stream.ReadToEnd();
                stream.Close();
                response.Close();
                return code;
            }
            catch (Exception ce)
            {
                throw ce;
            }
        }

        /// <summary>
        /// post 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public WebResponse doPost(string url, string postData)
        {
            try
            {
                Thread.Sleep(1000);
                byte[] paramByte = Encoding.UTF8.GetBytes(postData); // 转化
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Referer = "";
                webRequest.Accept = "application/x-shockwave-flash, image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-silverlight, */*";
                webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";
                webRequest.ContentLength = paramByte.Length;
                webRequest.CookieContainer = new CookieContainer();
                //webRequest.Timeout = 5000;
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(paramByte, 0, paramByte.Length);    //写入参数
                newStream.Close();
                return webRequest.GetResponse();
            }
            catch (Exception ce)
            {
                throw ce;
            }
        }

        /// <summary>
        /// 调用接口创建目录 返回空值表示创建失败
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="cataname"></param>
        /// <returns></returns>
        public string CreateDirectory(W_PolyvInfo Model, string cataname, out string message)
        {
            message = "";
            try
            {
                string url = string.Format("http://api.polyv.net/v2/video/{0}/addCata", Model.userid);
                string ts = ConvertDateTimeToInt(DateTime.Now).ToString().Substring(0, 13);
                string signn = string.Format("cataname={0}&catatype={3}&cataurl={4}&parentid=1&ptime={1}{2}", cataname, ts, Model.secretkey, "0", "cs");
                string Sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(signn, "sha1");
                string postData = string.Format("cataname={0}&catatype={3}&cataurl={4}&parentid=1&ptime={1}&sign={2}", cataname, ts, Sign, "0", "cs");
                string result = ResponseToString(doPost(url, postData));
                JavaScriptSerializer jss = new JavaScriptSerializer();
                JsonModel Json = jss.Deserialize<JsonModel>(result);
                if (Json.code == 200 && Json.data != null)
                    return Json.data.cataid;
                else
                {
                    message = Json.message;
                    return "";
                }

            }
            catch (Exception ex)
            {

                return "";
            }
        }

        /// <summary>
        /// 创建目录并保存 hx add 20180105
        /// </summary>
        /// <param name="Message">创建消息</param>
        /// <param name="PolyvInfo_ID">主表ID</param>
        /// <param name="cataid">分类cataid</param>
        /// <param name="Valid">禁用/启用</param>
        /// <param name="PolyvSource"></param>
        /// <param name="IsAdd">是添加还是修改</param>
        /// <returns></returns>
        public dynamic SavePolyvDetail(bool isAdd, int Station_ID, int PolyvInfo_ID, string cataid, int Valid, int PolyvSource)
        {
            bool save = false;

            W_PolyvInfo_Detail PolyvInfoModel_Detail = new W_PolyvInfo_Detail();
            //明细表添加数据
            PolyvInfoModel_Detail.System_Station_ID = Station_ID;
            PolyvInfoModel_Detail.PolyvInfo_ID = PolyvInfo_ID;
            PolyvInfoModel_Detail.cataid = cataid;
            PolyvInfoModel_Detail.Valid = Valid;
            try
            {
                SqlMapper.BeginTransaction();//开启事务

                if (isAdd)
                {
                    //添加明细数据
                    Orm.Insert(PolyvInfoModel_Detail);

                    //修改配置表账号类型
                    if (SqlMapper.Update("UpdatePolyvinfoType", new { PolyvinfoType = PolyvSource, System_Station_ID = Station_ID }) > 0)
                        save = true;
                }
                else
                {
                    //修改明细数据
                    if (SqlMapper.Update("UpdatePolyvInfoDetail", new { PolyvInfo_ID = PolyvInfo_ID, cataid = cataid, Valid = Valid, System_Station_ID = Station_ID }) > 0)
                    {
                        if (SqlMapper.Update("UpdatePolyvinfoType", new { PolyvinfoType = PolyvSource, System_Station_ID = Station_ID }) > 0)
                            save = true;
                    }
                    else
                    {
                        throw new ApiException("设置不成功，明细修改异常！");
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

            return save;
        }

        /// <summary>
        /// 查询当前机构配置信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public dynamic GetPolyvInfoModel(int System_Station_ID)
        {
            //查询当前机构是否使用自己的保利威视配置
            PolyvInfoModel Model = SqlMapper.QueryForObject<PolyvInfoModel>("GetPolyvInfo", new { System_Station_ID = System_Station_ID });
            W_PolyvInfo_Detail Model_Detail = SqlMapper.QueryForObject<W_PolyvInfo_Detail>("GetPolyvInfo_Detail", new { System_Station_ID = System_Station_ID });

            if (Model_Detail != null)
            {
                //是系统配置
                if (Model == null)
                {
                    Model = new PolyvInfoModel();
                    Model.PolyvSource = 1;//系统
                }
                else
                    Model.PolyvSource = 2;//自定义

                Model.PolyvInfo_ID = Model_Detail.PolyvInfo_ID;
                Model.Valid = Model_Detail.Valid;
                Model.cataid = Model_Detail.cataid == null ? "" : Model_Detail.cataid;
            }

            return Model;
        }

        /// <summary>
        /// 查询配置好的基础数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public dynamic GetPolyvInfoList()
        {
            return SqlMapper.QueryForList<W_PolyvInfo>("GetPolyvInfoList", null);
        }
    }
}
