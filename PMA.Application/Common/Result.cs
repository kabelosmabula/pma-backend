using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common
{
    public class Result<T>
    {

        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Error { get; set; }

        public static Result<T> Ok(T data) => new Result<T> { Success = true, Data = data };
        public static Result<T> Fail(string error) => new Result<T> { Success = false, Error = error };
    }
}
