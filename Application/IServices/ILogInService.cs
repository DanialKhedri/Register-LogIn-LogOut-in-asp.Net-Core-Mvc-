using Application.DTOs;

namespace Application.IServices
{
    public interface ILogInService 
    {
        public Task<bool> Login(UserLogInDTO userLogInDTO);
    }
}
