using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class UpdateEstadoInManagerAnalista
    {
        const string query = @"
                                UPDATE [AppSA].[MANAGER_ANALISTAS]
                                SET [ESTADO] = @ESTADO
                                    ,[FECHA_ACTUALIZADO] = GETDATE()
                                WHERE USER_RED = @USER_RED";


        private MANAGER_ANALISTA managerAnalista;
        private bool result;
        private string ESTADO = "ACTIVO";

        public UpdateEstadoInManagerAnalista(MANAGER_ANALISTA managerAnalista)
        {
            this.managerAnalista = managerAnalista;
            this.result = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@ESTADO", ESTADO);
            command.Parameters.AddWithValue("@USER_RED", managerAnalista.USER_RED);

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