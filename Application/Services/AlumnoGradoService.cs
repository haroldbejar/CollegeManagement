using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Infrastructure.Repositoties;

namespace Application.Services
{
    public class AlumnoGradoService : IService<AlumnoGradoDTO>, IAlumnoGradoService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<AlumnoGrado> _repository;
        private readonly IAlumnoGradoRepository _alumnoGradoRepository;

        public AlumnoGradoService(
            IMapper mapper,
            IRepository<AlumnoGrado> repository,
            IAlumnoGradoRepository alumnoGradoRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _alumnoGradoRepository = alumnoGradoRepository;

        }

        public async Task AddAsync(AlumnoGradoDTO alumnoGradoDTO)
        {
            var alumnoGrado = _mapper.Map<AlumnoGrado>(alumnoGradoDTO);
            await _repository.AddAsync(alumnoGrado);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var alumnoGrado = await _repository.GetByIdAsync(id);
            if (alumnoGrado == null) throw new ArgumentException("AlumnoGrado not found");
            await _repository.DeleteAsync(id);
        }

        public async Task<IReadOnlyCollection<AlumnoGradoListDTO>> GetAllAlumnosGrados(int pageNumber, int pageSize)
        {
            var alumnosGrados = await _alumnoGradoRepository.GetAllAlumnosGrados(pageNumber, pageSize);
            return _mapper.Map<IReadOnlyCollection<AlumnoGradoListDTO>>(alumnosGrados);
        }

        public async Task<IEnumerable<AlumnoGradoDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var alumnosGrados = await _repository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<AlumnoGradoDTO>>(alumnosGrados);
        }

        public async Task<AlumnoGradoDTO> GetByIdAsync(int id)
        {
            var alumnoGrado = await _repository.GetByIdAsync(id);
            return _mapper.Map<AlumnoGradoDTO>(alumnoGrado);
        }

        public async Task UpdateAsync(AlumnoGradoDTO alumnoGradoDTO)
        {
            var existingAlumnoGrado = await _repository.GetByIdAsync(alumnoGradoDTO.Id);
            if (existingAlumnoGrado == null) throw new ArgumentException("AlumnoGrado not found");

            existingAlumnoGrado.Grupo = alumnoGradoDTO.Grupo;
            existingAlumnoGrado.AlumnoId = alumnoGradoDTO.AlumnoId;
            existingAlumnoGrado.GradoId = alumnoGradoDTO.GradoId;
            await _repository.UpdateAsync(existingAlumnoGrado);

        }
    }
}