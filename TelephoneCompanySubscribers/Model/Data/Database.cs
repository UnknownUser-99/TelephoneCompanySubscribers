using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace TelephoneCompanySubscribers.Model
{
    public class Database
    {
        private readonly string connectionString;

        public Database()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            connectionString = config["ConnectionStrings:DefaultConnection"];
        }

        public async Task<List<Abonent>> GetAbonents()
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var queryResult = await dbConnection.QueryAsync<Abonent>("SELECT AbonentID, AbonentFullName FROM Abonents");
                return queryResult.AsList();
            }
        }

        public async Task<Dictionary<int, List<Phone>>> GetPhones()
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var queryResult = await dbConnection.QueryAsync<Phone>("SELECT PhoneID, PhoneType, PhoneNumber, AbonentID FROM Phones");

                var groupedPhones = queryResult.GroupBy(phone => phone.AbonentID).ToDictionary(group => group.Key, group => group.ToList());

                return groupedPhones;
            }
        }

        public async Task<Dictionary<int, Street>> GetStreets()
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var queryResult = await dbConnection.QueryAsync<Street>("SELECT StreetID, StreetName FROM Streets");

                var groupedStreets = queryResult.GroupBy(street => street.StreetID).ToDictionary(group => group.Key, group => group.Single());

                return groupedStreets;
            }
        }

        public async Task<Dictionary<int, Address>> GetAddresses()
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var queryResult = await dbConnection.QueryAsync<Address>("SELECT AddressID, StreetID, AddressHouse, AbonentID FROM Addresses");

                var groupedAddresses = queryResult.GroupBy(address => address.AbonentID).ToDictionary(group => group.Key, group => group.Single());

                return groupedAddresses;
            }
        }
    }
}
