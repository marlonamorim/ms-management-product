using MGM.MS.Management.Product.Api.ViewModels;
using MGM.MS.Management.Product.Notification.Interfaces;
using MGM.MS.Management.Product.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MGM.MS.Management.Product.Api.Controllers
{
    public class ProductController : NotificationController
    {
        private readonly IProductService _productService;
        public ProductController(INotification notification,
            IProductService productService) : base(notification)
        {
            _productService = productService;
        }

        [HttpPut()]
        [SwaggerOperation(
            Summary = "Cadastro de produto.",
            Description = "Cadastra ou atualiza um produto para uma categoria."
            )]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertAsync([FromBody] ProductViewModel model)
        {
            return await HandleResponseAsync(async () => await _productService.UpsertAsync(model.ToDTO()));
        }

        [HttpDelete("{id:length(24)}")]
        [SwaggerOperation(
            Summary = "Exclusão de produto.",
            Description = "Exclui um produto por id."
            )]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            return await HandleResponseAsync(async () => await _productService.DeleteAsync(id));
        }

        [HttpGet("{id:length(24)}")]
        [SwaggerOperation(
            Summary = "Busca de detalhe sdo produto.",
            Description = "Lista todos os detalhes do produto por id."
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductViewModel>))]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] string id)
        {
            return await HandleResponseAsync(async () => await _productService.GetDetailsAsync(id));
        }

        [HttpGet("categories/{category-id:length(24)}")]
        [SwaggerOperation(
            Summary = "Busca de produtos.",
            Description = "Lista todos os produtos por categoria."
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductViewModel>))]
        public async Task<IActionResult> ListByCategoryAsync([FromRoute(Name = "category-id")] string categoryId)
        {
            return await HandleResponseAsync(async () => await _productService.ListByCategoryAsync(categoryId));
        }
    }
}
