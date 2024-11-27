using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace webapp.Controllers
{
    /// <summary>
    /// Interfaz para el crud
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseController<T> where T : class
    {
        /// <summary>
        /// Get all
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>All the elements paginated</returns>
        Task<ActionResult<IEnumerable<T>>> GetAllAsync(int pageNumber, int pageSize);

        /// <summary>
        /// GetbyID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>An element</returns>
        Task<ActionResult<T>> GetByIdAsync(int id);

        /// <summary>
        /// Create an element
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ActionResult<T>> CreateAsync(T entity);

        /// <summary>
        /// Update element
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActionResult<T>> UpdateAsync(T entity, int id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActionResult> DeleteAsync(int id);
    }
}