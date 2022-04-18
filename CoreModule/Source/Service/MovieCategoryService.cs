using CoreModule.Source.Dto.MovieCategory;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public class MovieCategoryService : MovieCategoryServiceInterface
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        public MovieCategoryService(UnitOfWorkInterface unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(MovieCategoryCreateDto dto)
        {
            await ValidateMovieCategory(dto.Name).ConfigureAwait(false);
            var movieCategory = new MovieCategory(dto.Name);
            await _unitOfWork.Categories.AddAsync(movieCategory).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }

        public async Task Update(MovieCategoryUpdateDto dto)
        {
            var movieCategory = await _unitOfWork.Categories.GetByIdAsync(dto.Id).ConfigureAwait(false) ?? throw new MovieCategoryNotFoundException();
            await ValidateMovieCategory(dto.Name, movieCategory);
            movieCategory.Update(dto.Name);
            await _unitOfWork.Categories.Update(movieCategory).ConfigureAwait(false);
            await _unitOfWork.Complete();
        }
        private  async Task ValidateMovieCategory(string name, MovieCategory? movieCategory = null)
        {
            var movieCategoryWithSameName = await _unitOfWork.Categories.GetByName(name).ConfigureAwait(false);
            if(movieCategoryWithSameName != null && movieCategoryWithSameName != movieCategory)
            {
                throw new DuplicateMovieCategoryException();
            }
        }
    }
}
