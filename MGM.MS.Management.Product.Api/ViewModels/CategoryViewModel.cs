using MGM.MS.Management.Product.Services.Dtos;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MGM.MS.Management.Product.Api.ViewModels
{
    public class CategoryViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo nome da categoria é obrigatório")]
        [MinLength(4, ErrorMessage = "O campo nome da categoria precisa ter um tamanho mínimo de 4 caracteres")]
        [MaxLength(30, ErrorMessage = "O campo nome da categoria precisa ter um tamanho máximo de 30 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo descrição da categoria é obrigatório")]
        [MinLength(10, ErrorMessage = "O campo descrição da categoria precisa ter um tamanho mínimo de 10 caracteres")]
        [MaxLength(200, ErrorMessage = "O campo nome da categoria precisa ter um tamanho máximo de 200 caracteres")]
        public string Description { get; set; } = string.Empty;
    }

    internal static class CategoryViewModelUtils
    {
        internal static CategoryDto ToDTO(this CategoryViewModel vm)
            => new()
            {
                Id = string.IsNullOrEmpty(vm.Id) ? 
                    ObjectId.GenerateNewId(DateTime.Now).ToString() : 
                    vm.Id.ToString(),
                Name = vm.Name,
                Description = vm.Description,
            };
    }
}
