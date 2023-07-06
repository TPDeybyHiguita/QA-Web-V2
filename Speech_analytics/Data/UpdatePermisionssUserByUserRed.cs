using Speech_analytics.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class UpdatePermisionssUserByUserRed
    {
        const string query = @"UPDATE [AppSA].[PERMISOS_USERS]
                                   SET [createUser] = @createUser
                                      ,[editPermissions] = @editPermissions
                                      ,[AddCampaigns] = @AddCampaigns
                                      ,[myTeams] = @myTeams
                                      ,[randomnessResults] = @randomnessResults
                                      ,[random] = @random
                                      ,[planCalculator] = @planCalculator
                                      ,[daysCalculator] = @daysCalculator
                                      ,[bitacoraSura] = @bitacoraSura
                                 WHERE userRed = @userRed";

        private PERMISOS_USERcs permissionsUser;
        private bool result;

        public UpdatePermisionssUserByUserRed(PERMISOS_USERcs permissionsUser)
        {
            this.permissionsUser = permissionsUser;
            this.result = false;
        }

        public bool  Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnectionQaWeb);

            oQueryUpdate.Parameters.AddWithValue("@createUser", permissionsUser.createUser);
            oQueryUpdate.Parameters.AddWithValue("@editPermissions", permissionsUser.editPermissions);
            oQueryUpdate.Parameters.AddWithValue("@AddCampaigns", permissionsUser.AddCampaigns);
            oQueryUpdate.Parameters.AddWithValue("@myTeams", permissionsUser.myTeams);
            oQueryUpdate.Parameters.AddWithValue("@randomnessResults", permissionsUser.randomnessResults);
            oQueryUpdate.Parameters.AddWithValue("@random", permissionsUser.random);
            oQueryUpdate.Parameters.AddWithValue("@planCalculator", permissionsUser.planCalculator);
            oQueryUpdate.Parameters.AddWithValue("@daysCalculator", permissionsUser.daysCalculator);
            oQueryUpdate.Parameters.AddWithValue("@bitacoraSura", permissionsUser.bitacoraSura);
            oQueryUpdate.Parameters.AddWithValue("@userRed", permissionsUser.userRed);

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