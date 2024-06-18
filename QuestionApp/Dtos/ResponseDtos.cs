using System.ComponentModel.DataAnnotations;

namespace QuestionApp.Dtos
{
    public class ResponseDtos
    {
        [Required]
        public Guid QuestionId { get; set; }

        [Required]
        public string Response { get; set; }

        public string ResponseText { get; set; }

    }
}
