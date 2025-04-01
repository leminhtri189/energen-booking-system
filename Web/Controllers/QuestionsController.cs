using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;
        public QuestionsController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            var listQuestion =  await _questionService.GetQuestions();
            var listQuestionViewModel = _mapper.Map<List<QuestionsViewModel>>(listQuestion);
            return View(listQuestionViewModel);
        }

        public async Task<IActionResult> SubmitAnswers(Dictionary<int, Guid> SelectedAnswers)
        {
           var (skinTypes, services) = await _questionService.GetServiceRecommments(SelectedAnswers?.Values?.ToList());

            var viewModel = new ServiceRecommendationViewModel
            {
                SkinTypes = skinTypes,
                Services = services
            };

            return View("ServiceRecommendation", viewModel);

        }




        // GET: Questions/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = await _context.Questions
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(question);
        //}

        //// GET: Questions/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Questions/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderNo,Content,Id,CreatedAt,LastUpdate")] Question question)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        question.Id = Guid.NewGuid();
        //        _context.Add(question);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(question);
        //}

        //// GET: Questions/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = await _context.Questions.FindAsync(id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(question);
        //}

        //// POST: Questions/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("OrderNo,Content,Id,CreatedAt,LastUpdate")] Question question)
        //{
        //    if (id != question.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(question);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!QuestionExists(question.Id))
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
        //    return View(question);
        //}

        //// GET: Questions/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var question = await _context.Questions
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(question);
        //}

        //// POST: Questions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var question = await _context.Questions.FindAsync(id);
        //    if (question != null)
        //    {
        //        _context.Questions.Remove(question);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool QuestionExists(Guid id)
        //{
        //    return _context.Questions.Any(e => e.Id == id);
        //}
    }
}
