using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionWorkOrderApp.Models
{
    public class Equipment
    {
        [Key]
        public int Id { get;  set; }
        public string Name { get;  set; }

        public Equipment(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

}
