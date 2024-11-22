using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Infrastructure.Repositoties;

namespace Application.Services
{
    public class AlumnoService : IService<AlumnoDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Alumno> _repository;

        public AlumnoService(IMapper mapper, IRepository<Alumno> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(AlumnoDTO alumnoDTO)
        {
            var alumno = _mapper.Map<Alumno>(alumnoDTO);
            await _repository.AddAsync(alumno);

        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var alumno = await _repository.GetByIdAsync(id);
            if (alumno == null) throw new ArgumentException("Alumno not found");
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AlumnoDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var alumnos = await _repository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<AlumnoDTO>>(alumnos);
        }

        public async Task<AlumnoDTO> GetByIdAsync(int id)
        {
            var alumno = await _repository.GetByIdAsync(id);
            return _mapper.Map<AlumnoDTO>(alumno);
        }

        public async Task UpdateAsync(AlumnoDTO alumnoDTO)
        {
            var existingAlumno = await _repository.GetByIdAsync(alumnoDTO.Id);
            if (existingAlumno == null) throw new ArgumentException("Alumno not found");

            existingAlumno.Nombre = alumnoDTO.Nombre;
            existingAlumno.Apellido = alumnoDTO.Apellido;
            existingAlumno.FechaNacimiento = alumnoDTO.FechaNacimiento;
            existingAlumno.Genero = alumnoDTO.Genero;
            await _repository.UpdateAsync(existingAlumno);

        }
    }
}