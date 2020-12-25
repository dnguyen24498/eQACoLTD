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

        private HttpStatusCode _code;
        public HttpStatusCode Code { get=>_code;
            set
            {
                _code = value;
                isSuccess(_code);
            }
        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T ResultObj { get; set; }

        private void isSuccess(HttpStatusCode code)
        {
            if (code == HttpStatusCode.OK)
            {
                this.IsSuccess = true;
            }
            else
            {
                this.IsSuccess = false;
            }
        }
    }
}
