using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ILogInRepository 
    {
        Task<Boolean> LogIn(User user);

    }


}
