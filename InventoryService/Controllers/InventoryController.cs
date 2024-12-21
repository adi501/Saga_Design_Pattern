using InventoryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private static readonly List<Inventory> Inventories = new()
    {
        new Inventory { ProductId = "Product1", Stock = 100 },
        new Inventory { ProductId = "Product2", Stock = 200 }
    };
        [HttpPost("reserve")]
        public IActionResult ReserveInventory([FromBody] dynamic request)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(request.ToString());

            string productId = (string)data["productId"];
            int quantity = (int)data["quantity"];
            var inventory = Inventories.FirstOrDefault(i => i.ProductId == productId);
            if (inventory == null || inventory.Stock < quantity)
            {
                return BadRequest("Insufficient stock.");
            }
            inventory.Stock -= quantity;
            return Ok();
        }
        [HttpPost("release")]
        public IActionResult ReleaseInventory([FromBody] dynamic request)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(request.ToString());

            string productId = (string)data["productId"];
            int quantity = (int)data["quantity"];
            var inventory = Inventories.FirstOrDefault(i => i.ProductId == productId);
            if (inventory == null)
            {
                return NotFound();
            }

            inventory.Stock += quantity;
            return Ok();
        }
    }
}
