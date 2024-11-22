using Application.Services;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    /// <summary>
    /// Grado
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GradoController : ControllerBase, IBaseController<GradoDTO>
    {
        private readonly IService<GradoDTO> _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public GradoController(IService<GradoDTO> service)
        {
            _service = service;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="gradoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GradoDTO>> CreateAsync(GradoDTO gradoDTO)
        {
            try
            {
                await _service.AddAsync(gradoDTO);
                return Ok(new { datos = gradoDTO });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null) return NotFound("The resource doesn´t exists.");

                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("getall/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<IEnumerable<GradoDTO>>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalItems = await _service.CountAsync();
                var datos = await _service.GetAllAsync(pageNumber, pageSize);

                var paginationData = new
                {
                    totalCount = totalItems,
                    pageSize,
                    currentPage = pageNumber,
                    totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                };
                return Ok(new { datos, paginationData });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GradoDTO>> GetByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null) return NotFound("The resource doesn´t exists.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="gradoDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<GradoDTO>> UpdateAsync(GradoDTO gradoDTO, int id)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null) return NotFound("The resource doesn´t exists.");

                await _service.UpdateAsync(gradoDTO);
                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }
    }
}