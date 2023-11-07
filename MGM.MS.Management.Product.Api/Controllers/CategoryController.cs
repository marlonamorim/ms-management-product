using MGM.MS.Management.Product.Api.ViewModels;
using MGM.MS.Management.Product.Notification.Interfaces;
using MGM.MS.Management.Product.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MGM.MS.Management.Product.Api.Controllers
{
    public class CategoryController : NotificationController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(INotification notification,
            ICategoryService categoryService) : base(notification)
        {
            _categoryService = categoryService;
        }

        [HttpPut()]
        [SwaggerOperation(
            Summary = "Cadastro de categoria.",
            Description = "Cadastra ou atualiza uma categoria de produtos com campos nome e descrição."
            )]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertAsync([FromBody] CategoryViewModel model)
        {
            return await HandleResponseAsync(async () => await _categoryService.UpsertAsync(model.ToDTO()));
        }

        [HttpGet()]
        [SwaggerOperation(
            Summary = "Lista de categorias.",
            Description = "Consulta uma lista categorias de produtos."
            )]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(IEnumerable<CategoryViewModel>))]
        public async Task<IActionResult> ListAsync()
        {
            return await HandleResponseAsync(async () => await _categoryService.ListAllAsync());
        }

        [HttpDelete("{id:length(24)}")]
        [SwaggerOperation(
            Summary = "Exclusão de categoria.",
            Description = "Exclui uma categoria de produto por id."
            )]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            return await HandleResponseAsync(async () => await _categoryService.DeleteAsync(id));
        }
    }
}