using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class SaveDataUserLogin
    {
        const string query = @" INSERT INTO [AppSA].[USER_LOGIN] (ID_USER, USER_RED, ESTADO) 
                                VALUES (@ID_USER, @USER_RED, 'Active')";

        private USERDATA userData;
        private bool result;

        public SaveDataUserLogin (USERDATA userData)
        {
            this.userData = userData;
            this.result = false;
        } 

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand (query,Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@ID_USER", userData.CCMS);
            command.Parameters.AddWithValue("@USER_RED", userData.USER);

            int resultCommand = command.ExecuteNonQuery();

            if (resultCommand != 0)
            {
                result = true;
            }

            Connection.oConnectionQaWeb.Close();
            return result;
        }
        

    }
}