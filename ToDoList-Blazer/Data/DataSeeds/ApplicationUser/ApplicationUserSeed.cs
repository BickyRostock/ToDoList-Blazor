using System.Threading.Tasks;

namespace ToDoList_Blazer.Data.DataSeeds.ApplicationUser
{
    public class ApplicationUserSeed
    {
        public static Data.ApplicationUser[] ApplicationUsersSeed => new Data.ApplicationUser[]
        {
            new Data.ApplicationUser{ Id = "1", EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 },
            new Data.ApplicationUser{ Id = "2", EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 },
            new Data.ApplicationUser{ Id = "3", EmailConfirmed = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 },
        };

        public static async Task<int> InitialiseAsync(ApplicationDbContext context)
        {
            await context.AddRangeAsync(ApplicationUsersSeed);

            return await context.SaveChangesAsync();
        }
    }
}
