﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stan_Iustina_Lab2.Data;
using Stan_Iustina_Lab2.Models;

namespace Stan_Iustina_Lab2.Pages.Borrowings
{
    public class EditModel : PageModel
    {
        private readonly Stan_Iustina_Lab2.Data.Stan_Iustina_Lab2Context _context;

        public EditModel(Stan_Iustina_Lab2.Data.Stan_Iustina_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.Member)
                .FirstOrDefaultAsync(m => m.ID == id);

          
            if (Borrowing == null)
            {
                return NotFound();
            }
            var bookList = _context.Book
                .Include(b => b.Author)
                .Select(x => new
        {
            x.ID,
            BookFullName = x.Title + " - " + x.Author.LastName + " " + x.Author.FirstName
        });

            ViewData["BookID"] = new SelectList(_context.Book, "ID", "ID");
           ViewData["MemberID"] = new SelectList(_context.Member, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Borrowing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowingExists(Borrowing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BorrowingExists(int id)
        {
            return _context.Borrowing.Any(e => e.ID == id);
        }
    }
}
