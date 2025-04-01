using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IUnitOfWork _unitOfWork;
        public QuestionService(IQuestionRepository questionRepository,IUnitOfWork unitOfWork)
        {
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;   
        }
        public async Task<List<Question>> GetQuestions() => await _questionRepository.GetQuestions();

        public async Task<(Dictionary<SkinType, double> SkinTypes, List<Service>? Services)> GetServiceRecommments(List<Guid> listResult)
        {
            var skinTypePercentages = await _unitOfWork.Questions.GetSkinTypePercentagesAsync(listResult);

            var maxPercentage = skinTypePercentages.Max(st => st.Value);
            var highestSkinTypes = skinTypePercentages
                .Where(st => st.Value == maxPercentage)
                .Select(st => st.Key)
                .ToList();

            if (!highestSkinTypes.Any()) return (skinTypePercentages, new List<Service>());

            var recommendedServices = await _unitOfWork.GenericRepository<Service>()
                .GetAllAsync(s => s.SkinTypes.Any(st => highestSkinTypes.Select(hst => hst.Id).Contains(st.Id)), null);

            return (skinTypePercentages, recommendedServices.ToList());
        }
    }
}
