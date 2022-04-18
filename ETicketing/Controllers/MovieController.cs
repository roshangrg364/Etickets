using CoreModule.Source.Dto.Movie;
using CoreModule.Source.Entity;
using CoreModule.Source.Exceptions;
using CoreModule.Source.Service;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using ETicketing.ViewModels;
using ETicketing.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;


namespace ETicketing.Controllers
{
    [Authorize(Roles = ApplicationUser.RoleAdmin)]
    public class MovieController : Controller
    {

        private readonly UnitOfWorkInterface _unitOfWork;
        private readonly ILogger<MovieController> _logger;
        private readonly IToastNotification _notify;
        private readonly MovieServiceInterface _movieService;
        private readonly ImageUploaderInterface _imageUploader;

        public MovieController(UnitOfWorkInterface unitOfWork,
            ILogger<MovieController> logger,
            IToastNotification notify,
            MovieServiceInterface movieService,
            ImageUploaderInterface imageUploader)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _notify = notify;
            _movieService = movieService;
            _imageUploader = imageUploader;
        }
        public async Task<IActionResult> Index(MovieIndexViewModel model)
        {
            var dropdowns = await GetDropdowns();
            SetDropdowns(dropdowns);
            var movieQueryable = _unitOfWork.Movies.GetQueryable();
            movieQueryable = FilterMovies(model, movieQueryable);
            var movies =  movieQueryable;
            var moviesDataModel = await movies.Select(a => new MovieViewModel
            {
                Id = a.Id,
                CinemaHall = a.CinemaHall.Name,
                Name = a.Name,
                StartDate  =a.StartDate.ToString("yyy-MM-dd hh:mm tt"),
                EndDate  =a.EndDate.ToString("yyy-MM-dd hh:mm tt"),
                Image = a.Image,
                TicketPrice = a.TicketPrice,
                Status = a.EndDate < DateTime.Now ? Movie.Expired:Movie.Available,
                MovieCategory = a.Category.Name
            }).ToListAsync();
            model.MovieDatas = moviesDataModel;
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var dropdowns = await GetDropdowns();
            SetDropdowns(dropdowns);

            return View();
        }

     

        [HttpPost]
        public async Task<IActionResult> Create(MovieCreatViewModel model)
        {
                var dropdowns = await GetDropdowns();
                SetDropdowns(dropdowns);
            
        
            try
            {
                if (ModelState.IsValid) { 
                var imagepath = await _imageUploader.UploadToTemporary(model.Image);
                var movieCreateDto = new MovieCreateDto(model.Name, model.Description, model.TicketPrice, imagepath,
                    model.StartDate, model.EndDate, model.MovieCategoryId, model.CinemaHallId,
                    model.ProducerId, model.ActorIds);
                await _movieService.Create(movieCreateDto);
                _notify.AddSuccessToastMessage("Movie Added Successfully");
                return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex,ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var dropdowns = await GetDropdowns();
                SetDropdowns(dropdowns);
                var movie = await _unitOfWork.Movies.GetByIdAsync(id) ?? throw new MovieNotFoundException();
                var movieUpdateModel = new MovieUpdateViewModel() { 
                Id = movie.Id,
                Description = movie.Description,
                Name = movie.Name,
                TicketPrice = movie.TicketPrice,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategoryId= movie.MovieCategoryId,
                CinemaHallId  =movie.CinemaHallId,
                ProducerId = movie.ProducerId,
                ImageSource = movie.Image,
                ActorIds= movie.ActorMovies.Where(a=>a.MovieId==movie.Id).Select(a=>a.ActorId).ToList()
                };
                return View(movieUpdateModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Update(MovieUpdateViewModel model)
        {
            var dropdowns = await GetDropdowns();
            SetDropdowns(dropdowns);
            try
            {
                if(ModelState.IsValid)
                {
                    var imagepath = "";
                    if(model.Image != null &&model.Image.Length >0 )
                    {
                        imagepath = await _imageUploader.UploadToTemporary(model.Image);
                    }
                    var movieUpdateDto = new MovieUpdateDto(model.Id, model.Name, model.Description, model.TicketPrice, imagepath,
                        model.StartDate, model.EndDate, model.MovieCategoryId, model.CinemaHallId, model.ProducerId, model.ActorIds);
                    await _movieService.Update(movieUpdateDto);
                    _notify.AddSuccessToastMessage("Movie Updated Successfully");
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception  ex)
            {

                _logger.LogError(ex.Message);
                _notify.AddErrorToastMessage(ex.Message);
            }
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var movie = await _unitOfWork.Movies.GetByIdAsync(id) ?? throw new MovieNotFoundException();
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
                    Status = movie.EndDate < DateTime.Now ? Movie.Expired :Movie.Available,
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
                _notify.AddErrorToastMessage(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        private static IQueryable<Movie> FilterMovies(MovieIndexViewModel model, IQueryable<Movie> movieQueryable)
        {
            if (model.CinemaId > 0)
            {
                movieQueryable = movieQueryable.Where(a => a.CinemaHallId == model.CinemaId);
            }
            if (model.ProducerId > 0)
            {
                movieQueryable = movieQueryable.Where(a => a.ProducerId == model.ProducerId);
            }
            if (model.CategoryId > 0)
            {
                movieQueryable = movieQueryable.Where(a => a.MovieCategoryId == model.CategoryId);
            }
            if (model.ActorId > 0)
            {
                movieQueryable = movieQueryable.Where(a => a.ActorMovies.Any(a => a.ActorId == model.ActorId));
            }

            return movieQueryable;
        }

        private async Task<DropDownViewModel> GetDropdowns()
        {
            var dropdowns = new DropDownViewModel() { 
            Actors = await _unitOfWork.Actors.GetQueryable().OrderBy(a=>a.FullName).ToListAsync(),
            Producers = await _unitOfWork.Producers.GetQueryable().OrderBy(a=>a.FullName).ToListAsync(),
            Categories = await _unitOfWork.Categories.GetQueryable().OrderBy(a=>a.Name).ToListAsync(),
            Cinemas = await _unitOfWork.CinemaHalls.GetQueryable().OrderBy(a=>a.Name).ToListAsync()
            };
            return dropdowns;
        }

        private void SetDropdowns(DropDownViewModel dropdowns)
        {
            ViewBag.Cinemas = new SelectList(dropdowns.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(dropdowns.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(dropdowns.Actors, "Id", "FullName");
            ViewBag.Categories = new SelectList(dropdowns.Categories, "Id", "Name");
        }
    }
}
