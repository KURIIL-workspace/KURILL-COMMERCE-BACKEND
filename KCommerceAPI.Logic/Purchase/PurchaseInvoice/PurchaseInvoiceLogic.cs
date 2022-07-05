using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Common.AbstractLogic;
using KCommerceAPI.Logic.Exception;
using KCommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task UpdatePurchaseInvoiceStatus(Guid id, UpdateStatusModel updateStatusModel)
        {

            var statuses = new[] { new { Id = 1, Name = "Pending" },
                new { Id = 2, Name = "Sent to Approval" },
                new { Id = 3, Name = "Approved" },
                new { Id = 4, Name = "Rejected" }
                };

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                             TransactionScopeAsyncFlowOption.Enabled))
            {

                var purchaseInvoice = await kComDbContext.PurchaseInvoices.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (purchaseInvoice == null)
                {
                    throw new NotFoundException();
                }


                if (purchaseInvoice.StatusId == 1)
                {
                    if (updateStatusModel.Status == 3 || updateStatusModel.Status == 5 || updateStatusModel.Status == 6 || updateStatusModel.Status == 7)
                    {
                        throw new System.Exception($"Purchase Invoice  status can not be updated. Pending to {statuses.First(x => x.Id == updateStatusModel.Status).Name}");
                    }
                }

                if (purchaseInvoice.StatusId == 2)
                {
                    if (updateStatusModel.Status == 1 || updateStatusModel.Status == 2 || updateStatusModel.Status == 5 || updateStatusModel.Status == 6 || updateStatusModel.Status == 7)
                    {
                        throw new System.Exception($"Material Purchase Invoice status can not be updated. Sent to Approval to {statuses.First(x => x.Id == updateStatusModel.Status).Name}");
                    }
                }

                if (purchaseInvoice.StatusId == 3)
                {
                    if (updateStatusModel.Status == 1 || updateStatusModel.Status == 2 || updateStatusModel.Status == 4 || updateStatusModel.Status == 6 || updateStatusModel.Status == 7)
                    {
                        throw new System.Exception($"Material Purchase Invoice status can not be updated. Approved to {statuses.First(x => x.Id == updateStatusModel.Status).Name}");
                    }
                }

                if (purchaseInvoice.StatusId == 4)
                {
                    if (updateStatusModel.Status == 1 || updateStatusModel.Status == 2 || updateStatusModel.Status == 3 || updateStatusModel.Status == 5 || updateStatusModel.Status == 6 || updateStatusModel.Status == 7)
                    {
                        throw new System.Exception($"Material Purchase Invoice status can not be updated. Rejected to {statuses.First(x => x.Id == updateStatusModel.Status).Name}");
                    }
                }


                purchaseInvoice.StatusId = updateStatusModel.Status;
                /*if (updateStatusModel.Status == 3)
                {
                    purchaseInvoice.ApprovedDate = DateTime.Now;
                    purchaseInvoice.ApprovedEmployeeId = updateStatusModel.EmployeeId;
                }
                if (updateStatusModel.Status == 3)
                {
                    purchaseInvoice.ApprovedEmployeeId = updateStatusModel.EmployeeId;
                }*/

                purchaseInvoice.UpdatedDateTime = DateTime.Now;
                //purchaseInvoice.Description = !string.IsNullOrEmpty(updateStatusModel.Description) ? updateStatusModel.Description : materialPurchaseInvoice.Description;

                kComDbContext.PurchaseInvoices.Update(purchaseInvoice);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var purchaseInvoice = await kComDbContext.PurchaseInvoices.Where(b => b.Id == id).FirstOrDefaultAsync();

            if (purchaseInvoice == null)
            {
                throw new NotFoundException();
            }
            try
            {
                kComDbContext.PurchaseInvoices.Remove(purchaseInvoice);
                await kComDbContext.SaveChangesAsync();
            }
            catch
            {
                throw new System.Exception($"Unable to delete this employee, Employee({id}) has been used in somewhere else");
            }
        }

        public async Task UpdateAsync(Guid id, dbCore.PurchaseInvoice purchaseInvoice)
        {
            var existingPInvoice = await kComDbContext.PurchaseInvoices.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (existingPInvoice != null)
            {
                throw new NotFoundException();
            }

            using (var scope = new TransactionScope(TransactionScopeOption.Required,
                              TransactionScopeAsyncFlowOption.Enabled))
            {
                existingPInvoice.UpdatedDateTime = purchaseInvoice.UpdatedDateTime;
                existingPInvoice.SupplierId = purchaseInvoice.SupplierId;
                existingPInvoice.PreparedEmployee = purchaseInvoice.PreparedEmployee;
                existingPInvoice.InvoiceDate = purchaseInvoice.InvoiceDate;
                existingPInvoice.PurchaseOrderId = purchaseInvoice.PurchaseOrderId;
                existingPInvoice.TotalItemQty = purchaseInvoice.TotalItemQty;
                existingPInvoice.TotalAmount = purchaseInvoice.TotalAmount;

                var exsistingItems = await kComDbContext.PurchaseInvoiceItems
                .Where(s => s.PurchaseInvoiceId == id).ToListAsync();

                purchaseInvoice.PurchaseInvoiceItems.ToList().ForEach((x) =>
                {
                    x.PurchaseInvoiceId = id;
                    //x.CreatedDateTime = x.CreatedDateTime != null ? x.CreatedDateTime : DateTime.Now;
                    //x.UpdatedDateTime = DateTime.Now;
                });

                if (exsistingItems.Any())
                {
                    kComDbContext.PurchaseInvoiceItems.RemoveRange(exsistingItems);
                }

                await kComDbContext.PurchaseInvoiceItems.AddRangeAsync(purchaseInvoice.PurchaseInvoiceItems);

                kComDbContext.PurchaseInvoices.Update(existingPInvoice);
                await kComDbContext.SaveChangesAsync();
                scope.Complete();
            }
        }
    }
}
