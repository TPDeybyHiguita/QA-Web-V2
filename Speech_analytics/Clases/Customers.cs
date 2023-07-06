using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class CustomersClass
    {
        public static bool saveConfCliente(string cliente)
        {
            try
            {
                string[] custom = cliente.Split('-');
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand OQueyInsert = new SqlCommand(@"INSERT INTO [AppSA].[CAMPAÑAS]
                                                            ([id_Cliente]
                                                            ,[nombre]
                                                            ,[estado]
                                                            ,[fecha_Actualizado])
                                                            VALUES (@id_Cliente,@nombre,'ACTIVO',GETDATE())", Connection.oConnectionQaWeb);

                OQueyInsert.Parameters.AddWithValue("@id_Cliente", custom[0]);
                OQueyInsert.Parameters.AddWithValue("@nombre", custom[1]);

                if (OQueyInsert.ExecuteNonQuery() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        public static bool updateConfClienteInactivar(int idCliente)
        {

            try
            {

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand oQueryUpdate = new SqlCommand(@"     UPDATE [AppSA].[CAMPAÑAS] 
                                                                SET
                                                                [estado] = 'INACTIVO', 
                                                                [fecha_Actualizado] = GETDATE()
                                                                WHERE id_Cliente = @id_Cliente ", Connection.oConnectionQaWeb);

                oQueryUpdate.Parameters.AddWithValue("@id_Cliente", idCliente);

                if (oQueryUpdate.ExecuteNonQuery() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        public static bool updateConfClienteActivar(int idCliente)
        {

            try
            {

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand oQueryUpdate = new SqlCommand(@"     UPDATE [AppSA].[CAMPAÑAS] 
                                                                SET
                                                                [estado] = 'ACTIVO', 
                                                                [fecha_Actualizado] = GETDATE()
                                                                WHERE id_Cliente = @id_Cliente ", Connection.oConnectionQaWeb);
                
                oQueryUpdate.Parameters.AddWithValue("@id_Cliente", idCliente);

                if (oQueryUpdate.ExecuteNonQuery() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex )
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }

        }

        public static List<CAMPAÑAS> dataCliente()
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();


            SqlCommand oQueryList = new SqlCommand(@" SELECT    [id_Cliente],[nombre],[estado],[fecha_Actualizado]
                                                        FROM [AppSA].[CAMPAÑAS] ORDER BY estado ASC", Connection.oConnectionQaWeb);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<CAMPAÑAS> listClientes = new List<CAMPAÑAS>();

            while (oRead.Read())
            {
                listClientes.Add(new CAMPAÑAS
                {
                    id_Cliente = Convert.ToInt32(oRead["id_Cliente"]),
                    nombre = Convert.ToString(oRead["nombre"]),
                    estado = Convert.ToString(oRead["estado"]),
                    fecha_Actualizado = Convert.ToString(oRead["fecha_Actualizado"])

                });
            }
            Connection.oConnectionQaWeb.Close();

            return listClientes;
        }


        public static List<TBL_Campañas_TPModel> loadClientes()
        {
            string nombreCLIENTE;
            Connection.oConnection.Close();
            Connection.oConnection.Open();



            SqlCommand oQueryList = new SqlCommand(@"select ID_Cliente, Nombre_Campaña from [GENERAL].[TBL_Campañas_TP]", Connection.oConnection);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_Campañas_TPModel> oList = new List<TBL_Campañas_TPModel>();

            while (oRead.Read())
            {

                if (Convert.ToString(oRead["Nombre_Campaña"]) == "EPS_Sura")
                {
                    nombreCLIENTE = "SURA";
                }
                else
                {
                    nombreCLIENTE = Convert.ToString(oRead["Nombre_Campaña"]);
                }

                oList.Add(new TBL_Campañas_TPModel
                {
                    ID_Cliente = Convert.ToString(oRead["ID_Cliente"]),
                    Nombre_Campaña = nombreCLIENTE
                });
            }
            Connection.oConnection.Close();

            return oList;
        }
    }
}