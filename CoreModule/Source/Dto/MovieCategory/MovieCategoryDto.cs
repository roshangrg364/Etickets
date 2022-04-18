using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Dto.MovieCategory
{
    public class MovieCategoryCreateDto
    {
        public MovieCategoryCreateDto(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }

    public class MovieCategoryUpdateDto : MovieCategoryCreateDto
    {
        public int Id { get; set; }
        public MovieCategoryUpdateDto(int id,string name):base(name)
        {
            Id = id;
        }
    }

}
