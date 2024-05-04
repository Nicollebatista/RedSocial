using Microsoft.AspNetCore.Identity;
using RedSocial.Infrastructure.Identity.Entities;
using System.Data;


namespace StockApp.Infrastructure.Identity.Seeds
{
    public static class DefaultBasicUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new();
            defaultUser.UserName = "basicuser";
            defaultUser.Email = "basicuser@email.com";
            defaultUser.FirstName = "John";
            defaultUser.LastName = "Doe";
            defaultUser.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR5MTqV9PscZL10R7CISRE6-o0pDHzk8etQWEEgKueNZw&s";
            defaultUser.EmailConfirmed = true;
            defaultUser.PhoneNumber = "8093188856";
            

            if (userManager.Users.All(u=> u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");

                }
            }
         
        }
    }
}
