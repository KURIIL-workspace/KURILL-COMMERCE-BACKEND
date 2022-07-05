using KCommerceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Logic.Purchase.PurchaseInvoice
{
    public interface IPurchaseInvoiceLogic
    {
        Task<Guid> addNewAsync(dbCore.PurchaseInvoice purchaseInvoice);
        Task UpdatePurchaseInvoiceStatus(Guid id, UpdateStatusModel updateStatusModel);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Guid id, dbCore.PurchaseInvoice purchaseInvoice);
    }
}
