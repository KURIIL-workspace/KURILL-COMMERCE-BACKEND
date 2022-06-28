using AutoMapper;
using dbCore = KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Models.Json.Input.Person.Employee;

namespace KCommerceAPI.Mappers
{
    public class PersonRelatedMapper : Profile
    {
        public PersonRelatedMapper()
        {
            CreateMap<dbCore.Employee, EmployeeInputJson>();
            CreateMap<dbCore.EmployeeLogin, EmployeeLoginInputJson>();

        }
    }
}
