using CoreModule.Source.Dto.Movie;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public class MovieService : MovieServiceInterface
    {
        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly FilerHelperInterface _fileHelper;
        public MovieService(UnitOfWorkInterface unitOfWork,
            FilerHelperInterface fileHelper)
        {
            _unitOfWork = unitOfWork;
            _fileHelper = fileHelper;
        }
        public async Task Create(MovieCreateDto dto)
        {
            validateStartAndEndDate(dto.StartDate,dto.EndDate);
            await ValidateMovie(dto.Name, dto.CinemaHallId).ConfigureAwait(false);
            if (!dto.ActorIds.Any()) throw new ActorCannotBeEmptyInMovieException();
            dto.Image = _fileHelper.SaveImageAndGetFileName(dto.Image);
            var producer = await _unitOfWork.Producers.GetByIdAsync(dto.ProducerId).ConfigureAwait(false) ?? throw new ProducerNotFoundException();
            var cinemaHall = await _unitOfWork.CinemaHalls.GetByIdAsync(dto.CinemaHallId).ConfigureAwait(false) ?? throw new CinemaNotFoundException();
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.MovieCategoryId).ConfigureAwait(false) ?? throw new MovieCategoryNotFoundException();
            var movie = new Movie(dto.Name, dto.Description, dto.TicketPrice, dto.Image, dto.StartDate, dto.EndDate, category,
                cinemaHall, producer);
            foreach (var actorId in dto.ActorIds)
            {
                var actor = await _unitOfWork.Actors.GetByIdAsync(actorId).ConfigureAwait(false) ?? throw new ActorNotFoundException();
                movie.AddActor(actor);
            }
            await _unitOfWork.Movies.AddAsync(movie).ConfigureAwait(false);
            await _unitOfWork.Complete();

        }

       

        public async Task Update(MovieUpdateDto dto)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(dto.Id).ConfigureAwait(false) ?? throw new MovieNotFoundException();
            validateStartAndEndDate(dto.StartDate, dto.EndDate);
            await ValidateMovie(dto.Name, dto.CinemaHallId, movie).ConfigureAwait(false);
            if(!string.IsNullOrWhiteSpace(dto.Image))
            {
                _fileHelper.RemoveFile(movie.Image);
                dto.Image = _fileHelper.SaveImageAndGetFileName(dto.Image);
                movie.SetImage(dto.Image);
            }
           
            var producer = await _unitOfWork.Producers.GetByIdAsync(dto.ProducerId).ConfigureAwait(false) ?? throw new ProducerNotFoundException();
            var cinemaHall = await _unitOfWork.CinemaHalls.GetByIdAsync(dto.CinemaHallId).ConfigureAwait(false) ?? throw new CinemaNotFoundException();
            var category = await _unitOfWork.Categories.GetByIdAsync(dto.MovieCategoryId).ConfigureAwait(false) ?? throw new MovieCategoryNotFoundException();
            movie.Update(dto.Name, dto.Description, dto.TicketPrice, dto.StartDate, dto.EndDate,
                category, cinemaHall, producer);
            movie.ClearActor();
            foreach (var actorId in dto.ActorIds)
            {
                var actor = await _unitOfWork.Actors.GetByIdAsync(actorId).ConfigureAwait(false) ?? throw new ActorNotFoundException();
                movie.AddActor(actor);
            }

           // await _unitOfWork.Movies.Update(movie).ConfigureAwait(false);
            await _unitOfWork.Complete();


        }
        private  async Task ValidateMovie(string movieName, int cinemaId, Movie? movie =null)
        {
            var movieWithSameCinemaHall = await _unitOfWork.Movies.GetByCinemaHallAndMovie(movieName, cinemaId).ConfigureAwait(false);
            if (movieWithSameCinemaHall != null && movieWithSameCinemaHall != movie)
                throw new DuplicateMovieNameForCinemaHall(movieWithSameCinemaHall.CinemaHall.Name);
        }
        private static void validateStartAndEndDate(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate) throw new StartDateCannotBeMoreThanEndDateException();
        }
    }
}
