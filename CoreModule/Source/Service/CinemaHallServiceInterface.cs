using CoreModule.Source.Dto.CinemalHall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface CinemaHallServiceInterface
    {
        Task Create(CinemaHallCreatDto dto);
        Task Update(CinemaHallUpdateDto dto);
    }
}
