using System.ComponentModel.DataAnnotations;

namespace ETicketing.ViewModels.Actor
{
    public class ActorIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    public class ActorCreateViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class ActorUpdateViewModel : ActorCreateViewModel
    {
        public int Id { get; set; }
        public string? ImageSource { get; set; }
    }

    public class ActorDetailsViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }


}
