using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDoList_Api.Data;

namespace ToDoList_Api.Specifications
{
    // Data/GenericRepository.cs
    public class GenericRepository<T>(ApplicationDBContext context) : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _context = context;


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuerry(_context.Set<T>().AsQueryable(), spec);
        }


        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

    }
}
