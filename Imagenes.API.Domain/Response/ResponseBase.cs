using System;
using System.Collections.Generic;
using System.Text;

namespace Imagenes.API.Domain.Response
{
    public class ResponseBase<T>
    {
        public DateTime date;
        public int code;
        public bool hasError;
        public string message;
        public T Data { set; get; }
    }    
}
