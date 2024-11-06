using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Athenas.EjemploTemplateApi.Application.DataTransferObjects
{
    public class Response<T>
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, string> Links { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IDictionary<string, object> Meta { get; private set; }


        public Response()
        {
        }

        public Response(T data)
        {
            Data = data;
        }
        public void addLink(string key, string value)
        {
            if (Links is null) Links = new Dictionary<string, string>();
            Links.Add(key, value);
        }
        public void addMeta(string key, object value)
        {
            if (Meta is null) Meta = new Dictionary<string, object>();
            Meta.Add(key, value);
        }



    }
}
