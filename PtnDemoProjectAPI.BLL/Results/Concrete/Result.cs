using Microsoft.AspNetCore.Http;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IResult = PtnDemoProjectAPI.BLL.Results.Abstract.IResult;

namespace PtnDemoProjectAPI.BLL.Results.Concrete
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Result(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Message = string.Empty;
        }
    }
}
