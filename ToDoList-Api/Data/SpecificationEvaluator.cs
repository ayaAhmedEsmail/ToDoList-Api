using Microsoft.EntityFrameworkCore;
using ToDoList_Api.Specifications;

namespace ToDoList_Api.Data
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuerry(IQueryable<T> inputQuery, ISpecification<T> specification) {
            var query = inputQuery;

            if (specification != null) {
                query = query.Where(specification.Criteria);

            }

            //Includes
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            // Include strings for nested includes
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            // Apply ordering

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }






            return query;
        }
    }
}
