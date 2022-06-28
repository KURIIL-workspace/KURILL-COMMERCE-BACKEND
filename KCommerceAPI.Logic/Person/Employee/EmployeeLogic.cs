using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Common.AbstractLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Person.Employee
{
    public class EmployeeLogic : AbstractLogic,IEmployeeLogic
    {
        public EmployeeLogic(KComDbContext kComDbContext) : base(kComDbContext)
        {
        }

        public async Task<Guid> AddNewAsync(dbCore.Employee employee)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                               TransactionScopeAsyncFlowOption.Enabled))
            {
                employee.StatusId = 1;
                

                await kComDbContext.Employees.AddAsync(employee);
                await kComDbContext.SaveChangesAsync();

            }
            return employee.Id;
        }
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> GetRecordCount()
        {
            throw new NotImplementedException();
        }

        public Task<dbCore.Employee> UpdateAsync(Guid employeeId, dbCore.Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStatus(Guid employeeId, short statusId)
        {
            throw new NotImplementedException();
        }
    }
}
