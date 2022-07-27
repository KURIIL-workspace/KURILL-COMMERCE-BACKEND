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

namespace KCommerceAPI.Logic.Person.Customer
{
    public class CustomerLogic : AbstractLogic, ICustomerLogic
    {
        public CustomerLogic(KComDbContext kComDbContext) : base(kComDbContext)
        {
        }

        public async Task<Guid> AddNewAsync(dbCore.Customer customer)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                kComDbContext.Customers.Add(customer);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }
            return customer.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await kComDbContext.Customers.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (customer == null)
            {
                throw new NotFoundException();
            }
            try
            {
                kComDbContext.Customers.Remove(customer);
                await kComDbContext.SaveChangesAsync();
            }
            catch
            {
                throw new System.Exception($"Unable to delete this Customer, Customer({id}) has been used in somewhere else");
            }
        }

        public async Task UpdateAsync(Guid customerId, dbCore.Customer customer)
        {
            var existingCustomer = await kComDbContext.Customers.Where(x => x.Id == customerId).FirstOrDefaultAsync();

            if (existingCustomer != null)
            {
                throw new NotFoundException();
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                              TransactionScopeAsyncFlowOption.Enabled))
            {
                existingCustomer.CustomerName = customer.CustomerName;
                existingCustomer.CustomerContact = customer.CustomerContact;

                await kComDbContext.Customers.AddAsync(existingCustomer);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }


        }
    }
}
