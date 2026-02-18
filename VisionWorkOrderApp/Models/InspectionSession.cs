using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    //검사 세션
     class InspectionSession
    {
        public int Id { get; private set; }
        public int WorkOrderId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int OkCount { get; set; }
        public int NgCount { get; set; }
        public InspectionSession(int id, int workOrderId,int equipmentId)
        {
            Id = id;
            WorkOrderId = workOrderId;
            EquipmentId = equipmentId;
            StartTime = DateTime.Now;
            OkCount = 0;
            NgCount = 0;
        }
    }
}
