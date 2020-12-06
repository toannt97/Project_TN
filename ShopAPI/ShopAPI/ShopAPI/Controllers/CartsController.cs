using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;
using ShopAPI.Models.Responses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly OnlineShopContext _context;

        public CartsController(OnlineShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCart>>> GetCartItems()
        {
            return await _context.ShoppingCarts.Where(s => s.Status == 0)
                                              .Include(p => p.Product)
                                              .OrderBy(s => s.Product.Name)
                                              .ToListAsync();
        }

        // GET api/<CartController>/5
        [HttpGet("{idUser}")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCartItems(int idUser)
        {
            var result = await _context.ShoppingCarts.Where(s => s.Status != -1)
                                             .Select(i => new CartItemDTO
                                             {
                                                 ProductId = i.Product.Id,
                                                 ProductImage = i.Product.Image,
                                                 ProductInformation = i.Product.Information,
                                                 ProductName = i.Product.Name,
                                                 ProductQuantityAvailable = i.Product.Quantity,
                                                 Quantity = i.Quantity,
                                                 SupplierName = i.Product.Supplier.Name,
                                                 UnitPrice = i.Product.UnitPrice,
                                             })
                                             .OrderBy(s => s.ProductName)
                                             .ToListAsync();
            return result;
        }

        // POST api/<CartController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
