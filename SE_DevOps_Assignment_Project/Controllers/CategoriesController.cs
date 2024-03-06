using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SE_DevOps_DataLayer.Entities;
using SE_DevOps_DataLayer.Services;
using System.Security.Claims;

namespace SE_DevOps_Assignment_Project.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _categoryService.GetAllCategoriesAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            var createdCategory = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(Get), new { id = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Category>> Put(int id, [FromBody] Category category)
        {
            if (id != category.CategoryId) return BadRequest();
            return Ok(await _categoryService.UpdateCategoryAsync(category));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }

}
