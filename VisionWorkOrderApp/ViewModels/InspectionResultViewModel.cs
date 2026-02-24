using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionWorkOrderApp.Models;

namespace VisionWorkOrderApp.ViewModels
{
    public class InspectionResultViewModel :BaseViewModel
    {
        //검사 결과 목록
        public ObservableCollection<InspectionResult> Results { get; set; }

        //생성자
        public InspectionResultViewModel()
        {
            Results = new ObservableCollection<InspectionResult>()
            {
                new InspectionResult(1,1,"OK"),
                new InspectionResult(2,1,"NG"),
                new InspectionResult(3,1,"OK")
            };
        }
    }
}
