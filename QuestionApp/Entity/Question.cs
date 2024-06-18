namespace QuestionApp.Entity
{
    public class Question
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public List<string> Options { get; set; }
    }
}

