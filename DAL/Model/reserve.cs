using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class reserve
    {
        public reserve()
        {
            meals = new List<meal>();
        }
        public int ID { get; set; }
        public int Number_of_guests { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<meal> meals { get; set; }

        public string Special_requests { get; set; }

        [ForeignKey("user")]
        public string UserID { get; set; }
        public User user { get; set; }

    }
    
}
