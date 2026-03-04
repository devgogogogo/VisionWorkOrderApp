using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionWorkOrderApp.Models;

namespace VisionWorkOrderApp.ViewModels
{
    public class InspectionResultViewModel : BaseViewModel
    {

        // DB 전역 선언
        private VisionDbContext _db = new VisionDbContext();

        // 전체 결과 목록 (필터링 전 원본 데이터)
        private ObservableCollection<InspectionResult> _allResults;

        //필터 적용된 결과 목록
        public ObservableCollection<InspectionResult> FilteredResults { get; set; }

        //콤보박스 옵션 (전체/OK/NG)
        public ObservableCollection<string> FilterOptions { get; set; }  

        //필터 변경시 자동으로 ApplyFilter 호출
        private string _selectedFilter;
        public string SelectedFilter
        {
            get { return _selectedFilter; }
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();
                ApplyFilter(); //추가 필터 변경될 때마다 ApplyFilter 호출해야 함!

            }
        }
        //생성자
        public InspectionResultViewModel()
        {
            // DB에서 전체 데이터 가져오기
            _allResults = new ObservableCollection<InspectionResult>(_db.InspectionResults.ToList());
            //초기에는 전체 데이터 표시
            FilteredResults = new ObservableCollection<InspectionResult>(_allResults);
            //필터 옵션 초기화
            FilterOptions = new ObservableCollection<string>() { "전체", "OK", "NG" };
            SelectedFilter = "전체";
        }
        // 필터 적용 메서드
        private void ApplyFilter() //화면 목록 전부 비우기
        {
            FilteredResults.Clear();
            foreach (InspectionResult result in _allResults)
            {
                if (SelectedFilter == "전체" || result.Label == SelectedFilter)
                {
                    // "전체" 선택 → 다 보여줌
                    // "OK" 선택   → Label 이 "OK" 인 것만 보여줌
                    // "NG" 선택   → Label 이 "NG" 인 것만 보여줌
                    FilteredResults.Add(result); //조건 맞는 것만 화면에 추가
                }
            }
        }
    }
}
