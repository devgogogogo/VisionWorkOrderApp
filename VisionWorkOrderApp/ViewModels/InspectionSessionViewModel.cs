
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VisionWorkOrderApp.Commands;
using VisionWorkOrderApp.Models;
using OpenCvSharp;

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

        //생성자  →  화면 열릴 때 필요한 것들 미리 준비하는 곳!
        public InspectionSessionViewModel()
        {
            // 1. 데이터 초기화 (DB 에서 가져오기)
            WorkOrders = new ObservableCollection<WorkOrder>(_db.WorkOrders.ToList());

            // 2.커맨드 초기화(버튼 연결)
            OkCommand = new RelayCommand(AddOk);
            NgCommand = new RelayCommand(AddNg);

            // 3. 빈 컬렉션 초기화
            Results = new ObservableCollection<InspectionResult>();
            //테스트 카메라
            TestCamera();
        }

        //카메라 테스트 메서드
        private void TestCamera()
        {
            // 카메라 열기 (0 == 기본 카메라)
            VideoCapture capture = new VideoCapture(0);

            if (!capture.IsOpened())
            {
                MessageBox.Show("카메라를 찾을 수 없습니다.");
                return;
            }
            MessageBox.Show("카메라 연결 성공!");
            capture.Release();
        }

        private void AddOk()
        {
            //작업지시 선택 여부 확인
            if (SelectedWorkOrder == null)
            {
                MessageBox.Show("작업지시를 선택해주세요!");
                return;
            }
            OkCount++;
            string productName;
            if (SelectedWorkOrder == null)
            {
                productName = "미선택";
            }else
            {
                productName = SelectedWorkOrder.ProductName;
            }
            InspectionResult result = new InspectionResult("OK", productName);
            _db.InspectionResults.Add(result);
            _db.SaveChanges();
            Results.Add(result);
        }
        public void AddNg()
        {
            // 작업지시 선택 여부 확인
            if (SelectedWorkOrder == null)
            {
                MessageBox.Show("작업지시를 선택해주세요!");
                return;
            }
            NgCount++;
            string productName;
            if (SelectedWorkOrder == null)
            {
                productName = "미선택";
            }
            else
            {
                productName = SelectedWorkOrder.ProductName;
            }

            InspectionResult result = new InspectionResult("NG", productName);
            _db.InspectionResults.Add(result);
            _db.SaveChanges();
            Results.Add(result);
        }
    }
}
