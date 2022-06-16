using KCommerceAPI.DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommerceAPI.Logic.Person.Employee
{
    public interface IEmployeeLoginLogic
    {
        Task<Guid> AddNewAsync(EmployeeLogin employeeLogin, string password);
        Task<EmployeeLogin> FindByUserIdAsync(Guid id);
        Task DeleteAsync(Guid employeeLoginId);
        Task<EmployeeLogin> GetEmployeeLoginByUsername(string username);
    }
}
