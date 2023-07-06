using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadCcmsInDataUserByUserRed
    {
        const string query = @" SELECT CCMS FROM AppSA.USERDATA
                                WHERE USER_RED = @USER_RED";

        private string userRed;
        private int Ccms;

        public LoadCcmsInDataUserByUserRed(string userRed)
        {
            this.userRed = userRed;
            Ccms = 0;
        }

        public int Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open(); 
            

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED", userRed);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                Ccms = reader. GetInt32(0);
            }
            Connection.oConnectionQaWeb.Close();
            return Ccms;
        }


    }
}