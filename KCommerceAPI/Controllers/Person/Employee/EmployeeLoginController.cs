using AutoMapper;
using KCommerceAPI.Common;
using KCommerceAPI.DataAccess.EfCore;
using KCommerceAPI.Logic.Person.Employee;
using KCommerceAPI.Models.Json.Input.Person.Employee;
using KCommerceAPI.Models.Json.Result.Person.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KCommerceAPI.Controllers
{
    [Route("api/employee_login")]
    [ApiController]
    [Produces("application/json")]
    //[ProducesErrorResponseType(typeof(ErrorResultJson))]
    [Authorize]
    public class EmployeeLoginController : ControllerBase
    {
       
        
        private readonly IEmployeeLoginLogic employeeLoginLogic;
        private readonly UserAuthenticationHandler userAuthenticationHandler;
        private readonly IMapper mapper;

        public EmployeeLoginController(IMapper mapper, IEmployeeLoginLogic employeeLoginLogic, UserAuthenticationHandler userAuthenticationHandler)
        {
            this.mapper = mapper;
            this.employeeLoginLogic = employeeLoginLogic;
            this.userAuthenticationHandler = userAuthenticationHandler;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddNewEmployeeLogin([FromBody] EmployeeLoginInputJson employeeLoginInputJson)
        {
            var employeeLogin = mapper.Map<EmployeeLogin>(employeeLoginInputJson);

            //entrypt password

            employeeLogin.Password = BCrypt.Net.BCrypt.HashPassword(employeeLogin.Password);

            var savedEmployeeLoginId = await employeeLoginLogic.AddNewAsync(employeeLogin, employeeLoginInputJson.Password);

            return Created("", savedEmployeeLoginId);
        }

        /*[HttpPatch("{employeeLoginId}/make_active")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> MakeActive([FromRoute(Name = "employeeLoginId")] Guid employeeLoginId)
        {
            await employeeLoginLogic.MakeActiveAsync(employeeLoginId);

            return NoContent();
        }*/

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> GetUser([FromRoute(Name = "id")] Guid id)
        {
            var existingUser = await employeeLoginLogic.FindByUserIdAsync(id);
            var user = mapper.Map<EmployeeLoginResultJson>(existingUser);
            return Ok(user);
        }

        /*[HttpPatch("{employeeLoginId}/make_inactive")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> MakeInactive([FromRoute(Name = "employeeLoginId")] Guid employeeLoginId)
        {
            await employeeLoginLogic.MakeInactiveAsync(employeeLoginId);

            return NoContent();
        }*/

        [HttpDelete("{employeeLoginId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Delete([FromRoute(Name = "employeeLoginId")] Guid employeeLoginId)
        {
            await employeeLoginLogic.DeleteAsync(employeeLoginId);

            return NoContent();
        }

        /// <summary>
        /// You can use this end point to request a password update. Once called, the user will receive an email from the Keycloak requesting
        /// to update the password. So by following email instructions, the user can update his/her password.
        /// </summary>
        /// <param name="employeePasswordUpdateRequestInputJson"></param>
        /// <param name="employeeLoginId"></param>
        /// <returns></returns>
        /*[HttpPut("{employeeLoginId}/request_password_update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RequestPasswordUpdate([FromBody] EmployeePasswordUpdateRequestInputJson employeePasswordUpdateRequestInputJson,
            [FromRoute(Name = "employeeLoginId")] Guid employeeLoginId)
        {
            await employeeLoginLogic.UpdatePassword(employeeLoginId, employeePasswordUpdateRequestInputJson.RedirectURL);

            return NoContent();
        }*/

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Login([FromBody] EmployeeUserLoginInputJson employeeUserLogin)
        {

            var existingUserLogin = await employeeLoginLogic.GetEmployeeLoginByUsername(employeeUserLogin.UserName);

            if (existingUserLogin == null)
                return BadRequest(error: "invalid username or password");

            if (!(BCrypt.Net.BCrypt.Verify(text: employeeUserLogin.Password, hash: existingUserLogin.Password)))
                return BadRequest(error: "invalid username or password");


            var jwtResult = userAuthenticationHandler.HandleSignInProcessAsync(existingUserLogin,
                Request.HttpContext.Connection.RemoteIpAddress.ToString());
            Response.Cookies.Append("access_token", jwtResult.AccessToken, new CookieOptions { HttpOnly = true });
            return Ok(jwtResult);
        }


        /*[HttpPost("change_password")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> ChangePassword([FromBody] EmployeeUerLoginInputJson employeeUserLogin)
        {

            var existingUserLogin = await employeeLoginLogic.GetEmployeeLoginByUsername(employeeUserLogin.Username);

            if (existingUserLogin == null)
                return BadRequest(error: "invalid username or password");

            if (!(BCrypt.Net.BCrypt.Verify(text: employeeUserLogin.password, hash: existingUserLogin.Password)))
                return BadRequest(error: "invalid username or password");


            var jwtResult = userAuthenticationHandler.HandleSignInProcessAsync(existingUserLogin,
                Request.HttpContext.Connection.RemoteIpAddress.ToString());
            Response.Cookies.Append("access_token", jwtResult.AccessToken, new CookieOptions { HttpOnly = true });
            return Ok(jwtResult);
        }*/
    
    }
}
