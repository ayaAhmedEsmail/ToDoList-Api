using System.Linq.Expressions;
using Microsoft.Graph.Models;

namespace ToDoList_Api.Specifications
{
    public abstract class BaseSpecification<T>(Expression<Func<T, bool>> criteria) : ISpecification<T>
    {
        private readonly List<Expression<Func<T, object>>> _includes = new();
        private readonly List<string> _includeStrings = new();
        public int _take { get; private set; }
        public int _skip { get; private set; }
        public bool _isPagingEnabled { get; private set; }

        private Expression<Func<T, object>> _orderBy;
        private Expression<Func<T, object>> _orderByDescending;

        public Expression<Func<T, bool>> Criteria { get; } = criteria;

        public List<Expression<Func<T, object>>> Includes => _includes;

        public List<string> IncludeStrings => _includeStrings;

        public Expression<Func<T, object>> OrderBy => _orderBy;

        public Expression<Func<T, object>> OrderByDescending => _orderByDescending;

      


        protected void AddInclude(Expression<Func<T, object>> include)
        {
            _includes.Add(include);
        }

        protected string AddIncludeString(string includeString)
        {
            _includeStrings.Add(includeString);
            return includeString;
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            _orderBy = orderBy;
        }
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            _orderByDescending = orderByDescending;

        }

    }
}