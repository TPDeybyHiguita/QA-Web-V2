using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataUserByUserRedInUserData
    {
        const string query = @" SELECT NOMBRES, APELLIDOS, EMAIL FROM AppSA.USERDATA WHERE  USER_RED =  @USER_RED";

        private string userRed;
        private List<USERDATA> oListData = new List<USERDATA>();

        public LoadDataUserByUserRedInUserData(string userRed)
        {
            this.userRed = userRed;
        }

        public List<USERDATA> Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED", userRed);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                oListData.Add(new USERDATA
                {
                    NOMBRES = Convert.ToString(reader["NOMBRES"]),
                    APELLIDOS = Convert.ToString(reader["APELLIDOS"]),
                    EMAIL = Convert.ToString(reader["EMAIL"])
                });
            }
            Connection.oConnectionQaWeb.Close();
            return oListData;

        }
    }
}