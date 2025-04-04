using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccessLayer.Context;
using BusinessLogicLayer.Services.Interface;

namespace Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogService _service;

        public BlogsController(IBlogService blogService)
        {
            _service = blogService;
        }

        // GET: Blogs
        public async Task<IActionResult> Index(int page = 1)
        {
            return View(await _service.GetBlogPaginated(page, 6));
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            Console.WriteLine(id);

            if (id == null)
            {
                return NotFound();
            }

            Blog? blog = await _service.GetBlogWithId((Guid) id);

            if (blog == null)
            {
                return NotFound();
            }
            ViewData["other_blogs"] = await _service.GetBlogPaginated(1, 3);
            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,Thumbnail,AuthorId,Status,Id,CreatedAt,LastUpdate")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Id = Guid.NewGuid();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Content,Thumbnail,AuthorId,Status,Id,CreatedAt,LastUpdate")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
