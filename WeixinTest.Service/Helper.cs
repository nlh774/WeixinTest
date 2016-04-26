using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace WeixinTest.Service
{
    /// <summary>
    /// 帮助工具类库
    /// </summary>
    public class Helper
    {
        /// <summary>
        /// 解析http正文请求
        /// </summary>
        public static string HttpResuest2String(HttpRequestBase Request)
        {
            string requestStr = null;
            if (Request.InputStream.Length != 0)
                requestStr = new StreamReader(Request.InputStream).ReadToEnd();
            return requestStr;
        }

        /// <summary>
        /// 创建微信响应消息时间戳
        /// 格林威治时间1970-1-1 8:00:00至今的秒数
        /// </summary>
        public static int GetWeixinCreateTime()
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            return (int)(DateTime.Now - dateStart).TotalSeconds;
        }
    }
}
