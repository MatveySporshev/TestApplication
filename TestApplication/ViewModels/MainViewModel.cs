using System.Collections.ObjectModel;
using System.Windows.Input;
using TestApplication.Data;
using TestApplication.Models;
using TestApplication.Views;
using System.Windows;

namespace TestApplication.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly TestDataService _dataService;
        private Test _selectedTest;

        public ObservableCollection<Test> Tests { get; set; }
        public ICommand CreateTestCommand { get; }
        public ICommand TakeTestCommand { get; }
        public ICommand DeleteTestCommand { get; }



        public Test SelectedTest
        {
            get => _selectedTest;
            set
            {
                _selectedTest = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _dataService = new TestDataService();
            Tests = new ObservableCollection<Test>(_dataService.LoadTests());

            CreateTestCommand = new RelayCommand(CreateTest);
            TakeTestCommand = new RelayCommand(TakeTest, CanTakeTest);
            DeleteTestCommand = new RelayCommand(DeleteTest, CanDeleteTest);

        }

        private void CreateTest(object obj)
        {
            var createTestWindow = new CreateTestView();
            createTestWindow.ShowDialog();

            
            Tests.Clear();
            foreach (var test in _dataService.LoadTests())
            {
                Tests.Add(test);
            }
        }

        private bool CanTakeTest(object obj) => SelectedTest != null;

        private void TakeTest(object obj)
        {
            if (SelectedTest != null)
            {
                var takeTestWindow = new TakeTestView(SelectedTest);
                takeTestWindow.ShowDialog();
            }
        }

        private void DeleteTest(object obj)
        {
            if (SelectedTest != null)
            {
               
                var allTests = _dataService.LoadTests();

                var testToRemove = allTests.FirstOrDefault(t => t.TestId == SelectedTest.TestId);


                allTests.Remove(testToRemove);
                
                Tests.Remove(SelectedTest);
                
                _dataService.SaveTests(allTests);

            }
            else
            {
                Console.WriteLine("Выбранный тест равен null.");
            }
        }
        private bool CanDeleteTest(object obj) => SelectedTest != null;
    }
}
