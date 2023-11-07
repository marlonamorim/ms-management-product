using MGM.MS.Management.Product.Services.Dtos;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MGM.MS.Management.Product.Api.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo nome do produto é obrigatório")]
        [MinLength(4, ErrorMessage = "O campo nome do produto precisa ter um tamanho mínimo de 4 caracteres")]
        [MaxLength(30, ErrorMessage = "O campo nome do produto precisa ter um tamanho máximo de 30 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo descrição do produto é obrigatório")]
        [MinLength(10, ErrorMessage = "O campo descrição do produto precisa ter um tamanho mínimo de 10 caracteres")]
        [MaxLength(200, ErrorMessage = "O campo nome do produto precisa ter um tamanho máximo de 200 caracteres")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo preço do produto é obrigatório")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Campo detalhes do produto é obrigatório")]
        [MaxLength(200, ErrorMessage = "O campo detalhes do produto precisa ter um tamanho máximo de 200 caracteres")]
        public string Details { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo id da categoria é obrigatório")]
        [MaxLength(24, ErrorMessage = "O campo id da categoria precisa ter um tamanho máximo de 24 caracteres")]
        public string CategoryId { get; set; } = string.Empty;
    }

    internal static class ProductViewModelUtils
    {
        internal static ProductDto ToDTO(this ProductViewModel vm)
            => new()
            {
                Id = string.IsNullOrEmpty(vm.Id) ?
                    ObjectId.GenerateNewId(DateTime.Now).ToString() :
                    vm.Id.ToString(),
                Name = vm.Name,
                Description = vm.Description,
                Price = vm.Price,
                Details = vm.Details,
                CategoryId = vm.CategoryId
            };
    }
}
