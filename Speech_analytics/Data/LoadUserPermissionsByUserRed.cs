using Speech_analytics.Clases;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadUserPermissionsByUserRed
    {
        const string query = @"
                                SELECT [ccms]
                                      ,[userRed]
                                      ,[createUser]
                                      ,[editPermissions]
                                      ,[AddCampaigns]
                                      ,[myTeams]
                                      ,[randomnessResults]
                                      ,[random]
                                      ,[planCalculator]
                                      ,[daysCalculator]
                                      ,[bitacoraSura]
                                  FROM [AppSA].[PERMISOS_USERS]
                                  WHERE userRed = @USER_RED";

        private PERMISOS_USERcs permissionsUsers;
        private string userRed;

        public LoadUserPermissionsByUserRed(string userRed)
        {
            this.userRed = userRed;
            permissionsUsers= new PERMISOS_USERcs();
        }


        public PERMISOS_USERcs Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED", userRed);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                permissionsUsers.ccms = reader.GetString(0);
                permissionsUsers.userRed = reader.GetString(1);
                permissionsUsers.createUser = reader.GetInt32(2);
                permissionsUsers.editPermissions = reader.GetInt32(3);
                permissionsUsers.AddCampaigns = reader.GetInt32(4);
                permissionsUsers.myTeams = reader.GetInt32(5);
                permissionsUsers.randomnessResults = reader.GetInt32(6);
                permissionsUsers.random = reader.GetInt32(7);
                permissionsUsers.planCalculator = reader.GetInt32(8);
                permissionsUsers.daysCalculator = reader.GetInt32(9);
                permissionsUsers.bitacoraSura = reader.GetInt32(10);

            }
            Connection.oConnectionQaWeb.Close();
            return permissionsUsers;
        }
    }
}