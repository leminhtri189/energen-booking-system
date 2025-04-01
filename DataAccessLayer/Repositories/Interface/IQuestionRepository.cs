using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<List<Question>> GetQuestions();
        Task<Dictionary<SkinType, double>> GetSkinTypePercentagesAsync(List<Guid> listResult);
    }
}
