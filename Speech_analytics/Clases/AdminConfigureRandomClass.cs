using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class AdminConfigureRandomClass
    {

        public static string loadccmsCliente(string idCliente)
        {

            int ID = AdminConfigureRandomClass.checkCliente(idCliente);

            Connection.oConnection.Close();
            Connection.oConnection.Open();


            SqlCommand oQueryList = new SqlCommand("SELECT CodClienteCCMS FROM [GENERAL].[TBL_Campañas_TP] WHERE ID_Cliente = @ID_CLIENTE ", Connection.oConnection);

            oQueryList.Parameters.AddWithValue("ID_CLIENTE", ID);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            if (oRead.Read())
            {
                string ID_Cliente = Convert.ToString(oRead["CodClienteCCMS"]);
                return ID_Cliente;
            }
            else
            {
                return null;
            }
        }

        public static int checkCliente(string idCliente)
        {
            string[] custom = idCliente.Split('-');

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQuery = new SqlCommand("SELECT ID_CLIENTE FROM AppSA.FILTER_RANDOM_CALL WHERE ID_CLIENTE = @ID_CLIENTE ", Connection.oConnection);
            oQuery.Parameters.AddWithValue("@ID_CLIENTE", Convert.ToInt32(custom[0]));
            
            SqlDataReader oRead = oQuery.ExecuteReader();

            if (oRead.Read())
            {
                int ID = Convert.ToInt32(oRead["ID_CLIENTE"]);
                return ID;
            }
            else
            {
                return Convert.ToInt32(custom[0]);
            }
        }

        public static int updateRandom(FILTER_RANDOM_CALL data, string user)
        {
            try
            {
                string[] custom = data.ID_CLIENTE.Split('-');
                int idccms = ProfileClass.dataIDCCMS(user);

                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand oQueryUpdate = new SqlCommand("UPDATE [AppSA].[FILTER_RANDOM_CALL]" +
                                                            "SET "+
                                                            " [SKILL_INICIAL] = @SKILL_INICIAL, " +
                                                            " [SKILL_FINAL] = @SKILL_FINAL, " +
                                                            " [AHI_INICIAL] = @AHI_INICIAL," +
                                                            " [AHI_FINAL] = @AHI_FINAL, " +
                                                            " [FECHA_ACTUALIZADO] = GETDATE(), " +
                                                            " [CCMS_ACTUALIZADO] = @CCMS_ACTUALIZADO " +
                                                            " WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB", Connection.oConnection);

                oQueryUpdate.Parameters.AddWithValue("@SKILL_INICIAL", data.SKILL_INICIAL);
                oQueryUpdate.Parameters.AddWithValue("@SKILL_FINAL", data.SKILL_FINAL);
                oQueryUpdate.Parameters.AddWithValue("@AHI_INICIAL", data.AHT_INI);
                oQueryUpdate.Parameters.AddWithValue("@AHI_FINAL", data.AHT_FIN);
                oQueryUpdate.Parameters.AddWithValue("@CCMS_ACTUALIZADO", idccms);
                oQueryUpdate.Parameters.AddWithValue("@ID_CLIENTE", Convert.ToString(custom[0]));
                oQueryUpdate.Parameters.AddWithValue("@LOB", data.LOB);

                if (oQueryUpdate.ExecuteNonQuery() != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }

        }

        public static List<FILTER_RANDOM_CALL> loadingFilter(int CCMS, FILTER_RANDOM_CALL data)
        {
            try
            {
                string[] custom = data.CAMPAÑA.Split('-');
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();


                SqlCommand oQueryList = new SqlCommand("SELECT * " +
                                                        "FROM AppSA.FILTER_RANDOM_CALL " +
                                                        "WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND MES_ASIGNADO = @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO AND CCMS_ANALISTA = @CCMS_ANALISTA", Connection.oConnectionQaWeb);

                oQueryList.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
                oQueryList.Parameters.AddWithValue("@LOB", data.LOB);
                oQueryList.Parameters.AddWithValue("@MES_ASIGNADO", data.MES_ASIGNADO);
                oQueryList.Parameters.AddWithValue("@AÑO_ASIGNADO", data.AÑO_ASIGNADO);
                oQueryList.Parameters.AddWithValue("@CCMS_ANALISTA", CCMS);
                
                SqlDataReader oRead = oQueryList.ExecuteReader();

                List<FILTER_RANDOM_CALL> oListActGenerada = new List<FILTER_RANDOM_CALL>();

                while (oRead.Read())
                {
                    oListActGenerada.Add(new FILTER_RANDOM_CALL
                    {
                        FECHA_INICIAL = Convert.ToString(oRead["FECHA_INICIAL"]),
                        FECHA_FINAL = Convert.ToString(oRead["FECHA_FINAL"]),
                        NUMERO_MONITOREOS = Convert.ToInt32(oRead["NUMERO_MONITOREOS"]),
                        INICIO_LLAMADA_CORTA = Convert.ToString(oRead["INICIO_LLAMADA_CORTA"]),
                        FIN_LLAMADA_CORTA = Convert.ToString(oRead["FIN_LLAMADA_CORTA"]),
                        INICIO_LLAMADA_LARGA = Convert.ToString(oRead["INICIO_LLAMADA_LARGA"]),
                        FIN_LLAMADA_LARGA = Convert.ToString(oRead["FIN_LLAMADA_LARGA"]),
                        FECHA_ACTUALIZADO = Convert.ToString(oRead["FECHA_ACTUALIZADO"]),
                        CCMS_ACTUALIZADO = Convert.ToInt32(oRead["CCMS_ACTUALIZADO"]),
                        CSAT = Convert.ToString(oRead["CSAT"]),
                        NPS = Convert.ToString(oRead["NPS"]),
                        FCR = Convert.ToString(oRead["FCR"]),
                        CES = Convert.ToString(oRead["CES"]),
                        HOLD_INICIAL = Convert.ToString(oRead["HOLD_INICIAL"]),
                        HOLD_FINAL = Convert.ToString(oRead["HOLD_FINAL"]),
                        RECONTACTO = Convert.ToString(oRead["RECONTACTO"]),
                        TRASFERIDA = Convert.ToString(oRead["TRASFERIDA"]),
                        AGENTE_EVALUAR = Convert.ToString(oRead["AGENTE_EVALUAR"]),
                        AHT_INI = Convert.ToString(oRead["AHT_INICIAL"]),
                        AHT_FIN = Convert.ToString(oRead["AHT_FINAL"])
                    });
                }
                Connection.oConnectionQaWeb.Close();
                return oListActGenerada;
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
                       
        }

        public static List<MANAGER_ANALISTA> loadAnalysLob(string cliente, string lob, string ccms_manager)
        {
            int idccms = ProfileClass.dataIDCCMS(ccms_manager);
            string[] custom = cliente.Split('-');

            Connection.oConnection.Close();
            Connection.oConnection.Open();


            SqlCommand oQueryList = new SqlCommand("SELECT  DISTINCT  MA.CCMS, MA.NOMBRE, MP.ESTADO FROM AppSA.MANAGER_PERMISOS AS MP " +
                                                    "inner join AppSA.MANAGER_ANALISTAS AS MA ON MP.CCMS = MA.CCMS " +
                                                    "WHERE MP.CLIENTE = @CLIENTE AND MP.LOB = @LOB", Connection.oConnection);
            
            oQueryList.Parameters.AddWithValue("@CLIENTE", cliente);
            oQueryList.Parameters.AddWithValue("@LOB", lob);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<MANAGER_ANALISTA> listAnalys = new List<MANAGER_ANALISTA>();

            while (oRead.Read())
            {
                listAnalys.Add(new MANAGER_ANALISTA
                {
                    NOMBRE_ANALISTA = Convert.ToString(oRead["NOMBRE"]),
                    CCMS_ANALISTA = Convert.ToString(oRead["CCMS"]),
                    ESTADO = Convert.ToString(oRead["ESTADO"])
                });
            }

            return listAnalys;
        }
    }

    }



