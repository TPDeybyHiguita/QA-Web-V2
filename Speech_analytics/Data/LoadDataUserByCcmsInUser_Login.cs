using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataUserByCcmsInUser_Login
    {
        const string query = @"     SELECT CCMS, USER_RED, CCMS_MANAGER, NOMBRES, APELLIDOS, EMAIL, EMAIL_ALTERNO 
                                    FROM AppSA.USERDATA
                                    WHERE CCMS = @CCMS";

        private USERDATA userData;
        private int ccms;

        public LoadDataUserByCcmsInUser_Login(int ccms)
        {
            this.ccms = ccms;
            userData = new USERDATA();
        }

        public USERDATA Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@CCMS", ccms);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                userData.NOMBRES = reader.GetString(3);
                userData.APELLIDOS = reader.GetString(4);
                userData.EMAIL = reader.GetString(5);
                userData.EMAIL_ALTERNO = reader.GetString(6);
                userData.USER = reader.GetString(1);
            }
            Connection.oConnectionQaWeb.Close();
            return userData;
        }


    }
}