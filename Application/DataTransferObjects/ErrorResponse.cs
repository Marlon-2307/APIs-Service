using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Athenas.EjemploTemplateApi.Application.DataTransferObjects
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ErrorDetail> Errors { get; set; }
        public ErrorResponse()
        {
            Id = string.Empty;
            Message = string.Empty;
        }
        public override string ToString() => JsonSerializer.Serialize(this);


    }
    public class ErrorDetail
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Path { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Uri Url { get; set; }
    }
}
