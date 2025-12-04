using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project1.Application.DTOs;
using Project1.Application.Interfaces;
using Project1.Domain.Entities;

namespace Project1.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo){
             _repo = repo;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price));
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p is null ? null : new ProductDto(p.Id, p.Name, p.Description, p.Price);
        }

        public async Task<ProductDto> CreateAsync(string name, decimal price, string? description = null)
        {
            var product = new Product(name, price, description);
            await _repo.AddAsync(product);
            return new ProductDto(product.Id, product.Name, product.Description, product.Price);
        }

        public async Task<bool> UpdateAsync(Guid id, string name, decimal price, string? description = null)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;
            p.Update(name, price, description);
            await _repo.UpdateAsync(p);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var p = await _repo.GetByIdAsync(id);
            if (p == null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
