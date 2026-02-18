using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using VisionWorkOrderApp.Commands;
using VisionWorkOrderApp.Models;


namespace VisionWorkOrderApp.ViewModels
{
    public class WorkOrderViewModel : INotifyPropertyChanged
    {
        // PropertyChanged 이벤트 + 메서드
        // ([CallerMemberName] : 자동으로 호출한 속성 이름을 채워줌
        public event PropertyChangedEventHandler PropertyChanged; //UI에게 알려주는 확성기

        public void OnPropertyChanged([CallerMemberName] string name = null) //확성기를 울리는 버튼
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<WorkOrder> WorkOrders { get; set; } // 속성 (Property) = 필드 + getter + setter를 한 번에!

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
                    NewEquipmentId = value.EquipmentId;
                }
            }
        }
        // ────────────────
        // 입력 폼속성
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
        private int _newEquipmentId;
        public int NewEquipmentId
        {
            get { return _newEquipmentId; }
            set { _newEquipmentId = value; OnPropertyChanged(); }
        }

        // Command (버튼 동작)
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public ICommand DeleteCommand { get; set; }
        public ICommand ClearCommand { get; set; }


        // 생성자 (초기화)
        public WorkOrderViewModel()
        {
            WorkOrders = new ObservableCollection<WorkOrder>()
            {
                 new WorkOrder(1,"스마트폰 케이스", 100, "대기",1),
                 new WorkOrder(2,"노트북 스탠드", 200, "진행중",2),
                 new WorkOrder(3,"무선 마우스", 150, "완료",1)
            };
            // Command 초기화
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
                System.Windows.MessageBox.Show("제품명을 입력하세요!");
                return;
            }
            if (NewQuantity <= 0)
            {
                System.Windows.MessageBox.Show("수량을 입력하세요!");
                return;
            }
            if (string.IsNullOrWhiteSpace(NewStatus))
            {
                System.Windows.MessageBox.Show("상태를 선택하세요!");
                return;
            }
            if (NewEquipmentId <= 0)
            {
                System.Windows.MessageBox.Show("설비ID를 입력하세요!");
                return;
            }

            int newId = WorkOrders.Count + 1;
            WorkOrders.Add(new WorkOrder(newId, NewProductName, NewQuantity, NewStatus, NewEquipmentId));
            ClearForm();
        }
        // ────────────────
        // 수정
        // ────────────────
        private void EditWorkOrder()
        {
            if (SelectedWorkOrder == null)
            {
                System.Windows.MessageBox.Show("수정할 항목을 선택하세요!");
                return;
            }
            int index = WorkOrders.IndexOf(SelectedWorkOrder);
            WorkOrders[index] = new WorkOrder(SelectedWorkOrder.Id, NewProductName, NewQuantity, NewStatus,NewEquipmentId);

            ClearForm();

        }

        // ────────────────
        // 삭제
        // ────────────────
        private void DeleteWorkOrder()
        {
            if (SelectedWorkOrder == null)
            {
                System.Windows.MessageBox.Show("삭제할 항목을 선택하세요!");
                return;
            }
            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show($"{SelectedWorkOrder.ProductName}을(를) 삭제하시겠습니까?", "삭제 확인", System.Windows.MessageBoxButton.YesNo);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                WorkOrders.Remove(SelectedWorkOrder);
                System.Windows.MessageBox.Show("삭제되었습니다!");
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
            NewEquipmentId = 0;
            SelectedWorkOrder = null;

        }

    }
}
