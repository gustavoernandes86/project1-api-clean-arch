using System;

namespace Project1.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }

        // Construtor para criação
        public Product(string name, decimal price, string? description = null)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetPrice(price);
            Description = description;
        }

        // Construtor para reconstituição (ORM / testes)
        public Product(Guid id, string name, decimal price, string? description = null)
        {
            Id = id;
            SetName(name);
            SetPrice(price);
            Description = description;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome inválido", nameof(name));
            if (name.Length > 200) throw new ArgumentException("Nome muito longo", nameof(name));
            Name = name.Trim();
        }

        public void SetPrice(decimal price)
        {
            if (price < 0) throw new ArgumentException("Preço não pode ser negativo", nameof(price));
            Price = price;
        }

        public void Update(string name, decimal price, string? description = null)
        {
            SetName(name);
            SetPrice(price);
            Description = description;
        }
    }
}
