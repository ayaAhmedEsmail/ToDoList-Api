﻿using System.Linq.Expressions;

namespace ToDoList_Api.Specifications
{
    public interface ISpecification<T>
    {
       
        Expression<Func<T, bool>> Criteria { get; }

     
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }

        Expression<Func<T, object>> OrderBy { get; }

        Expression<Func<T, object>> OrderByDescending { get; }


        

    }
}
