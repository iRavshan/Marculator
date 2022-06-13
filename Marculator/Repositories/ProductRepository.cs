using Marculator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marculator.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid Id)
        {
            Product eProduct = await dbContext.Products.FindAsync(Id);

            if(eProduct is not null)
            {
                dbContext.Remove(eProduct);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
           return await dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid Id)
        {
            return await dbContext.Products.FindAsync(Id);
        }

        public async Task<Product> GetByName(string name)
        {
            IEnumerable<Product> allProducts = await dbContext.Products.ToListAsync();

            Product product = allProducts.Where(p => p.Name.Equals(name)).FirstOrDefault();

            return product;
        }

        public async Task<IEnumerable<Product>> GetByShortName(string shortname)
        {
            return (await dbContext.Products.ToListAsync())
                    .Where(w => w.Name.Contains(shortname)).ToList();
        }

        public async Task Update(Product product)
        {
            var pro =  dbContext.Products.Attach(product);

            pro.State = EntityState.Modified;

            await dbContext.SaveChangesAsync();
        }
    }
}
