namespace Web.Models
{
    public class QuestionsViewModel
    {
        public int? OrderNo { get; set; }
        public string Content { get; set; }
        public ICollection<AnswerViewModel> AnswerViewModels { get; set; } = new List<AnswerViewModel>();
    }
    public class AnswerViewModel
    {
        public Guid Id { get; set; }

        public string AnswerContent { get; set; }
    }
    public class QuizSubmissionViewModel
    {
        public List<Guid> SelectedAnswers { get; set; } = new List<Guid>();
    }

}
