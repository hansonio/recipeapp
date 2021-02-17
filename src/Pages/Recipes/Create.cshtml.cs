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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context){
            _context = context;
        }
        
        [Required]
        [MaxLength(125)]
        [BindProperty]
        public string Name { get; set; }
        
        [Required]
        [BindProperty]
        public string Details { get; set; }

        public void OnGet()
        {
        }

         public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recipe = new Recipe(){
                Name = Name,
                Details = Details
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new{id = recipe.Id});
        }
    }
}
