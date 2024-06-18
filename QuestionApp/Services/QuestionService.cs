using Microsoft.EntityFrameworkCore;
using QuestionApp.Data;
using QuestionApp.Entity;

namespace QuestionApp.Services
{

    public class QuestionService : IQuestionService
    {
        private readonly QuestionAppDbContext _context;

        public QuestionService(QuestionAppDbContext context)
        {
            _context = context;
        }

        public async Task AddQuestion(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuestion(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> GetQuestionById(Guid id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task AddResponses(List<Response> responses)
        {
            _context.Responses.AddRange(responses);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Response>> GetResponsesByIds(List<Guid> id)
        {
            return await _context.Responses.Where(r => id.Contains(r.Id)).ToListAsync();
        }
        public void AddResponses(IEnumerable<Response> responses)
        {
            _context.Responses.AddRange(responses);
            _context.SaveChanges();
        }

    }
}



