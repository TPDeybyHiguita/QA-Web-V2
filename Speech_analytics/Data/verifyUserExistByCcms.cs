using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class verifyUserExistByCcms
    {
        const string query = @"SELECT COUNT(*) AS STATE FROM AppSA.USER_LOGIN WHERE USER_RED = @USER_RED AND ESTADO = 'active'";

        private USERDATA USERDATA;
        private bool state;

        public verifyUserExistByCcms(USERDATA userData)
        {
            this.USERDATA = userData;
            state= false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED", USERDATA.USER);

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