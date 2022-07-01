using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Common.AbstractLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Purchase.PurchaseOrder
{
    public class PurchaseOrderLogic : AbstractLogic,IPurchaseOrderLogic
    {
        public PurchaseOrderLogic(KComDbContext kComDbContext) : base(kComDbContext)
        {
        }

        public async Task<Guid> addNewAsync(dbCore.PurchaseOrder purchaseOrder)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                              TransactionScopeAsyncFlowOption.Enabled))
            {
                purchaseOrder.StatusId = 1;
                purchaseOrder.CreatedDateTime = DateTime.Now;
                purchaseOrder.UpdatedDateTime = DateTime.Now;

                kComDbContext.PurchaseOrders.Add(purchaseOrder);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }
            return purchaseOrder.Id;
        }
    }
}
