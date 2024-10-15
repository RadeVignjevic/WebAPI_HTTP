using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI_HTTP.Models
{
    public static class DeserilizeExtension
    {
        private static JsonSerializerOptions defaultSerializerSettings = new JsonSerializerOptions();

        // set this up how you need to!
        private static JsonSerializerOptions featureXSerializerSettings = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };


        public static T Deserialize<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, defaultSerializerSettings);
        }

        public static T DeserializeCustom<T>(this string json, JsonSerializerOptions settings)
        {
            return JsonSerializer.Deserialize<T>(json, settings);
        }

        public static T DeserializeCaseSensitive<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, featureXSerializerSettings);
        }
    }
}
