using Microsoft.AspNetCore.Identity;

namespace BloggieDemoApplication.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
