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
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context){
            _context = context;
        }
        
        [Required]
        [MaxLength(125)]
        [BindProperty]
        public string Name { get; set; }
        
        [Required]
        [BindProperty]
        public string Details { get; set; }
       
        public Recipe Recipe {get;set;}
        public async void OnGetAsync(Guid id)
        {
            Recipe = await _context.Recipes.FindAsync(id);
            Name = Recipe.Name;
            Details = Recipe.Details;
        }

         public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recipe = await _context.Recipes.FindAsync(id);

            recipe.Name = Name;
            recipe.Details = Details;

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new{id = recipe.Id});
        }
    }
}
