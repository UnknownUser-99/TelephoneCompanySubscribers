using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneCompanySubscribers.Model
{
    public class StreetsTable
    {
        private DataTable streetsTable;

        public StreetsTable(Dictionary<int, Street> streets, Dictionary<int, Address> addresses)
        {
            streetsTable = new DataTable();

            streetsTable.Columns.Add("Street");
            streetsTable.Columns.Add("Abonents");

            CreateTable(streets, addresses);
        }

        public DataTable GetTable()
        {
            return streetsTable;
        }

        private void CreateTable(Dictionary<int, Street> streets, Dictionary<int, Address> addresses)
        {
            Dictionary<string, int> streetsCount = new Dictionary<string, int>();

            foreach (Address address in addresses.Values)
            {
                string streetName = streets[address.StreetID].StreetName;

                if (streetsCount.ContainsKey(streetName))
                {
                    streetsCount[streetName]++;
                }
                else
                {
                    streetsCount.Add(streetName, 1);
                }
            }

            foreach (var kvp in streetsCount)
            {
                streetsTable.Rows.Add(kvp.Key, kvp.Value);
            }
        }
    }
}
