using Application.Services;
using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    /// <summary>
    /// Alumno Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AlumnoController : ControllerBase, IBaseController<AlumnoDTO>
    {
        private readonly IService<AlumnoDTO> _service;
        private readonly IAlumnoService _alumnoService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service"></param>
        /// <param name="alumnoService"></param>
        public AlumnoController(IService<AlumnoDTO> service, IAlumnoService alumnoService)
        {
            _service = service;
            _alumnoService = alumnoService;
        }

        /// <summary>
        /// Create Alumno
        /// </summary>
        /// <param name="alumnoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AlumnoDTO>> CreateAsync(AlumnoDTO alumnoDTO)
        {
            try
            {
                // await _service.AddAsync(alumnoDTO);
                var alumnoCreateDTO = _alumnoService.MapAlumnoDTO(alumnoDTO);
                await _alumnoService.AddAlumnoAsync(alumnoCreateDTO);
                return Ok(new { datos = alumnoDTO });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error interno {ex.Message}" });
            }
        }

        /// <summary>
        /// Delete Alumno
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
        /// Get All Alumnos
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("getall/{pageNumber:int}/{pageSize:int}")]
        public async Task<ActionResult<IEnumerable<AlumnoDTO>>> GetAllAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalItems = await _service.CountAsync();
                var datos = await _alumnoService.GetAllAlumnos(pageNumber, pageSize);
                // var datos = await _service.GetAllAsync(pageNumber, pageSize);

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
        /// Get Alumno by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlumnoDTO>> GetByIdAsync(int id)
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
        /// Update Alumno
        /// </summary>
        /// <param name="alumnoDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AlumnoDTO>> UpdateAsync(AlumnoDTO alumnoDTO, int id)
        {
            try
            {
                var existing = await _service.GetByIdAsync(id);
                if (existing == null) return NotFound("The resource doesn´t exists.");

                await _service.UpdateAsync(alumnoDTO);
                return Ok(alumnoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno {ex.Message}");
            }
        }
    }
}