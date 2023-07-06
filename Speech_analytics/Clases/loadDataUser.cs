using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class loadDataUser
    {

        public static USERDATA loadDataUserBitacora(int idCcms)
        {
            Connection.oConnection.Close();
            Connection.oConnection.Open();

            USERDATA userData = new USERDATA();
            string query = "SELECT Nombre, email, idcuenta FROM [HC].[TBL_FactEmpleados] WHERE idccms = @idCcms";


            SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnection);
            oQueryRead.Parameters.AddWithValue("@idCcms", idCcms);

            using (SqlDataReader reader = oQueryRead.ExecuteReader())
            {
                if (reader.Read())
                {
                    userData.NOMBRES = reader["Nombre"].ToString();
                    userData.EMAIL = reader["email"].ToString();
                    userData.USER = reader["idcuenta"].ToString();
                }
            }

            return userData;
        }

        public static bool savemanager(USERDATA data)
        {

            try
            {
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                string estado = "ACTIVO";

                USERDATA userData = new USERDATA();
                string query = @"   INSERT INTO AppSA.managerBitacoraSura (nombre, correoElectronico, idccms, fechaActualizado, estado, usuarioRed) 
                                VALUES (@nombre, @correoElectronico, @idccms, GETDATE(), @estado, @usuarioRed)";


                SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnectionQaWeb);

                oQueryRead.Parameters.AddWithValue("@nombre", data.NOMBRES);
                oQueryRead.Parameters.AddWithValue("@correoElectronico", data.EMAIL);
                oQueryRead.Parameters.AddWithValue("@idccms", data.CCMS);
                oQueryRead.Parameters.AddWithValue("@estado", estado);
                oQueryRead.Parameters.AddWithValue("@usuarioRed", data.USER);

                if (oQueryRead.ExecuteNonQuery() != 0)
                {
                    Connection.oConnectionQaWeb.Close();
                    return true;
                }
                else
                {
                    Connection.oConnectionQaWeb.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public static List<managerBitacoraSura> loadListManagerBitacoraSura()
        {
            List<managerBitacoraSura> listManagers = new List<managerBitacoraSura>();
            string query = "SELECT nombre, correoElectronico, idccms, fechaActualizado, estado, usuarioRed FROM AppSA.managerBitacoraSura WHERE estado = 'ACTIVO'";

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnectionQaWeb);
            using (SqlDataReader reader = oQueryRead.ExecuteReader())
            {
                while (reader.Read())
                {
                    listManagers.Add(new managerBitacoraSura
                    {
                        Nombre = Convert.ToString(reader["nombre"]),
                        CorreoElectronico = Convert.ToString(reader["correoElectronico"]),
                        IdCcms = Convert.ToInt32(reader["idccms"]),
                        Estado = Convert.ToString(reader["estado"]),
                        UsuarioRed = Convert.ToString(reader["usuarioRed"])

                    });
                }
            }
            Connection.oConnectionQaWeb.Close();
            return listManagers;

        }

        public static bool updateManagerBitacoraInactivo(int CCMS)
        {

            try
            {
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                string query = @"   UPDATE [AppSA].[managerBitacoraSura]
                                    SET [estado] = 'INACTIVO'
                                    WHERE [idccms] = @idccms"
                ;

                SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnectionQaWeb);
                oQueryRead.Parameters.AddWithValue("@idccms", CCMS);
                using (SqlDataReader reader = oQueryRead.ExecuteReader())
                {
                    Connection.oConnectionQaWeb.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool updateManagerBitacoraActivo(int CCMS)
        {

            try
            {
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                string query = @"   UPDATE [AppSA].[managerBitacoraSura]
                                    SET [estado] = 'ACTIVO'
                                    WHERE [idccms] = @idccms"
                ;

                SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnectionQaWeb);
                oQueryRead.Parameters.AddWithValue("@idccms", CCMS);
                using (SqlDataReader reader = oQueryRead.ExecuteReader())
                {
                    if (oQueryRead.ExecuteNonQuery() != 0)
                    {
                        Connection.oConnectionQaWeb.Close();
                        return true;
                    }
                    else
                    {
                        Connection.oConnectionQaWeb.Close();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public static bool validateManagerExists(int CCMS)
        {

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            string query = "SELECT idccms FROM AppSA.managerBitacoraSura WHERE idccms = @idccms";


            SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnectionQaWeb);
            oQueryRead.Parameters.AddWithValue("@idccms", CCMS);

            using (SqlDataReader reader = oQueryRead.ExecuteReader())
            {
                if (reader.Read())
                {
                    Connection.oConnectionQaWeb.Close();
                    return true;
                }
                else
                {
                    Connection.oConnectionQaWeb.Close();
                    return false;
                }
            }
        }
    }
}
