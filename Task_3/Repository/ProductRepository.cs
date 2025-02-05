﻿using Microsoft.EntityFrameworkCore;
using Task_3.Data;
using Task_3.Entities;

namespace Task_3.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

       
        public async Task<List<Product>> GetAllAsync(string key)
        {
            var keyCode = key.Trim().ToLower();
            return keyCode != "" ? await _context.Products.Where(s => s.Name.ToLower().Contains(keyCode)).ToListAsync()
                : await _context.Products.ToListAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
