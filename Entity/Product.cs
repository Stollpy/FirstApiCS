using System;

namespace FirstApiCS.Entity
{
    public class Product
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public int Price { get; init; }

        public int Quantity { get; init; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}