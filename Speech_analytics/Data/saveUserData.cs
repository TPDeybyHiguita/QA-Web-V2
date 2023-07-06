using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class saveUserData
    {
        const string query = @" INSERT INTO [AppSA].[USERDATA] (CCMS, CCMS_MANAGER, NOMBRES, APELLIDOS, EMAIL, EMAIL_ALTERNO, USER_RED)
                                VALUES (@CCMS, @CCMSMANAGER, @NOMBRES,@APELLIDOS, @EMAIL, @EMAIL_ALTERNO, @USER_RED)";

        private USERDATA userData;
        private bool result;

        public saveUserData(USERDATA userData)
        {
            this.userData = userData;
            this.result = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@CCMS", userData.CCMS);
            command.Parameters.AddWithValue("@CCMSMANAGER", userData.CCMS_MANAGER);
            command.Parameters.AddWithValue("@NOMBRES", userData.NOMBRES);
            command.Parameters.AddWithValue("@APELLIDOS", userData.APELLIDOS);
            command.Parameters.AddWithValue("@EMAIL", userData.EMAIL);
            command.Parameters.AddWithValue("@EMAIL_ALTERNO", userData.EMAIL_ALTERNO);
            command.Parameters.AddWithValue("@USER_RED", userData.USER);

            int resultCommand = command.ExecuteNonQuery();

            if (resultCommand != 0) 
            {
                result= true;
            }

            Connection.oConnectionQaWeb.Close();
            return result;
        }

    }
}