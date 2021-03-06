﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            return await _context.Products.Where(p =>p.Quantity > 0 && p.Status == Constant.IS_ACTIVE)
                         .ToListAsync();
        }

        // GET: api/Products
        [HttpGet("{pageNo}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int pageNo, int pageSize = 10)
        {
            var result = await _context.Products.Where(p => p.Quantity > 0 && p.Status == Constant.IS_ACTIVE)
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
            var product = await _context.Products
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
                                    CategoryId = r.CategoryId,
                                    CreateDate = r.CreateDate,
                                    Quantity = r.Quantity,
                                })
                                .FirstOrDefaultAsync();

            if (product == null)
            {
                return StatusCode(Constant.NOT_FOUND);
            }

            return product;
        }

        [HttpGet("{idSupplier}/{idCategory}/{pageNo}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int idSupplier, int idCategory, int pageNo,int pageSize = 10)
        {
            var result = await _context.Products
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
                return StatusCode(Constant.NOT_FOUND);

            return result;
        }

        [HttpGet("GetProductsRelated/{idProduct}/{idSupplier}/{idCategory}/{quantity}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsRelated(int idProduct, int idSupplier, int idCategory, int quantity = 4)
        {
            var productsRelated = await _context.Products.Where(p => p.SupplierId == idSupplier
                                                                && p.CategoryId == idCategory
                                                                && p.Id != idProduct
                                                                && p.Quantity > 0
                                                                && p.Status == Constant.IS_ACTIVE)
                                                        .Take(quantity)
                                                        .ToListAsync();
            if (productsRelated == null || productsRelated.Count == 0)
            {
                return StatusCode(Constant.NOT_FOUND);
            }

            return productsRelated;
        }

        [HttpGet("GetTotalProduct/{categoryId}/{supplierId}")]
        public async Task<ActionResult<int>> GetTotalProduct(int categoryId = 0, int supplierId = 0)
        {
            return await _context.Products.Where(p=> p.CategoryId ==  (categoryId == 0 ? p.CategoryId: categoryId)
                                                && p.SupplierId == (supplierId == 0 ? p.SupplierId : supplierId)
                                                && p.Status == Constant.IS_ACTIVE
                                                && p.Quantity > 0)
                                         .CountAsync();
        }

        [HttpGet("SearchProduct/{keyword}")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProduct(string keyword)
        {
            try
            {
            return await _context.Products.Where(p => (p.Name.Contains(keyword) || p.Supplier.Name.Contains(keyword))
                                                && p.Quantity > 0 && p.Status == Constant.IS_ACTIVE)
                                          .OrderBy(p => p.Name)
                                          .ToListAsync();
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
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }
        #endregion

        #region Private Method
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        #endregion
    }
}
