using Application.Services;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    /// <summary>
    /// Profesor Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorController : ControllerBase, IBaseController<ProfesorDTO>
    {
        private readonly IService<ProfesorDTO> _service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        public ProfesorController(IService<ProfesorDTO> service)
        {
            _service = service;
        }

        /// <summary>
        /// Create profesor
        /// </summary>
        /// <param name="profesorDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProfesorDTO>> CreateAsync(ProfesorDTO profesorDTO)
        {
            try
            {
                await _service.AddAsync(profesorDTO);
                return Ok(new { datos = profesorDTO });
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
        /// Get All profesores
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("getall/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<IEnumerable<ProfesorDTO>>> GetAllAsync(int pageNumber, int pageSize)
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
        /// Get by Id profesor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProfesorDTO>> GetByIdAsync(int id)
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
        /// Update profesor
        /// </summary>
        /// <param name="profesorDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProfesorDTO>> UpdateAsync(ProfesorDTO profesorDTO, int id)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null) return NotFound("The resource doesn´t exists.");

                await _service.UpdateAsync(profesorDTO);
                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }
    }
}