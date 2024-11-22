using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Infrastructure.Repositoties;

namespace Application.Services
{
    public class ProfesorService : IService<ProfesorDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Profesor> _repository;

        public ProfesorService(IMapper mapper, IRepository<Profesor> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(ProfesorDTO profesorDTO)
        {
            var profesor = _mapper.Map<Profesor>(profesorDTO);
            await _repository.AddAsync(profesor);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var profesor = await _repository.GetByIdAsync(id);
            if (profesor == null) throw new ArgumentException("Profesor not found");
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProfesorDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var profesores = await _repository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<ProfesorDTO>>(profesores);
        }

        public async Task<ProfesorDTO> GetByIdAsync(int id)
        {
            var profesor = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProfesorDTO>(profesor);
        }

        public async Task UpdateAsync(ProfesorDTO profesorDTO)
        {
            var existingProfesor = await _repository.GetByIdAsync(profesorDTO.Id);
            if (existingProfesor == null) throw new ArgumentException("Profesor not found");

            existingProfesor.Nombre = profesorDTO.Nombre;
            existingProfesor.Genero = profesorDTO.Genero;
            await _repository.UpdateAsync(existingProfesor);
        }
    }
}