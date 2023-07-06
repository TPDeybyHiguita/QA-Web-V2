using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class SaveDataInManagerAnalista
    {
        const string query = @"
                                INSERT INTO [AppSA].[MANAGER_ANALISTAS]
                                           ([USER_RED]
                                           ,[NOMBRE]
                                           ,[ESTADO]
                                           ,[FECHA_ACTUALIZADO]
                                           ,[CORREO]
                                           ,[USER_RED_MANAGER])
                                     VALUES
                                           (@USER_RED
                                           ,@NOMBRE
                                           ,@ESTADO
                                           ,GETDATE()
                                           ,@CORREO
                                           ,@USER_RED_MANAGER)";


        private MANAGER_ANALISTA managerAnalista;
        private bool result;
        private string ESTADO = "ACTIVO";

        public SaveDataInManagerAnalista(MANAGER_ANALISTA managerAnalista)
        {
            this.managerAnalista = managerAnalista;
            this.result = false;
        }

        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED", managerAnalista.USER_RED);
            command.Parameters.AddWithValue("@NOMBRE", managerAnalista.NOMBRE_ANALISTA);
            command.Parameters.AddWithValue("@ESTADO", ESTADO);
            command.Parameters.AddWithValue("@CORREO", managerAnalista.EMAIL_ANALISTA);
            command.Parameters.AddWithValue("@USER_RED_MANAGER", managerAnalista.USER_RED_MANAGER);
            

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