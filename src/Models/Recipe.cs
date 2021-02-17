using System;
using System.Linq;
using System.Linq.Expressions;
using Hanson.RecipeApp.Data;
using Quill.Delta;

namespace Hanson.RecipeApp.Models{

    public class Recipe{

        public Guid Id{get;set;}
        public string Name {get;set;}
        public string Details{get;set;}
    }

public static class RecipeQueryExtensions{

    public static IQueryable<Recipe> Filter(this IQueryable<Recipe> query, string filter){
        if(string.IsNullOrWhiteSpace(filter)){
            return query;
        }

        filter = filter.ToLower();

        return query.Where(x =>
            x.Name.ToLower().Contains(filter)
        );
    }

    public static IQueryable<Recipe> OrderBy(this IQueryable<Recipe> query, string name, SortDirection direction = SortDirection.Asc){

        Expression<Func<Recipe, object>> exp = name?.ToLower() switch
        {
            "details" => x => x.Details,
            _ => x => x.Name
        };

        return direction == SortDirection.Asc ? query.OrderBy(exp) : query.OrderByDescending(exp);
    }
  }

}