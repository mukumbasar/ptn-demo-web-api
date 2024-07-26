using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Dtos.Abstract
{
    public interface IIdenftifiableDto : IDto
    {
        string Id { get; set; }
    }
}
