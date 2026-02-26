using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace VisionWorkOrderApp.Models
{
    public class WorkOrder
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string EquipmentName { get; set; }

        //빈생성자 추가
        public WorkOrder() { }
        public WorkOrder (string productName, int quantity, string status, string equipmentName)
        {
            ProductName = productName;
            Quantity = quantity;
            Status = status;
            EquipmentName = equipmentName;
        }
    }
}
