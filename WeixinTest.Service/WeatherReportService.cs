using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommTools;
using Newtonsoft.Json;
using WeixinTest.Service;
using System.Reflection;
using System.Configuration;

namespace WeixinTest.Service
{
    #region 百度天气实体
    public class Index
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("zs")]
        public string Zs { get; set; }

        [JsonProperty("tipt")]
        public string Tipt { get; set; }

        [JsonProperty("des")]
        public string Des { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("dayPictureUrl")]
        public string DayPictureUrl { get; set; }

        [JsonProperty("nightPictureUrl")]
        public string NightPictureUrl { get; set; }

        [JsonProperty("weather")]
        public string Weather { get; set; }

        [JsonProperty("wind")]
        public string Wind { get; set; }

        [JsonProperty("temperature")]
        public string Temperature { get; set; }
    }

    public class Result
    {
        [JsonProperty("currentCity")]
        public string CurrentCity { get; set; }

        [JsonProperty("pm25")]
        public string Pm25 { get; set; }

        [JsonProperty("index")]
        public Index[] Index { get; set; }

        [JsonProperty("weather_data")]
        public WeatherData[] WeatherData { get; set; }
    }

    public class BaiduResponse
    {
        [JsonProperty("error")]
        public int Error { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }
    #endregion

    public class WeatherReportService
    {
        public OperationResponse<string> QuerySimpleDecription(string cityName)
        {
            string simpleDecription = string.Empty;

            var weather = Query(cityName);
            if (weather.IsError)
                return new OperationResponse<string>(MethodBase.GetCurrentMethod().DeclaringType, weather.ErrorMessage, false);
                
            simpleDecription = string.Format("城市：{0}", weather.Content.Results[0].CurrentCity);
            for (int i = 0; i < 2; i++) //今明两天天气
            {
                var dayWeather = weather.Content.Results[0].WeatherData[i];
                simpleDecription += string.Format("时间：{0},温度：{1},天气：{2},风：{3}；", 
                    dayWeather.Date, dayWeather.Temperature,dayWeather.Weather, dayWeather.Wind);
            }
            return new OperationResponse<string>(simpleDecription);
        }
        public OperationResponse<BaiduResponse> Query(string cityName)
        {
            if (cityName.IsNullOrWhiteSpace()) 
                return new OperationResponse<BaiduResponse>(MethodBase.GetCurrentMethod().DeclaringType, "城市名为空", false);

            string requestUrl = string.Format("http://api.map.baidu.com/telematics/v3/weather?location={0}&output=json&ak={1}",
                cityName, ConfigurationManager.AppSettings["BaiduAK"]);
            
            WebUtils web = new WebUtils(requestUrl) { Method = "Get" }; //务必使用Get方式，默认是Post
            var translateResponse = web.Do();
            if (translateResponse.Key.Equals(ResultCode.Success))   //状态正常
            {
                BaiduResponse data = JsonConvert.DeserializeObject<BaiduResponse>(translateResponse.Value);
                if (data.Error == 0 && data.Results != null && data.Results.Count() > 0)
                    return new OperationResponse<BaiduResponse>(data);
                else
                    return new OperationResponse<BaiduResponse>(MethodBase.GetCurrentMethod().DeclaringType, "查询无数据，信息源返回错误描述：" + data.Status, false);
            }
            else
            {
                return new OperationResponse<BaiduResponse>(MethodBase.GetCurrentMethod().DeclaringType, "密钥错误或网络错误", false);
            }
        }
    }
}
