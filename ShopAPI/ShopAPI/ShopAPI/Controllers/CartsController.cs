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
                return new NumOfCartItemsDTO { Quantity = numOfCartItemDTO };
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
                var result = await _context.ShoppingCarts.Where(s => s.Status == Constant.IS_ACTIVE && s.UserId == idUser)
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

        [HttpGet("CheckOut/{id}")]
        public async Task<ActionResult<IEnumerable<CartItemInvalidDTO>>> CheckOut(int id)
        {
            try
            {
                var user = await _context.Users.Where(u => u.Id == id && u.Status == Constant.IS_ACTIVE).FirstOrDefaultAsync();
                if (user == null)
                    return StatusCode(Constant.USER_NOT_EXIST);
                var cartItems = await _context.ShoppingCarts.Where(c => c.UserId == id && c.Status == Constant.IS_ACTIVE
                                                                   && (c.Quantity > c.Product.Quantity || c.Product.Status == Constant.IS_DELETED))
                                                            .Select(r => new CartItemInvalidDTO
                                                            {
                                                                ProductId = r.ProductId,
                                                                ProductName = r.Product.Name,
                                                                QuantityAvailable = r.Product.Quantity,
                                                                QuantityRequest = r.Quantity,
                                                                Status = r.Status,
                                                            })
                                                            .ToListAsync();
                if (cartItems.Count == 0)
                {
                    await CreateOrder(id);
                    return StatusCode(Constant.EMPTY_LIST);
                }
                return cartItems;
            }
            catch (SqlException)
            {
                return StatusCode(Constant.SQL_EXECUTION_ERROR);
            }
            catch (Exception ex)
            {
                return StatusCode(Constant.INTERNAL_ERROR);
            }
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> UpdateItem(CartItemUpdateRequest cartItemRequest)
        {
            try
            {
                var cartItem = _context.ShoppingCarts.Where(c => c.ProductId == cartItemRequest.ProductId
                                                            && c.UserId == cartItemRequest.UserId && c.Status == Constant.IS_ACTIVE)
                                                     .FirstOrDefault();
                if (cartItem != null)
                {
                    cartItem.Status = cartItemRequest.Status;
                    cartItem.Quantity = cartItemRequest.Quantity;
                    _context.Entry(cartItem).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    var items = await GetCartItems(cartItemRequest.UserId);
                    return items;
                }
                return StatusCode(Constant.NOT_FOUND);
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
        private async Task CreateOrder(int id)
        {
            var cartItems = await _context.ShoppingCarts.Where(c => c.UserId == id && c.Status == Constant.IS_ACTIVE).ToListAsync();
            // Create new order            
            var newOrder = new Order();
            var createDate = DateTime.Now;
            newOrder.CreateDate = createDate;
            newOrder.CustomerId = id;
            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();
            // Get id of order just created
            var orderId = _context.Orders.Where(o => o.CustomerId == id && o.CreateDate == createDate).Select(o => o.Id).FirstOrDefault();
            foreach (var item in cartItems)
            {
                var newOrderDetail = new OrderDetail();
                newOrderDetail.OrderId = orderId;
                newOrderDetail.ProductId = item.ProductId;
                newOrderDetail.Quantity = item.Quantity;
                _context.OrderDetails.Add(newOrderDetail);
            }
            // Remove items in cart
            cartItems.Select(c => { 
                            c.Status = 1; 
                            c.UpdateDate = DateTime.Now; 
                            _context.Entry(c).State = EntityState.Modified; 
                            return c; 
                            })
                    .ToList();
            await _context.SaveChangesAsync();
        }
    }
}
