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

            // GET api/geometry
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var response = await geometryService.GetAllAsync();
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }

            // GET api/geometry/5
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var response = await geometryService.GetByIdAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }

            // POST api/geometry
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

            // PUT api/geometry/5
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

            // DELETE api/geometry/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var response = await geometryService.DeleteGeometryAsync(id);
                if (!response.Success)
                    return NotFound(response);

                return Ok(response);
            }
        }
    }

