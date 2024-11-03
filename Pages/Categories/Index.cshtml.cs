﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Stan_Iustina_Lab2.Data;
using Stan_Iustina_Lab2.Models;
using Stan_Iustina_Lab2.Models.ViewModels;

namespace Stan_Iustina_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Stan_Iustina_Lab2.Data.Stan_Iustina_Lab2Context _context;

        public IndexModel(Stan_Iustina_Lab2.Data.Stan_Iustina_Lab2Context context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            CategoryData = new CategoryIndexData();
            CategoryData.Categories = await _context.Category
                .Include(c => c.BookCategories)
                    .ThenInclude(bc => bc.Book)
                    .ThenInclude(b => b.Author)
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategoryID = id.Value;
                var selectedCategory = CategoryData.Categories
                    .Where(c => c.ID == id.Value).Single();
                CategoryData.Books = selectedCategory.BookCategories
                    .Select(bc => bc.Book);
            }
        }
    }
}
    

