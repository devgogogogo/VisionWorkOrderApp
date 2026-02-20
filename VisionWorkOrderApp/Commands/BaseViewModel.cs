using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.ViewModels
{

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; //UI에게 알려주는 확성기

        // PropertyChanged 이벤트 + 메서드
        // ([CallerMemberName] : 자동으로 호출한 속성 이름을 채워줌
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}



/*
 * WorkOrderViewModel
public event PropertyChangedEventHandler PropertyChanged;
public void OnPropertyChanged([CallerMemberName] string name = null) { ... }

// EquipmentViewModel
public event PropertyChangedEventHandler PropertyChanged;  // 중복!
public void OnPropertyChanged([CallerMemberName] string name = null) { ... }  // 중복!

이렇게 중복이 있기 때문에 BaseViewModel 을 만들어줘서 중복을 줄인다.
 */
