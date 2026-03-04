using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using VisionWorkOrderApp.Commands;
using VisionWorkOrderApp.Models;


namespace VisionWorkOrderApp.ViewModels
{
    public class WorkOrderViewModel : BaseViewModel
    {
        //DB 전역 선언
        private VisionDbContext _db = new VisionDbContext();
        public ObservableCollection<WorkOrder> WorkOrders { get; set; }
        public ObservableCollection<Equipment> Equipments { get; set; }

        private WorkOrder _selectedWorkOrder;
        public WorkOrder SelectedWorkOrder
        {
            get => _selectedWorkOrder;
            set
            {
                _selectedWorkOrder = value;
                OnPropertyChanged();

                // 선택한 항목을 입력 폼에 채우기
                if (value != null)
                {
                    NewProductName = value.ProductName;
                    NewQuantity = value.Quantity;
                    NewStatus = value.Status;
                    NewEquipmentName = value.EquipmentName;
                }
            }
        }
        // ────────────────
        // 입력 폼속성 (상품이름)
        // ────────────────
        private string _newProductName;
        public string NewProductName
        {
            get { return _newProductName; }
            set
            {
                _newProductName = value;
                OnPropertyChanged();
            }
        }
        // ────────────────
        // 입력 폼속성 (수량)
        // ────────────────
        private int _newQuantity;
        public int NewQuantity
        {
            get { return _newQuantity; }
            set
            {
                _newQuantity = value;
                OnPropertyChanged();
            }
        }
        // ────────────────
        // 입력 폼속성 (상태)
        // ────────────────
        private string _newStatus;
        public string NewStatus
        {
            get { return _newStatus; }
            set
            {
                _newStatus = value;
                OnPropertyChanged();
            }
        }
        // ────────────────
        // 입력 ComboBox (새로운 장비번호)
        // ────────────────
        private string _newEquipmentName;
        public string NewEquipmentName
        {
            get { return _newEquipmentName; }
            set { _newEquipmentName = value; OnPropertyChanged(); }
        }

        private Equipment _selectedEquipment;
        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set {_selectedEquipment = value; OnPropertyChanged();
                // ID 자동 설정 
                if (value != null)
                {
                    NewEquipmentName = value.Name;
                }
            }
        }


        // Command (버튼 동작)
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }


        // 생성자 (초기화)
        public WorkOrderViewModel()
        {
            try
            {
                WorkOrders = new ObservableCollection<WorkOrder>(_db.WorkOrders.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB 오류: " + ex.Message);
                WorkOrders = new ObservableCollection<WorkOrder>();
            }

            Equipments = new ObservableCollection<Equipment>()
            {
                new Equipment(1, "검사기 A호"),
                new Equipment(2, "검사기 B호"),
                new Equipment(3, "검사기 C호"),
            };

            AddCommand = new RelayCommand(AddWorkOrder);
            EditCommand = new RelayCommand(EditWorkOrder);
            DeleteCommand = new RelayCommand(DeleteWorkOrder);
            ClearCommand = new RelayCommand(ClearForm);
        }
        // ────────────────
        // 추가
        // ────────────────
        private void AddWorkOrder()
        {
            //유효성 검사
            if (string.IsNullOrWhiteSpace(NewProductName))
            {
                MessageBox.Show("제품명을 입력하세요!");
                return;
            }
            if (NewQuantity <= 0)
            {
                MessageBox.Show("수량을 입력하세요!");
                return;
            }
            if (string.IsNullOrWhiteSpace(NewStatus))
            {
                MessageBox.Show("상태를 선택하세요!");
                return;
            }
            if (SelectedEquipment == null)
            {
                MessageBox.Show("설비를 선택하세요!");
                return;
            }
            WorkOrder newWorkOrder = new WorkOrder(NewProductName, NewQuantity, NewStatus, NewEquipmentName);
            //입력 폼에서 받은 값으로 WorkOrder 객체를 만드는 것
            //Id 는 0 → DB 가 자동으로 채워줌

            /*
             * _db.WorkOrders
             → MSSQL 의 WorkOrders 테이블
             → DB 안에 있는 테이블
             */

            /*
             * WorkOrders
            → 화면에 표시되는 목록
            → ObservableCollection (메모리)
             */
            _db.WorkOrders.Add(newWorkOrder); //EF 에게 "이 객체를 DB 에 추가할 거야" 라고 알려주는 것
            _db.SaveChanges(); //실제로 DB 에 INSERT INTO WorkOrders (...) VALUES (...) 실행
            WorkOrders.Add(newWorkOrder);
            ClearForm();
        }
        // ────────────────
        // 수정
        // ────────────────
        private void EditWorkOrder()
        {
            if (SelectedWorkOrder == null)
            {
                MessageBox.Show("수정할 항목을 선택하세요!");
                return;
            }
            //나중에 트러블슈팅
            ////새 객체를 만들어서 교체했지만 화면만 바뀌고 DB에는 반영이 안됨
            ////EF는 리스트에 있는 객체를 추적한다. -> 새로운 객체로 만들었서 그 index에 넣었지만 화면은 보임, 하지만! EF는 모르는 객체로 판단한다.->
            //int index = WorkOrders.IndexOf(SelectedWorkOrder); //새로운 객체
            //WorkOrders[index] = new WorkOrder(NewProductName, NewQuantity, NewStatus, NewEquipmentName);

            SelectedWorkOrder.ProductName = NewProductName;
            SelectedWorkOrder.Quantity = NewQuantity;
            SelectedWorkOrder.Status = NewStatus;
            SelectedWorkOrder.EquipmentName = NewEquipmentName;
            _db.SaveChanges();
            ClearForm();
        }
        // ────────────────
        // 삭제
        // ────────────────
        private void DeleteWorkOrder()
        {
            if (SelectedWorkOrder == null)
            {
                MessageBox.Show("삭제할 항목을 선택하세요!");
                return;
            }
            MessageBoxResult result = MessageBox.Show($"{SelectedWorkOrder.ProductName}을(를) 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _db.WorkOrders.Remove(SelectedWorkOrder);
                _db.SaveChanges();
                WorkOrders.Remove(SelectedWorkOrder);
                MessageBox.Show("삭제되었습니다!");
            }
        }
        // ────────────────
        //초기화
        // ────────────────
        private void ClearForm()
        {
            NewProductName = "";
            NewQuantity = 0;
            NewStatus = null;
            NewEquipmentName = "";
            SelectedWorkOrder = null;

        }

    }
}
