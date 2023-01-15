using CloudCustomers.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.Interface
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
