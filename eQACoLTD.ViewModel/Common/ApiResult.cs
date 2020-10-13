using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace eQACoLTD.ViewModel.Common
{
    public class ApiResult<T>
    {
        public ApiResult(HttpStatusCode code,string mess)
        {
            Code = code;
            Message = mess;
        }

        public ApiResult(HttpStatusCode code,T resultObj)
        {
            Code = code;
            ResultObj = resultObj;
        }

        public ApiResult(HttpStatusCode code)
        {
            Code = code;
        }

        public ApiResult()
        {
            
        }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
