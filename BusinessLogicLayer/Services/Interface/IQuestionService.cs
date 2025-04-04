using BusinessObject.Entities;


namespace BusinessLogicLayer.Services.Interface
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestions();
        Task<(Dictionary<SkinType, double> SkinTypes, List<Service>? Services)> GetServiceRecommments(List<Guid> listResult);
    }
}
