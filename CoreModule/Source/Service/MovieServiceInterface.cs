using CoreModule.Source.Dto.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface MovieServiceInterface
    {
        Task Create(MovieCreateDto dto);
        Task Update(MovieUpdateDto dto);
    }
}
