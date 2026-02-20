using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    public class InspectionResult
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Label { get; set; }
        public double? confidence { get; set; }
        public string ImagePath { get; set; }

        public InspectionResult(int id, int sessionId, string label)
        {
            Id = id;
            SessionId = sessionId;
            Label = label;
            TimeStamp = DateTime.Now;
        }
    }
}
