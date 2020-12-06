using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Constants;
using ShopAPI.Models;
using ShopAPI.Models.Requests;
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

        [HttpGet("GetNumOfCartItems/{userId}")]
        public async Task<ActionResult<NumOfCartItemsDTO>> GetNumOfCartItems(int userId)
        {
            try
            {
                var numOfCartItemDTO = await _context.ShoppingCarts.Where(s => s.UserId == userId && s.Status == Constant.IS_ACTIVE)
                                                                   .SumAsync(s => s.Quantity);
                return new NumOfCartItemsDTO { Quantity= numOfCartItemDTO };
            }
            catch (SqlException)
            {
                return StatusCode(Constant.SQL_EXECUTION_ERROR);
            }
            catch (Exception)
            {
                return StatusCode(Constant.INTERNAL_ERROR);
            }
        }
        
        // GET api/<CartController>/5
        [HttpGet("{idUser}")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetCartItems(int idUser)
        {
            try
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
            catch (SqlException)
            {
                return StatusCode(Constant.SQL_EXECUTION_ERROR);
            }
            catch (Exception)
            {
                return StatusCode(Constant.INTERNAL_ERROR);
            }

        }

        // POST api/<CartController>
        [HttpPost]
        public async Task<ActionResult> Post(CartProductRequest productRequest)
        {
            try
            {
                var product = await _context.Products.Where(p => p.Id == productRequest.ProductId
                                                            && p.Quantity > 0
                                                            && p.Status == Constant.IS_ACTIVE)
                                                 .FirstOrDefaultAsync();
                if (product == null)
                {
                    return StatusCode(Constant.NOT_FOUND);
                }

                var cartItem = await _context.ShoppingCarts.Where(s => s.ProductId == product.Id
                                                                  && s.UserId == productRequest.UserId
                                                                  && s.Status == Constant.IS_ACTIVE)
                                                            .FirstOrDefaultAsync();

                if (cartItem == null)
                {
                    var newCartItem = new ShoppingCart
                    {
                        ProductId = product.Id,
                        CreateDate = DateTime.Now,
                        UserId = productRequest.UserId,
                        Quantity = Constant.INIT_QUANTITY,
                    };
                    _context.ShoppingCarts.Add(newCartItem);
                }
                else
                {
                    cartItem.Quantity += 1;
                    cartItem.UpdateDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                return StatusCode(Constant.OK);
            }
            catch (SqlException)
            {
                return StatusCode(Constant.SQL_EXECUTION_ERROR);
            }
            catch (Exception)
            {
                return StatusCode(Constant.INTERNAL_ERROR);
            }
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
