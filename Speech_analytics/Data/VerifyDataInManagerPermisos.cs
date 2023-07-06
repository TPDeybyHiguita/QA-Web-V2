using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class VerifyDataInManagerPermisos
    {
        const string query = @" SELECT COUNT(*) AS STATE FROM AppSA.MANAGER_PERMISOS
                                WHERE CCMS_ANALISTA = @CCMS_ANALISTA AND CLIENTE = @CLIENTE AND LOB = @LOB";

        private MANAGER_ANALISTA managerAnalista;
        private bool state;

        public VerifyDataInManagerPermisos(MANAGER_ANALISTA managerAnalista)
        {
            this.managerAnalista = managerAnalista;
            this.state = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@CCMS_ANALISTA", managerAnalista.CCMS_ANALISTA);
            command.Parameters.AddWithValue("@CLIENTE", managerAnalista.CLIENTE_ANALISTA);
            command.Parameters.AddWithValue("@LOB", managerAnalista.LOB_ANALISTA);

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