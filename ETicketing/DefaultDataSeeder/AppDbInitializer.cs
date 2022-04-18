using CoreModule.DbContextConfig;
using CoreModule.Source.Entity;
using Microsoft.AspNetCore.Identity;

namespace ETicketing.Extensions
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MyDbContext>();

                context.Database.EnsureCreated();

                //Cinema
                if (!context.Set<CinemaHall>().Any())
                {
                    var cinema1 = new CinemaHall("Cine Plaza", "Cine Plaza is the multiplex with more than 100 branches of it. One of the top cinema theatres");
                    cinema1.SetImage("Cineplaza.jpg");
                    context.Set<CinemaHall>().Add(cinema1);
                    var cinema2 = new CinemaHall("QFX Movies", "QFX is the multiplex with more than 100 branches of it. One of the top cinema theatres");
                    cinema2.SetImage("qfx.jpg");
                    context.Set<CinemaHall>().Add(cinema2);
                    var cinema3 = new CinemaHall("Big Movies", "Big Movies is the multiplex with more than 100 branches of it. One of the top cinema theatres");
                    cinema3.SetImage("bigmovies.jpg");
                    context.Set<CinemaHall>().Add(cinema3);
                    context.SaveChanges();
                }
                if(!context.Set<MovieCategory>().Any())
                {
                    context.Set<MovieCategory>().AddRange( new List<MovieCategory>{
                        new MovieCategory("Action"),
                        new MovieCategory("Thriller"),
                        new MovieCategory("Romance"),
                        new MovieCategory("Sci-fi"),
                    });
                    context.SaveChanges();
                }
                if (!context.Set<Actor>().Any())
                {
                    var actor1 = new Actor("Benedit Cumberbatch", "He is popular as Dr Strange");
                    actor1.SetImage("benedict.jpg");
                    context.Set<Actor>().Add(actor1);
                    var actor2 = new Actor("Chris Hemsworth", "He is popular asThor");
                    actor2.SetImage("chris.jpg");
                    context.Set<Actor>().Add(actor2);
                    var actor3 = new Actor("Tom Holland", "He is popular as spiderman");
                    actor3.SetImage("tomholland.jpg");
                    context.Set<Actor>().Add(actor3);
                    var actor4 = new Actor("Yash", "He is popular as Rocky Bhai");
                    actor4.SetImage("yash.jpg");
                    context.Set<Actor>().Add(actor4);

                    context.SaveChanges();
                }

                if(!context.Set<Producer>().Any())
                {
                    context.Set<Producer>().AddRange(new List<Producer>{
                  new Producer("Chris Nolan", "He is popular producer"),
                  new Producer("Karan Johar", "He is popular producer")
                    });
                    context.SaveChanges();
                }

               

            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MyDbContext>();

               // context.Database.EnsureCreated();

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(ApplicationUser.RoleAdmin))
                    await roleManager.CreateAsync(new IdentityRole(ApplicationUser.RoleAdmin));
                if (!await roleManager.RoleExistsAsync(ApplicationUser.RoleUser))
                    await roleManager.CreateAsync(new IdentityRole(ApplicationUser.RoleUser));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@admin.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = adminUserEmail
                    };
                    await userManager.CreateAsync(newAdminUser, "admin");
                    await userManager.AddToRoleAsync(newAdminUser, ApplicationUser.RoleAdmin);
                }


               
            }
        }
    }
}
