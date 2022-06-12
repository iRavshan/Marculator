using Marculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marculator.Repositories
{
    public interface IProductRepository
    {
        Task Create(Product product);
        Task Delete(Guid Id);
        Task Update(Product product);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(Guid Id);
    }
}
