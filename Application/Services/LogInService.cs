using Application.DTOs;
using Application.IServices;
using Application.Security;
using Domain.Entities;
using Domain.IRepositories;

namespace Application.Services
{
    public class LogInService : ILogInService
    {
        #region Ctor
        private readonly ILogInRepository _ILogInRepository;

        public LogInService(ILogInRepository ILogInRepository)
        {
            _ILogInRepository = ILogInRepository;
        }
        #endregion


        public async Task<bool> Login(UserLogInDTO userLogInDTO)
        {

            //Object Mapping

            User user = new User
            {
                UserName = userLogInDTO.UserName,
                PassWord = PasswordHasher.EncodePasswordMd5(userLogInDTO.PassWord) ,
            };


            //Send ToRepository and Recive Result
            var IsSucces = await _ILogInRepository.LogIn(user);

            return IsSucces;

        }
    }
}
