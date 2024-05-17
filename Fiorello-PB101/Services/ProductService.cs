using Fiorello_PB101.Data;
using Fiorello_PB101.Models;
using Fiorello_PB101.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            return await _context.Products.Where(m=>m.Id == id)
                                                 .Include(m=>m.Category)
                                                 .Include(m=>m.ProductImages)
                                                 .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(m=>m.ProductImages).ToListAsync();
        }
    }
}
