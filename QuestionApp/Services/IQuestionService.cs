using QuestionApp.Entity;

namespace QuestionApp.Services
{
    public interface IQuestionService
    {

        Task AddQuestion(Question question);
        Task UpdateQuestion(Question question);
        Task<IEnumerable<Question>> GetAllQuestions();
        Task<Question> GetQuestionById(Guid id);
        Task AddResponses(List<Response> responses);
        Task<IEnumerable<Response>> GetResponsesByIds(List<Guid> id);
        void AddResponses(IEnumerable<Response> responses);
    }
}