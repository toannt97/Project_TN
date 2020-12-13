using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Constants;
using ShopAPI.Models;
using ShopAPI.Models.Responses;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OnlineShopContext _context;

        public OrderDetailsController(OnlineShopContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails()
        {
            return await _context.OrderDetails.ToListAsync();
        }


        [HttpGet("GetOrderDetailsByOrderId/{id}/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDetailsDTO>>> GetOrderDetailsByOrderId(int id, int userId)
        {
            try
            {
                return await _context.OrderDetails.Where(o => o.OrderId == id && o.Order.CustomerId == userId && o.Status == Constant.IS_ACTIVE)
                    .Select(r => new OrderDetailsDTO
                    { 
                        OrderId = r.OrderId,
                        ProductId = r.ProductId,
                        ProductImage = r.Product.Image,
                        ProductInformation = r.Product.Information,
                        Quantity = r.Quantity,
                        ProductName = r.Product.Name,
                        UnitPrice = r.Product.UnitPrice,
                    }).ToListAsync();
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

        [HttpGet("PaymentCompleted/{id}/{userId}")]
        public async Task<ActionResult> PaymentCompleted(int id, int userId)
        {
            try
            {
                var order = await _context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
                order.Status = 2;
                _context.Entry(order).State = EntityState.Modified;

                var newItemHistory = new History();
                newItemHistory.OrderId = id;
                newItemHistory.UserId = userId;
                _context.Histories.Add(newItemHistory);

                await _context.SaveChangesAsync();

                return null;
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


        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetail(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetail", new { id = orderDetail.Id }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDetail>> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return orderDetail;
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }
    }
}
