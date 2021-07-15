using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
   public class ReserveDto
    {
        public int ID { get; set; }
        public int Number_of_guests { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<meal> meals { get; set; }
        public string Special_requests { get; set; }

        public string UserID { get; set; }
       
    }
}
