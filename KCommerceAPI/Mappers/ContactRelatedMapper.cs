using AutoMapper;
using KCommerceAPI.Models.Json.Input.Contact.Address;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Mappers
{
    public class ContactRelatedMapper:Profile
    {
        public ContactRelatedMapper()
        {
            CreateMap<AddressInputJson, dbCore.Address>();
        }
    }
}
