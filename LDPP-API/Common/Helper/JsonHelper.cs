using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDPP_API.Common.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// 把对象转换为JSON字符串
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJSON(object o)
        {
            if (o == null)
            {
                return null;
            }
            return JsonConvert.SerializeObject(o);
        }
        /// <summary>
        /// 把Json文本转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T FromJSON<T>(string input)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(input);
            }
            catch (Exception ex)
            {
                return default;
            }
        }
        public static T ReadFromJsonPath<T>(string path) where T : class
        {
            try
            {
                using (StreamReader file = File.OpenText(path))
                {
                    var jSting = file.ReadToEnd();
                    var jsonObject = JsonConvert.DeserializeObject<T>(jSting);
                    return jsonObject;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
