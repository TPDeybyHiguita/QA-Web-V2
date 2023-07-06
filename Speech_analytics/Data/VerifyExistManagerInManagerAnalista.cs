using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class VerifyExistManagerInManagerAnalista
    {
        const string query = @"SELECT COUNT(NOMBRE) AS STATE  FROM [AppSA].[MANAGER_ANALISTAS]  
                                    WHERE CCMS_MANAGER = @USER_RED_MANAGER AND CCMS = @CCMS_ANALISTA";

        private string CCMS_ANALISTA;
        private string CCMS_MANAGER;
        private bool state;

        public VerifyExistManagerInManagerAnalista(string CCMS_ANALISTA, string CCMS_MANAGER)
        {
            this.CCMS_ANALISTA = CCMS_ANALISTA;
            this.CCMS_MANAGER = CCMS_MANAGER;
            state = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED_MANAGER", CCMS_MANAGER);
            command.Parameters.AddWithValue("@CCMS_ANALISTA", CCMS_ANALISTA);

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