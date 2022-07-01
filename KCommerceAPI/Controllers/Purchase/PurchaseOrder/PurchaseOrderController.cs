using KCommerceAPI.Models.Json.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KCommerceAPI.Controllers.Purchase.PurchaseOrder
{
    [Route("api/purchase_invoice")]
    [ApiController]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(ErrorResultJson))]
    [Authorize]
    public class PurchaseOrderController : ControllerBase
    {

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
       // public Task<IActionResult> AddNew([FromBody])
    }
}
