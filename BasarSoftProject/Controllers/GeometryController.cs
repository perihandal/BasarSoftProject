using App.Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace App.API.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class GeometryController : ControllerBase
        {
            private readonly IGeometryService geometryService;

            public GeometryController(IGeometryService geometryService)
            {
                this.geometryService = geometryService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var response = await geometryService.GetAllAsync();
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var response = await geometryService.GetByIdAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }

            [HttpPost]
            public async Task<IActionResult> Create([FromBody] GeometryDto dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await geometryService.AddGeometryAsync(dto);
                if (!response.Success)
                    return BadRequest(response);

                return Ok(response);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] GeometryDto dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await geometryService.UpdateGeometryAsync(id, dto);
                if (!response.Success)
                    return BadRequest(response);

                return Ok(response);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await geometryService.DeleteGeometryAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
        {
            var response = await geometryService.PaginationAsync(pageNumber, pageSize);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
        [HttpGet("count")]
        public async Task<IActionResult> GetGeometryCount()
        {
            var count = await geometryService.GetGeometryCountAsync();
            return Ok(new { totalCount = count });
        }


    }
}

