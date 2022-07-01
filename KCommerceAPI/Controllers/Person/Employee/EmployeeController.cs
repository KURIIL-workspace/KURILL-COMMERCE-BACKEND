using AutoMapper;
using KCommerceAPI.Logic.Person.Employee;
using KCommerceAPI.Models.Json.Input.Person.Employee;
using KCommerceAPI.Models.Json.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Controllers.Person.Employee
{
    [Route("api/employees")]
    [ApiController]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(ErrorResultJson))]
    [Authorize]
    
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmployeeLogic employeeLogic;

        public EmployeeController(IMapper mapper, IEmployeeLogic employeeLogic)
        {
            this.mapper = mapper;
            this.employeeLogic = employeeLogic;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddNew([FromBody] EmployeeInputJson employeeInputJson)
        {
            var employee = mapper.Map<dbCore.Employee>(employeeInputJson);
            var employeeId = await employeeLogic.AddNewAsync(employee);
            return Created("", employeeId.ToString());
        }

        /// <summary>
        /// update employee status 
        /// Excepts the following json body {  "status" :  valid employee status id }
        /// </summary>
        /// <example>
        ///    {  "status" :  valid employee status id }
        /// </example>
        /// <param name="employeeId"></param>
        /// <param name="jsonEle"></param>
        /// <returns></returns>
        [HttpPatch("{employeeId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus([FromRoute(Name = "employeeId")] Guid employeeId, [FromBody] JsonElement jsonEle)
        {
            var statusId = jsonEle.GetProperty("status").GetString();
            await employeeLogic.UpdateStatus(employeeId, Convert.ToInt16(statusId));

            return Ok();
        }

        /// <summary>
        /// delete by employee id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] Guid id)
        {
            await employeeLogic.DeleteAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Update employee details by id 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employeeInputJson"></param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(EmployeeResultJson), 200)]
        public async Task<IActionResult> Update([FromRoute(Name = "employeeId")] Guid employeeId, [FromBody] EmployeeInputJson employeeInputJson)
        {
            var employee = mapper.Map<dbCore.Employee>(employeeInputJson);
             await employeeLogic.UpdateAsync(employeeId, employee);

            //var result = mapper.Map<EmployeeResultJson>(employee);

            return Ok();
        }
        /// <summary>
        /// Get count of employees
        /// </summary>
        /// <returns></returns>
        [HttpGet("record_count")]
        public async Task<IActionResult> GetRowCount()
        {
            var count = await employeeLogic.GetRecordCount();

            return Ok(new { record_count = count });
        }
    }
}
