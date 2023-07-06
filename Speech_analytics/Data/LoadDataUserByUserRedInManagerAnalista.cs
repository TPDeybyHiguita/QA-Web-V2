using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataUserByUserRedInManagerAnalista
    {
        const string query = @"
                                SELECT COUNT(NOMBRE) AS RESULT FROM AppSA.MANAGER_ANALISTAS 
                                WHERE USER_RED = @USER_RED";

        private string USER_RED;
        private bool state;

        public LoadDataUserByUserRedInManagerAnalista(string USER_RED)
        {
            this.USER_RED = USER_RED;
            this.state = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED", USER_RED);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

                if (reader.GetInt32(0) != 0)
                {
                    state = true;
                }
            }
            Connection.oConnectionQaWeb.Close();
            return state;
        }

    }
}