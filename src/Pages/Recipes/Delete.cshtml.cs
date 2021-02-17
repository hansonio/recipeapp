using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Hanson.RecipeApp.Data;
using Hanson.RecipeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Hanson.RecipeApp.Pages.Recipes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context){
            _context = context;
        }
       
        public Recipe Recipe {get;set;}
        public async void OnGetAsync(Guid id)
        {
            Recipe = await _context.Recipes.FindAsync(id);
        }

         public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recipe = await _context.Recipes.FindAsync(id);

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new{id = recipe.Id});
        }
    }
}
