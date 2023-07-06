using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class UpdateSenttingManagerInManagerConfiguration
    {
        const string query = @" UPDATE [AppSA].[MANAGER_CONFIGURATION]
                                   SET [USER_RED_MANAGER] = @USER_RED_MANAGER
                                      ,[CCMS_MANAGER] = @CCMS_MANAGER
                                      ,[CLIENTE] = @CLIENTE
                                      ,[ID_CLIENTE] = @ID_CLIENTE
                                      ,[LOB] = @LOB
                                      ,[ID_LOB] = @ID_LOB
                                      ,[NUM_ANALISTAS] = @NUM_ANALISTAS
                                      ,[MES] = @MES
                                      ,[AÑO] = @AÑO
                                      ,[CUOTA_MENSUAL] = @CUOTA_MENSUAL
                                      ,[DIAS_ACTIVIDAD] = @DIAS_ACTIVIDAD
                                      ,[PROMEDIO_ACTIVIDAD] = @PROMEDIO_ACTIVIDAD
                                      ,[FECHA_ACTUALIZADO] = GETDATE()
                                WHERE AÑO = @AÑO AND MES = @MES AND ID_LOB = @ID_LOB";

        private MANAGER_CONFIGURATION managerSentting;
        private bool result;

        public UpdateSenttingManagerInManagerConfiguration(MANAGER_CONFIGURATION managerSentting)
        {
            this.managerSentting = managerSentting;
            this.result = false;
        }

        public bool Process()
        {

            string[] cliente = managerSentting.Cliente.Split('-');
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnectionQaWeb);

            oQueryUpdate.Parameters.AddWithValue("@USER_RED_MANAGER", managerSentting.UserRedManager);
            oQueryUpdate.Parameters.AddWithValue("@CCMS_MANAGER", managerSentting.CcmsManager);
            oQueryUpdate.Parameters.AddWithValue("@CLIENTE", cliente[1]);
            oQueryUpdate.Parameters.AddWithValue("@ID_CLIENTE", cliente[0]);
            oQueryUpdate.Parameters.AddWithValue("@LOB", managerSentting.Lob);
            oQueryUpdate.Parameters.AddWithValue("@ID_LOB", managerSentting.IdLob);
            oQueryUpdate.Parameters.AddWithValue("@NUM_ANALISTAS", managerSentting.NumAnalistas);
            oQueryUpdate.Parameters.AddWithValue("@MES", managerSentting.Mes);
            oQueryUpdate.Parameters.AddWithValue("@AÑO", managerSentting.Año);
            oQueryUpdate.Parameters.AddWithValue("@CUOTA_MENSUAL", managerSentting.CuotaMensual);
            oQueryUpdate.Parameters.AddWithValue("@DIAS_ACTIVIDAD", managerSentting.DiasActividad);
            oQueryUpdate.Parameters.AddWithValue("@PROMEDIO_ACTIVIDAD", managerSentting.PromedioActividad);

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