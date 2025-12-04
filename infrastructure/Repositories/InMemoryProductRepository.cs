using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Project1.Application.Interfaces;
using Project1.Domain.Entities;

namespace Project1.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _items = new();

        public InMemoryProductRepository()
        {
            // seed data
            _items.Add(new Product(Guid.NewGuid(), "Caneta", 2.50m, "Caneta azul"));
            _items.Add(new Product(Guid.NewGuid(), "Caderno", 15.00m, "Caderno 100 páginas"));
        }

        public Task AddAsync(Product product, CancellationToken ct = default)
        {
            _items.Add(product);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var p = _items.FirstOrDefault(x => x.Id == id);
            if (p != null) _items.Remove(p);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct = default) =>
            Task.FromResult(_items.AsEnumerable());

        public Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            Task.FromResult(_items.FirstOrDefault(x => x.Id == id));

        public Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            var idx = _items.FindIndex(x => x.Id == product.Id);
            if (idx >= 0) _items[idx] = product;
            return Task.CompletedTask;
        }
    }
}
