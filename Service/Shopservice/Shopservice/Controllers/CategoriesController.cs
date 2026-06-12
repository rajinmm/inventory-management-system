using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopApplication.Commands;
using ShopApplication.Queries;
using ShopDTO.DTOs;

namespace Shopservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllCategoriesResponse>>> GetAllCategories()
        {
            try
            {
                var query = new GetAllCategoriesQuery();
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving categories.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<CreateCategoryResponse>> CreateCategory([FromBody] CreateCategoryRequest categoryRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var command = new CreateCategoryCommand(categoryRequest.CategoryName);
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(CreateCategory), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the category.", error = ex.Message });
            }
        }
    }
}
