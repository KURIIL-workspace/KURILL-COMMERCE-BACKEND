using AutoMapper;
using dbCore = KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Models.Json.Input.Purchase.PurchaseOrder;

namespace KCommerceAPI.Mappers
{
    public class PurchaseRelatedMapper : Profile
    {
        public PurchaseRelatedMapper()
        {
            CreateMap<PurchaseOrderInputJson, dbCore.PurchaseOrder>();

        }
        
    }
}
