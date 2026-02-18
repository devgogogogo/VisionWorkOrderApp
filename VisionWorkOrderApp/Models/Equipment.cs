using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    public class Equipment
    {
        public int Id { get; private set; }
        public string Name { get;  set; }

        public Equipment(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
