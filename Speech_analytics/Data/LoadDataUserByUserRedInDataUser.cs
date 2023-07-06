using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataUserByUserRedInDataUser
    {
        const string query = @"     SELECT CCMS
                                          ,USERDATA.USER_RED
                                          ,[CCMS_MANAGER]
                                          ,[NOMBRES]
                                          ,[APELLIDOS]
                                          ,[EMAIL]
                                          ,[EMAIL_ALTERNO]
										  ,ESTADO
                                    FROM AppSA.USERDATA AS USERDATA
									INNER JOIN AppSA.USER_LOGIN AS USER_LOGIN ON USERDATA.USER_RED = USER_LOGIN.USER_RED
                                    WHERE USERDATA.USER_RED = @USER_RED";

        private USERDATA userData;
        private string userRed;

        public LoadDataUserByUserRedInDataUser(string userRed)
        {
            this.userRed = userRed;
            userData = new USERDATA();
        }

        public USERDATA Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED", userRed);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                userData.CCMS = reader.GetInt32(0);
                userData.USER = reader.GetString(1);
                userData.CCMS_MANAGER = reader.GetInt32(2);
                userData.NOMBRES = reader.GetString(3);
                userData.APELLIDOS = reader.GetString(4);
                userData.EMAIL = reader.GetString(5);
                userData.EMAIL_ALTERNO = reader.GetString(6);
                userData.Estado = reader.GetString(7);
            }
            Connection.oConnectionQaWeb.Close();
            return userData;
        }
    }
}