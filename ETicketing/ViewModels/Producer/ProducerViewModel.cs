using System.ComponentModel.DataAnnotations;

namespace ETicketing.ViewModels.Producer
{
    public class ProducerIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
    public class ProducerCreateViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class ProducerUpdateViewModel : ProducerCreateViewModel
    {
        public int Id { get; set; }
        public string? ImageSource { get; set; }
    }

    public class ProducerDetailsViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }

}
