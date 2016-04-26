using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using WeixinTest.Entity;
using CommTools;

namespace WeixinTest.Service
{
    public class TextMsgService
    {
        public string Handle(string requestXml)
        {
            var requestMsg = Receive(requestXml);
            LogService.AsycWriteLog("接收消息", requestXml, requestMsg != null);
            if (requestMsg == null)    return null;

            var responseMsg = new TextMsg
            {
                FromUserName = requestMsg.ToUserName,
                ToUserName = requestMsg.FromUserName,
                MsgType = "text",
            };
            responseMsg.Content = ResponseContent(requestMsg.Content);
            LogService.AsycWriteLog("处理接收消息", responseMsg.Content, responseMsg.Content != null);
            if (responseMsg.Content == null) return null;
            LogService.AsycWriteLog("处理接收消息",ConverntHelper.SerializeToJson(responseMsg), responseMsg.Content != null);
            return CreateResponseString(responseMsg);
        }

        public TextMsg Receive(string strXml)
        {
            #region 微信接收消息示例
            /*
            <xml>
            <ToUserName><![CDATA[toUser]]></ToUserName>
            <FromUserName><![CDATA[fromUser]]></FromUserName> 
            <CreateTime>1348831860</CreateTime>
            <MsgType><![CDATA[text]]></MsgType>
            <Content><![CDATA[this is a test]]></Content>
            <MsgId>1234567890123456</MsgId>
            </xml>
            */
            /*
            ToUserName	 开发者微信号
            FromUserName	 发送方帐号（一个OpenID）
            CreateTime	 消息创建时间 （整型）
            MsgType	 text
            Content	 文本消息内容
            MsgId	 消息id，64位整型 
            */
            
            #endregion
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);
                XmlElement rootElement = doc.DocumentElement;//获取根节点
                string toUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                string fromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
                string msgType = rootElement.SelectSingleNode("MsgType").InnerText;
                string content = rootElement.SelectSingleNode("Content").InnerText;
                return new TextMsg
                {
                    ToUserName = toUserName,
                    FromUserName = fromUserName,
                    MsgType = msgType,
                    Content = content
                };
            }
            catch (Exception ex)
            {
                LogService.AsycWriteLog("接收消息",string.Join(",",ex.Message,ex.StackTrace),false);
                return null;
            }
        }


        public string CreateResponseString(TextMsg textMsg)
        {
            #region 微信发送消息示例
            /*
            <xml>
            <ToUserName><![CDATA[toUser]]></ToUserName>
            <FromUserName><![CDATA[fromUser]]></FromUserName> 
            <CreateTime>1348831860</CreateTime>
            <MsgType><![CDATA[text]]></MsgType>
            <Content><![CDATA[this is a test]]></Content>
            <MsgId>1234567890123456</MsgId>
            </xml>
            */
            
            #endregion
            string xmlMsg = "<xml>" + "<ToUserName><![CDATA[" + textMsg.ToUserName + "]]></ToUserName>"
                       + "<FromUserName><![CDATA[" + textMsg.FromUserName + "]]></FromUserName>"
                       + "<CreateTime>" + GetCreateTime() + "</CreateTime>"
                       + "<MsgType><![CDATA[text]]></MsgType>"  //textMsg.MsgType
                       + "<Content><![CDATA[" + textMsg.Content + "]]></Content>"
                       + "</xml>";
            return xmlMsg;
        }

        public int GetCreateTime()//创建时间戳
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);//格林威治时间1970，1，1，0，0，0
            return (int)(DateTime.Now - dateStart).TotalSeconds;
        }

        public string ResponseContent(string requestContent)
        {
            if (!requestContent.Contains("+")) return null;

            string responseContent = "";
            string[] requestParams = requestContent.Split('+');
            string commandNo = requestParams[0];
            string commangParam = requestParams[1];
            switch (commandNo)
            {
                case "1":
                    responseContent = "您输入的命名代号是1,输入的参数是"+commangParam;
                    break;
                case "2":
                    responseContent = new WeatherReportService().Query(commangParam);
                    break;
                default:
                    break;
            }
            return responseContent;
        }

    }
}
