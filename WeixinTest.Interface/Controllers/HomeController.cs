using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeixinTest.Service;
using System.IO;
using System.Xml;
using CommTools;

namespace WeixinTest.Interface.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 接口主方法
        /// </summary>
        public void Index()
        {
            if (Request.HttpMethod.ToLower() == "get")
                Response.Write("欢迎访问钮林华微信测试公众号的后端服务");
            else
                HandleMsg();
        }

        /// <summary>
        /// 微信开发第一次验证接口url,会获取token等值
        /// </summary>
        public void ValidateUrl(string signature, string timestamp, string nonce, string echostr)
        {
            const string token = "nlh774token";

            bool isCheckOk = new CheckSignatureService().CheckSignature(signature, timestamp , nonce, echostr, token);
            if (isCheckOk)
                Response.Write(echostr);
            else
                Response.Write("false");
            #region 获取到的相关token值
            //sig:  c05cb41842c15579579482ed6513ced12044482b
            //time: 1460470458
            //nonce: 1283169717
            //echostr: 7602788682314875647 
            #endregion
            LogService.AsycWriteLog("CheckSignature", string.Join("||", signature, timestamp, nonce, echostr), isCheckOk);
        }

        /// <summary>
        /// 处理原始微信请求报文，返回完整报文
        /// </summary>
        private void HandleMsg()
        {
            string requestStr = Helper.HttpResuest2String(Request);
            if (requestStr.IsNullOrWhiteSpace())
            {
                WeixinResponseEmpty();
                return;
            }

            string response = new HandleTextMsgService().Handle(requestStr);
            Response.Write(response);
        }

        /// <summary>
        /// 微信建议服务端无响应、出现意外错误、超时时返回success
        /// 否则将向客户报错“该公众号暂时无法提供服务，请稍后再试”
        /// </summary>
        private void WeixinResponseEmpty()
        {
            Response.Write("success");
        }
    }
}
