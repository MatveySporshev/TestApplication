using System.Collections.ObjectModel;

namespace TestApplication.Models
{
    public class Question
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; }
        public ObservableCollection<Answer> Answers { get; set; } = new ObservableCollection<Answer>();
    }
}
