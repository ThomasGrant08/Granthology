﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Granthology.Data;
using Granthology.Models;
using Granthology.Enums;

namespace Granthology.Controllers
{
    public class RelationshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Relationships
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Relationships.Include(r => r.PersonA).Include(r => r.PersonB);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Relationships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationship = await _context.Relationships
                .Include(r => r.PersonA)
                .Include(r => r.PersonB)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationship == null)
            {
                return NotFound();
            }

            return View(relationship);
        }

        // GET: Relationships/Create
        public IActionResult Create()
        {
            ViewData["PersonAId"] = new SelectList(_context.People, "Id", "Id");
            ViewData["PersonBId"] = new SelectList(_context.People, "Id", "Id");
            ViewData["RelationshipType"] = new SelectList(Enum.GetValues(typeof(RelationshipType)));
            return View();

        }

        // POST: Relationships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonAId,PersonBId,RelationshipType,Id,CreatedAt,LastUpdatedAt,DeletedAt")] Relationship relationship)
        {
            ModelState.Remove("RelationshipTypeDiscriminator");
            ModelState.Remove("PersonA");
            ModelState.Remove("PersonB");


            if (ModelState.IsValid)
            {
                _context.Add(relationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonAId"] = new SelectList(_context.People, "Id", "Id", relationship.PersonAId);
            ViewData["PersonBId"] = new SelectList(_context.People, "Id", "Id", relationship.PersonBId);
            ViewData["RelationshipType"] = new SelectList(Enum.GetValues(typeof(RelationshipType)));

            ViewData["ValidationErrors"] = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

            // Return the model along with validation errors to the view
            return View(relationship);
        }

        // GET: Relationships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationship = await _context.Relationships.FindAsync(id);
            if (relationship == null)
            {
                return NotFound();
            }
            ViewData["PersonAId"] = new SelectList(_context.People, "Id", "Id", relationship.PersonAId);
            ViewData["PersonBId"] = new SelectList(_context.People, "Id", "Id", relationship.PersonBId);
            return View(relationship);
        }

        // POST: Relationships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonAId,PersonBId,RelationshipTypeDiscriminator,Id,CreatedAt,LastUpdatedAt,DeletedAt")] Relationship relationship)
        {
            if (id != relationship.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationshipExists(relationship.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonAId"] = new SelectList(_context.People, "Id", "Id", relationship.PersonAId);
            ViewData["PersonBId"] = new SelectList(_context.People, "Id", "Id", relationship.PersonBId);
            return View(relationship);
        }

        // GET: Relationships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relationship = await _context.Relationships
                .Include(r => r.PersonA)
                .Include(r => r.PersonB)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relationship == null)
            {
                return NotFound();
            }

            return View(relationship);
        }

        // POST: Relationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relationship = await _context.Relationships.FindAsync(id);
            if (relationship != null)
            {
                _context.Relationships.Remove(relationship);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationshipExists(int id)
        {
            return _context.Relationships.Any(e => e.Id == id);
        }
    }
}
