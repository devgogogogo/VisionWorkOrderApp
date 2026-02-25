using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VisionWorkOrderApp.Models;

namespace VisionWorkOrderApp.ViewModels
{
    public class InspectionSessionViewModel : BaseViewModel
    {
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

            WorkOrders = new ObservableCollection<WorkOrder>()
            {
                new WorkOrder("스마트폰 케이스",100,"대기",1),
                new WorkOrder("노트북 스탠드",200,"진행중",2),
                new WorkOrder("스마트폰 케이스",300,"완료",1),
            };
            OkCommand = new RelayCommand(AddOk);
            NgCommand = new RelayCommand(AddNg);

            Results = new ObservableCollection<InspectionResult>();
        }
        private void AddOk()
        {
            OkCount++;
            Results.Add(new InspectionResult(Results.Count + 1, 1, "Ok"));
        }
        public void AddNg()
        {
            NgCount++;
            Results.Add(new InspectionResult(Results.Count + 1, 1, "NG"));
        }
    }
}
