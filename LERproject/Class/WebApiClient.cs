using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LERproject;
using LERproject.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LERproject.Class
{
    public class WebApiClient
    {
        private string _urlApi;
        private string _zipCode;
        private string _appId;
        private string _appKey;

        //public IWeatherConfig weatherCon;
        public WeatherConfig _weatherConfig;

        public WebApiClient(string strUrl, string strZipCode, WeatherConfig weatherConfig)
        {
            //_weatherConfig = weatherConfig.Value;

            _urlApi = strUrl + strZipCode;
            _zipCode = strZipCode;
            _appId = weatherConfig.appId;
            _appKey = weatherConfig.appKey;

        }


        public List<Weather> queryApi()
        {
            List<Weather> resWeather = new List<Weather>();

            try
            { 


                var options = new RestClientOptions(_urlApi)
                {
                    ThrowOnAnyError = true,
                    Timeout = 10000
                };

                var client = new RestClient(options);

                var request = new RestRequest()
                    .AddQueryParameter("app_id", _appId)
                    .AddQueryParameter("app_key", _appKey);

                //var response = await client.PostAsync
                //var response = client.Execute(request) as RestResponse;
                var response = client.ExecuteGet(request);
                if (response != null &&
                    ((response.StatusCode == System.Net.HttpStatusCode.OK) &&
                    (response.ResponseStatus == ResponseStatus.Completed)))
                {

                    string valor = "";
                    JArray arrData;
                    var arrValuesWeather = (JObject)JsonConvert.DeserializeObject<JObject>(response.Content);
                    foreach (var item in arrValuesWeather)
                    {
                        valor = (string)item.Key;
                        arrData = (JArray)item.Value;


                        for (int i = 0; i < arrData.Count; i++)
                        {
                            Weather itemWeather = new Weather();

                            JObject itemDay = (JObject)arrData[i];
                            foreach (var item2 in itemDay)
                            {
                                var fieldName = item2.Key;
                                var fieldValue = item2.Value;

                                switch (fieldName)
                                {
                                    case "date":
                                        itemWeather.date = fieldValue.ToString();
                                        break;
                                    case "temp_max_f":
                                        itemWeather.tempMax = fieldValue.ToString();
                                        break;
                                    case "temp_min_f":
                                        itemWeather.tempMin = fieldValue.ToString();
                                        break;
                                    case "windspd_max_mph":
                                        itemWeather.windSpeedMax = fieldValue.ToString();
                                        break;
                                }
                            }

                            resWeather.Add(itemWeather);
                        }

                    }



                //JObject arrValuesWeather = JObject.Parse(response.Content);


                }

            }
            catch (Exception e)
            {
                Weather itemWeather = new Weather();
                itemWeather.date = "No records found";
                resWeather.Add(itemWeather);
            }


            return resWeather;
        }


    }
}
