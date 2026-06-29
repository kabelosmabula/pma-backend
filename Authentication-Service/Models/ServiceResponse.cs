using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = false;

        public string Message { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        public ServiceResponse() { }

        public ServiceResponse(T data, string message = "", bool success = true)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }

}
