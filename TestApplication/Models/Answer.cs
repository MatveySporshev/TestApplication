namespace TestApplication.Models
{
    public class Answer
    {
        public Guid AnswerId { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; }
        public bool IsEditing { get; set; }
    }
}
