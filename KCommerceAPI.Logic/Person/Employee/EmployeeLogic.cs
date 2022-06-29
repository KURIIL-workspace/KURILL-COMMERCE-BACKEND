using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Common;
using KCommerceAPI.Logic.Common.AbstractLogic;
using KCommerceAPI.Logic.Exception;
using Microsoft.EntityFrameworkCore;
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

            employee.Code = await GetNextDocumentCode(DocumentType.EMPLOYEE, kComDbContext.Employees.AsNoTracking().Count());
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                               TransactionScopeAsyncFlowOption.Enabled))
            {
                employee.StatusId = 1;
                

                kComDbContext.Employees.Add(employee);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }
            return employee.Id;
        }
        public async Task DeleteAsync(Guid id)
        {
            var employee = await kComDbContext.Employees.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (employee == null)
            {
                throw new NotFoundException();
            }
            try
            {
                kComDbContext.Employees.Remove(employee);
                await kComDbContext.SaveChangesAsync();
            }
            catch
            {
                throw new System.Exception($"Unable to delete this employee, Employee({id}) has been used in somewhere else");
            }
        }

        public async Task<int> GetRecordCount()
        {
            try
            {
                return await kComDbContext.Employees.CountAsync();
            }
            catch
            {
                return 0;
            }
        }

        public async Task UpdateAsync(Guid employeeId, dbCore.Employee employee)
        {
            var existingEmployee = await kComDbContext.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();

            if (existingEmployee != null)
            {
                throw new NotFoundException();
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                              TransactionScopeAsyncFlowOption.Enabled))
            {
                existingEmployee.Code = employee.Code;
                existingEmployee.FirstName = employee.FirstName;
                existingEmployee.LastName = employee.LastName;
                existingEmployee.BirthDate = employee.BirthDate;
                existingEmployee.Contact = employee.Contact;
                existingEmployee.StatusId = employee.StatusId;

                await kComDbContext.Employees.AddAsync(existingEmployee);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }

            
        }

        public async Task UpdateStatus(Guid employeeId, short statusId)
        {

            var employee = await kComDbContext.Employees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();

            if (employee == null)
            {
                throw new NotFoundException();
            }

            employee.StatusId = statusId;
            //employee.UpdatedDateTime = DateTime.Now;

            kComDbContext.Employees.Update(employee);
            await kComDbContext.SaveChangesAsync();

        }
    }
}
