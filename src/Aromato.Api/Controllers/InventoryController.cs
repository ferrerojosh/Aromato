using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Api.Controllers
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

        [HttpGet]
        [Authorize("inventory.read")]
        public IEnumerable<IData> Index()
        {
            return _inventoryService.RetrieveAll();
        }

        [HttpGet("{id}")]
        [Authorize("inventory.read")]
        public IData Get(long id)
        {
            return _inventoryService.RetrieveById(id);
        }

        [HttpPost]
        [Authorize("inventory.write")]
        public void Create([FromBody] InventoryWebData value)
        {
            _inventoryService.CreateInventory(value);
        }

        [HttpPut("{id}/item")]
        [Authorize("inventory.write")]
        public void AddItem(long id, [FromBody] ItemWebData item)
        {
            _inventoryService.AddItemToInventory(id, item);
        }
    }
}