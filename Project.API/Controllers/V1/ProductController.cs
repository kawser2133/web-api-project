using Asp.Versioning;
using Project.Core.Entities.Business;
using Project.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }


        [HttpGet("paginated")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize, CancellationToken cancellationToken)
        {
            try
            {
                int pageSizeValue = pageSize ?? 10;
                int pageNumberValue = pageNumber ?? 1;

                //Get peginated data
                var products = await _productService.GetPaginatedData(pageNumberValue, pageSizeValue, cancellationToken);

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productService.GetAll(cancellationToken);
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _productService.GetById(id, cancellationToken);
                return Ok(data);
            }
            catch (Exception ex)
            {
                if (ex.Message == "No data found")
                {
                    return StatusCode(StatusCodes.Status404NotFound, ex.Message);
                }
                _logger.LogError(ex, $"An error occurred while retrieving the product");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                string message = "";
                if (await _productService.IsExists("Name", model.Name, cancellationToken))
                {
                    message = $"The product name- '{model.Name}' already exists";
                    return StatusCode(StatusCodes.Status400BadRequest, message);
                }

                if (await _productService.IsExists("Code", model.Code, cancellationToken))
                {
                    message = $"The product code- '{model.Code}' already exists";
                    return StatusCode(StatusCodes.Status400BadRequest, message);
                }

                try
                {
                    var data = await _productService.Create(model, cancellationToken);
                    return Ok(data);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while adding the product");
                    message = $"An error occurred while adding the product- " + ex.Message;

                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Please input all required data");
        }


        [HttpPut]
        public async Task<IActionResult> Edit(ProductUpdateViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                string message = "";
                if (await _productService.IsExistsForUpdate(model.Id, "Name", model.Name, cancellationToken))
                {
                    message = "The product name- '{model.Name}' already exists";
                    return StatusCode(StatusCodes.Status400BadRequest, message);
                }

                if (await _productService.IsExistsForUpdate(model.Id, "Code", model.Code, cancellationToken))
                {
                    message = $"The product code- '{model.Code}' already exists";
                    return StatusCode(StatusCodes.Status400BadRequest, message);
                }

                try
                {
                    await _productService.Update(model, cancellationToken);
                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while updating the product");
                    message = $"An error occurred while updating the product- " + ex.Message;

                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }
            }
            return StatusCode(StatusCodes.Status400BadRequest, "Please input all required data");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _productService.Delete(id, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the product- " + ex.Message);
            }
        }

        [HttpGet("PriceCheck/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> PriceCheck(int productId, CancellationToken cancellationToken)
        {
            try
            {
                var price = await _productService.PriceCheck(productId, cancellationToken);
                return Ok(price);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking product price");
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while checking product price- {ex.Message}");
            }
        }

    }
}
