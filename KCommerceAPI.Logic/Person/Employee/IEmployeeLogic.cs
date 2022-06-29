using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Person.Employee
{
    public interface IEmployeeLogic
    {
        Task<Guid> AddNewAsync(dbCore.Employee employee);
        Task UpdateStatus(Guid employeeId, short statusId);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid employeeId, dbCore.Employee employee);
        Task<int> GetRecordCount();
    }
}
