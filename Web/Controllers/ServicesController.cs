using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccessLayer.Context;
using Web.Models;
using BusinessLogicLayer.Services.Interface;
using AutoMapper;
using System.Drawing.Printing;

namespace Web.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ISkinTimeService _service;
        private readonly IMapper _mapper;
        private readonly ICategotiryService _categotiryService;
        private readonly ISkinTypeService _skinTypeService;
        public string? SearchQuery { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 12;
        public ServicesController(ISkinTimeService service,ISkinTypeService skinTypeService,ICategotiryService categotiryService ,IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
            _skinTypeService = skinTypeService;
            _categotiryService = categotiryService;
        }

        public async Task<ActionResult> Index(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page)
        {
            var categories = await _categotiryService.GetCategories();
            var skinTypes = await _skinTypeService.GetSkinTypes();
             int pageNumber = page ?? 1; 
            var listServices = await _service.GetServices(searchKey,categoryId,skinTypeId ,pageNumber, PageSize);
            var litServiceViewModel =  _mapper.Map<ICollection<ServicesViewModel>>(listServices);
            var totalService = await _service.CountTotalServices();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.SkinTypes = new SelectList(skinTypes, "Id", "Name");
            int totalPages = (int)Math.Ceiling((double)totalService / PageSize.Value);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            int role = 2;// 1.manager,2 user

            if (role == 1)
            {
                return View("IndexDaboard", litServiceViewModel);
            }

            return View(litServiceViewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {

            var services = await _service.GetService(id);            
            return View(services);
        }

        //// GET: Services/Create
  

        //// POST: Services/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ServiceName,Description,Duration,Thumbnail,Price,Status,ServiceCategoryId,Id,CreatedAt,LastUpdate")] Service service)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        service.Id = Guid.NewGuid();
        //        _context.Add(service);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories, "Id", "Id", service.ServiceCategoryId);
        //    return View(service);
        //}

        //// GET: Services/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var service = await _context.Services.FindAsync(id);
        //    if (service == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories, "Id", "Id", service.ServiceCategoryId);
        //    return View(service);
        //}

        //// POST: Services/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("ServiceName,Description,Duration,Thumbnail,Price,Status,ServiceCategoryId,Id,CreatedAt,LastUpdate")] Service service)
        //{
        //    if (id != service.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(service);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ServiceExists(service.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ServiceCategoryId"] = new SelectList(_context.ServiceCategories, "Id", "Id", service.ServiceCategoryId);
        //    return View(service);
        //}

        //// GET: Services/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var service = await _context.Services
        //        .Include(s => s.ServiceCategoryNavigation)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (service == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(service);
        //}

        //// POST: Services/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var service = await _context.Services.FindAsync(id);
        //    if (service != null)
        //    {
        //        _context.Services.Remove(service);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ServiceExists(Guid id)
        //{
        //    return _context.Services.Any(e => e.Id == id);
        //}
    }
}
