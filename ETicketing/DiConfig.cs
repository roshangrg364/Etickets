
using CoreModule.Source.Repository;
using CoreModule.Source.Service;
using CoreModule.Source.Service.Image;
using CoreModule.UOW;
using ETicketing.Helper;
namespace ETicketing
{
    public static class DiConfig
    {
        public static void UseETicketing(this IServiceCollection services)
        {
            services.AddTransient<UnitOfWorkInterface, UnitOfWork>();
            services.AddScoped<FilerHelperInterface, FileHelper>();
            services.AddScoped<ImageUploaderInterface, ImageUploader>();
            UseRepository(services);
            UserService(services);
        }
        private static void UseRepository(this IServiceCollection services)
        {
            services.AddScoped<ActorRepositoryInterface, ActorRepository>();
            services.AddScoped<CinemaHallRepositoryInterface, CinemaHallRepository>();
            services.AddScoped<MovieCategoryRespositoryInterface, MovieCategoryRepository>();
            services.AddScoped<ProducerRepositoryInterface, ProducerRepository>();
            services.AddScoped<ActorMovieRepositoryInterface, ActorMovieRepository>();
            services.AddScoped<UserRepositoryInterface, UserRepository>();
            services.AddScoped<CartRepositoryInterface, CartRepository>();
            services.AddScoped<ShippingAddressRepositoryInterface, ShippingAddressRepository>();
            services.AddScoped<OrderRepositoryInterface, OrderRepository>();
           
        }
        private static void UserService(this IServiceCollection services)
        {
            services.AddScoped<ActorServiceInterface, ActorService>();
            services.AddScoped<ProducerServiceInterface, ProducerService>();
            services.AddScoped<MovieCategoryServiceInterface, MovieCategoryService>();
            services.AddScoped<CinemaHallServiceInterface, CinemaHallService>();
            services.AddScoped<MovieServiceInterface, MovieService>();
            services.AddScoped<UserServiceInterface, UserService>();
            services.AddScoped<CartServiceInterface, CartService>();
            services.AddScoped<CheckoutServiceInterface, CheckoutService>();
        }
    }
}
