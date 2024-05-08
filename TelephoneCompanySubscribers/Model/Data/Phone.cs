using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneCompanySubscribers.Model
{
    public class Phone
    {
        public int PhoneID { get; set; }
        public string PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public int AbonentID { get; set; }
    }
}
