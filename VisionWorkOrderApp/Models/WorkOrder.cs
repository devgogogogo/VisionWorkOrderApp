using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    public class WorkOrder : INotifyPropertyChanged
    {
        [Key]
        public int Id { get; set; }

        private string _productName;
        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(); }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(); }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        private string _equipmentName;
        public string EquipmentName
        {
            get => _equipmentName;
            set { _equipmentName = value; OnPropertyChanged(); }
        }

        public WorkOrder() { }
        public WorkOrder(string productName, int quantity, string status, string equipmentName)
        {
            ProductName = productName;
            Quantity = quantity;
            Status = status;
            EquipmentName = equipmentName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
