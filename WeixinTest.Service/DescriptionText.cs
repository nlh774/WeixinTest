using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinTest.Service
{
    public class DescriptionText
    {
        /// <summary>
        /// 微信建议服务端无响应、出现意外错误、超时时返回success
        /// 若不无返回将对用户显示“该公众号暂时无法提供服务，请稍后再试”
        /// </summary>
        public static string WeiXinDefaultResponse 
        {
            get { return "success"; }
        }

        /// <summary>
        /// 本系统默认返回提示语
        /// </summary>
        public static string SystemTipResponse
        {
            get { return "1+快递公司+快递单号 ：查快递;2+城市名 ：查天气预报;3 ：联系我们;4+订单号:您在我司的订单查询"; }
        }

        /// <summary>
        /// 参数错误
        /// </summary>
        public static string ParamError
        {
            get { return "参数错误，正确参数：" + SystemTipResponse; }
        }

        public static string WebsiteDomainName
        {
            get { return "http://nlh774.top"; }
        }
    }
}
