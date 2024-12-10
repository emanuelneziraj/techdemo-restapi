using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private static List<Item> items = new List<Item>
        {
            new Item { Name = "Apple", Price = 0.99M },
            new Item { Name = "Banana", Price = 1.20M }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll() => Ok(items);

        [HttpGet("{id}")]
        public ActionResult<Item> GetById(Guid id)
        {
            var item = items.Find(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<Item> Create(Item newItem)
        {
            items.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Item updatedItem)
        {
            var item = items.Find(i => i.Id == id);
            if (item == null){
                return NotFound();
            }
            item.Name = updatedItem.Name;
            item.Price = updatedItem.Price;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var item = items.Find(i => i.Id == id);
            if (item == null){
                return NotFound();
            }            
            items.Remove(item);
            return NoContent();
        }
    }
}
