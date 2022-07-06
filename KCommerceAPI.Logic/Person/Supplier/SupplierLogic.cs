using KCommerceAPI.DataAccess.EfCore;
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

        public async Task DeleteAsync(Guid id)
        {
            var supplier = await kComDbContext.Suppliers.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (supplier == null)
            {
                throw new NotFoundException();
            }
            try
            {
                kComDbContext.Suppliers.Remove(supplier);
                await kComDbContext.SaveChangesAsync();
            }
            catch
            {
                throw new System.Exception($"Unable to delete this Supplier, Supplier({id}) has been used in somewhere else");
            }
        }

        public async Task UpdateAsync(Guid supplierId, dbCore.Supplier supplier)
        {
            var existingSupplier = await kComDbContext.Suppliers.Where(x => x.Id == supplierId).FirstOrDefaultAsync();

            if (existingSupplier != null)
            {
                throw new NotFoundException();
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                              TransactionScopeAsyncFlowOption.Enabled))
            {
                existingSupplier.SupplierName = supplier.SupplierName;
                existingSupplier.SupplierContact = supplier.SupplierContact;

                await kComDbContext.Suppliers.AddAsync(existingSupplier);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }


        }
    }
}
