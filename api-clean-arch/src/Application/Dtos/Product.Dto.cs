using System;

namespace Project1.Application.DTOs
{
    public record ProductDto(Guid Id, string Name, string? Description, decimal Price);
}
