using ShopBridgeApi.Filter;
using ShopBridgeData;
using ShopBridgeData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopBridgeApi.Controllers
{
    /// <summary>
    /// Inventory API
    /// </summary>
    [CustomExceptionFilter]
    [JwtAuthentication]
    public class InventoryController : ApiController
    {
        private readonly IInventoryRepository _inventoryRepository; //= new InventoryRepository();

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        // GET api/inventory
        public IHttpActionResult Get()
        {
            var inventories =  _inventoryRepository.GetAllInventories();
            if (inventories == null)
            {
                return Content(HttpStatusCode.NotFound, "Record not found in inventory.");
            }
            return Ok(inventories);                 
        }

        // GET api/inventory/id
        public IHttpActionResult Get(int id)
        {
            if(id == 0)
              return Content(HttpStatusCode.BadRequest, "Inventory id is not passed.");
            var inventory = _inventoryRepository.GetInventoryById(id);
            if(inventory == null)
            {
                return Content(HttpStatusCode.NotFound, "Record not found in inventory.");
            }
            return Ok(inventory);
        }

        // POST api/inventory
        public IHttpActionResult Post([FromBody]Inventory inventory)
        {
            try
            {
                _inventoryRepository.AddInventory(inventory);
                return Ok();
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, "Unable to add record in inventory.");
            }
        }

        // PUT api/inventory/id
        public IHttpActionResult Put(int id, [FromBody]Inventory inventory)
        {
            if (id == 0)
                return Content(HttpStatusCode.BadRequest, "Inventory id is not passed.");
            try
            {
                _inventoryRepository.UpdateInventory(inventory);
                return Ok();
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Unable to update record in inventory.");
            }          
        }

        // DELETE api/inventory/id
        public IHttpActionResult Delete(int id)
        {
            if (id == 0)
                return Content(HttpStatusCode.BadRequest, "Inventory id is not passed.");
            try
            {
                _inventoryRepository.DeleteInvetory(id);
                return Ok();
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "Unable to add record in inventory.");
            }            
        }
    }
}
