using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context) { }

        public async Task<List<Question>> GetQuestions()
     => await ((ApplicationDbContext)context).Questions
         .Include(m => m.QuestionOptions)
         .OrderBy(q => q.OrderNo) 
         .ToListAsync();
        public async Task<Dictionary<SkinType, double>> GetSkinTypePercentagesAsync(List<Guid> listResult)
        {
            var allSkinTypes = await ((ApplicationDbContext)context).Set<SkinType>().ToListAsync();

            var questionOptions = await ((ApplicationDbContext)context).Set<QuestionOption>()
                .Include(qo => qo.SkinTypeNavigation)
                .Where(qo => listResult.Contains(qo.Id))
                .ToListAsync();

            if (!questionOptions.Any())
            {
                return allSkinTypes.ToDictionary(st => st, st => 0.0);
            }

            var skinTypeCounts = questionOptions
                .SelectMany(qo => qo.SkinTypeNavigation)
                .GroupBy(st => st)
                .ToDictionary(g => g.Key, g => g.Count());

            int totalSelections = skinTypeCounts.Values.Sum();

            var skinTypePercentages = allSkinTypes.ToDictionary(
                st => st,
                st => skinTypeCounts.ContainsKey(st)
                    ? Math.Round((double)skinTypeCounts[st] / totalSelections * 100, 1)
                    : 0.0
            );

            return skinTypePercentages;
        }
    }
}
