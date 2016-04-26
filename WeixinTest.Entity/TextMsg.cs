using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinTest.Entity
{
    public class TextMsg
    {
        public string ToUserName { get; set; }

        public string FromUserName { get; set; }
        
        public string MsgType{get;set;}

        /// <summary>
        /// 消息内容区分  这里是文本，为text
        /// </summary>
        public string Content{get;set;}
    }
}
