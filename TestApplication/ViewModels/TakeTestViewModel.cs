using System.Collections.ObjectModel;
using System.Windows.Input;
using TestApplication.Models;
using System.Linq;
using System.Windows;
using TestApplication.Views;

namespace TestApplication.ViewModels
{
    public class TakeTestViewModel : ViewModelBase
    {
        public ObservableCollection<Question> Questions { get; set; }
        public ICommand SubmitTestCommand { get; }

        public TakeTestViewModel(Test test)
        {
            Questions = new ObservableCollection<Question>(test.Questions);
            SubmitTestCommand = new RelayCommand(SubmitTest);
        }

        private void SubmitTest(object obj)
        {
            int correctAnswers = 0;
            foreach (var question in Questions)
            {
                if (question.Answers.All(a => a.IsSelected == a.IsCorrect))
                {
                    correctAnswers++;
                }
            }

            double score = (double)correctAnswers / Questions.Count * 100;

            var result = MessageBox.Show($"Вы набрали {score:F0}%", "Результат теста", MessageBoxButton.OK, MessageBoxImage.Information);

            if (result == MessageBoxResult.OK)
            {
               
                var window = Application.Current.Windows.OfType<TakeTestView>().FirstOrDefault();
                if (window != null)
                {
                    window.Close();
                }
            }
        }
    }
}
