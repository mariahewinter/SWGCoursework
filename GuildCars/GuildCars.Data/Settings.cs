using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data
{
    public class Settings
    {
        private static string _mode;
        private static string _connectionString;
        // The first time any repo requests the conn string it'll store it in this variable and stay
        // in memory the same the entire time the website is running. Do the work once, store forever~
        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(_connectionString))
                _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return _connectionString;
        }

        public static string GetRepositoryType()
        {
            if (string.IsNullOrEmpty(_mode))
                _mode = ConfigurationManager.AppSettings["Mode"].ToString();

            return _mode;
        }
    }
}
