using System.ComponentModel.DataAnnotations;

namespace QuestionApp.Dtos
{
    public class QuestionDtos
    {

        [Required]
        [StringLength(50)]
        public string QuestionText { get; set; }
        [Required]
        [StringLength(500)]
        public string QuestionType { get; set; }
        public List<string> Options { get; set; }
    }
}
