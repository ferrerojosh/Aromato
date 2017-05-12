using System.Collections.Generic;
using Aromato.Application;
using Aromato.Application.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aromato.Api.Controllers
{
    [Authorize(Roles = "sysad,wscholar")]
    [Produces("application/json")]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public IEnumerable<IData> Index()
        {
            return _inventoryService.RetrieveAll();
        }

        [HttpGet("{id}")]
        public IData Get(long id)
        {
            return _inventoryService.RetrieveById(id);
        }

        [HttpPost]
        public void Create([FromBody] InventoryWebData value)
        {
            _inventoryService.CreateInventory(value);
        }

        [HttpPut("{id}/item")]
        public void AddItem(long id, [FromBody] ItemWebData item)
        {
            _inventoryService.AddItemToInventory(id, item);
        }
    }
}