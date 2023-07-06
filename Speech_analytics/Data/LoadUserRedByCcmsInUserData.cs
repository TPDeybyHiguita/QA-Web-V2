using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadUserRedByCcmsInUserData
    {

        const string query = @" SELECT USER_RED FROM AppSA.USERDATA
                                WHERE CCMS = @CCMS";

        private string userRed;
        private int ccms;

        public LoadUserRedByCcmsInUserData(int ccms)
        {
            this.ccms = ccms;
            userRed= null;
        }

        public string Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@CCMS", ccms);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                userRed = reader.GetString(0);
            }
            Connection.oConnectionQaWeb.Close();
            return userRed;
        }
    }
}