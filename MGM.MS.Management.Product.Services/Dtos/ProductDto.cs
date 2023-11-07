namespace MGM.MS.Management.Product.Services.Dtos
{
    public class ProductDto
    {
        public string? Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        public decimal Price { get; set; }
        
        public string Details { get; set; } = string.Empty;
        
        public string CategoryId { get; set; } = string.Empty;
    }
}
