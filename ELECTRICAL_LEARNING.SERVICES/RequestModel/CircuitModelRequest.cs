using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Services.RequestModel
{
    public static class CircuitModelRequest
    {
        public class CreateModel
        {
            public string Name { get; set; }
            public string JsonData { get; set; }
            public int AccountId { get; set; }
            public int LessonId { get; set; }
        }

        public class UpdateModel
        {
            public string Name { get; set; }
            public string JsonData { get; set; }
            public int AccountId { get; set; }
            public int LessonId { get; set; }
        }
    }
}
