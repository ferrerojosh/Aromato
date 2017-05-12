using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [Authorize("inventory.read")]
        [HttpGet]
        public IEnumerable<IData> Index()
        {
            return _inventoryService.RetrieveAll();
        }

        [Authorize("inventory.read")]
        [HttpGet("{id}")]
        public IData Get(long id)
        {
            return _inventoryService.RetrieveById(id);
        }

        [Authorize("inventory.write")]
        [HttpPost]
        public void Create([FromBody] InventoryWebData value)
        {
            _inventoryService.CreateInventory(value);
        }

        [Authorize("inventory.write")]
        [HttpPut("{id}/item")]
        public void AddItem(long id, [FromBody] ItemWebData item)
        {
            _inventoryService.AddItemToInventory(id, item);
        }
    }
}