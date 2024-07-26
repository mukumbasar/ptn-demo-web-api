using PtnDemoProjectAPI.BLL.Dtos.Abstract;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Results.Concrete
{
    public class DataResult<TDto> : Result, IDataResult<TDto>
    {
        public TDto Data { get; set; }

        public DataResult(bool isSuccess, TDto data) : base(isSuccess) 
        { 
            Data = data;
        }

        public DataResult(bool isSuccess, string message, TDto data) : base(isSuccess, message) 
        {
            Data = data;
        }
    }
}
