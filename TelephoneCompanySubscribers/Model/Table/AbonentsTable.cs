using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneCompanySubscribers.Model
{
    public class AbonentsTable
    {
        private DataTable abonentsTable;
        private DataTable filteredTable;

        public AbonentsTable(List<Abonent> abonents, Dictionary<int, List<Phone>> phones, Dictionary<int, Street> streets, Dictionary<int, Address> addresses)
        {
            abonentsTable = new DataTable();

            abonentsTable.Columns.Add("FullName");
            abonentsTable.Columns.Add("Street");
            abonentsTable.Columns.Add("Home");
            abonentsTable.Columns.Add("HomeNumber");
            abonentsTable.Columns.Add("WorkNumber");
            abonentsTable.Columns.Add("MobileNumber");

            CreateTable(abonents, phones, streets, addresses);
            SortTable("FullName", "ASC");
        }

        public AbonentsTable(DataTable abonentsTable)
        {
            this.abonentsTable = abonentsTable;
        }

        public DataTable GetTable()
        {
            return abonentsTable;
        }

        public DataTable GetFilteredTable()
        {
            return filteredTable;
        }

        private void CreateTable(List<Abonent> abonents, Dictionary<int, List<Phone>> phones, Dictionary<int, Street> streets, Dictionary<int, Address> addresses)
        {
            foreach (Abonent abonent in abonents)
            {
                DataRow row = abonentsTable.NewRow();
                row["FullName"] = abonent.AbonentFullName;

                List<Phone> abonentPhones = phones[abonent.AbonentID];

                foreach (Phone phone in abonentPhones)
                {
                    switch (phone.PhoneType)
                    {
                        case "Home":
                            row["HomeNumber"] = phone.PhoneNumber;
                            break;
                        case "Work":
                            row["WorkNumber"] = phone.PhoneNumber;
                            break;
                        case "Mobile":
                            row["MobileNumber"] = phone.PhoneNumber;
                            break;
                    }
                }

                Address abonentAddress = addresses[abonent.AbonentID];
                Street street = streets[abonentAddress.StreetID];

                row["Street"] = street.StreetName;
                row["Home"] = abonentAddress.AddressHouse;

                abonentsTable.Rows.Add(row);
            }
        }

        public bool FindNumber(string phoneNumber)
        {
            DataTable filteredTable = abonentsTable.Clone();
            bool result = false;

            foreach (DataRow row in abonentsTable.Rows)
            {
                foreach (DataColumn column in abonentsTable.Columns)
                {
                    if (column.ColumnName == "HomeNumber" || column.ColumnName == "WorkNumber" || column.ColumnName == "MobileNumber")
                    {
                        string value = row[column].ToString();

                        if (value.Contains(phoneNumber))
                        {
                            filteredTable.ImportRow(row);
                            break;
                        }
                    }
                }
            }

            if (filteredTable.Rows.Count > 0)
            {
                result = true;
            }

            this.filteredTable = filteredTable;

            return result;
        }

        public void SortTable(string column, string direction)
        {
            DataView view = abonentsTable.DefaultView;

            view.Sort = $"{column} {direction}";

            abonentsTable = view.ToTable();
        }
    }
}
