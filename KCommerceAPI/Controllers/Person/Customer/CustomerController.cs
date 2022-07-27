using AutoMapper;
using KCommerceAPI.Logic.Person.Customer;
using KCommerceAPI.Models.Json.Input.Person.Customer;
using KCommerceAPI.Models.Json.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Controllers.Person.Customer
{
    [Route("api/customers")]
    [ApiController]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(ErrorResultJson))]
    [Authorize]

    public class CustomerController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICustomerLogic customerLogic;
        public CustomerController(IMapper mapper, ICustomerLogic customerLogic)
        {
            this.mapper = mapper;
            this.customerLogic = customerLogic;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddNew([FromBody] CustomerInputJson customerInputJson)
        {
            var customer = mapper.Map<dbCore.Customer>(customerInputJson);
            customer.Addresses = customerInputJson.Addresses.Select(i => mapper.Map<dbCore.Address>(i)).ToList();
            var customerId = await customerLogic.AddNewAsync(customer);
            return Created("", customerId.ToString());
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] Guid id)
        {
            await customerLogic.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(EmployeeResultJson), 200)]
        public async Task<IActionResult> Update([FromRoute(Name = "customerId")] Guid customerId, [FromBody] CustomerInputJson customerInputJson)
        {
            var customer = mapper.Map<dbCore.Customer>(customerInputJson);
            await customerLogic.UpdateAsync(customerId, customer);

            //var result = mapper.Map<EmployeeResultJson>();

            return Ok();
        }
    }
}
