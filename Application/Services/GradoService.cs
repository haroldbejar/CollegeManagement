using AutoMapper;
using Data.DTOs;
using Data.Entities;
using Infrastructure.Repositoties;

namespace Application.Services
{
    public class GradoService : IService<GradoDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Grado> _repository;

        public GradoService(IMapper mapper, IRepository<Grado> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddAsync(GradoDTO gradoDTO)
        {
            var grado = _mapper.Map<Grado>(gradoDTO);
            await _repository.AddAsync(grado);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var grado = await _repository.GetByIdAsync(id);
            if (grado == null) throw new ArgumentException("Grado not found");
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<GradoDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var grados = await _repository.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<GradoDTO>>(grados);
        }

        public async Task<GradoDTO> GetByIdAsync(int id)
        {
            var grado = await _repository.GetByIdAsync(id);
            return _mapper.Map<GradoDTO>(grado);
        }

        public async Task UpdateAsync(GradoDTO gradoDTO)
        {
            var existingGrado = await _repository.GetByIdAsync(gradoDTO.Id);
            if (existingGrado == null) throw new ArgumentException("Grado not found");

            existingGrado.Nombre = gradoDTO.Nombre;
            existingGrado.ProfesorId = gradoDTO.ProfesorId;
            await _repository.UpdateAsync(existingGrado);
        }
    }
}