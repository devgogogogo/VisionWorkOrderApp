using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    public class InspectionResult
    {
        [Key]
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public string Label { get; set; }
        public string ProductName { get; set; }
        public double? confidence { get; set; }
        public string ImagePath { get; set; }

        public InspectionResult() { }  // 기본 생성자 추가 해줘야함

        public InspectionResult(string label, string productName)
        {
            Label = label;
            ProductName = productName;
            TimeStamp = DateTime.Now;
        }
    }
}
