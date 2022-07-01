using AutoMapper;
using KCommerceAPI.Logic.Person.Supplier;
using KCommerceAPI.Models.Json.Input.Person.Supplier;
using KCommerceAPI.Models.Json.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Controllers.Person.Supplier
{
    [Route("api/suppliers")]
    [ApiController]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(ErrorResultJson))]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ISupplierLogic supplierLogic;
        public SupplierController(IMapper mapper, ISupplierLogic supplierLogic)
        {
            this.mapper = mapper;
            this.supplierLogic = supplierLogic;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddNew([FromBody] SupplierInputJson supplierInputJson) 
        { 
            var supplier = mapper.Map<dbCore.Supplier>(supplierInputJson);
            supplier.Addresses = supplierInputJson.Addresses.Select(i=>mapper.Map<dbCore.Address>(i)).ToList();
            var supplierId = await supplierLogic.AddNewAsync(supplier);
            return Created("", supplierId.ToString());
        }
    }
}
