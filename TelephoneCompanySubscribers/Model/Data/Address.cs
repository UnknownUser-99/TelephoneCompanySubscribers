using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneCompanySubscribers.Model
{
    public class Address
    {
        public int AddressID { get; set; }
        public int StreetID { get; set; }
        public string AddressHouse { get; set; }
        public int AbonentID { get; set; }
    }
}
