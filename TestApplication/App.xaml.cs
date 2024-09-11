using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using TestApplication.Models; 

namespace TestApplication
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            GenerateStartData();
        }

        private void GenerateStartData()
        {
            var tests = new List<Test>
            {
                new Test
                {
                    TestId = Guid.NewGuid(),
                    Title = "Тест по географии",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            QuestionId = Guid.NewGuid(),
                            Text = "Что из перечисленного страны?",
                            Answers = new ObservableCollection<Answer>
                            {
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Мексика", IsCorrect = true },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Лондон", IsCorrect = false }, 
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Греция", IsCorrect = true },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Атлантида", IsCorrect = false }
                            }
                        },
                        new Question
                        {
                            QuestionId = Guid.NewGuid(),
                            Text = "Столица Франции",
                            Answers = new ObservableCollection<Answer>
                            {
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Москва", IsCorrect = false },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Вашингтон", IsCorrect = false },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Берлин", IsCorrect = false },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "Париж", IsCorrect = true }
                            }
                        }
                    }
                },
                new Test
                {
                    TestId = Guid.NewGuid(),
                    Title = "Тест по математике",
                    Questions = new List<Question>
                    {
                        new Question
                        {
                            QuestionId = Guid.NewGuid(),
                            Text = "2 + 2 = ?",
                            Answers = new ObservableCollection<Answer>
                            {
                                new Answer { AnswerId = Guid.NewGuid(), Text = "2", IsCorrect = false },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "4", IsCorrect = true },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "9", IsCorrect = false },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "3", IsCorrect = false }
                            }
                        },
                        new Question
                        {
                            QuestionId = Guid.NewGuid(),
                            Text = "5 + ? = 10",
                            Answers = new ObservableCollection<Answer>
                            {
                                new Answer { AnswerId = Guid.NewGuid(), Text = "3", IsCorrect = false },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "10", IsCorrect = false },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "5", IsCorrect = true },
                                new Answer { AnswerId = Guid.NewGuid(), Text = "1", IsCorrect = false }
                            }
                        }
                    }
                }
            };

            var filePath = "tests.json";
            if (!File.Exists(filePath))
            {
                var json = JsonConvert.SerializeObject(tests, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
        }
    }
}
