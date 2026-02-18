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
    /// WorkOrderView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WorkOrderView : UserControl
    {
        public WorkOrderView()
        {
            // ViewModel을 DataContext에 연결
            // 이제 XAML에서 ViewModel의 데이터를 사용할 수 있음!
            InitializeComponent();
            this.DataContext = new WorkOrderViewModel();
        }
    }

    /*
     * DataContext 란 ?
     * View(화면)와 ViewModel(데이터)를 연결하는 다리
       Java Spring의 Model과 비슷한 개념
     */
}
