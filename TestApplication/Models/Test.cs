using System.Collections.Generic;

namespace TestApplication.Models
{
    public class Test
    {
        public Guid TestId { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();


        public Test()
        {
            TestId = Guid.NewGuid();
        }
    }

}
