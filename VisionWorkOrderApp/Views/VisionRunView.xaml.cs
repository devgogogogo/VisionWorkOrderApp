using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VisionWorkOrderApp.ViewModels;

namespace VisionWorkOrderApp.Views
{
    /// <summary>
    /// VisionRunView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VisionRunView : UserControl
    {
        public VisionRunView()
        {
            InitializeComponent();
            this.DataContext = new InspectionSessionViewModel();    
        }
    }
}

/*
 *  왜 하는 건가요?
```
DataContext 연결 안 하면
→ {Binding WorkOrders}, {Binding OkCount} 가
   어디서 데이터를 가져올지 몰라서 화면에 아무것도 안 뜸!

DataContext = InspectionSessionViewModel 연결하면
→ 모든 {Binding ...} 이 InspectionSessionViewModel 을 바라봄
 */
