using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class UpdateLastLoginByUser
    {
        const string query = @"UPDATE  AppSA.USER_LOGIN SET LAST_LOGIN = GETDATE() WHERE USER_RED = @USER";

        private string user;

        public UpdateLastLoginByUser(string user)
        {
            this.user = user;
        }

        public void Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnectionQaWeb);

            oQueryUpdate.Parameters.AddWithValue("@USER", user);

            oQueryUpdate.ExecuteNonQuery();
            Connection.oConnectionQaWeb.Close();
        }
    }
}