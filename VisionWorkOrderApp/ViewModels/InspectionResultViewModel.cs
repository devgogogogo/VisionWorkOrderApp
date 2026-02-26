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

        // DB 전역 선언
        private VisionDbContext _db = new VisionDbContext();

        //검사 결과 목록
        public ObservableCollection<InspectionResult> Results { get; set; }

        //생성자
        public InspectionResultViewModel()
        {
            //DB에서 데이터 가져오기 
            Results = new ObservableCollection<InspectionResult>(_db.InspectionResults.ToList());
        }
    }
}
