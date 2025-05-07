using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Data;

public class DbSeeder
{
    public static async Task SeedDefaultData(IServiceProvider service)
    {
        try
        {
            var context = service.GetService<ApplicationDbContext>();

            if ((await context.Database.GetPendingMigrationsAsync()).Count() > 0)
            {
                await context.Database.MigrateAsync();
            }

            var userMgr = service.GetService<UserManager<IdentityUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();

            var adminRoleExists = await roleMgr.RoleExistsAsync(Roles.Admin.ToString());

            if (!adminRoleExists)
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }

            var userRoleExists = await roleMgr.RoleExistsAsync(Roles.User.ToString());

            if (!userRoleExists)
            {
                await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }


            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var userInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }


            if (!context.Genres.Any())
            {
                await SeedGenreAsync(context);
            }

            if (!context.Books.Any())
            {
                await SeedBooksAsync(context);
                await context.Database.ExecuteSqlRawAsync(@"
                     INSERT INTO Stock(BookId,Quantity) 
                     SELECT 
                     b.Id,
                     10 
                     FROM Book b
                     WHERE NOT EXISTS (
                     SELECT * FROM [Stock]
                     );
           ");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    #region private methods

    private static async Task SeedGenreAsync(ApplicationDbContext context)
    {
        var genres = new[]
         {
            new Genre { GenreName = "Romance" },
            new Genre { GenreName = "Action" },
            new Genre { GenreName = "Thriller" },
            new Genre { GenreName = "Crime" },
            new Genre { GenreName = "SelfHelp" },
            new Genre { GenreName = "Programming" }
        };

        await context.Genres.AddRangeAsync(genres);
        await context.SaveChangesAsync();
    }

    private static async Task SeedBooksAsync(ApplicationDbContext context)
    {
     
    }

    #endregion
}
