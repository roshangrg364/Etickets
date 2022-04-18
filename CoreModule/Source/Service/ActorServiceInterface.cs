using CoreModule.Source.Dto.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface ActorServiceInterface
    {
        Task Create(ActorCreateDto dto);
        Task Update(ActorUpdateDto dto);
        Task Remove(int id);
    }
}
