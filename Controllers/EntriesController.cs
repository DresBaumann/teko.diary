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
	public class EntriesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public EntriesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Entries
		public async Task<IActionResult> Index(int? id, DateTime? date)
		{
			var applicationDbContext =
				_context.Entry.Include(e => e.Diary);

			var entries = date != null ? applicationDbContext.Where(e => e.Date == date) : applicationDbContext;

			return View(nameof(Index), await entries.ToListAsync());
		}

		public async Task<IActionResult> ByTag(int id)
		{
			var applicationDbContext =
				_context.Entry.Include(e => e.Diary);
			var tag = _context.Tag.SingleOrDefault(t => t.Id == id);
			var assignedTags = await _context.TagAssignment.Where(t => t.TagId == id).Select(s => s.EntryId)
				.ToListAsync();

			var entries = applicationDbContext.Where(e => assignedTags.Contains(e.Id));

			ViewData["Tag"] = tag.Name;

			return View(await entries.ToListAsync());
		}

		// GET: Entries/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Entry == null)
			{
				return NotFound();
			}

			var entry = await _context.Entry
				.Include(e => e.Diary)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (entry == null)
			{
				return NotFound();
			}

			return View(entry);
		}

		// GET: Entries/Create
		public IActionResult Create(int? id, DateTime? date)
		{
			if (date != null)
			{
				ViewData["Date"] = date;
			}

			ViewData["Date"] = DateTime.Now;

			ViewData["DiaryId"] = id == null ? new SelectList(_context.Diary, "Id", "Name") : id;
			ViewData["Tags"] = new SelectList(_context.Tag, "Id", "Name");
			return View();
		}

		// POST: Entries/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(int[] tagIds,
			[Bind("Id,Date,Title,Text,DiaryId,ImagePath")]
			Entry entry)
		{
			if (ModelState.IsValid)
			{
				_context.Add(entry);
				await _context.SaveChangesAsync();

				foreach (var tagId in tagIds)
				{
					_context.TagAssignment.Add(new TagAssignment
					{
						EntryId = entry.Id,
						TagId = tagId,
					});
				}

				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			var tags = await _context.Tag.Where(t => tagIds.Contains(t.Id)).ToListAsync();
			entry.Tags = tags;

			ViewData["DiaryId"] = new SelectList(_context.Diary, "Id", "Id", entry.DiaryId);
			return View(entry);
		}

		// GET: Entries/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Entry == null)
			{
				return NotFound();
			}

			var entry = await _context.Entry.FindAsync(id);
			if (entry == null)
			{
				return NotFound();
			}

			ViewData["DiaryId"] = new SelectList(_context.Diary, "Id", "Id", entry.DiaryId);
			return View(entry);
		}

		// POST: Entries/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Text,DiaryId,ImagePath")] Entry entry)
		{
			if (id != entry.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(entry);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EntryExists(entry.Id))
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

			ViewData["DiaryId"] = new SelectList(_context.Diary, "Id", "Id", entry.DiaryId);
			return View(entry);
		}

		// GET: Entries/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Entry == null)
			{
				return NotFound();
			}

			var entry = await _context.Entry
				.Include(e => e.Diary)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (entry == null)
			{
				return NotFound();
			}

			return View(entry);
		}

		// POST: Entries/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Entry == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Entry'  is null.");
			}

			var entry = await _context.Entry.FindAsync(id);
			if (entry != null)
			{
				_context.Entry.Remove(entry);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool EntryExists(int id)
		{
			return (_context.Entry?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}