using AutoMapper;
using KCommerceAPI.Logic.Purchase.PurchaseOrder;
using KCommerceAPI.Models.Json.Input.Purchase.PurchaseOrder;
using KCommerceAPI.Models.Json.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Controllers.Purchase.PurchaseOrder
{
    [Route("api/purchase_order")]
    [ApiController]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(ErrorResultJson))]
    [Authorize]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPurchaseOrderLogic purchaseOrderLogic;
        public PurchaseOrderController(IMapper mapper, IPurchaseOrderLogic purchaseOrderLogic)
        {
            this.mapper = mapper;
            this.purchaseOrderLogic = purchaseOrderLogic;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddNew([FromBody] PurchaseOrderInputJson purchaseOrderInputJson) 
        {
            var purchaseOrder = mapper.Map<dbCore.PurchaseOrder>(purchaseOrderInputJson);
            purchaseOrder.PurchaseOrderItems = purchaseOrderInputJson.Items.Select(x => mapper.Map<dbCore.PurchaseOrderItem>(x)).ToList();
            var purchaseOrderId = await purchaseOrderLogic.addNewAsync(purchaseOrder);

            return Created("",purchaseOrderId.ToString());
        
        }

    }
}
