
using System.ComponentModel.DataAnnotations;

namespace ETicketing.ViewModels.Movie
{
    public class MovieIndexViewModel
    {
        public int CinemaId { get; set; }
        public int ActorId { get; set; }
        public int ProducerId { get; set; }
        public int CategoryId { get; set; }
        public IList<MovieViewModel> MovieDatas { get; set; } = new List<MovieViewModel>();
    }

    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public decimal TicketPrice { get; set; }
        public string Image { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string MovieCategory { get; set; }

        public string CinemaHall { get; set; }
        public string Status { get; set; }

     
      
    }
    public class MovieCreatViewModel
    {
        [Display(Name ="Name")]
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Ticket Price is required")]
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }
        [Required(ErrorMessage = "Image is required")]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int MovieCategoryId { get; set; }

        [Required(ErrorMessage = "Cinema Hall is required")]
        [Display(Name = "Cinema")]
        public int CinemaHallId { get; set; }
        [Required(ErrorMessage = "Producer is required")]
        [Display(Name = "Producer")]
        public int ProducerId { get; set; }
        [Required(ErrorMessage = "Actor is required")]
        [Display(Name = "Actors")]
        public List<int> ActorIds { get; set; } = new List<int>();

    }
    public class MovieUpdateViewModel : MovieCreatViewModel
    {
        public int Id { get; set; }
        public string? ImageSource { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Ticket Price is required")]
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }
       
        [Display(Name = "Image")]
        public IFormFile? Image { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int MovieCategoryId { get; set; }

        [Required(ErrorMessage = "Cinema Hall is required")]
        [Display(Name = "Cinema")]
        public int CinemaHallId { get; set; }
        [Required(ErrorMessage = "Producer is required")]
        [Display(Name = "Producer")]
        public int ProducerId { get; set; }
        [Required(ErrorMessage = "Actor is required")]
        [Display(Name = "Actors")]
        public List<int> ActorIds { get; set; } = new List<int>();

    }
    public class MovieDetailsViewModel
    {
        public int Id { get; set; }
        public string ImageSource { get; set; }
        
        public string Name { get; set; }
  
        public string Description { get; set; }
        
        public decimal TicketPrice { get; set; }

        public string StartDate { get; set; }
      
        public string EndDate { get; set; }
       
        public string MovieCategory { get; set; }
   

       
        public string CinemaHall { get; set; }
        public int CinemaHallId { get; set; }
        
        public string Producer { get; set; }
        public int ProducerId { get; set; }
        public string Status { get; set; }

        public List<ActorModel> Actors { get; set; } = new List<ActorModel>();
    }
    public class ActorModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }



}
