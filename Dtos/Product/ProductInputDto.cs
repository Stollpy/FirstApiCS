using System.ComponentModel.DataAnnotations;

namespace FirstApiCS.Dtos.Product
{
    public record ProductInputDto
    {
        [Required]
        public string Name { get; init; }

        [Required]
        [Range(0,1000000)]
        public int Price { get; init; }

        [Required]
        public int Quantity { get; init; }
    }
}