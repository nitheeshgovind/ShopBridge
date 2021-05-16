using ShopBridge.Application.Abstractions;
using ShopBridge.Application.Common.Exceptions;
using ShopBridge.Domain.Models;
using ShopBridge.WebApi.Extensions;
using ShopBridge.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ShopBridge.WebApi.Controllers
{
    public class InventoryController : ApiController
    {
        private readonly IInventoryRepository repository;

        public InventoryController(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetInventoriesAsync()
        {
            return Ok(await repository.GetInventoryAsync());
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetInventoryAsync(int id)
        {
            try
            {
                var inventory = await repository.GetInventoryAsync(id);

                return Ok(inventory);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/inventory/add")]
        public async Task<IHttpActionResult> AddInventoryAsync([FromBody] InventoryModel inventory)
        {
            if (ModelState.IsValid)
                return Ok(await repository.AddInventoryAsync(inventory));
            else
                return Content(HttpStatusCode.BadRequest, ModelState.GetErrorMessage());

        }

        [HttpPost]
        [Route("api/inventory/update")]
        public async Task<IHttpActionResult> UpdateInventoryAsync([FromBody] InventoryModel inventory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateInventoryAsync(inventory);
                    return Ok();
                }
                catch (NotFoundException)
                {
                    return NotFound();
                }                
            }
            else
                return Content(HttpStatusCode.BadRequest, ModelState.GetErrorMessage());
        }


        [HttpPost]
        [Route("api/inventory/delete")]
        [Filters.ValiadationFilter]
        public async Task<IHttpActionResult> DeleteInventoryAsync([FromBody] int id)
        {
            try
            {
                await repository.DeleteInventoryAsync(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

        }
    }
}
