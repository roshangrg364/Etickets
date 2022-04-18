using System.ComponentModel.DataAnnotations;

namespace ETicketing.ViewModels.MovieCategory
{
    public class MovieCategoryIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MovieCategoryCreateViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
    }
    public class MovieCategoryUpdateViewModel : MovieCategoryCreateViewModel
    {
        public int Id { get; set; }
    }


}
