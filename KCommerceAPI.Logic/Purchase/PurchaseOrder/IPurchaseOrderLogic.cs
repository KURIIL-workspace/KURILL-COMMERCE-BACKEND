using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Purchase.PurchaseOrder
{
    public interface IPurchaseOrderLogic
    {
        Task<Guid> addNewAsync(dbCore.PurchaseOrder purchaseOrder);
    }
}
