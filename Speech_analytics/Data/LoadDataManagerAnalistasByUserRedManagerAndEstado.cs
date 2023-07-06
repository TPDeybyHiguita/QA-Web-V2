using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataManagerAnalistasByUserRedManagerAndEstado
    {
        const string query = @" SELECT MA.USER_RED, NOMBRE, ESTADO, FECHA_ACTUALIZADO, CORREO, USER_RED_MANAGER, CCMS
                                    FROM AppSA.MANAGER_ANALISTAS AS MA INNER JOIN AppSA.USERDATA AS UD ON MA.USER_RED = UD.USER_RED
                                    WHERE USER_RED_MANAGER = @USER_RED_MANAGER AND ESTADO = 'ACTIVO' ORDER BY ESTADO";

        private string USER_RED_MANAGER;
        private List<MANAGER_ANALISTA> mangerAnalista = new List<MANAGER_ANALISTA>();

        public LoadDataManagerAnalistasByUserRedManagerAndEstado(string userRedManager)
        {
            this.USER_RED_MANAGER = userRedManager;
        }

        public List<MANAGER_ANALISTA> process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@USER_RED_MANAGER", USER_RED_MANAGER);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                mangerAnalista.Add(new MANAGER_ANALISTA
                {
                    USER_RED = Convert.ToString(reader["USER_RED"]),
                    CCMS_ANALISTA = Convert.ToString(reader["CCMS"]),
                    NOMBRE_ANALISTA = Convert.ToString(reader["NOMBRE"]),
                    EMAIL_ANALISTA = Convert.ToString(reader["CORREO"]),
                    USER_RED_MANAGER = Convert.ToString(reader["USER_RED_MANAGER"]),
                    ESTADO = Convert.ToString(reader["ESTADO"])
                });
            }

            return mangerAnalista;
        }
    }
}