using CoreModule.DbContextConfig;
using CoreModule.Source.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.UOW
{
    public class UnitOfWork : UnitOfWorkInterface
    {
        private readonly MyDbContext _context;
        public UnitOfWork(MyDbContext context)
        {
            _context = context;
            Actors = new ActorRepository(_context);
            CinemaHalls = new CinemaHallRepository(_context);
            Producers = new ProducerRepository(_context);
            Categories = new MovieCategoryRepository(_context);
            Movies = new MovieRepository(_context);
            ActorMovies = new ActorMovieRepository(_context);
            CartItems = new CartRepository(_context);
            Users = new UserRepository(_context);
            ShippingAddress = new ShippingAddressRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public ActorRepositoryInterface Actors { get; }
        public UserRepositoryInterface Users { get; }
        public ActorMovieRepositoryInterface ActorMovies { get; }

        public CinemaHallRepositoryInterface CinemaHalls { get; }

        public ProducerRepositoryInterface Producers { get; }

        public MovieCategoryRespositoryInterface Categories { get; }
        public CartRepositoryInterface CartItems { get; }

        public MovieRepositoryInterface Movies { get; }

        public ShippingAddressRepositoryInterface ShippingAddress { get; }

        public OrderRepositoryInterface Orders { get; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
