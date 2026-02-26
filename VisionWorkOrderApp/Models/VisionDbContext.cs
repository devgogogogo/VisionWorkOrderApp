using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    //DbContext  →  JPA 의 EntityManager 랑 같은 역할
    //DbSet      →  JPA 의 Repository 랑 같은 역할
    public class VisionDbContext : DbContext
    {
        public VisionDbContext()
            :base("name=VisionMES")
        {
        }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<InspectionResult> InspectionResults { get; set; }
    }
}
