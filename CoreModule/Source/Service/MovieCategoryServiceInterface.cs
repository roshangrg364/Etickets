using CoreModule.Source.Dto.MovieCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface MovieCategoryServiceInterface
    {
        Task Create(MovieCategoryCreateDto dto);
        Task Update(MovieCategoryUpdateDto dto);
    }
}
