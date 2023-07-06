using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class SaveSenttingManagerInManagerCondiguration
    {
        const string query = @" INSERT INTO [AppSA].[MANAGER_CONFIGURATION]
                                   ([USER_RED_MANAGER]
                                   ,[CCMS_MANAGER]
                                   ,[CLIENTE]
                                   ,[ID_CLIENTE]
                                   ,[LOB]
                                   ,[ID_LOB]
                                   ,[NUM_ANALISTAS]
                                   ,[MES]
                                   ,[AÑO]
                                   ,[CUOTA_MENSUAL]
                                   ,[DIAS_ACTIVIDAD]
                                   ,[PROMEDIO_ACTIVIDAD]
                                   ,[FECHA_ACTUALIZADO])
                             VALUES
                                   (@USER_RED_MANAGER
                                   ,@CCMS_MANAGER
                                   ,@CLIENTE
                                   ,@ID_CLIENTE
                                   ,@LOB
                                   ,@ID_LOB
                                   ,@NUM_ANALISTAS
                                   ,@MES
                                   ,@AÑO
                                   ,@CUOTA_MENSUAL
                                   ,@DIAS_ACTIVIDAD
                                   ,@PROMEDIO_ACTIVIDAD
                                   ,GETDATE())";

        private MANAGER_CONFIGURATION managerSentting;
        private bool state;

        public SaveSenttingManagerInManagerCondiguration(MANAGER_CONFIGURATION managerSentting)
        {
            this.managerSentting = managerSentting;
            this.state = false;
        }
        public bool Process()
        {
            Connection.oConnectionQaWeb.Open();
            string[] cliente = managerSentting.Cliente.Split('-');

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED_MANAGER", managerSentting.UserRedManager);
            command.Parameters.AddWithValue("@CCMS_MANAGER", managerSentting.CcmsManager);
            command.Parameters.AddWithValue("@CLIENTE", cliente[1]);
            command.Parameters.AddWithValue("@ID_CLIENTE", cliente[0]);
            command.Parameters.AddWithValue("@LOB", managerSentting.Lob);
            command.Parameters.AddWithValue("@ID_LOB", managerSentting.IdLob);
            command.Parameters.AddWithValue("@NUM_ANALISTAS", managerSentting.NumAnalistas);
            command.Parameters.AddWithValue("@MES", managerSentting.Mes);
            command.Parameters.AddWithValue("@AÑO", managerSentting.Año);
            command.Parameters.AddWithValue("@CUOTA_MENSUAL", managerSentting.CuotaMensual);
            command.Parameters.AddWithValue("@DIAS_ACTIVIDAD", managerSentting.DiasActividad);
            command.Parameters.AddWithValue("@PROMEDIO_ACTIVIDAD", managerSentting.PromedioActividad);

            int resultCommand = command.ExecuteNonQuery();

            if (resultCommand != 0)
            {
                state = true;
            }

            Connection.oConnectionQaWeb.Close();
            return state;
        }

    }
}