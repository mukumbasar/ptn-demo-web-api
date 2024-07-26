using PtnDemoProjectAPI.BLL.Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Results.Concrete
{
    public class SuccessDataResult<TDto> : DataResult<TDto>
    {
        public SuccessDataResult(TDto data) : base(true, data) { }

        public SuccessDataResult(string message, TDto data) : base(true, message, data) { }
    }
}
