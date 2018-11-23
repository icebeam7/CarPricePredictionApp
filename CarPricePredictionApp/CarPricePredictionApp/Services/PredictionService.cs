using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

using CarPricePredictionApp.Helpers;
using CarPricePredictionApp.Models;

namespace CarPricePredictionApp.Services
{
    public static class PredictionService
    {
        public async static Task<double> PredictPrice(Car car)
        {
            try
            {
                var inputs = new
                {
                    Inputs = new Dictionary<string, StringTable>()
                    {
                        {
                            "input1",
                                new StringTable()
                                {
                                    ColumnNames = new string[]
                                    {
                                        "make",
                                        "body-style",
                                        "wheel-base",
                                        "engine-size",
                                        "horsepower",
                                        "peak-rpm",
                                        "highway-mpg",
                                        "price"
                                    },
                                    Values = new string[,]
                                    {
                                        {
                                            car.Make,
                                            car.BodyStyle,
                                            car.WheelSize.ToString(),
                                            car.EngineSize.ToString(),
                                            car.HorsePower.ToString(),
                                            car.PeakRPM.ToString(),
                                            car.HighwayMPG.ToString(),
                                            car.Price.ToString()
                                        }
                                    }
                                }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                var json = JsonConvert.SerializeObject(inputs);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new
                        AuthenticationHeaderValue("Bearer", Constants.PredictionKey);
                    client.BaseAddress = new Uri(Constants.PredictionURL);

                    var content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var prediction = JsonConvert.DeserializeObject<Prediction>(result);

                        return double.Parse(prediction.Results.output1.value.Values[0][8]);
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
