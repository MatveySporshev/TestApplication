using System.Windows;
using TestApplication.Models;
using TestApplication.ViewModels;

namespace TestApplication.Views
{
    public partial class TakeTestView : Window
    {
        public TakeTestView(Test test)
        {
            InitializeComponent();

            DataContext = new TakeTestViewModel(test);
        }
    }
}
