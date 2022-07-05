using AutoMapper;
using KCommerceAPI.Logic.Purchase.PurchaseInvoice;
using KCommerceAPI.Models;
using KCommerceAPI.Models.Json.Input.Common;
using KCommerceAPI.Models.Json.Input.Purchase.PurchaseInvoice;
using KCommerceAPI.Models.Json.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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

        [HttpPatch("{id}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus([FromRoute(Name = "id")] Guid Id, [FromBody] UpdateStatusInputJson statusChangeInputJson)
        {
            var updateStatusModel = mapper.Map<UpdateStatusModel>(statusChangeInputJson);
            await purchaseInvoiceLogic.UpdatePurchaseInvoiceStatus(Id, updateStatusModel);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] Guid id)
        {
            await purchaseInvoiceLogic.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(EmployeeResultJson), 200)]
        public async Task<IActionResult> Update([FromRoute(Name = "id")] Guid id, [FromBody] PurchaseInvoiceInputJson purchaseInvoiceInputJson)
        {
            var purchaseInvoice = mapper.Map<dbCore.PurchaseInvoice>(purchaseInvoiceInputJson);
            await purchaseInvoiceLogic.UpdateAsync(id, purchaseInvoice);

            //var result = mapper.Map<EmployeeResultJson>(employee);

            return Ok();
        }
    }
}
