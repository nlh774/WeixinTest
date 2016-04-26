using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeixinTest.Service;
using System.IO;
using System.Xml;

namespace WeixinTest.Interface.Controllers
{
    public class HomeController : Controller
    {
        #region 老代码，已注释
        //public void Index(string signature, string timestamp, string nonce, string echostr)
        //{
        //    if (Request.HttpMethod.ToLower() == "get")
        //    {
        //        ValidateUrl(signature, timestamp, nonce, echostr);
        //    }
        //    else
        //    {
        //        HandleMsg();
        //    }
        //} 
        #endregion

        public void Index()
        {
            if (Request.HttpMethod.ToLower() == "get")
            {
                Response.Write("欢迎访问钮林华微信测试公众号的后端服务");
            }
            else
            {
                HandleMsg();
            }
        }

        public void ValidateUrl(string signature, string timestamp, string nonce, string echostr)
        {
            const string token = "nlh774token";

            bool isCheckOk = new CheckSignatureService().CheckSignature(signature, timestamp
                , nonce, echostr, token);
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

        public void Test(string param)
        {
            Response.Write("Test");
            Response.Write(param);
        }

        public void HandleMsg()
        {
            string requestStr = HttpResuest2String(Request.InputStream);
            if (requestStr == null)
            {
                //WeixinResponseEmpty();
                Response.Write("输入:1+参数来测试，或者2+地区名来查询天气");
                return;
            }

            string response = new TextMsgService().Handle(requestStr);
            if (response == null)
            {
                //WeixinResponseEmpty();
                Response.Write("输入:1+参数来测试，或者2+地区名来查询天气");
            }
            else
                Response.Write(response);

            #region 老代码，已注释
            //Stream XmlStream = Request.InputStream;
            ////构造xml对象
            //XmlDocument doc = new XmlDocument();
            //doc.Load(XmlStream);
            //XmlElement rootElement = doc.DocumentElement;//获取根节点
            ////解析XML数据
            //string toUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            //string fromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
            //string msgType = rootElement.SelectSingleNode("MsgType").InnerText;
            ////消息内容区分  这里是文本，为text
            //string content = rootElement.SelectSingleNode("Content").InnerText;
            ////msg = string.Format("{0}-{1}-{2}-{3}", toUserName, fromUserName, msgType, content);//测试
            //string xmlMsg = "";
            ////响应
            ///*
            // *   <xml>
            //   <ToUserName><![CDATA[toUser]]></ToUserName>
            //   <FromUserName><![CDATA[fromUser]]></FromUserName> 
            //   <CreateTime>1348831860</CreateTime>
            //   <MsgType><![CDATA[text]]></MsgType>
            //   <Content><![CDATA[this is a test]]></Content>
            //   <MsgId>1234567890123456</MsgId>
            //   </xml>
            // */
            //if (content.Contains("BD") || content.Contains("bd"))
            ////content.Contains返回一个值，该值指示指定的system.string对象是否存在于此字符串中
            //{
            //    string[] sArray = content.Split('+');
            //    string name = sArray[2];
            //    string no = sArray[1];
            //    string str = "您的信息为：" + "姓名：" + name + "学号：" + no;
            //    //SelectStudentInfo();

            //    xmlMsg = "<xml>" + "<ToUserName><![CDATA[" + fromUserName + "]]></ToUserName>"
            //           + "<FromUserName><![CDATA[" + toUserName + "]]></FromUserName>"
            //           + "<CreateTime>" + GetCreateTime() + "</CreateTime>"
            //           + "<MsgType><![CDATA[text]]></MsgType>"
            //           + "<Content><![CDATA[" + str + "]]></Content>"
            //           + "</xml>";
            //}
            //else
            //{
            //    string qita = "您现在还未进行绑定，请先回复【BD+学号+姓名】先进行绑定！";
            //    xmlMsg = "<xml>" + "<ToUserName><![CDATA[" + fromUserName + "]]></ToUserName>"
            //           + "<FromUserName><![CDATA[" + toUserName + "]]></FromUserName>"
            //           + "<CreateTime>" + GetCreateTime() + "</CreateTime>"
            //           + "<MsgType><![CDATA[text]]></MsgType>"
            //           + "<Content><![CDATA[" + qita + "]]></Content>"
            //           + "</xml>";
            //}
            //LogService.WriteLog("SendText", xmlMsg, true);
            //Response.Write(xmlMsg); 
            #endregion
        }

        public string HttpResuest2String(Stream stream)
        {
            string requestStr = null;
            if (stream.Length != 0)
                requestStr = new StreamReader(stream).ReadToEnd();
            return requestStr;
        }

        private void WeixinResponseEmpty()
        {
            Response.Write("success");  //微信建议返回success
        }
    }
}
