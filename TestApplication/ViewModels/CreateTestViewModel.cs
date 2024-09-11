using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestApplication.Data;
using TestApplication.Models;
using System.Windows;
using TestApplication.Views;
using System.IO;

namespace TestApplication.ViewModels
{
    public class CreateTestViewModel : ViewModelBase
    {
        private readonly TestDataService _dataService;
        private Answer _newAnswer; 

        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
        public ICommand AddQuestionCommand { get; }
        public ICommand SaveTestCommand { get; }
        public ICommand AddAnswerCommand { get; }
        public ICommand SaveNewAnswerCommand { get; } 

        public ICommand DeleteQuestionCommand { get; }

        public ICommand DeleteAnswerCommand { get; }



        public string TestTitle { get; set; }

        public Answer NewAnswer
        {
            get => _newAnswer;
            set
            {
                _newAnswer = value;
                OnPropertyChanged(nameof(NewAnswer));
            }
        }

        public CreateTestViewModel()
        {
            _dataService = new TestDataService();
            AddQuestionCommand = new RelayCommand(AddQuestion);
            AddAnswerCommand = new RelayCommand(AddAnswer);
            SaveTestCommand = new RelayCommand(SaveTest);
            SaveNewAnswerCommand = new RelayCommand(SaveNewAnswer); 
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);
            DeleteAnswerCommand = new RelayCommand(DeleteAnswer);
        }

        private void AddQuestion(object obj)
        {
            Questions.Add(new Question
            {
                Text = "Введите вопрос...",
                Answers = new ObservableCollection<Answer>()
            });
        }

        private void AddAnswer(object obj)
        {
            if (obj is Question question)
            {
                var newAnswer = new Answer { Text = "Введите ответ...", IsCorrect = false };
                question.Answers.Add(newAnswer);
                OnPropertyChanged(nameof(Questions));
            }
        }

        public void ToggleCorrectAnswer(Answer answer)
        {
            if (answer != null)
            {
                answer.IsCorrect = !answer.IsCorrect;
                OnPropertyChanged(nameof(Questions)); 
            }
        }

        private void SaveNewAnswer(object obj)
        {
            if (NewAnswer != null && !string.IsNullOrWhiteSpace(NewAnswer.Text))
            {
                NewAnswer = null; 
            }
        }

        private void SaveTest(object obj)
        {
            if (string.IsNullOrEmpty(TestTitle) || !Questions.Any()) return;

            var test = new Test
            {
                Title = TestTitle,
                Questions = new List<Question>(Questions)
            };

            var tests = _dataService.LoadTests();
            tests.Add(test);
            _dataService.SaveTests(tests);

            if (obj is Window window)
            {
                window.Close();
            }
            
            var result = MessageBox.Show("Тест успешно сохранен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            if (result == MessageBoxResult.OK)
            {
                
                var currentWindow = Application.Current.Windows.OfType<CreateTestView>().FirstOrDefault();
                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
        }

        private void DeleteQuestion(object obj)
        {
            if (obj is Question question && Questions.Contains(question))
            {
                Questions.Remove(question);
                OnPropertyChanged(nameof(Questions));
            }
        }

        private void DeleteAnswer(object obj)
        {
            if (obj is Answer answerToDelete)
            {
                foreach (var question in Questions)
                {
                    if (question.Answers.Contains(answerToDelete))
                    {
                        question.Answers.Remove(answerToDelete);
                        break; 
                    }
                }

                OnPropertyChanged(nameof(Questions)); 
            }
        }
    }
}
