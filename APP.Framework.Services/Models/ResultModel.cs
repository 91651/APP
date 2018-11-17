using System;
using IView.AspNetCore.DynamicLinq;

namespace APP.Framework.Services.Models
{
    public class ResultModel<T>
    {
        public T Data { get; set; }
        public int Total { get; set; }
    }
}
