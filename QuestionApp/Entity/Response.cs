namespace QuestionApp.Entity
{
    public class Response
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public string ResponseText { get; set; }
    }

}
