using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinTest.Entity
{
    /// <summary>
    /// 文本消息对象
    /// </summary>
    public class TextMsg
    {
        /// <summary>
        /// 接收人
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public string FromUserName { get; set; }
        
        /// <summary>
        /// 消息类型：text、media等
        /// </summary>
        public string MsgType{get;set;}

        /// <summary>
        /// 消息内容，不同的消息类型内容不同
        /// </summary>
        public string Content{get;set;}
    }
}
