using System.ComponentModel.DataAnnotations;

namespace ETicketing.ViewModels.CinemalHall
{
    public class CinemaHallIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    public class CinemaHallCreateViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class CinemaHallUpdateViewModel : CinemaHallCreateViewModel
    {
        public int Id { get; set; }
        public string? ImageSource { get; set; }
    }

    public class CinemaHallDetailsViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }

}
