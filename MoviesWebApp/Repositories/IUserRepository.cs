using Microsoft.AspNetCore.Identity;

namespace ProductsWebApp.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
