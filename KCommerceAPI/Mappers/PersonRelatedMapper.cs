using AutoMapper;
using dbCore = KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Models.Json.Input.Person.Employee;
using KCommerceAPI.Models.Json.Result.Person.Employee;

namespace KCommerceAPI.Mappers
{
    public class PersonRelatedMapper : Profile
    {
        public PersonRelatedMapper()
        {
            CreateMap<EmployeeInputJson, dbCore.Employee>();
            CreateMap<EmployeeLoginInputJson, dbCore.EmployeeLogin>();
            CreateMap<dbCore.EmployeeLogin, EmployeeLoginResultJson>();

        }
    }
}
