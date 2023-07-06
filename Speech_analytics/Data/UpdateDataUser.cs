using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class UpdateDataUser
    {
        const string query = @"UPDATE [AppSA].[USERDATA]
                               SET [CCMS] = @CCMS
                                  ,[USER_RED] = @USER_RED
                                  ,[CCMS_MANAGER] = @CCMS_MANAGER
                                  ,[NOMBRES] = @NOMBRES
                                  ,[APELLIDOS] = @APELLIDOS
                                  ,[EMAIL] = @EMAIL
                                  ,[EMAIL_ALTERNO] = @EMAIL_ALTERNO
                             WHERE USER_RED = @USER_RED


                             UPDATE [AppSA].[USER_LOGIN]
                               SET [ID_USER] = @ID_USER
                                  ,[USER_RED] = @USER_RED
                                  ,[LAST_LOGIN] = GETDATE()
                                  ,[ESTADO] = @ESTADO
                             WHERE USER_RED = @USER_RED";

        private USERDATA userData;
        private bool result;

        public UpdateDataUser(USERDATA userData) 
        { 
            this.userData = userData;
            this.result = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnectionQaWeb);

            oQueryUpdate.Parameters.AddWithValue("@CCMS", userData.CCMS);
            oQueryUpdate.Parameters.AddWithValue("@USER", userData.USER);
            oQueryUpdate.Parameters.AddWithValue("@CCMS_MANAGER", userData.CCMS_MANAGER);
            oQueryUpdate.Parameters.AddWithValue("@NOMBRES", userData.NOMBRES);
            oQueryUpdate.Parameters.AddWithValue("@APELLIDOS", userData.APELLIDOS);
            oQueryUpdate.Parameters.AddWithValue("@EMAIL", userData.EMAIL);
            oQueryUpdate.Parameters.AddWithValue("@EMAIL_ALTERNO", userData.EMAIL_ALTERNO);
            oQueryUpdate.Parameters.AddWithValue("@ID_USER", userData.CCMS);
            oQueryUpdate.Parameters.AddWithValue("@USER_RED", userData.USER);
            oQueryUpdate.Parameters.AddWithValue("@ESTADO", userData.Estado);

            int resultCommand = oQueryUpdate.ExecuteNonQuery();

            if (resultCommand != 0)
            {
                result = true;
            }
            Connection.oConnectionQaWeb.Close();

            return result;
        }



    }
}