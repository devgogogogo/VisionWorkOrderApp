
using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using VisionWorkOrderApp.Commands;
using VisionWorkOrderApp.Models;

namespace VisionWorkOrderApp.ViewModels
{
    public class InspectionSessionViewModel : BaseViewModel
    {
        // 프레임
        Mat frame = new Mat();
        // DB 전역 선언
        private VisionDbContext _db = new VisionDbContext();

        //카메라 관련
        private VideoCapture videoCapture;
        private Thread thread;
        private bool _isRunning;

        //카메라 화면 (XAML 의 Image 와 바인딩)
        private BitmapSource bitmapSource;
        public BitmapSource BitmapSource
        {
            get { return bitmapSource; }
            set { bitmapSource = value; OnPropertyChanged(); }
        }

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
            StartCamera();
        }

        // 카메라 시작 메서드
        private void StartCamera()
        {
            videoCapture = new VideoCapture(0);

            if (!videoCapture.IsOpened())
            {
                MessageBox.Show("카메라를 찾을 수 없습니다!");
                return;
            }

            // 별도 스레드에서 카메라 실행 (UI 가 멈추지 않도록!)
            _isRunning = true;
            thread = new Thread(CameraLoop);
            thread.IsBackground = true;
            thread.Start();
        }
        // 카메라 루프 (계속 프레임 읽기)
        private void CameraLoop()
        {
            while (_isRunning)
            {
                // 카메라에서 프레임 1장 읽어서 frame 에 저장
                videoCapture.Read(frame);

                // 프레임이 비어있으면 다음 루프로 건너뜀
                // (카메라 연결 불안정할 때 대비)
                if (frame.Empty()) continue;

                // UI 스레드에서 화면 업데이트
                Application.Current.Dispatcher.Invoke(UpdateFrame);
                // Dispatcher.Invoke → UI 스레드에서 실행
                // ToBitmapSource() → Mat 을 WPF Image 로 변환
                // BitmapSource 바뀌면 → 화면 자동 업데이트!
                Thread.Sleep(33); // 33ms 마다 갱신 = 약 30fps
            }
        }
        private void UpdateFrame()
        {
            // 1. 카메라에서 프레임 읽기 + 좌우대칭
            Mat flipped = new Mat();
            Cv2.Flip(frame, flipped, FlipMode.Y); // 첫번째 파라미터 : 원본 프레임,두번째 파라미터 : 결과 저장할 Mat,세번째 파라미터 : FlipMode
                                                  // FlipMode.X → 상하 대칭
                                                  // FlipMode.Y → 좌우 대칭

            // 2. HSV 색상으로 변환 (빨간색 감지에 더 정확!) 
            Mat hsv = new Mat();
            Cv2.CvtColor(flipped, hsv, ColorConversionCodes.BGR2HSV);

            // 3. 빨간색 영역 마스킹
            // 빨간색은 HSV 에서 0~10 과 170~180 두 구간에 존재
            Mat mask1 = new Mat();
            Mat mask2 = new Mat();
            Mat mask = new Mat();
            Cv2.InRange(hsv, new Scalar(0, 100, 100), new Scalar(10, 255, 255), mask1);
            Cv2.InRange(hsv, new Scalar(170, 100, 100), new Scalar(180, 255, 255), mask2);
            Cv2.Add(mask1, mask2,mask);// 두 마스크 합치기

            // 4. 빨간색 비율 계산
            double totalPixels = flipped.Rows * flipped.Cols; // 전체 픽셀 수
            double redPixels = Cv2.CountNonZero(mask); // 빨간색 픽셀 수 
            double redRatio = redPixels / totalPixels; //빨간색 비율

            // 5. 비율이 기준치 이상 → OK / 미만 → NG
            string resultText;
            Scalar color;

            if (redRatio>0.05)
            {
                resultText = "Ok";
                color = Scalar.Green;
            }else
            {
                resultText = "NG";
                color = Scalar.Red;
            }

            // 6. 화면에 결과 텍스트 표시
            Cv2.PutText(flipped, resultText, new OpenCvSharp.Point(30, 60), HersheyFonts.HersheySimplex, 2, color, 3);
            BitmapSource = flipped.ToBitmapSource();
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
            string productName = SelectedWorkOrder.ProductName;
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
            string productName = SelectedWorkOrder.ProductName;
            InspectionResult result = new InspectionResult("NG", productName);
            _db.InspectionResults.Add(result);
            _db.SaveChanges();
            Results.Add(result);
        }
    }
}

/*
 * HSV 가 뭔가요?
 * RGB  →  빨강, 초록, 파랑 조합
   HSV  →  색상(Hue), 채도(Saturation), 밝기(Value)
       → 빛의 밝기에 덜 영향받음
       → 색상 감지에 더 정확!
 */
