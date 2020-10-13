using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace eQACoLTD.ViewModel.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        // public string[] ValidationErrors { get; set; }
        //
        // public ApiErrorResult() { }
        //
        // public ApiErrorResult(string message)
        // {
        //     IsSuccess = false;
        //     Message = message;
        // }
        //
        // public ApiErrorResult(string[] validationErrors)
        // {
        //     IsSuccess = false;
        //     ValidationErrors = validationErrors;
        // }
        public ApiErrorResult(HttpStatusCode code, string mess) : base(code, mess)
        {
        }

        public ApiErrorResult(HttpStatusCode code, T resultObj) : base(code, resultObj)
        {
        }
    }
}
