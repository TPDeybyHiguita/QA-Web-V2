using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Speech_analytics.Data
{
    public class loadUserDataByCcms
    {
        const string query = @" SELECT *   
                                FROM [HC].[TBL_FactEmpleados] 
                                WHERE  idccms = @CCMS and Estado = 'Active'";

        private int idCcms;
        private TBL_FactEmpleados tbl_FactEmpleados;

        public loadUserDataByCcms(int idCcms)
        {
            this.idCcms = idCcms;
            tbl_FactEmpleados = new TBL_FactEmpleados();
        }

        public TBL_FactEmpleados Process()
        {
            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnection);
            command.Parameters.AddWithValue("@CCMS", idCcms);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                tbl_FactEmpleados.idcuenta = Convert.ToString(reader["idcuenta"]);
                tbl_FactEmpleados.Nombre = Convert.ToString(reader["Nombre"]);
                tbl_FactEmpleados.email = Convert.ToString(reader["email"]);
                tbl_FactEmpleados.idfiscal = Convert.ToString(reader["idfiscal"]);
                tbl_FactEmpleados.idmanager = Convert.ToInt32(reader["idmanager"]);
                tbl_FactEmpleados.idccms = Convert.ToInt32(reader["idccms"]);
            }

            Connection.oConnection.Close();
            return tbl_FactEmpleados;
        }
    }
}