using dbCore = KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Common.AbstractLogic;
using KCommerceAPI.Logic.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using KCommerceAPI.DataAccess.EfCore;
using Microsoft.EntityFrameworkCore;

namespace KCommerceAPI.Logic.Person.Employee
{
    public class EmployeeLoginLogic : AbstractLogic, IEmployeeLoginLogic
    {
        public EmployeeLoginLogic(KComDbContext kComDbContext) : base(kComDbContext)
        {
        }

        public async Task<Guid> AddNewAsync(EmployeeLogin employeeLogin, string password)
        {
            await this.ValidateUserName(employeeLogin.UserName);
           // await this.ValidateEmail(employeeLogin.AccountRelatedCommunicationEmail);

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                TransactionScopeAsyncFlowOption.Enabled))
            {
                var employee = await kComDbContext.Employees.Where(e => e.Id == employeeLogin.EmployeeId).FirstOrDefaultAsync();

                if (employee == null)
                    throw new NotFoundException($"Employee {employeeLogin.EmployeeId} not found");

                //employeeLogin.ActivatedDate = DateTime.Now;
                //employeeLogin.CreatedDateTime = DateTime.Now;
                //employeeLogin.UpdatedDateTime = DateTime.Now;

                await kComDbContext.AddAsync(employeeLogin);
                await kComDbContext.SaveChangesAsync();

                scope.Complete();
            }

            return employeeLogin.Id;
        }

        private async Task ValidateUserName(string userName)
        {
            var userNameExists = await kComDbContext.EmployeeLogins
                .Where(el => el.UserName.Trim().ToLower() == userName.Trim().ToLower()).AnyAsync();

            if (userNameExists == true)
            {
                throw new ApplicationLogicException($"The user name '{userName}' already exists in the system.");
            }
        }

        /*private async Task ValidateEmail(string email)
        {
            var userNameExists = await kComDbContext.EmployeeLogins
                .Where(el => el..Trim().ToLower() == email.Trim().ToLower()).AnyAsync();

            if (userNameExists == true)
            {
                throw new ApplicationLogicException($"The Email '{email}' already exists in the system.");
            }
        }*/

        public async Task DeleteAsync(Guid employeeLoginId)
        {
            var existingEmployeeLogin = await kComDbContext.EmployeeLogins.Where(el => el.Id == employeeLoginId).FirstOrDefaultAsync();

            if (existingEmployeeLogin == null)
            {
                throw new NotFoundException($"Unable to find an employee login with the id '{employeeLoginId}'");
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                TransactionScopeAsyncFlowOption.Enabled))
            {
                kComDbContext.EmployeeLogins.Remove(existingEmployeeLogin);

                await kComDbContext.SaveChangesAsync();

                scope.Complete();
            }
        }

        public async Task<EmployeeLogin> FindByUserIdAsync(Guid id)
        {
            var employeeLogins = await kComDbContext.EmployeeLogins.Where(el => el.Id == id).FirstOrDefaultAsync();

            if(employeeLogins == null)return null;

            return employeeLogins;
        }

        public async Task<EmployeeLogin> GetEmployeeLoginByUsername(string username)
        {
            var employeeLogin = await kComDbContext.EmployeeLogins
                .Where(c => c.UserName.ToLower().Trim() == username.ToLower().Trim()).FirstOrDefaultAsync();
            return employeeLogin;
        }
    }
}
