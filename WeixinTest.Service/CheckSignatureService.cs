using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace WeixinTest.Service
{
    public class CheckSignatureService
    {
        public bool CheckSignature(string signature, string timestamp, string nonce, string echostr, string token)
        {
            string[] arr = { token, timestamp, nonce };
            //1. 将token、timestamp、nonce三个参数进行字典序排序
            Array.Sort(arr);//排序
            //2. 将三个参数字符串拼接成一个字符串进行sha1加密
            string encryptValue = FormsAuthentication.HashPasswordForStoringInConfigFile(arr[0] + arr[1] + arr[2], "SHA1");
            //3. 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信
            //SHA1有大小写区别，先转成小写再对比
            //如果相同就返回微信服务器要求的signature，不相同就没有必要处理
            return encryptValue.ToLower() == signature;
        }
    }
}
