using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class saveDataUserPermissions
    {
        const string query = @" INSERT INTO [AppSA].[PERMISOS_USERS] (ccms, userRed, createUser, editPermissions, AddCampaigns, myTeams, randomnessResults, random, planCalculator, daysCalculator, bitacoraSura)
                                VALUES (@ccms, @userRed, @createUser, @editPermissions, @AddCampaigns, @myTeams, @randomnessResults,@random, @planCalculator, @daysCalculator, @bitacoraSura)";

        private USERDATA userData;
        private bool result;
        public saveDataUserPermissions(USERDATA userData) 
        { 
            this.userData = userData;
            this.result = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@ccms", userData.CCMS);
            command.Parameters.AddWithValue("@userRed", userData.USER);
            command.Parameters.AddWithValue("@createUser", userData.PERMISOS_USERS.createUser);
            command.Parameters.AddWithValue("@editPermissions", userData.PERMISOS_USERS.editPermissions);
            command.Parameters.AddWithValue("@AddCampaigns", userData.PERMISOS_USERS.AddCampaigns);
            command.Parameters.AddWithValue("@myTeams", userData.PERMISOS_USERS.myTeams);
            command.Parameters.AddWithValue("@randomnessResults", userData.PERMISOS_USERS.randomnessResults);
            command.Parameters.AddWithValue("@random", userData.PERMISOS_USERS.random);
            command.Parameters.AddWithValue("@planCalculator", userData.PERMISOS_USERS.planCalculator);
            command.Parameters.AddWithValue("@daysCalculator", userData.PERMISOS_USERS.daysCalculator);
            command.Parameters.AddWithValue("@bitacoraSura", userData.PERMISOS_USERS.bitacoraSura);

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