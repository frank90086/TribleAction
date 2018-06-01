using Newtonsoft.Json;
using System.Net;

namespace TribleAction.Models.ApiModels
{
    public class ReturnResult<T>
    {
        private HttpStatusCode _httpStatusCode;
        private T _result;

        public ReturnResult()
        {
            this._httpStatusCode = HttpStatusCode.InternalServerError;
        }

        [JsonConstructor]
        public ReturnResult(HttpStatusCode httpStatusCode, T result)
        {
            this._httpStatusCode = httpStatusCode;
            this._result = result;
        }

        public HttpStatusCode HttpStatusCode
        {
            get { return _httpStatusCode; }
            private set { }
        }

        public T Result
        {
            get { return _result; }
            private set { }
        }
    }
}