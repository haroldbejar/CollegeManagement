using Application.Services;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    /// <summary>
    /// AlumnoGrado Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoGradoController : ControllerBase, IBaseController<AlumnoGradoDTO>
    {
        private readonly IService<AlumnoGradoDTO> _service;
        private readonly IAlumnoGradoService _alumnoGradoService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="alumnoGradoService"></param>
        public AlumnoGradoController(
            IService<AlumnoGradoDTO> service,
            IAlumnoGradoService alumnoGradoService)
        {
            _service = service;
            _alumnoGradoService = alumnoGradoService;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="alumnoGradoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AlumnoGradoDTO>> CreateAsync(AlumnoGradoDTO alumnoGradoDTO)
        {
            try
            {
                await _service.AddAsync(alumnoGradoDTO);
                return Ok(new { datos = alumnoGradoDTO });
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
        public async Task<ActionResult<IEnumerable<AlumnoGradoDTO>>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalItems = await _service.CountAsync();
                var datos = await _alumnoGradoService.GetAllAlumnosGrados(pageNumber, pageSize);

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
        public async Task<ActionResult<AlumnoGradoDTO>> GetByIdAsync(int id)
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
        /// <param name="alumnoGradoDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AlumnoGradoDTO>> UpdateAsync(AlumnoGradoDTO alumnoGradoDTO, int id)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null) return NotFound("The resource doesn´t exists.");

                await _service.UpdateAsync(alumnoGradoDTO);
                return Ok(existing);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }
    }
}