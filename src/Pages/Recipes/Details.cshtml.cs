using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Hanson.RecipeApp.Data;
using Hanson.RecipeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quill.Delta;

namespace Hanson.RecipeApp.Pages.Recipes
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context){
            _context = context;
        }
        
        public Recipe Recipe{get;set;}

        public async void OnGetAsync(Guid id)
        {
            Recipe = await _context.Recipes.FindAsync(id);
        }
    }
}
