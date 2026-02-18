using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace VisionWorkOrderApp.Models
{
    public class WorkOrder
    {

        public int Id { get; private set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public int EquipmentId { get; set; }  

        public WorkOrder (int id, string productName, int quantity, string status, int equipmentId)
        {
            Id = id;
            ProductName = productName;
            Quantity = quantity;
            Status = status;
            EquipmentId = equipmentId;
        }
    }
}
