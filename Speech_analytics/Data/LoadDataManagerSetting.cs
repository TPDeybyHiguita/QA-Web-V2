using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataManagerSetting
    {
        const string query = @"
                                SELECT [USER_RED_MANAGER]
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
                                      ,[FECHA_ACTUALIZADO]
                                  FROM [AppSA].[MANAGER_CONFIGURATION]
                                  WHERE AÑO = @AÑO AND MES = @MES AND ID_LOB = @ID_LOB AND ID_CLIENTE = @ID_CLIENTE";

        private MANAGER_CONFIGURATION managerSetting;

        public LoadDataManagerSetting(MANAGER_CONFIGURATION managerSetting)
        {
            this.managerSetting = managerSetting;
            managerSetting = new MANAGER_CONFIGURATION();
        }

        public MANAGER_CONFIGURATION Process()
        {
            string[] cliente = managerSetting.Cliente.Split('-');

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@AÑO", managerSetting.Año);
            command.Parameters.AddWithValue("@MES", managerSetting.Mes);
            command.Parameters.AddWithValue("@ID_LOB", managerSetting.IdLob);
            command.Parameters.AddWithValue("@ID_CLIENTE", cliente[0]);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                managerSetting.UserRedManager = reader.GetString(0);
                managerSetting.CcmsManager = reader.GetString(1);
                managerSetting.Cliente = reader.GetString(2);
                managerSetting.IdCliente = reader.GetInt32(3);
                managerSetting.Lob = reader.GetString(4);
                managerSetting.IdLob = reader.GetString(5);
                managerSetting.NumAnalistas = reader.GetInt32(6);
                managerSetting.Mes = reader.GetInt32(7);
                managerSetting.Año = reader.GetInt32(8);
                managerSetting.CuotaMensual = reader.GetInt32(9);
                managerSetting.DiasActividad = reader.GetInt32(10);
                managerSetting.PromedioActividad = reader.GetInt32(11);
                managerSetting.FechaActualizado = reader.GetDateTime(12);
            }
            Connection.oConnectionQaWeb.Close();
            return managerSetting;
        }

    }
}