using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Speech_analytics.Clases
{
    public class AppConfigureRandom
    {
        public static List<TBL_ECH> agentesFinales(string idCliente, string filtro)
        {

            string[] custom = idCliente.Split('-');
            string tabla;

            if (custom[1] == "EPS_Sura")
            {
                tabla = "SURA";
            }
            else
            {
                tabla = custom[1];
            }
            string query = "SELECT DISTINCT AGENTE_FINAL, FAC.idccms, FAC.Nombre " +
                                "FROM CMS.TBL_ECH_" + tabla.ToUpper() + " AS TBL " +
                                "INNER JOIN HC.TBL_FactEmpleados AS FAC ON TBL.AGENTE_FINAL = FAC.Login " +
                                "WHERE AGENTE_FINAL IS NOT NULL AND Nombre LIKE @Nombre AND FAC.Estado = 'Active' " +
                                "ORDER BY FAC.Nombre ASC";



            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryList = new SqlCommand(query, Connection.oConnection);

            oQueryList.Parameters.AddWithValue("@Nombre", "%" + filtro +"%");

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_ECH> oListAgentes = new List<TBL_ECH>();

            while (oRead.Read())
            {
                oListAgentes.Add(new TBL_ECH
                {
                    AGENTE_FINAL = Convert.ToInt32(oRead["AGENTE_FINAL"]),
                    TBL_FactEmpleados = new TBL_FactEmpleados 
                    {
                        idccms = Convert.ToInt32(oRead["idccms"]),  
                        Nombre = Convert.ToString(oRead["Nombre"]),  
                    },
                });
            }
            return oListAgentes;
        }

        public static int validarExisteFiltrosAleatoriedad(int CCMS, FILTER_RANDOM_CALL data)
        {

            try
            {
                string[] custom = data.CAMPAÑA.Split('-');
                const string query = @" SELECT FECHA_ACTUALIZADO
                                            FROM AppSA.FILTER_RANDOM_CALL
                                            WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND CCMS_ANALISTA = @CCMS_ANALISTA AND MES_ASIGNADO = @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO";

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand oQuerySelect = new SqlCommand(query, Connection.oConnectionQaWeb);

                oQuerySelect.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
                oQuerySelect.Parameters.AddWithValue("@LOB", data.LOB);
                oQuerySelect.Parameters.AddWithValue("@CCMS_ANALISTA",CCMS);
                oQuerySelect.Parameters.AddWithValue("@MES_ASIGNADO", data.MES_ASIGNADO);
                oQuerySelect.Parameters.AddWithValue("@AÑO_ASIGNADO", data.AÑO_ASIGNADO);

                SqlDataReader oRead = oQuerySelect.ExecuteReader();

                if (oRead.Read())
                {
                    Connection.oConnectionQaWeb.Close();
                    return 1;
                }
                else
                {
                    Connection.oConnectionQaWeb.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int validarExisteFiltrosAleatoriedadAgentes(int idAgentesEvaluar)
        {

            try
            {
                int count = 0;
                const string query = @" SELECT FECHA_ACTUALIZADO FROM AppSA.EVALUAR_AGENTES
                                            WHERE ID_AGENTES = @ID_AGENTES";

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand oQuerySelect = new SqlCommand(query, Connection.oConnectionQaWeb);

                oQuerySelect.Parameters.AddWithValue("@ID_AGENTES", idAgentesEvaluar);

                SqlDataReader oRead = oQuerySelect.ExecuteReader();                

                while (oRead.Read())
                {
                    count++;
                }

                Connection.oConnectionQaWeb.Close();
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string loadCuotaAleatoriedad(int CCMS, FILTER_RANDOM_CALL data)
        {

            try
            {
                const string query = @" SELECT NUM_MONITOREOS 
                                            FROM AppSA.MANAGER_CONF
                                            WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND MES_ASIGNADO = @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO";
                string[] custom = data.CAMPAÑA.Split('-');
                string NUM_MONITOREOS;

                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand oQuerySelect = new SqlCommand("SELECT NUM_MONITOREOS " +
                "FROM AppSA.MANAGER_CONF   " +
                "WHERE ID_CLIENTE = '"+custom[0]+"' AND LOB = '"+data.LOB+"' AND MES_ASIGNADO = '"+data.MES_ASIGNADO+"' AND AÑO_ASIGNADO = '"+data  .AÑO_ASIGNADO+"'", Connection.oConnection);

                oQuerySelect.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
                oQuerySelect.Parameters.AddWithValue("@LOB", data.LOB);
                oQuerySelect.Parameters.AddWithValue("@MES_ASIGNADO", data.MES_ASIGNADO);
                oQuerySelect.Parameters.AddWithValue("@AÑO_ASIGNADO", data.AÑO_ASIGNADO);

                SqlDataReader oRead = oQuerySelect.ExecuteReader();

                if (oRead.Read())
                {
                    return NUM_MONITOREOS = Convert.ToString(oRead["NUM_MONITOREOS"]);
                }
                else
                {
                    return "not";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static int saveDataConfiguracionAleatoriedad(Dictionary<string, string> valuesForSql)
        {
            try
            {
                int idAgenteEvaluar;
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand OQueyInsert = new SqlCommand(@"  INSERT INTO [AppSA].[FILTER_RANDOM_CALL]
                                                           ([ID_CLIENTE]
                                                           ,[CAMPAÑA]
                                                           ,[FECHA_INICIAL]
                                                           ,[FECHA_FINAL]
                                                           ,[NUMERO_MONITOREOS]
                                                           ,[FECHA_ACTUALIZADO]
                                                           ,[CCMS_ACTUALIZADO]
                                                           ,[LOB]
                                                           ,[INICIO_LLAMADA_CORTA]
                                                           ,[FIN_LLAMADA_CORTA]
                                                           ,[INICIO_LLAMADA_LARGA]
                                                           ,[FIN_LLAMADA_LARGA]
                                                           ,[CCMS_ANALISTA]
                                                           ,[MES_ASIGNADO]
                                                           ,[AÑO_ASIGNADO]
                                                           ,[CSAT]
                                                           ,[NPS]
                                                           ,[FCR]
                                                           ,[CES]
                                                           ,[RECONTACTO]
                                                           ,[HOLD_INICIAL]
                                                           ,[HOLD_FINAL]
                                                           ,[AHT_INICIAL]
                                                           ,[AHT_FINAL]
                                                           ,[TRASFERIDA])
                                                             VALUES (
                                                                    @ID_CLIENTE,
                                                                    @CAMPAÑA,
                                                                    @FECHA_INICIAL,
                                                                    @FECHA_FINAL,
                                                                    @NUMERO_MONITOREOS,
                                                                    @FECHA_ACTUALIZADO,
                                                                    @CCMS_ACTUALIZADO,
                                                                    @LOB,
                                                                    @INICIO_LLAMADA_CORTA,
                                                                    @FIN_LLAMADA_CORTA,
                                                                    @INICIO_LLAMADA_LARGA,
                                                                    @FIN_LLAMADA_LARGA,
                                                                    @CCMS_ANALISTA,
                                                                    @MES_ASIGNADO,
                                                                    @AÑO_ASIGNADO,
                                                                    @CSAT,
                                                                    @NPS,
                                                                    @FCR,
                                                                    @CES,
                                                                    @RECONTACTO,
                                                                    @HOLD_INICIAL,
                                                                    @HOLD_FINAL,
                                                                    @AHT_INI,
                                                                    @AHT_FIN,
                                                                    @TRASFERIDA

                )", Connection.oConnectionQaWeb);



                foreach (var item in valuesForSql)
                {
                    OQueyInsert.Parameters.AddWithValue(item.Key, item.Value);
                }

                if (OQueyInsert.ExecuteNonQuery() != 0)
                {
                    SqlCommand oQueryList = new SqlCommand(@"   SELECT AGENTE_EVALUAR
                                                                FROM AppSA.FILTER_RANDOM_CALL
                                                                WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND MES_ASIGNADO =  @MES_ASIGNADO AND	AÑO_ASIGNADO = @AÑO_ASIGNADO AND CCMS_ACTUALIZADO = @CCMS_ACTUALIZADO", Connection.oConnectionQaWeb);
                    foreach (var item in valuesForSql)
                    {
                        oQueryList.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    SqlDataReader oRead = oQueryList.ExecuteReader();

                    if (oRead.Read()) 
                    {
                        idAgenteEvaluar = Convert.ToInt16(oRead["AGENTE_EVALUAR"]);
                    }
                    else
                    {
                        idAgenteEvaluar = 0;
                    }
                    Connection.oConnectionQaWeb.Close();
                    return idAgenteEvaluar;

                }
                else
                {
                    idAgenteEvaluar = 0;
                    Connection.oConnectionQaWeb.Close();
                    return idAgenteEvaluar;
                }

                
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int updateConfiguracionAleatoriedad(Dictionary<string, string> valuesForSql)
        {
            int idAgenteEvaluar;
            try
            {

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand oQueryUpdate = new SqlCommand(@" UPDATE [AppSA].[FILTER_RANDOM_CALL]
                                                            SET 
                                                            [ID_CLIENTE]=@ID_CLIENTE,  
                                                            [CAMPAÑA]=@CAMPAÑA,  
                                                            [FECHA_INICIAL]=@FECHA_INICIAL,  
                                                            [FECHA_FINAL]=@FECHA_FINAL,  
                                                            [NUMERO_MONITOREOS]=@NUMERO_MONITOREOS,  
                                                            [FECHA_ACTUALIZADO]=@FECHA_ACTUALIZADO,  
                                                            [CCMS_ACTUALIZADO]=@CCMS_ACTUALIZADO,  
                                                            [LOB]=@LOB,  
                                                            [INICIO_LLAMADA_CORTA]=@INICIO_LLAMADA_CORTA,  
                                                            [FIN_LLAMADA_CORTA]=@FIN_LLAMADA_CORTA,  
                                                            [INICIO_LLAMADA_LARGA]=@INICIO_LLAMADA_LARGA,  
                                                            [FIN_LLAMADA_LARGA]=@FIN_LLAMADA_LARGA,  
                                                            [CCMS_ANALISTA]=@CCMS_ANALISTA,  
                                                            [MES_ASIGNADO]=@MES_ASIGNADO,  
                                                            [AÑO_ASIGNADO]=@AÑO_ASIGNADO, 
                                                            [CSAT]=@CSAT,  
                                                            [NPS]=@NPS,  
                                                            [FCR]=@FCR,  
                                                            [CES]=@CES,  
                                                            [RECONTACTO]=@RECONTACTO,  
                                                            [HOLD_INICIAL]=@HOLD_INICIAL,  
                                                            [HOLD_FINAL]=@HOLD_FINAL,  
                                                            [AHT_INICIAL]=@AHT_INI,  
                                                            [AHT_FINAL]=@AHT_FIN,  
                                                            [TRASFERIDA]=@TRASFERIDA 
                                                            WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND CCMS_ANALISTA = @CCMS_ANALISTA AND MES_ASIGNADO = @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO "
                                                            , Connection.oConnectionQaWeb);

                foreach (var item in valuesForSql)
                {
                    oQueryUpdate.Parameters.AddWithValue(item.Key, item.Value);
                }

                if (oQueryUpdate.ExecuteNonQuery() != 0)
                {
                    SqlCommand oQueryList = new SqlCommand("SELECT AGENTE_EVALUAR    " +
                                                        "FROM AppSA.FILTER_RANDOM_CALL " +
                                                        "WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND MES_ASIGNADO =  @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO AND CCMS_ACTUALIZADO = @CCMS_ACTUALIZADO "
                                                        , Connection.oConnectionQaWeb);

                    foreach (var item in valuesForSql)
                    {
                        oQueryList.Parameters.AddWithValue(item.Key, item.Value);
                    }

                    SqlDataReader oRead = oQueryList.ExecuteReader();

                    if (oRead.Read())
                    {
                        idAgenteEvaluar = Convert.ToInt16(oRead["AGENTE_EVALUAR"]);
                    }
                    else
                    {
                        idAgenteEvaluar = 0;
                    }
                    Connection.oConnectionQaWeb.Close();
                    return idAgenteEvaluar;

                }
                else
                {
                    idAgenteEvaluar = 0;
                    Connection.oConnectionQaWeb.Close();
                    return idAgenteEvaluar;
                }

                
            }
            catch (Exception)
            {
                idAgenteEvaluar = 0;
                return idAgenteEvaluar;
            }

        }


        public static List<FILTER_RANDOM_CALL> loadingFilterMiPlantilla(int CCMS, FILTER_RANDOM_CALL data)
        {

            try
            {
                string[] custom = data.CAMPAÑA.Split('-');
                const string query = @" SELECT * FROM AppSA.FILTER_RANDOM_CALL 
                                            WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND CCMS_ANALISTA = @CCMS_ANALISTA";

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand oQueryList = new SqlCommand("SELECT *  " +
                                                        "FROM AppSA.FILTER_RANDOM_CALL " +
                                                        "WHERE ID_CLIENTE = '" + custom[0] + "' AND LOB = '" + data.LOB + "' AND CCMS_ANALISTA = '" + CCMS + "'", Connection.oConnectionQaWeb);

                oQueryList.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
                oQueryList.Parameters.AddWithValue("@LOB", data.LOB);
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
                        AHT_INI = Convert.ToString(oRead["AHT_INICIAL"]),
                        AHT_FIN = Convert.ToString(oRead["AHT_FINAL"]),
                        RECONTACTO = Convert.ToString(oRead["RECONTACTO"]),
                        TRASFERIDA = Convert.ToString(oRead["TRASFERIDA"]),
                        AGENTE_EVALUAR = Convert.ToString(oRead["AGENTE_EVALUAR"])

                    });
                }
                Connection.oConnectionQaWeb.Close();
                return oListActGenerada;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static int loadMesActividad(int CCMS, FILTER_RANDOM_CALL data)
        {

            try
            {
                string[] custom = data.CAMPAÑA.Split('-');
                int NUMERO_MONITOREOS;
                const string query = @" SELECT DISTINCT NUMERO_MONITOREOS FROM AppSA.FILTER_RANDOM_CALL AS FRC
	                                        INNER JOIN AppSA.MANAGER_CONFIGURATION AS MC ON FRC.LOB = MC.ID_LOB
	                                        WHERE FRC.ID_CLIENTE = @ID_CLIENTE AND FRC.LOB = @LOB AND FRC.CCMS_ANALISTA = @CCMS_ANALISTA AND MES_ASIGNADO = @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO";
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
                command.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
                command.Parameters.AddWithValue("@LOB", data.LOB);
                command.Parameters.AddWithValue("@CCMS_ANALISTA", CCMS);
                command.Parameters.AddWithValue("@MES_ASIGNADO", data.MES_ASIGNADO);
                command.Parameters.AddWithValue("@AÑO_ASIGNADO", data.AÑO_ASIGNADO);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    NUMERO_MONITOREOS = reader.GetInt32(0);
                }
                else
                {
                    NUMERO_MONITOREOS = 0;
                }
                Connection.oConnectionQaWeb.Close();
                return NUMERO_MONITOREOS;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int loadActividadMensual(int CCMS, FILTER_RANDOM_CALL data)
        {

            try
            {
                const string query = @" SELECT DISTINCT PROMEDIO_ACTIVIDAD, DIAS_ACTIVIDAD, NUM_ANALISTAS
	                                        FROM AppSA.MANAGER_CONFIGURATION
	                                        WHERE ID_CLIENTE = @ID_CLIENTE AND ID_LOB =  @ID_LOB AND AÑO = @AÑO AND MES = @MES";
                string[] custom = data.CAMPAÑA.Split('-');
                int NUM_MONITOREOS;
                int DIAS_ACTIVIDAD;
                int NUM_ANALISTAS;
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
                command.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
                command.Parameters.AddWithValue("@ID_LOB", data.LOB);
                command.Parameters.AddWithValue("@AÑO", data.AÑO_ASIGNADO);
                command.Parameters.AddWithValue("@MES", data.MES_ASIGNADO);

                SqlDataReader oRead = command.ExecuteReader();

                if (oRead.Read())
                {
                    NUM_MONITOREOS = Convert.ToInt16(oRead["PROMEDIO_ACTIVIDAD"]);
                    DIAS_ACTIVIDAD = Convert.ToInt16(oRead["DIAS_ACTIVIDAD"]);
                    NUM_ANALISTAS = Convert.ToInt16(oRead["NUM_ANALISTAS"]);
                }
                else
                {
                    NUM_MONITOREOS = 0;
                    DIAS_ACTIVIDAD = 0;
                    NUM_ANALISTAS = 0;
                }

                return (NUM_MONITOREOS *DIAS_ACTIVIDAD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static int loadDiaActividadCumplimiento(int CCMS, FILTER_RANDOM_CALL data, string FECHA)
        {

            try
            {
                
                string[] custom = data.CAMPAÑA.Split('-');
                const string query = @" SELECT COUNT(PA.ID_PDF) AS COUNT_PDF
                                            FROM AppSA.PDF_AL AS PA
                                            INNER JOIN AppSA.PDF_AL_USER AS PAU ON PA.ID_PDF = PAU.ID_PDF
                                            INNER JOIN(SELECT DISTINCT *
                                            FROM AppSA.MANAGER_CONFIGURATION) MC ON PAU.LOB = MC.LOB
                                            WHERE PAU.CRETE_DATETIME LIKE @CRETE_DATETIME AND PA.CUMPLIMIENTO = 'true' AND MC.ID_LOB = @ID_LOB AND PAU.CCMS_EVALUATOR = @CCMS_EVALUATOR AND PA.CAMPAÑA = @CAMPAÑA";
                int NUMERO_MONITOREOS;
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();


                SqlCommand oQueryList = new SqlCommand(query, Connection.oConnectionQaWeb);
                
                oQueryList.Parameters.AddWithValue("@CRETE_DATETIME", "%" + FECHA + "%");
                oQueryList.Parameters.AddWithValue("@ID_LOB", data.LOB);
                oQueryList.Parameters.AddWithValue("@CCMS_EVALUATOR", CCMS);
                oQueryList.Parameters.AddWithValue("@CAMPAÑA", custom[0]);

                SqlDataReader oRead = oQueryList.ExecuteReader();

                if (oRead.Read())
                {
                    NUMERO_MONITOREOS = Convert.ToInt16(oRead["COUNT_PDF"]);
                }
                else
                {
                    NUMERO_MONITOREOS = 0;
                }

                Connection.oConnectionQaWeb.Close();
                return NUMERO_MONITOREOS;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public static int loadActividadRealizadaMes(int CCMS, FILTER_RANDOM_CALL data,string FECHA)
        {

            try
            {

                const string query = @"SELECT COUNT(*) AS QUANTITY
                                            FROM AppSA.PDF_AL AS P
                                                INNER JOIN AppSA.PDF_AL_USER AS U ON P.ID_PDF = U.ID_PDF
                                                INNER JOIN(SELECT DISTINCT ID_LOB, LOB
                                                FROM AppSA.MANAGER_CONFIGURATION) MC ON U.LOB = MC.LOB
                                                WHERE U.CRETE_DATETIME LIKE @FECHA AND P.CUMPLIMIENTO = 'true' AND MC.ID_LOB = @ID_LOB AND U.CCMS_EVALUATOR = @CCMS_EVALUATOR AND P.CAMPAÑA = @CAMPAÑA";
                
                string[] custom = data.CAMPAÑA.Split('-');
                int NUMERO_MONITOREOS;
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();


                SqlCommand oQueryList = new SqlCommand(query, Connection.oConnectionQaWeb);

                oQueryList.Parameters.AddWithValue("@FECHA", "%" + FECHA + "%");
                oQueryList.Parameters.AddWithValue("@ID_LOB", data.LOB);
                oQueryList.Parameters.AddWithValue("@CCMS_EVALUATOR", CCMS);
                oQueryList.Parameters.AddWithValue("@CAMPAÑA", custom[1]);

                SqlDataReader oRead = oQueryList.ExecuteReader();

                if (oRead.Read())
                {
                    NUMERO_MONITOREOS = Convert.ToInt16(oRead["QUANTITY"]);
                }
                else
                {
                    NUMERO_MONITOREOS = 0;
                }
                Connection.oConnectionQaWeb.Close();
                return NUMERO_MONITOREOS;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static int loadCuotaCumplidaMes(int CCMS, MANAGER_CONFcs data, string FECHA)
        {

            try
            {
                const string query = @" SELECT COUNT(U.ID_PDF) AS QUANTITY
                                            FROM AppSA.PDF_AL AS P
                                            INNER JOIN AppSA.PDF_AL_USER AS U ON P.ID_PDF = U.ID_PDF
                                            INNER JOIN (select distinct ID_LOB, CLIENTE, LOB
                                            from AppSA.MANAGER_CONFIGURATION) SKILL ON U.LOB = SKILL.LOB
                                                        WHERE U.CRETE_DATETIME LIKE @FECHA AND P.CUMPLIMIENTO = 'true' AND SKILL.ID_LOB = @ID_LOB AND P.CAMPAÑA = @CAMPAÑA";
                string[] custom = data.CLIENTE.Split('-');
                int NUMERO_MONITOREOS;
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();


                SqlCommand oQueryList = new SqlCommand(query, Connection.oConnectionQaWeb);

                oQueryList.Parameters.AddWithValue("@FECHA", "%" + FECHA + "%");
                oQueryList.Parameters.AddWithValue("@ID_LOB", data.LOB);
                oQueryList.Parameters.AddWithValue("@CAMPAÑA", custom[1]);

                SqlDataReader oRead = oQueryList.ExecuteReader();

                if (oRead.Read())
                {
                    NUMERO_MONITOREOS = Convert.ToInt16(oRead["QUANTITY"]);
                }
                else
                {
                    NUMERO_MONITOREOS = 0;
                }
                Connection.oConnectionQaWeb.Close();
                return NUMERO_MONITOREOS;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static List<MANAGER_ANALISTA> loadListManager(string userRed)
        {

            try
            {
                const string query = @" SELECT  NOMBRE, MA.USER_RED
                                            FROM AppSA.MANAGER_ANALISTAS AS MA
                                            INNER JOIN AppSA.USERDATA AS U ON U.USER_RED = MA.USER_RED 
                                            WHERE USER_RED_MANAGER = @USER_RED_MANAGER AND ESTADO = 'ACTIVO'";

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();


                SqlCommand oQueryList = new SqlCommand(query, Connection.oConnectionQaWeb);
                
                oQueryList.Parameters.AddWithValue("@CCMS_ACTUALIZADO", userRed);

                SqlDataReader oRead = oQueryList.ExecuteReader();

                List<MANAGER_ANALISTA> oListActGenerada = new List<MANAGER_ANALISTA>();

                while (oRead.Read())
                {
                    oListActGenerada.Add(new MANAGER_ANALISTA
                    {

                        NOMBRE = Convert.ToString(oRead["NOMBRE"]),
                        CCMS_ANALISTA = Convert.ToString(oRead["USER_RED"]),
                    });
                }

                return oListActGenerada;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static bool saveAnalistasEvaluar(EVALUAR_AGENTES data, int CCMS)
        {
            try
            {
                const string query = @" INSERT INTO [AppSA].[EVALUAR_AGENTES]
                                            ([ID_AGENTES],[LOGIN_AGENTE],[FECHA_ACTUALIZADO],[CCMS_ACTUALIZADO],[NUMERO_MONITOREOS],[CCMS_AGENTE],[NOMBRE_AGENTE])
                                                VALUES (@ID_AGENTES,
                                                        @LOGIN_AGENTE,
                                                        GETDATE(),
                                                        @CCMS,
                                                        @NUMERO_MONITOREOS,
                                                        @CCMS_AGENTE,
                                                        @NOMBRE_AGENTE)";

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand OQueyInsert = new SqlCommand(query, Connection.oConnectionQaWeb);

                OQueyInsert.Parameters.AddWithValue("@ID_AGENTES", data.ID_AGENTES);
                OQueyInsert.Parameters.AddWithValue("@LOGIN_AGENTE", data.LOGIN_AGENTE);
                OQueyInsert.Parameters.AddWithValue("@CCMS", CCMS);
                OQueyInsert.Parameters.AddWithValue("@NUMERO_MONITOREOS", data.NUMERO_MONITOREOS);
                OQueyInsert.Parameters.AddWithValue("@CCMS_AGENTE", data.CCMS_AGENTE);
                OQueyInsert.Parameters.AddWithValue("@NOMBRE_AGENTE", data.NOMBRE_AGENTE);

                if (OQueyInsert.ExecuteNonQuery() != 0)
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
            catch (Exception)
            {
                return false;
            }

        }

        public static bool eliminarAnalistasEvaluar(int ID_AGENTES, int CCMS)
        {
            try
            {
                const string query = @" DELETE FROM AppSA.EVALUAR_AGENTES
                                            WHERE ID_AGENTES = @ID_AGENTES AND CCMS_ACTUALIZADO = @CCMS";

                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand OqueryDelete = new SqlCommand("DELETE FROM AppSA.EVALUAR_AGENTES " +
                                                        "WHERE ID_AGENTES = '"+ID_AGENTES+"' AND CCMS_ACTUALIZADO = '"+CCMS+"'", Connection.oConnectionQaWeb);

                OqueryDelete.Parameters.AddWithValue("@ID_AGENTES", ID_AGENTES);
                OqueryDelete.Parameters.AddWithValue("@CCMS", CCMS);

                if (OqueryDelete.ExecuteNonQuery() != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public static List<EVALUAR_AGENTES> loadAgentesEvaluar(int idAgentes, int CCMS)
        {
            const string query = @"SELECT LOGIN_AGENTE, NUMERO_MONITOREOS, NOMBRE_AGENTE,CCMS_ACTUALIZADO 
                                        FROM AppSA.EVALUAR_AGENTES 
                                        WHERE ID_AGENTES = @ID_AGENTES AND CCMS_ACTUALIZADO = @CCMS_ACTUALIZADO";

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryList = new SqlCommand(query, Connection.oConnectionQaWeb);
            
            oQueryList.Parameters.AddWithValue("@ID_AGENTES", idAgentes);
            oQueryList.Parameters.AddWithValue("@CCMS_ACTUALIZADO", CCMS);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<EVALUAR_AGENTES> oListData = new List<EVALUAR_AGENTES>();

            while (oRead.Read())
            {
                oListData.Add(new EVALUAR_AGENTES
                {

                    LOGIN_AGENTE = Convert.ToString(oRead["LOGIN_AGENTE"]),
                    NUMERO_MONITOREOS = Convert.ToInt16(oRead["NUMERO_MONITOREOS"]),
                    CCMS_AGENTE = Convert.ToInt32(oRead["CCMS_ACTUALIZADO"]),
                    tbl_factempleados = new TBL_FactEmpleados
                    {
                        Nombre = Convert.ToString(oRead["NOMBRE_AGENTE"]),
                    }
                });
            }

            return oListData;
        }


    }
}