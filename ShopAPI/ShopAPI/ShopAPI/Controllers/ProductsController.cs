﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Constants;
using ShopAPI.Models;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Private Fields
        private readonly OnlineShopContext _context;
        #endregion

        #region Constructor
        public ProductsController(OnlineShopContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.Where(p =>p.Quantity > 0 && p.Status == Constant.IS_ACTIVE)
                         .ToListAsync();
        }

        // GET: api/Products
        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int pageNo, int pageSize = 10)
        {
            var result = await _context.Product.Where(p => p.Quantity > 0 && p.Status == Constant.IS_ACTIVE)
                               .OrderBy(p => p.UnitPrice)
                               .Skip(pageNo * pageSize)
                               .Take(pageSize)
                               .Select(p => new Product {
                                   Id = p.Id,
                                   CategoryId = p.CategoryId,
                                   Image = p.Image,
                                   Name = p.Name,
                                   Description = p.Description,
                                   Information = p.Information,
                                   Quantity = p.Quantity,
                                   UnitPrice = p.UnitPrice,
                                   SupplierId = p.SupplierId
                        }).ToListAsync();

            return result;
        }

        // GET: api/Products/5
        [HttpGet("{idProduct}")]
        public async Task<ActionResult<Product>> GetProduct(int idProduct)
        {
            var product = await _context.Product
                                .Include(s => s.Supplier)
                                .Where(p => p.Id == idProduct && p.Quantity > 0 && p.Status == Constant.IS_ACTIVE)
                                .Select(r => new Product
                                {
                                    Id = r.Id,
                                    Description = r.Description,
                                    Image = r.Image,
                                    Information = r.Information,
                                    Name = r.Name,
                                    UnitPrice =r.UnitPrice,
                                    SupplierId = r.SupplierId,
                                    SupplierName = r.Supplier.Name,
                                    CategoryId = r.CategoryId,
                                })
                                .FirstOrDefaultAsync();

            if (product == null)
            {
                return StatusCode(Constant.NOT_FOUND, Constant.NOT_FOUND_MESSAGE);
            }

            return product;
        }

        [HttpGet("{idSupplier}/{idCategory}/{pageNo}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int idSupplier, int idCategory, int pageNo,int pageSize = 10)
        {
            //var query = await _context.Product
            //                 .Where(p => p.SupplierId == (idSupplier == 0 ? p.SupplierId : idSupplier)
            //                        && p.CategoryId == (idCategory == 0 ? p.CategoryId : idCategory)
            //                        && p.Quantity > 0
            //                        && p.Status == Constant.IS_ACTIVE)
            //                 .ToListAsync();

            var result = await _context.Product
                             .Where(p => p.SupplierId == (idSupplier == 0 ? p.SupplierId : idSupplier)
                                    && p.CategoryId == (idCategory == 0 ? p.CategoryId : idCategory)
                                    && p.Quantity > 0
                                    && p.Status == Constant.IS_ACTIVE).OrderBy(p => p.UnitPrice)
                              .Skip(pageNo * pageSize)
                              .Take(pageSize)
                              .Select(p => new Product
                                  {
                                      Id = p.Id,
                                      CategoryId = p.CategoryId,
                                      Image = p.Image,
                                      Name = p.Name,
                                      Description = p.Description,
                                      Information = p.Information,
                                      Quantity = p.Quantity,
                                      UnitPrice = p.UnitPrice,
                                      SupplierId = p.SupplierId
                                  })
                              .ToListAsync();

            if (result == null || result.Count == 0)
                return StatusCode(Constant.NOT_FOUND, Constant.NOT_FOUND_MESSAGE);

            return result;
        }

        [HttpGet("GetProductsRelated/{idProduct}/{idSupplier}/{idCategory}/{quantity}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsRelated(int idProduct, int idSupplier, int idCategory, int quantity = 4)
        {
            var productsRelated = await _context.Product.Where(p => p.SupplierId == idSupplier
                                                                && p.CategoryId == idCategory
                                                                && p.Id != idProduct
                                                                && p.Quantity > 0
                                                                && p.Status == Constant.IS_ACTIVE)
                                                        .Take(quantity)
                                                        .ToListAsync();
            if (productsRelated == null || productsRelated.Count == 0)
            {
                return StatusCode(Constant.NOT_FOUND, Constant.NOT_FOUND_MESSAGE);
            }

            return productsRelated;
        }

        [HttpGet("GetTotalProduct/{categoryId}/{supplierId}")]
        public async Task<ActionResult<int>> GetTotalProduct(int categoryId = 0, int supplierId = 0)
        {
            return await _context.Product.Where(p=> p.CategoryId ==  (categoryId == 0 ? p.CategoryId: categoryId)
                                                && p.SupplierId == (supplierId == 0 ? p.SupplierId : supplierId)
                                                && p.Status == Constant.IS_ACTIVE
                                                && p.Quantity > 0)
                                         .CountAsync();
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
        #endregion

        #region Private Method
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
        #endregion
    }
}
