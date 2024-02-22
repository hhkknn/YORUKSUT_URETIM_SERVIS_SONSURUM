using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AIF.UVTService.DatabaseLayer
{
    public class GetConnectionAsString
    {
        public string getConnectionAsString(string dbName, string mKodValue)
        {
            if (mKodValue == "10B1C4")
            {
                string connectionString = "";
                connectionString = string.Format("Server=172.55.10.16;Database={0};User Id=sa;Password=Qaz1Wsx2", dbName);

                return connectionString;
            }

            if (mKodValue == "20R5DB")
            {
                string connectionString = "";
                connectionString = string.Format("Server=192.168.2.53;Database={0};User Id=sa;Password=Yoruk123", dbName);

                return connectionString;
            }

            return null;
        }
    }
}