using CoreModule.Source.Dto.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface ProducerServiceInterface
    {
        Task Create(ProducerCreateDto dto);
        Task Update(ProducerUpdateDto dto);
    }
}
