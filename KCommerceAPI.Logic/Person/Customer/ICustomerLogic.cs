using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Person.Customer
{
    public interface ICustomerLogic
    {
        Task<Guid> AddNewAsync(dbCore.Customer customer);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid customerId, dbCore.Customer customer);
    }
}
