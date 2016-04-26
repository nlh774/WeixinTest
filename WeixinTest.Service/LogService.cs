using System;
using WeixinTest.Model.Models;
using System.Linq;
using System.Threading;

namespace WeixinTest.Service
{
    /// <summary>
    /// 日志记录工具类
    /// </summary>
    public class LogService
    {
        /// <summary>
        /// 异步记录日志
        /// </summary>
        public static void AsycWriteLog(string type, string info, bool IsSuccess)
        {
            ThreadPool.QueueUserWorkItem(t =>
            {
                Log log = new Log
                {
                    Type = type ?? "empty",
                    Info = info ?? "empty",
                    IsSuccess = IsSuccess,
                    CreatedBy = "system",
                    CreatedOn = DateTime.Now,
                };
                var context = new WeixinTestContext();
                context.Logs.Add(log);
                context.SaveChanges();
            });
        }
    }
}
