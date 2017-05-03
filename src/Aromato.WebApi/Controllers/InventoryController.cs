using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/inventory")]
    public class InventoryController : Controller
    {

        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: api/Inventory
        [HttpGet]
        public IEnumerable<IData> GetInventories()
        {
            return _inventoryService.RetrieveAll();
        }

        // GET: api/Inventory/5
        [HttpGet("{id}")]
        public IData GetInventory(long id)
        {
            return _inventoryService.RetrieveById(id);
        }
        
        // POST: api/Inventory
        [HttpPost]
        public void CreateInventory([FromBody] InventoryWebData value)
        {
            _inventoryService.CreateInventory(value);
        }

        [HttpPut("{id}/additem")]
        public void AddItemToInventory(long id, [FromBody] ItemWebData item)
        {
            _inventoryService.AddItemToInventory(id, item);
        }
    }
}
