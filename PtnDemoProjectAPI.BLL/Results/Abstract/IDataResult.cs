using PtnDemoProjectAPI.BLL.Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Results.Abstract
{
    public interface IDataResult<TDto>
    {
        TDto Data { get; set; }
    }
}
