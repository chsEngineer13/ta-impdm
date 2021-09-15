using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TA.IMPDM.Service.OData
{
    public class ODataClientResult
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
        public bool IsSuccessStatusCode { get; }

        public ODataClientResult(bool success, HttpStatusCode statusCode, string errorMessage = null)
        {
            IsSuccessStatusCode = success;
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
