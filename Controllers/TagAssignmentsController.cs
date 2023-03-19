using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teko.Diary.Data;
using Teko.Diary.Models;

namespace Teko.Diary.Controllers
{
	[Authorize]
	public class TagAssignmentsController : Controller
	{
		private readonly ApplicationDbContext _context;

		public TagAssignmentsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: TagAssignments
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.TagAssignment.Include(t => t.Entry).Include(t => t.Tag);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: TagAssignments/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.TagAssignment == null)
			{
				return NotFound();
			}

			var tagAssignment = await _context.TagAssignment
				.Include(t => t.Entry)
				.Include(t => t.Tag)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (tagAssignment == null)
			{
				return NotFound();
			}

			return View(tagAssignment);
		}

		// GET: TagAssignments/Create
		public IActionResult Create()
		{
			ViewData["EntryId"] = new SelectList(_context.Entry, "Id", "Text");
			ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id");
			return View();
		}

		// POST: TagAssignments/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,TagId,EntryId")] TagAssignment tagAssignment)
		{
			if (ModelState.IsValid)
			{
				_context.Add(tagAssignment);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewData["EntryId"] = new SelectList(_context.Entry, "Id", "Text", tagAssignment.EntryId);
			ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", tagAssignment.TagId);
			return View(tagAssignment);
		}

		// GET: TagAssignments/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.TagAssignment == null)
			{
				return NotFound();
			}

			var tagAssignment = await _context.TagAssignment.FindAsync(id);
			if (tagAssignment == null)
			{
				return NotFound();
			}

			ViewData["EntryId"] = new SelectList(_context.Entry, "Id", "Text", tagAssignment.EntryId);
			ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", tagAssignment.TagId);
			return View(tagAssignment);
		}

		// POST: TagAssignments/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,TagId,EntryId")] TagAssignment tagAssignment)
		{
			if (id != tagAssignment.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(tagAssignment);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TagAssignmentExists(tagAssignment.Id))
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

			ViewData["EntryId"] = new SelectList(_context.Entry, "Id", "Text", tagAssignment.EntryId);
			ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Id", tagAssignment.TagId);
			return View(tagAssignment);
		}

		// GET: TagAssignments/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.TagAssignment == null)
			{
				return NotFound();
			}

			var tagAssignment = await _context.TagAssignment
				.Include(t => t.Entry)
				.Include(t => t.Tag)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (tagAssignment == null)
			{
				return NotFound();
			}

			return View(tagAssignment);
		}

		// POST: TagAssignments/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.TagAssignment == null)
			{
				return Problem("Entity set 'ApplicationDbContext.TagAssignment'  is null.");
			}

			var tagAssignment = await _context.TagAssignment.FindAsync(id);
			if (tagAssignment != null)
			{
				_context.TagAssignment.Remove(tagAssignment);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TagAssignmentExists(int id)
		{
			return (_context.TagAssignment?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}