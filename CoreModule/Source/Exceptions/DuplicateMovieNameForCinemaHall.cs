using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Exceptions
{
    public class DuplicateMovieNameForCinemaHall:Exception
    {
        public DuplicateMovieNameForCinemaHall(string cinemaHall):base($"Duplicate movie name for cinema hall {cinemaHall} ")
        {

        }
    }
}
