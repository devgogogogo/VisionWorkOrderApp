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
using VisionWorkOrderApp.Views;
namespace VisionWorkOrderApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new WorkOrderView();
            PageTile.Text = "작업지시 관리";
        }

        private void Button_ClicK(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new WorkOrderView();
            PageTile.Text = "작업지시 관리";
        }

        private void BtnVisionRun_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new VisionRunView();
            PageTile.Text = "비젼 검사 실행";
        }

        private void BtnResultHistory_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ResultHistoryView();
            PageTile.Text = "검사 결과 이력";
        }

        private void BtnEquipment_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new EquipmentView();
            PageTile.Text = "설비관리";
        }
    }
}
