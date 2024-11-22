namespace Application.Services
{
    public interface IService<T> where T : class
    {
        Task AddAsync(T entity);
        Task<int> CountAsync();
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);
        Task<T> GetByIdAsync(int id);
        Task UpdateAsync(T entity);
    }
}