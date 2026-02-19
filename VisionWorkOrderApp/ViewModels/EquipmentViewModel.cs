using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using VisionWorkOrderApp.Commands;
using VisionWorkOrderApp.Models;

namespace VisionWorkOrderApp.ViewModels
{
    public class EquipmentViewModel : BaseViewModel
    {
        
        public ObservableCollection<Equipment> Equipments { get; set; }

        private Equipment _selectedEquipment;
        public Equipment SelectedEquipment
        {
            get { return _selectedEquipment; }
            set
            {
                _selectedEquipment = value;
                OnPropertyChanged();

                //선택할때 입력 폼에 채워지기 
                if (value != null)
                {
                    NewName = value.Name;
                }
            }
        }
        // ────────────────
        // 입력 폼 속성 XAML: Text="{Binding NewName}" 와 연결
        // ────────────────
        private string _newName;
        public string NewName
        {
            get => _newName;
            set
            {
                _newName = value;
                OnPropertyChanged();
            }
        }
        // ────────────────
        // Commands (버튼) XAML 버튼의 Command="{Binding AddCommand}"와 연결
        // ────────────────
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand ClearCommand { get; set; }
        // ────────────────
        // 생성자
        // ────────────────
        public EquipmentViewModel()
        {
            // 샘플 데이터
            Equipments = new ObservableCollection<Equipment>()
            {
                new Equipment(1,"검사기 A호"),
                new Equipment(2,"검사기 B호"),
                new Equipment(3,"검사기 C호")
            };
            // Command 초기화
            AddCommand = new RelayCommand(AddEquipment);
            EditCommand = new RelayCommand(EditEquipment);
            DeleteCommand = new RelayCommand(DeleteEquipment);
            ClearCommand = new RelayCommand(ClearForm);
        }



        // ────────────────
        // 추가
        // ────────────────
        private void AddEquipment()
        {
            if (String.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("설비 이름을 입력하세요!");
                return;
            }
            // ID 중복 방지 - 가장 큰 ID + 1
            int newId;
            if (Equipments.Count > 0)
            {
                newId = Equipments.Max(e => e.Id) + 1;
            }
            else
            {
                newId = 1;
            }
            Equipments.Add(new Equipment(newId, NewName));

            // 입력 폼 초기화
            ClearForm();
        }
        // ────────────────
        // 수정
        // ────────────────
        private void EditEquipment()
        {
            // 선택 확인
            if (SelectedEquipment == null)
            {
                System.Windows.MessageBox.Show("수정할 항목을 선택하세요!");
                return;
            }
            // 유효성 검사
            if (String.IsNullOrWhiteSpace(NewName))
            {
                MessageBox.Show("입력란을 채워주세요!");
            }
            // 위치 찾기
            int index = Equipments.IndexOf(SelectedEquipment);
            // 그 자리에 새 객체로 교체
            Equipments[index] = new Equipment(SelectedEquipment.Id, NewName);
            // 입력 폼 초기화
            ClearForm();
        }
        // ────────────────
        // 삭제
        // ────────────────
        private void DeleteEquipment()
        {
            if (SelectedEquipment == null)
            {
                System.Windows.MessageBox.Show("삭제할 항목을 선택하세요!");
                return;
            }
            // 삭제 확인
            // 첫번째 파라미터 : 팝업창 표시될때 메인 텍스트
            // 두번째 파라미터 : 팝업창 제목
            // 세번째 파라미터 :  버튼 종류
            MessageBoxResult result = MessageBox.Show("정말로 삭제하시겠습니까?", "삭제확인", MessageBoxButton.YesNo);

            // [예]버튼을 눌렀을때 삭제
            if (result == MessageBoxResult.Yes)
            {
                Equipments.Remove(SelectedEquipment);
                MessageBox.Show("삭제되었습니다.");
            }
        }
        private void ClearForm()
        {
            NewName = "";
            SelectedEquipment = null;
        }
    }

}
