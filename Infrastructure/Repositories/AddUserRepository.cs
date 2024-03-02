using Domain.Entities;
using Domain.IRepositories;
using Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AddUserRepository : IAddUserRepository
    {

        //Ctor
        #region Ctor
        private readonly DataContext _datacontext;

        public AddUserRepository(DataContext dataContext)
        {
            _datacontext = dataContext;
        }

        #endregion

        //AddUser
        #region AddUser
        public async Task<bool> AddUser(User user)
        {
            if (!_datacontext.Users.Any(p => p.UserName == user.UserName || p.Phone == user.Phone))
            {
                _datacontext.Users.Add(user);
               await _datacontext.SaveChangesAsync();


                return true;
            }
            else           
                return false;
            
        }

        #endregion

    }
}
