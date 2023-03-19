using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teko.Diary.Data;
using Teko.Diary.Models;

namespace Teko.Diary.Controllers
{
	[Authorize]
	public class DiariesController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<IdentityUser> _userManager;

		public DiariesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Diaries
		public async Task<IActionResult> Index()
		{
			IdentityUser? user = await _userManager.GetUserAsync(HttpContext.User);
			return _context.Diary != null
				? View(await _context.Diary.Where(d => d.User.Id == user.Id).ToListAsync())
				: Problem("Entity set 'ApplicationDbContext.Diary'  is null.");
		}

		// GET: Diaries/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Diaries/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] Models.Diary diary)
		{
			IdentityUser? user = await _userManager.GetUserAsync(HttpContext.User);
			diary.User = user;

			if (ModelState.IsValid)
			{
				_context.Add(diary);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(diary);
		}

		// GET: Diaries/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Diary == null)
			{
				return NotFound();
			}

			var diary = await _context.Diary.FindAsync(id);
			if (diary == null)
			{
				return NotFound();
			}

			if (diary?.User?.Id != null && !await IsAuthorized(diary?.User?.Id))
			{
				return Unauthorized();
			}

			return View(diary);
		}

		// POST: Diaries/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Models.Diary diary)
		{
			if (id != diary.Id)
			{
				return NotFound();
			}

			if (diary?.User?.Id != null && !await IsAuthorized(diary?.User?.Id))
			{
				return Unauthorized();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(diary);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!DiaryExists(diary.Id))
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

			return View(diary);
		}

		// GET: Diaries/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Diary == null)
			{
				return NotFound();
			}

			var diary = await _context.Diary
				.FirstOrDefaultAsync(m => m.Id == id);
			if (diary == null)
			{
				return NotFound();
			}

			if (diary?.User?.Id != null && !await IsAuthorized(diary?.User?.Id))
			{
				return Unauthorized();
			}

			return View(diary);
		}

		// POST: Diaries/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Diary == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Diary'  is null.");
			}

			var diary = await _context.Diary.FindAsync(id);
			if (diary != null)
			{
				_context.Diary.Remove(diary);
			}

			if (diary?.User?.Id != null && !await IsAuthorized(diary?.User?.Id))
			{
				return Unauthorized();
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private async Task<bool> IsAuthorized(string id)
		{
			IdentityUser? user = await _userManager.GetUserAsync(HttpContext.User);
			return user.Id == id;
		}

		private bool DiaryExists(int id)
		{
			return (_context.Diary?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}