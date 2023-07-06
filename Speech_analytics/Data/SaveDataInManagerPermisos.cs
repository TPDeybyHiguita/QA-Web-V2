using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class SaveDataInManagerPermisos
    {
        const string query = @"
                                INSERT INTO [AppSA].[MANAGER_PERMISOS]
                               ([USER_ANALISTA]
                               ,[USER_MANAGER]
                               ,[CCMS_ANALISTA]
                               ,[CCMS_MANAGER]
                               ,[CLIENTE]
                               ,[LOB]
                               ,[ESTADO]
                               ,[FECHA_ACTUALIZADO])
                         VALUES
                               (@USER_ANALISTA
                               ,@USER_MANAGER
                               ,@CCMS_ANALISTA
                               ,@CCMS_MANAGER
                               ,@CLIENTE
                               ,@LOB
                               ,'ACTIVO'
                               ,GETDATE())";

        private MANAGER_ANALISTA managerAnalista;
        private bool result;

        public SaveDataInManagerPermisos(MANAGER_ANALISTA managerAnalista)
        {
            this.managerAnalista = managerAnalista;
            this.result = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_ANALISTA", managerAnalista.USER_RED);
            command.Parameters.AddWithValue("@USER_MANAGER", managerAnalista.USER_RED_MANAGER);
            command.Parameters.AddWithValue("@CCMS_ANALISTA", managerAnalista.CCMS_ANALISTA);
            command.Parameters.AddWithValue("@CCMS_MANAGER", managerAnalista.CCMS_MANAGER);
            command.Parameters.AddWithValue("@CLIENTE", managerAnalista.CLIENTE_ANALISTA);
            command.Parameters.AddWithValue("@LOB", managerAnalista.LOB_ANALISTA);

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