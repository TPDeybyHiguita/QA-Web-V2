using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataInManagerPermisosByCcmsManagerAndCcmsAnalista
    {
        const string query = @" SELECT ID, CLIENTE, LOB, ESTADO FROM AppSA.MANAGER_PERMISOS
                                    WHERE CCMS_ANALISTA = @CCMS_ANALISTA AND CCMS_MANAGER = @CCMS_MANAGER";

        private string CCMS_MANAGER;
        private string CCMS_ANALISTA;
        private List<MANAGER_PERMISOS> managerPermisos = new List<MANAGER_PERMISOS>();

        public LoadDataInManagerPermisosByCcmsManagerAndCcmsAnalista(string ccmsManager, string ccmsAnalista)
        {
            this.CCMS_MANAGER = ccmsManager;
            this.CCMS_ANALISTA= ccmsAnalista;
        }

        public List<MANAGER_PERMISOS> process()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@CCMS_MANAGER", CCMS_MANAGER);
            command.Parameters.AddWithValue("@CCMS_ANALISTA", CCMS_ANALISTA);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                managerPermisos.Add(new MANAGER_PERMISOS
                {
                    CLIENTE = Convert.ToString(reader["CLIENTE"]),
                    ID = Convert.ToInt16(reader["ID"]),
                    LOB = Convert.ToString(reader["LOB"]),
                    ESTADO = Convert.ToString(reader["ESTADO"])
                });
            }

            return managerPermisos;
        }
    }
}