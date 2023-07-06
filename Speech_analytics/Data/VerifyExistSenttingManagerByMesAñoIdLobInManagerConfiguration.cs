using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class VerifyExistSenttingManagerByMesAñoIdLobInManagerConfiguration
    {
        const string query = @" SELECT COUNT(LOB) FROM AppSA.MANAGER_CONFIGURATION 
                                WHERE AÑO = @AÑO AND MES = @MES AND ID_LOB = @ID_LOB ";

        private MANAGER_CONFIGURATION managerSentting;
        private bool state;

        public VerifyExistSenttingManagerByMesAñoIdLobInManagerConfiguration(MANAGER_CONFIGURATION managerSentting)
        {
            this.managerSentting = managerSentting;
            this.state = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@AÑO", managerSentting.Año);
            command.Parameters.AddWithValue("@MES", managerSentting.Mes);
            command.Parameters.AddWithValue("@ID_LOB", managerSentting.IdLob);

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