using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Person.Supplier
{
    public interface ISupplierLogic
    {
        Task<Guid> AddNewAsync(dbCore.Supplier supplier);
    }
}
