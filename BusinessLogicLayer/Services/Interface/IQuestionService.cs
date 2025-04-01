using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestions();
        Task<(Dictionary<SkinType, double> SkinTypes, List<Service>? Services)> GetServiceRecommments(List<Guid> listResult);
    }
}
