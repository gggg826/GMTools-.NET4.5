using System;
using System.Collections.Generic;
using System.Web;
using GameToolsCommon;
using System.Text;
using System.Reflection;
using FengNiao.GameToolsCommon;

namespace GameToolsHttpService
{
    public class ActivityItemModule : ProcessRequestBase
    {
        public ActivityItemModule(string tableName)
            : base(tableName)
        {
        }
        public override string Update(HttpContext context)
        {
            string strModel = context.Request["Model"];
            //byte[] modelBytes = Encoding.UTF8.GetBytes(strModel);
            //strModel = Encoding.UTF8.GetString(modelBytes);
            string strOperationType = context.Request["OperationType"];
            string strGuid = context.Request["guid"];
            ResultModel requestResult = new ResultModel();
            if (!CheckUserSession(context))
            {
                requestResult.IsSuccess = false;
                requestResult.Content = "请登陆后操作";
                return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
            }
            List<OperateResultModel> resultModelList = new List<OperateResultModel>();
            try
            {
                //创建bll对象开始
                //Assembly asmb = typeof(FengNiao.GMTools.Database.BLL.tbl_server).Assembly;
                //string bllType = "FengNiao.GMTools.Database.BLL." + TableName;
                //string modelType = "FengNiao.GMTools.Database.Model." + TableName;
                //object bll = Activator.CreateInstance(asmb.GetType(bllType));
                //实例化bll对象结束

                //序列化开始
                //asmb = typeof(FengNiao.GMTools.Database.Model.tbl_server).Assembly;
                //Type serializeType = typeof(FengNiao.GameTools.Json.Serialize);
                //MethodInfo convertObjectToJsonList = serializeType.GetMethod("ConvertJsonToObjectList");
                //convertObjectToJsonList = convertObjectToJsonList.MakeGenericMethod(Type.GetType(modelType + "," + asmb.FullName));
                //object modelListObj = convertObjectToJsonList.Invoke(null, new object[] { strModel });

                List<OperationType> operationTypeList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<OperationType>(strOperationType);
                List<string> guidList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<string>(strGuid);
                List<FengNiao.GMTools.Database.Model.tbl_activity_details> modelList = FengNiao.GameTools.Json.Serialize.ConvertJsonToObjectList<FengNiao.GMTools.Database.Model.tbl_activity_details>(strModel);
                //序列化结束

                FengNiao.GMTools.Database.BLL.tbl_activity_details activityItemBLL = new FengNiao.GMTools.Database.BLL.tbl_activity_details();

                //获取长度
                //int modelLength = (int)modelListObj.GetType().GetProperty("Count").GetValue(modelListObj, null);
                //int guidLength = guidList.Count ;
                //int operationTypeLength = operationTypeList.Count;

                if (operationTypeList != null && guidList != null && modelList.Count == guidList.Count && modelList.Count == operationTypeList.Count)
                {
                    //for (int i = 0; i < modelLength; i++)
                    //{
                    //    PropertyInfo modelProperty = modelListObj.GetType().GetProperty("Item");
                    //    object modelObj = modelProperty.GetValue(modelListObj, new object[] { i });
                    //    if (!DataCheck.CheckObjectIsvalid(modelObj))
                    //    {
                    //        requestResult.Content = "非法操作";
                    //        return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
                    //    }
                    //}
                    for (int i = 0; i < modelList.Count; i++)
                    {
                        OperateResultModel result = new OperateResultModel();
                        result.IsSuccess = false;
                        result.Guid = guidList[i];
                        OperationType operationType = operationTypeList[i];
                        //PropertyInfo modelProperty = modelListObj.GetType().GetProperty("Item");
                        //object modelObj = modelProperty.GetValue(modelListObj, new object[] { i });
                        if (operationType == OperationType.Add)
                        {
                            try
                            {
                                if (activityItemBLL.Add(modelList[i]))
                                {
                                    result.IsSuccess = true;
                                    result.Content = "新增成功";
                                }
                            }
                            catch
                            {
                                result.IsSuccess = false;
                                result.Content = "新增失败";
                            }
                        }
                        else if (operationType == OperationType.Update)
                        {
                            try
                            {
                                if (activityItemBLL.Update(modelList[i]))
                                {
                                    result.IsSuccess = true;
                                    result.Content = "更新成功";
                                }
                            }
                            catch
                            {
                                result.IsSuccess = false;
                                result.Content = "更新失败";
                            }
                        }
                        resultModelList.Add(result);
                    }
                }
                requestResult.IsSuccess = true;
                requestResult.Content = FengNiao.GameTools.Json.Serialize.ConvertObjectToJsonList<List<OperateResultModel>>(resultModelList);
            }
            catch
            {
                requestResult.Content = "系统错误";
            }
            return FengNiao.GameTools.Json.Serialize.ConvertObjectToJson<ResultModel>(requestResult);
        }
    }
}