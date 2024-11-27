using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Infrastructure.Repositoties;

namespace Application.Services
{
    public class AlumnoService : IService<AlumnoDTO>, IAlumnoService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Alumno> _repository;

        private readonly IAlumnoRepostiroy _alumnoRepository;

        public AlumnoService(IMapper mapper, IRepository<Alumno> repository, IAlumnoRepostiroy alumnoRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _alumnoRepository = alumnoRepository;
        }

        public AlumnoCreateDTO MapAlumnoDTO(AlumnoDTO alumnoDTO)
        {
            var alumnoCreateDTO = _mapper.Map<AlumnoCreateDTO>(alumnoDTO);
            return alumnoCreateDTO;
        }

        public async Task AddAlumnoAsync(AlumnoCreateDTO alumnoDTO)
        {
            bool containstNumberName = alumnoDTO.Nombre.Any(char.IsDigit);
            bool containsNumberLastName = alumnoDTO.Apellido.Any(char.IsDigit);
            if (containstNumberName || containsNumberLastName)
                throw new ArgumentException("El nombre del alumno no puede contener números");

            var alumno = _mapper.Map<Alumno>(alumnoDTO);
            await _repository.AddAsync(alumno);

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

        public async Task<IReadOnlyCollection<AlumnoDTO>> GetAllAlumnos(int pageNumber, int pageSize)
        {
            var alumnos = await _alumnoRepository.GetAllAlumnos(pageNumber, pageSize);
            return _mapper.Map<IReadOnlyCollection<AlumnoDTO>>(alumnos);
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