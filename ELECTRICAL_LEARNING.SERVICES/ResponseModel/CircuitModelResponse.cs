using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Services.ResponseModel
{
    public static class CircuitModelResponse
    {
        public class GetModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string JsonData { get; set; }
            public int AccountId { get; set; }
            public int LessonId { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
            public DateTimeOffset UpdatedAt { get; set; }
        }
    }
}
