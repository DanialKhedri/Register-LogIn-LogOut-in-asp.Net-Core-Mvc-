using Application.DTOs;
using Application.IServices;
using Application.Security;
using Domain.Entities;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AddUserService : IAddUserService
    {


        //Ctor
        #region Ctor
        private readonly IAddUserRepository _Iadduserrepository;

        public AddUserService(IAddUserRepository addUserRepository)
        {
            _Iadduserrepository = addUserRepository;

        }
        #endregion

        //AddUser
        #region addUser
        public async Task<bool> AddUser(UserRegisterDTO userRegisterDTO)
        {

            User user = new User
            {
                UserName = userRegisterDTO.UserName,
                PassWord = PasswordHasher.EncodePasswordMd5(userRegisterDTO.Password),
                Email = userRegisterDTO.Email,
                Phone = userRegisterDTO.Phone,
            };




            return await _Iadduserrepository.AddUser(user);
        }
        #endregion

    
    }
}
