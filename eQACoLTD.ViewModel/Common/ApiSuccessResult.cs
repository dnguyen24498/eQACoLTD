using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace eQACoLTD.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        // public ApiSuccessResult(T resultObj)
        // {
        //     IsSuccess = true;
        //     ResultObj = resultObj;
        // }
        //
        // public ApiSuccessResult()
        // {
        //     IsSuccess = true;
        // }
        public ApiSuccessResult(HttpStatusCode code, string mess) : base(code, mess)
        {
        }

        public ApiSuccessResult(HttpStatusCode code, T resultObj) : base(code, resultObj)
        {
        }
    }
}
