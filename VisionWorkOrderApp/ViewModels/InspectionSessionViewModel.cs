
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisionWorkOrderApp.Models;
using VisionWorkOrderApp.Commands;

namespace VisionWorkOrderApp.ViewModels
{
    public class InspectionSessionViewModel : BaseViewModel
    {
        // DB 전역 선언
        private VisionDbContext _db = new VisionDbContext();

        // 작업지시 목록 (comboBox용)
        public ObservableCollection<WorkOrder> WorkOrders { get; set; }

        // 검사 결과 목록
        public ObservableCollection<InspectionResult> Results { get; set; }

        //선택된 작업 지시
        private WorkOrder _selectedWorkOrder;

        public WorkOrder SelectedWorkOrder
        {
            get { return _selectedWorkOrder; }
            set { _selectedWorkOrder = value; OnPropertyChanged(); }
        }

        //Ok 카운트
        private int _okCount;

        public int OkCount
        {
            get { return _okCount; }
            set { _okCount = value; OnPropertyChanged(); }
        }
        // NG 카운트
        private int ngCount;
        public int NgCount
        {
            get { return ngCount; }
            set { ngCount = value; OnPropertyChanged(); }
        }
        public ICommand OkCommand { get; set; }
        public ICommand NgCommand { get; set; }
        //생성자
        public InspectionSessionViewModel()
        {
            // DB 에서 작업지시 가져오기
            WorkOrders = new ObservableCollection<WorkOrder>(_db.WorkOrders.ToList());
            OkCommand = new RelayCommand(AddOk);
            NgCommand = new RelayCommand(AddNg);

            Results = new ObservableCollection<InspectionResult>();
        }
        private void AddOk()
        {
            OkCount++;
            Results.Add(new InspectionResult(1, "Ok"));
        }
        public void AddNg()
        {
            NgCount++;
            Results.Add(new InspectionResult(1, "NG"));
        }
    }
}
