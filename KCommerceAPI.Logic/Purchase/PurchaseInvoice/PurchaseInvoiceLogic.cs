using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Common.AbstractLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Purchase.PurchaseInvoice
{
    public class PurchaseInvoiceLogic : AbstractLogic, IPurchaseInvoiceLogic
    {
        public PurchaseInvoiceLogic(KComDbContext kComDbContext) : base(kComDbContext)
        {
        }

        public async Task<Guid> addNewAsync(dbCore.PurchaseInvoice purchaseInvoice)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                              TransactionScopeAsyncFlowOption.Enabled))
            {
                purchaseInvoice.StatusId = 1;
                purchaseInvoice.CreatedDateTime = DateTime.Now;
                purchaseInvoice.UpdatedDateTime = DateTime.Now;

                kComDbContext.PurchaseInvoices.Add(purchaseInvoice);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }
            return purchaseInvoice.Id;
        }
    }
}
