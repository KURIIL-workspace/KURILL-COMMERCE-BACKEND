using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Common.AbstractLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Person.Supplier
{
    public class SupplierLogic : AbstractLogic,ISupplierLogic
    {
        public SupplierLogic(KComDbContext kComDbContext): base(kComDbContext)
        {
        }

        public async Task<Guid> AddNewAsync(dbCore.Supplier supplier)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                kComDbContext.Suppliers.Add(supplier);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }
            return supplier.Id;
        }
    }
}
