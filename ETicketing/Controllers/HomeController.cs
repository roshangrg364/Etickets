using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.UOW;
using ETicketing.Models;
using ETicketing.ViewModels;
using ETicketing.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ETicketing.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UnitOfWorkInterface _unitOfWork;
        public HomeController(ILogger<HomeController> logger,
            UnitOfWorkInterface unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var movieQueryable =  _unitOfWork.Movies.GetQueryable();
             if(!string.IsNullOrWhiteSpace(searchString))
            {
                movieQueryable = movieQueryable.Where(n => n.Name.Contains(searchString)
                || n.Category.Name.Contains(searchString));
            }
            var availableMovies = await movieQueryable.ToListAsync();
            var homePageDetailModels = availableMovies.Select(a=> new HomePageViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                MovieCategory = a.Category.Name,
                CinemaHall = a.CinemaHall.Name,
                Producer = a.Producer.FullName,
                StartDate = a.StartDate.ToString("yyyy-MM-dd "),
                EndDate = a.EndDate.ToString("yyyy-MM-dd "),
                ImageSource = a.Image,
                TicketPrice = a.TicketPrice,
                Status = a.EndDate < DateTime.Now ? Movie.Expired : Movie.Available
              

            });
            return View(homePageDetailModels);
        }
        public async Task<IActionResult> MovieDetails(int movieId)
        {
            try
            {
                var movie = await _unitOfWork.Movies.GetByIdAsync(movieId) ?? throw new MovieNotFoundException();
                var movieDetailViewModel = new MovieDetailsViewModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Description = movie.Description,
                    MovieCategory = movie.Category.Name,
                    CinemaHall = movie.CinemaHall.Name,
                    Producer = movie.Producer.FullName,
                    StartDate = movie.StartDate.ToString("yyyy-MM-dd hh:mm tt"),
                    EndDate = movie.EndDate.ToString("yyyy-MM-dd hh:mm tt"),
                    ImageSource = movie.Image,
                    TicketPrice = movie.TicketPrice,
                    CinemaHallId = movie.CinemaHallId,
                    ProducerId = movie.ProducerId,
                    Status = movie.EndDate < DateTime.Now ? Movie.Expired : Movie.Available,
                    Actors = movie.ActorMovies.Where(a => a.MovieId == movie.Id).Select(b => new ActorModel
                    {
                        Id = b.Actor.Id,
                        Name = b.Actor.FullName,
                        Image = b.Actor.Image ?? ""
                    }).ToList()

                };
                return View(movieDetailViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}