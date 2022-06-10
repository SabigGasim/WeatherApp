using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System;
using WeatherApp.Models;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    static class JsonService
    {
        public static readonly string _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JSON.json");
        public static readonly HttpClient _client = new HttpClient();

        public static async Task<T> HttpGet<T>(string url)
        {
            var jsonString = await _client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static Root ReadJsonFile()
        {
            if (!File.Exists(_fileName))
            {
                return null;
            }

            try
            {
                var jsonString = File.ReadAllText(_fileName);
                if (string.IsNullOrEmpty(jsonString))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<Root>(jsonString);
            }
            catch (IOException) { return null; }
            catch (JsonSerializationException)
            {
                File.Delete(_fileName);
                return null;
            }
        }

        public static void WriteJsonFile<T>(T obj)
        {
            string fullWeatherCastJsonString = JsonConvert.SerializeObject(obj);
            File.WriteAllText(_fileName, fullWeatherCastJsonString);
        }
    }
}


