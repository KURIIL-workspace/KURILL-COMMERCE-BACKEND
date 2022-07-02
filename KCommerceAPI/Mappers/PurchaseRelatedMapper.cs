using AutoMapper;
using dbCore = KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Models.Json.Input.Purchase.PurchaseOrder;
using KCommerceAPI.Models.Json.Input.Purchase.PurchaseInvoice;

namespace KCommerceAPI.Mappers
{
    public class PurchaseRelatedMapper : Profile
    {
        public PurchaseRelatedMapper()
        {
            CreateMap<PurchaseOrderInputJson, dbCore.PurchaseOrder>();
            CreateMap<PurchaseOrderItemInputJson, dbCore.PurchaseOrderItem>();

            CreateMap<PurchaseInvoiceInputJson, dbCore.PurchaseInvoice>();
            CreateMap<PurchaseInvoiceItemInputJson, dbCore.PurchaseInvoiceItem>();

        }
        
    }
}
