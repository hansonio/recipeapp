using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hanson.RecipeApp.Data;
using Hanson.RecipeApp.Models;
using Faker;
using Hanson.RecipeApp.Configuration;
using Hanson.RecipeApp.Extensions;
using System.Text;


namespace Hanson.RecipeApp.Pages.Recipes
{
    public class RecipesModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public RecipesModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Sort = "name";
        }

        public PaginatedList<Recipe> Recipes{get;set;}

        [BindProperty(SupportsGet = true, Name = "p")]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public string Filter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Sort { get; set; }

        [BindProperty(SupportsGet = true)]
        public SortDirection Direction { get; set; }

         public Dictionary<string, string> LinkData =>
            new()
            {
                {"filter", Filter},
                {"p", Recipes.CurrentPage.ToString()},
                {"sort", Sort},
                {"direction", Direction.ToString()}
            };

        public async Task<IActionResult> OnGetAsync()
        {
            Recipes = await _dbContext.Recipes
                                .Filter(Filter)
                                .OrderBy(Sort, Direction)
                                .ToPagedList(PageIndex, AppConfig.PageSize);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(){

            for(int i = 0; i < 100; i++){
                var recipe = new Recipe(){
                    Id = Guid.NewGuid(),
                    Name = string.Join(" ",Faker.Lorem.Words( (i % 2) + 1)).Capitalize()
                };

                recipe.Details = BuildRecipe(recipe.Name);


                await _dbContext.Recipes.AddAsync(recipe);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public SortDirection GetNextSortDirection(string name, SortDirection defaultOrder){
            if(Sort?.ToLower() != name?.ToLower()){
                return defaultOrder;
            }

            return Direction == SortDirection.Asc? SortDirection.Desc: SortDirection.Asc;
        }

        private static string BuildRecipe(string name){
            var random = Faker.RandomNumber.Next(1,6);

            var sb = new StringBuilder();

            sb.Append($"<div>How to make epic {name}!</div>");
            sb.Append(Faker.Lorem.Paragraph());
            sb.Append("<h1>Ingredients</h1>");
            sb.Append("<p><ul>");
            for(var i = 1; i <= random; i++ ){
                sb.Append($"<li>{string.Join("", Faker.Lorem.Words(1))}</li>");
            }
            sb.Append("</ul></p><br/>");
            sb.Append("<h1>Directions</h1><br/><div>");
            sb.Append($"<p>{string.Join("</p><p>", Faker.Lorem.Paragraphs(random))}</p><div>");
            sb.Append("<h1>Nutrition Facts</h1><br/>");
            sb.Append($"<p>{string.Join(" ", Faker.Lorem.Sentences(2))}</p>");

            return sb.ToString();
        }
    }
}
 