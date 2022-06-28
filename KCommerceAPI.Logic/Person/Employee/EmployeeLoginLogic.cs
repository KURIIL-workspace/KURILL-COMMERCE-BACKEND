using KCommerceAPI.DataAccess.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCommerceAPI.Logic.Person.Employee
{
    public class EmployeeLoginLogic : IEmployeeLoginLogic
    {
        public Task<Guid> AddNewAsync(EmployeeLogin employeeLogin, string password)
        {
            throw new NotImplementedException();
            //implement the logic and connect witrh transaction scope
        }

        public Task DeleteAsync(Guid employeeLoginId)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeLogin> FindByUserIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeLogin> GetEmployeeLoginByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
