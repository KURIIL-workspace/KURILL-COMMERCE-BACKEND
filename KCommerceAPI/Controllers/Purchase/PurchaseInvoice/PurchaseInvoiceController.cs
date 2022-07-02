using AutoMapper;
using KCommerceAPI.Logic.Purchase.PurchaseInvoice;
using KCommerceAPI.Models.Json.Input.Purchase.PurchaseInvoice;
using KCommerceAPI.Models.Json.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dbCore = KCommerceAPI.DataAccess.EfCore;

namespace KCommerceAPI.Controllers.Purchase.PurchaseInvoice
{
    [Route("api/purchase_invoice")]
    [ApiController]
    [Produces("application/json")]
    [ProducesErrorResponseType(typeof(ErrorResultJson))]
    [Authorize]
    public class PurchaseInvoiceController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPurchaseInvoiceLogic purchaseInvoiceLogic;
        public PurchaseInvoiceController(IMapper mapper, IPurchaseInvoiceLogic purchaseInvoiceLogic)
        {
            this.mapper = mapper;
            this.purchaseInvoiceLogic = purchaseInvoiceLogic;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> AddNew([FromBody] PurchaseInvoiceInputJson purchaseInvoiceInputJson)
        {
            var purchaseInvoice = mapper.Map<dbCore.PurchaseInvoice>(purchaseInvoiceInputJson);
            purchaseInvoice.PurchaseInvoiceItems = purchaseInvoiceInputJson.Items.Select(x => mapper.Map<dbCore.PurchaseInvoiceItem>(x)).ToList();
            var purchaseInvoiceId = await purchaseInvoiceLogic.addNewAsync(purchaseInvoice);

            return Created("", purchaseInvoiceId.ToString());
        }
    }
}
