using System;
using System.Collections.Generic;
using DynamicPixels.GameService.Services.Table;
using DynamicPixels.GameService.Utils.HttpClient;
using Newtonsoft.Json;
using UnityEditor.PackageManager;

namespace DynamicPixels.GameService.Models.outputs
{
    public class BaseResponse
    {
        public ErrorCode ErrorCode;
        public string ErrorMessage;
        public bool IsSuccessful;
    }
    public class RowListResponse : BaseResponse
    {
        [JsonProperty("list")]
        public List<Row> List { get; set; } = new List<Row>();

        [JsonProperty("totalCount")]
        public Int64 TotalCount { get; set; }
    }
    
    public class RowListResponse<T> : BaseResponse
    {
        [JsonProperty("list")]
        public List<T> List { get; set; } = new List<T>();

        [JsonProperty("totalCount")]
        public Int64 TotalCount { get; set; }
    }

    public class RowResponse : BaseResponse
    {
        public Row Row { get; set; }
    }

    public class RowResponse<T> : BaseResponse
    {
        [JsonProperty("row")]
        public T Row { get; set; }
    }
    public class ActionResponse : BaseResponse
    {
        public int Affected { get; set; }
    }
}