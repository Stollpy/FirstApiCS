using System;

namespace FirstApiCS.Dtos.Product
{
    public record ProductOutputDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public int Price { get; init; }

        public int Quantity { get; init; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}