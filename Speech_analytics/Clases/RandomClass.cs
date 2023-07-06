using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Web;

namespace Speech_analytics.Clases
{
    public class RandomClass
    {
        public static List<TBL_Campañas_TPModel> lsClientes()
        {
            string nombreCLIENTE;

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();



            SqlCommand oQueryList = new SqlCommand(@"   SELECT id_Cliente, nombre, estado, fecha_Actualizado
                                                        FROM AppSA.CAMPAÑAS
                                                        WHERE estado != 'INACTIVO'", Connection.oConnectionQaWeb);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_Campañas_TPModel> oList = new List<TBL_Campañas_TPModel>();

            while (oRead.Read())
            {

                if (Convert.ToString(oRead["nombre"]) == "EPS_Sura")
                {
                    nombreCLIENTE = "SURA";
                }
                else
                {
                    nombreCLIENTE = Convert.ToString(oRead["nombre"]);
                }

                oList.Add(new TBL_Campañas_TPModel
                {
                    ID_Cliente = Convert.ToString(oRead["id_Cliente"]),
                    Nombre_Campaña = nombreCLIENTE
                });
            }
            Connection.oConnectionQaWeb.Close();
            return oList;
        }

        public static List<TBL_FactEmpleados> dataUser(string user)
        {

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryList = new SqlCommand("select idccms, Nombre, idfiscal, Rol from HC.TBL_FactEmpleados  WHERE  idcuenta =  @idcuenta ", Connection.oConnection);
            oQueryList.Parameters.AddWithValue("@idcuenta", user);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_FactEmpleados> oListEmpleado = new List<TBL_FactEmpleados>();

            while (oRead.Read())
            {
                oListEmpleado.Add(new TBL_FactEmpleados
                {
                    idccms = Convert.ToInt32(oRead["idccms"]),
                    Nombre = Convert.ToString(oRead["Nombre"]),
                    idfiscal = Convert.ToString(oRead["idfiscal"]),
                    Rol = Convert.ToString(oRead["Rol"])
                });
            }
            Connection.oConnection.Close();
            return oListEmpleado;
        }


        public static List<TBL_IVR_Resolutivo> dataRamdomSurveys(string idCliente)
        {
            string[] custom = idCliente.Split('-');

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryList = new SqlCommand("select TOP 5 CAMPAÑA,Fecha_Fin, UCID,ID_Encuesta, Split, Login, ANI, STRPregunta, Respuesta  " +
                                                   "from [Calidad].[TBL_IVR_Resolutivo] " +
                                                   "WHERE Campaña = @CAMPAÑA AND Respuesta <= '2' AND Fecha_Fin >= GETDATE() -1 AND Fecha_Fin < GETDATE()   ORDER BY newid() ", Connection.oConnection);
            
            oQueryList.Parameters.AddWithValue("@CAMPAÑA", custom[1].ToUpper());

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_IVR_Resolutivo> surveys = new List<TBL_IVR_Resolutivo>();

            while (oRead.Read())
            {
                surveys.Add(new TBL_IVR_Resolutivo
                {
                    Campaña = Convert.ToString(oRead["Campaña"]),
                    UCID = Convert.ToString(oRead["UCID"]),
                    ID_Encuesta = Convert.ToString(oRead["ID_Encuesta"]),
                    STRPregunta = Convert.ToString(oRead["STRPregunta"]),
                    Respuesta = Convert.ToString(oRead["Respuesta"]),
                    Split = Convert.ToString(oRead["Split"]),
                    Fecha_Fin = Convert.ToString(oRead["Fecha_Fin"]),
                    Login = Convert.ToString(oRead["Login"]),
                    ANI = Convert.ToString(oRead["ANI"]),

                });
            }
            Connection.oConnection.Close();
            return surveys;
        }


        public static List<TBL_Info_Clientes> lsSubClientesFromCliente(string idCliente)
        {
            string[] custom = idCliente.Split('-');
            int IdCliente = Convert.ToInt32(custom[0]);
            string ID = "";

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            if (IdCliente <= 9)
            {
                ID = "0" + IdCliente;
            }
            else
            {
                ID = Convert.ToString(IdCliente);
            }


            SqlCommand oQueryList = new SqlCommand("SELECT DISTINCT Skill, Campaña, Nombre " +
                                                    "FROM [CMS].TBL_Homologacion_Skill_09_Noviembre " +
                                                    "WHERE Campaña = @CAMPAÑA ORDER BY Nombre ASC ", Connection.oConnection);

            oQueryList.Parameters.AddWithValue("@CAMPAÑA", custom[1]);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_Info_Clientes> oListSubClientes = new List<TBL_Info_Clientes>();

            while (oRead.Read())
            {
                oListSubClientes.Add(new TBL_Info_Clientes
                {
                    Skill = Convert.ToString(oRead["Skill"]),
                    Campaña = Convert.ToString(oRead["Campaña"]),
                    Nombre = Convert.ToString(oRead["Nombre"])

                });
            }

            Connection.oConnection.Close();
            return oListSubClientes;
        }

        public static int SaveNewDataForPDF(PDF_AL_USER oPdf_Al_USER, object dataRamdomCallCustoms)
        {
            try
            {
                string[] custom = oPdf_Al_USER.ID_CLIENTE.Split('-');
                int cont = 1;
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand OQueyInsert = new SqlCommand(@"  INSERT INTO [AppSA].[PDF_AL_USER]([ID_PDF],[CCMS_EVALUATOR],[CRETE_DATETIME],[ID_CLIENTE],[LOB])
                                                                VALUES ( @ID_PDF, @CCMS_EVALUATOR, @CRETE_DATETIME, ID_CLIENTE, @LOB)", Connection.oConnectionQaWeb);

                OQueyInsert.Parameters.AddWithValue("@ID_PDF", oPdf_Al_USER.ID_PDF);
                OQueyInsert.Parameters.AddWithValue("@CCMS_EVALUATOR", oPdf_Al_USER.CCMS_EVALUATOR);
                OQueyInsert.Parameters.AddWithValue("@CRETE_DATETIME", oPdf_Al_USER.CRETE_DATETIME);
                OQueyInsert.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
                OQueyInsert.Parameters.AddWithValue("@LOB", oPdf_Al_USER.LOB);

                OQueyInsert.ExecuteNonQuery();                

                List<TBL_ECH> oList = dataRamdomCallCustoms as List<TBL_ECH>;

                foreach (var item in oList)
                {
                    SqlCommand oQueryList = new SqlCommand("INSERT INTO [AppSA].[PDF_AL]([POSITION_PDF],[ID_PDF],[AGENTE_INICIAL],[LOB],[TALK_TIME],[HOLD_TIME] ,[TIMES_HELD],[DURACION],[ORIGEN_COLGADA],[TRANSFERIDA],[CAMPAÑA],[UCID], [CUMPLIMIENTO])" +
                                       "VALUES (" + "'" + cont + "'" + "," + "'" + oPdf_Al_USER.ID_PDF + "'" + "," + "'" + item.AGENTE_INICIAL + "'" + "," + "'" + oPdf_Al_USER.LOB + "'" + "," + "'" + item.TALK_TIME + "'" + "," + "'" + item.HOLD_TIME + "'" + "," + "'" + item.TIMES_HELD + "'" + "," + "'" + item.DURACION + "'" + "," + "'" + item.ORIGEN_COLGADA + "'" + "," + "'" + item.TRANSFERIDA + "'" + "," + "'" + item.CAMPAÑA + "'" + "," + "'" + item.UCID + "'" + "," + "'" + "false" + "'" + ")", Connection.oConnectionQaWeb);
                    
                    oQueryList.ExecuteNonQuery();
                    cont++;
                }

                Connection.oConnectionQaWeb.Close();
                return 1;
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }


        public static int SaveNewDataForPDF_Surveys(PDF_AL_USER oPdf_Al_USER, object dataRamdomSurveys)
        {
            try
            {
                string[] custom = oPdf_Al_USER.ID_CLIENTE.Split('-');
                int cont = 1;
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand OQueyInsert = new SqlCommand(@"  INSERT INTO [AppSA].[PDF_AL_USER_SURV]([ID_PDF],[CCMS_EVALUATOR],[CRETE_DATETIME],[ID_CLIENTE])
                                                                VALUES (@ID_PDF, @CCMS_EVALUATOR, @CRETE_DATETIME, @ID_CLIENTE)", Connection.oConnectionQaWeb);

                OQueyInsert.Parameters.AddWithValue("@ID_PDF", oPdf_Al_USER.ID_PDF );
                OQueyInsert.Parameters.AddWithValue("@CCMS_EVALUATOR", oPdf_Al_USER.CCMS_EVALUATOR);
                OQueyInsert.Parameters.AddWithValue("@CRETE_DATETIME", oPdf_Al_USER.CRETE_DATETIME);
                OQueyInsert.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);

                OQueyInsert.ExecuteNonQuery();                

                List<TBL_IVR_Resolutivo> oList = dataRamdomSurveys as List<TBL_IVR_Resolutivo>;

                foreach (var item in oList)
                {

                    SqlCommand oQueryList = new SqlCommand(@"   INSERT INTO [AppSA].[PDF_AL_SURV]([POSITION_PDF],[ID_PDF],[Campaña],[UCID],[ID_Encuesta] ,[Split],[Fecha_Fin],[Login],[ANI],[STRPregunta] ,[Respuesta])
                                                                    VALUES ( @CONT, @ID_PDF , @CAMPAÑA, @UCID, @ID_ENCUESTA, @SPLIT, @FECHA_FIN, @LOGIN, @ANI, @STRPregunta, @RESPUESTA)", Connection.oConnectionQaWeb);

                    oQueryList.Parameters.AddWithValue("@CONT", cont);
                    oQueryList.Parameters.AddWithValue("@ID_PDF", oPdf_Al_USER.ID_PDF);
                    oQueryList.Parameters.AddWithValue("@CAMPAÑA", item.Campaña);
                    oQueryList.Parameters.AddWithValue("@UCID", item.UCID);
                    oQueryList.Parameters.AddWithValue("@ID_ENCUESTA", item.ID_Encuesta);
                    oQueryList.Parameters.AddWithValue("@SPLIT", item.Split);
                    oQueryList.Parameters.AddWithValue("@FECHA_FIN", item.Fecha_Fin);
                    oQueryList.Parameters.AddWithValue("@LOGIN", item.Login);
                    oQueryList.Parameters.AddWithValue("@ANI", item.ANI);
                    oQueryList.Parameters.AddWithValue("@STRPregunta", item.STRPregunta);
                    oQueryList.Parameters.AddWithValue("@RESPUESTA", item.Respuesta);
                    
                    oQueryList.ExecuteNonQuery();
                    cont++;
                }
                Connection.oConnectionQaWeb.Close();

                return 1;
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }


        public static List<loadActGenerada> dataUserRamdomCallCustoms(int user, string fechaInicial, string fechaFinal)
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();


            SqlCommand oQueryList = new SqlCommand(@"SELECT DISTINCT  U.ID_PDF, U.CRETE_DATETIME, C.CLIENTE, p.LOB  
                                                    FROM AppSA.PDF_AL AS P
                                                    INNER JOIN AppSA.PDF_AL_USER AS U on P.ID_PDF = U.ID_PDF
                                                    INNER JOIN AppSA.MANAGER_CONFIGURATION as C on U.ID_CLIENTE = C.ID_CLIENTE
                                                    WHERE CCMS_EVALUATOR = @CCMS_EVALUATOR AND U.CRETE_DATETIME BETWEEN @FECHA_INICIAL AND @FECHA_FINAL", Connection.oConnectionQaWeb);

            oQueryList.Parameters.AddWithValue("@CCMS_EVALUATOR", user);
            oQueryList.Parameters.AddWithValue("@FECHA_INICIAL", fechaInicial + "00:00:00");
            oQueryList.Parameters.AddWithValue("@FECHA_FINAL", fechaFinal + "59:59:59");

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<loadActGenerada> oListActGenerada = new List<loadActGenerada>();

            while (oRead.Read())
            {
                oListActGenerada.Add(new loadActGenerada
                {
                    ID_PDF = Convert.ToString(oRead["ID_PDF"]),
                    CRETE_DATETIME = Convert.ToString(oRead["CRETE_DATETIME"]),
                    Nombre_Campaña = Convert.ToString(oRead["CLIENTE"]),
                    Lob = Convert.ToString(oRead["LOB"])

                });
            }
            Connection.oConnectionQaWeb.Close();
            return oListActGenerada;
        }

        public static List<loadActGenerada> dataUserRamdomCallCustomsS(string user, string fechaInicial, string fechaFinal)
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();


            SqlCommand oQueryList = new SqlCommand(@"SELECT DISTINCT  U.ID_PDF, U.CRETE_DATETIME, C.CLIENTE, p.LOB  
                                                    FROM AppSA.PDF_AL AS P
                                                    INNER JOIN AppSA.PDF_AL_USER AS U on P.ID_PDF = U.ID_PDF
                                                    INNER JOIN AppSA.MANAGER_CONFIGURATION as C on U.ID_CLIENTE = C.ID_CLIENTE
                                                    WHERE CCMS_EVALUATOR = @CCMS_EVALUATOR AND U.CRETE_DATETIME BETWEEN @FECHA_INICIAL AND @FECHA_FINAL", Connection.oConnectionQaWeb);

            oQueryList.Parameters.AddWithValue("@CCMS_EVALUATOR", user);
            oQueryList.Parameters.AddWithValue("@FECHA_INICIAL", fechaInicial + "00:00:00");
            oQueryList.Parameters.AddWithValue("@FECHA_FINAL", fechaFinal + "59:59:59");

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<loadActGenerada> oListActGenerada = new List<loadActGenerada>();

            while (oRead.Read())
            {
                oListActGenerada.Add(new loadActGenerada
                {
                    ID_PDF = Convert.ToString(oRead["ID_PDF"]),
                    CRETE_DATETIME = Convert.ToString(oRead["CRETE_DATETIME"]),
                    Nombre_Campaña = Convert.ToString(oRead["CLIENTE"]),
                    Lob = Convert.ToString(oRead["LOB"])

                });
            }
            Connection.oConnectionQaWeb.Close();
            return oListActGenerada;
        }

        public static List<MANAGER_PERMISOS> verifyPermission(string cliente, string lob, string CCMS)
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryList = new SqlCommand("SELECT ESTADO FROM AppSA.MANAGER_PERMISOS " +
                "WHERE CCMS_ANALISTA = @CCMS_ANALISTA AND CLIENTE = @CLIENTE AND LOB = @LOB", Connection.oConnectionQaWeb);

            oQueryList.Parameters.AddWithValue("@CCMS_ANALISTA", CCMS);
            oQueryList.Parameters.AddWithValue("@CLIENTE", cliente);
            oQueryList.Parameters.AddWithValue("@LOB", lob);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<MANAGER_PERMISOS> listPermission = new List<MANAGER_PERMISOS>();

            while (oRead.Read())
            {
                listPermission.Add(new MANAGER_PERMISOS
                {
                    ESTADO = Convert.ToString(oRead["ESTADO"]),
                });
            }

            Connection.oConnectionQaWeb.Close();
            return listPermission;
        }

        public static string loadLobCliente(int skill)
        {
            string nombreSkill;
            Connection.oConnection.Close();
            Connection.oConnection.Open();


            SqlCommand oQueryList = new SqlCommand("SELECT DISTINCT Nombre FROM [CMS].TBL_Homologacion_Skill_09_Noviembre " +
                "WHERE Skill = @skill", Connection.oConnection);


            oQueryList.Parameters.AddWithValue("@Skill", skill);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            if (oRead.Read())
            {
                nombreSkill = Convert.ToString(oRead["Nombre"]);
            }
            else
            {
                nombreSkill = "Skill no disponible";
            }

            return nombreSkill;
        }

        public static List<TBL_ECH> dataRamdomCallCustomsAlternativa(FILTER_RANDOM_CALL data, int CCMS)
        {
            List<FILTER_RANDOM_CALL> listaFiltrosActividad = new List<FILTER_RANDOM_CALL>();
            listaFiltrosActividad = AdminConfigureRandomClass.loadingFilter(CCMS, data);

            string consultaSQL = "";
            string WhatSQl = "";
            int generarRandom = 0;
            string WhatNivel = "";
            string WhatFecha = "";
            string WhatFechaDuracion = "";
            string agenteEvaluar = "";
            string FECHA_INICIAL = "";
            string FECHA_FINAL = "";
            string NUMERO_MONITOREOS = "";
            string INICIO_LLAMADA_CORTA = "";
            string FIN_LLAMADA_CORTA = "";
            string INICIO_LLAMADA_LARGA = "";
            string FIN_LLAMADA_LARGA = "";
            string FECHA_ACTUALIZADO = "";
            string CCMS_ACTUALIZADO = "";
            string CSAT = "";
            string NPS = "";
            string FCR = "";
            string CES = "";
            string HOLD_INICIAL = "";
            string HOLD_FINAL = "";
            string AHI_INICIAL = "";
            string AHI_FINAL = "";
            string RECONTACTO = "";
            string TRASFERIDA = "";
            string AGENTE_EVALUAR = "";
            int WhatAgente = 0;
            DateTime currentDate = DateTime.Now;
            DateTime yesterday = currentDate.AddDays(-1);
            DateTime day2 = currentDate.AddDays(-2);
            DateTime dat3 = currentDate.AddDays(-3);
            DateTime month1 = currentDate.AddMonths(-1);
            DateTime month2 = currentDate.AddMonths(-2);
            DateTime month3 = currentDate.AddMonths(-3);
            String date = yesterday.ToString("yyyy-MM-dd");
            string fechaInicial = date + " 00:00:00:000";
            string fechaFinal = date + " 23:59:59:000";
            string fechaSinHora = currentDate.ToString("yyyy-MM-dd");
            string yesterday1 = currentDate.ToString("yyyy-MM-dd");
            string yesterday2 = currentDate.ToString("yyyy-MM-dd");
            string yesterday3 = currentDate.ToString("yyyy-MM-dd");

            string fechaRecontacto1 = "";
            string fechaRecontacto2 = "";
            string[] custom = data.CAMPAÑA.Split('-');

            foreach (var item in listaFiltrosActividad)
            {
                FECHA_INICIAL = Convert.ToString(item.FECHA_INICIAL) + " " + "00:00:00:000";
                FECHA_FINAL = Convert.ToString(item.FECHA_FINAL) + " " + "23:59:59:000";
                NUMERO_MONITOREOS = Convert.ToString(item.NUMERO_MONITOREOS);
                INICIO_LLAMADA_CORTA = Convert.ToString(item.INICIO_LLAMADA_CORTA);
                FIN_LLAMADA_CORTA = Convert.ToString(item.FIN_LLAMADA_CORTA);
                INICIO_LLAMADA_LARGA = Convert.ToString(item.INICIO_LLAMADA_LARGA);
                FIN_LLAMADA_LARGA = Convert.ToString(item.FIN_LLAMADA_LARGA);
                FECHA_ACTUALIZADO = Convert.ToString(item.FECHA_ACTUALIZADO);
                CCMS_ACTUALIZADO = Convert.ToString(item.CCMS_ACTUALIZADO);
                CSAT = Convert.ToString(item.CSAT);
                NPS = Convert.ToString(item.NPS);
                FCR = Convert.ToString(item.FCR);
                CES = Convert.ToString(item.CES);
                HOLD_INICIAL = Convert.ToString(item.HOLD_INICIAL);
                HOLD_FINAL = Convert.ToString(item.HOLD_FINAL);
                AHI_INICIAL = Convert.ToString(item.AHT_INI);
                AHI_FINAL = Convert.ToString(item.AHT_FIN);
                RECONTACTO = Convert.ToString(item.RECONTACTO);
                TRASFERIDA = Convert.ToString(item.TRASFERIDA);
                AGENTE_EVALUAR = Convert.ToString(item.AGENTE_EVALUAR);
            }

            if (data.FILTRO_NIVEL == "1")
            {

                string[] cadenaCSAT = CSAT.Split('-');
                string[] cadenaCES = CES.Split('-');
                string[] cadenaFCR = FCR.Split('-');
                string[] cadenaNPS = NPS.Split('-');

                switch (RECONTACTO)
                {
                    case "1":
                        fechaRecontacto1 = fechaSinHora + " 00:00:00:000";
                        fechaRecontacto2 = yesterday1 + " 23:59:59:000";
                        break;
                    case "2":
                        fechaRecontacto1 = yesterday1 + " 23:59:59:000";
                        fechaRecontacto2 = yesterday2 + " 23:59:59:000";
                        break;

                    case "3":
                        fechaRecontacto1 = yesterday1 + " 23:59:59:000";
                        fechaRecontacto2 = yesterday3 + " 23:59:59:000";
                        break;

                    case "4":
                        fechaRecontacto1 = date + " 00:00:00:000";
                        fechaRecontacto2 = month1.ToString("yyyy-MM-dd") + " 23:59:59:000";
                        break;

                    case "5":
                        fechaRecontacto1 = date + " 00:00:00:000";
                        fechaRecontacto2 = month2.ToString("yyyy-MM-dd") + " 23:59:59:000";
                        break;

                    case "6":
                        fechaRecontacto1 = date + " 00:00:00:000";
                        fechaRecontacto2 = month3.ToString("yyyy-MM-dd") + " 23:59:59:000";
                        break;

                }

                WhatNivel += @"
                                WITH CTE AS (
                                SELECT
                                ANI,
                                COUNT(DISTINCT UCID) AS NUMERO_CONTACTOS
                                FROM CALIDAD.TBL_IVR_Resolutivo
                                WHERE Fecha_Inicio < '" + fechaRecontacto1 + @"' AND Fecha_Inicio > '" + fechaRecontacto2 + @"' AND Campaña = '" + custom[1].ToUpper() + "'" +
                                @"
                                GROUP BY ANI
                                )
                                SELECT TOP " + NUMERO_MONITOREOS +
                                @"
                                    CLIENTE.UCID,
                                    CLIENTE.CAMPAÑA,
	                                CLIENTE.DURACION,
	                                CLIENTE.CONCTACT_ID,
	                                CLIENTE.AGENTE_FINAL,
	                                EMPLEADOS.Nombre,
	                                CLIENTE.TALK_TIME,
	                                CLIENTE.HOLD_TIME,
	                                CLIENTE.ORIGEN_COLGADA,
	                                CLIENTE.TRANSFERIDA,
                                    ENCUESTA.ANI,
                                    CTE.NUMERO_CONTACTOS
                                FROM CMS.TBL_ECH_" + custom[1].ToUpper() + @" AS CLIENTE
                                INNER JOIN CALIDAD.TBL_IVR_Resolutivo AS ENCUESTA
                                ON CLIENTE.UCID COLLATE SQL_Latin1_General_CP1_CI_AS = ENCUESTA.UCID COLLATE SQL_Latin1_General_CP1_CI_AS
                                INNER JOIN [HC].[TBL_FactEmpleados] AS EMPLEADOS
                                ON CLIENTE.AGENTE_FINAL = EMPLEADOS.Login
                                INNER JOIN CTE
                                ON ENCUESTA.ANI = CTE.ANI
                                WHERE EMPLEADOS.Estado = 'Active' AND HOLD_TIME != 0 AND DURACION > 0 AND SKILL = '" + data.LOB + "'";

                if (cadenaCSAT[0] != "0" || cadenaCES[0] != "0" || cadenaFCR[0] != "0" || cadenaNPS[0] != "0")
                {
                    WhatNivel += "AND (( ENCUESTA.Pregunta = 'P01' OR " +
                                " ";
                    for (int i = 1; i < cadenaCSAT.Length; i++)
                    {
                        if (i == 1)
                        {
                            WhatNivel += " ENCUESTA.Respuesta = '" + cadenaCSAT[i] + "'";
                        }
                        else
                        {
                            WhatNivel += " OR ENCUESTA.Respuesta = '" + cadenaCSAT[i] + "'";
                        }

                        if (i == cadenaCSAT.Length - 1)
                        {
                            WhatNivel += " ) ";
                        }
                    }

                    WhatNivel += " OR (ENCUESTA.Pregunta = 'P02'";

                    for (int i = 1; i < cadenaCES.Length; i++)
                    {
                        if (i == 1)
                        {
                            WhatNivel += " OR ENCUESTA.Respuesta = '" + cadenaCES[i] + "'";
                        }
                        else
                        {
                            WhatNivel += " OR ENCUESTA.Respuesta = '" + cadenaCES[i] + "'";
                        }

                        if (i == cadenaCES.Length - 1)
                        {
                            WhatNivel += " ) ";
                        }
                    }

                    WhatNivel += " OR (ENCUESTA.Pregunta = 'P03'";

                    for (int i = 1; i < cadenaFCR.Length; i++)
                    {
                        if (i == 1)
                        {
                            WhatNivel += " OR ENCUESTA.Respuesta = '" + cadenaFCR[i] + "'";
                        }
                        else
                        {
                            WhatNivel += " OR ENCUESTA.Respuesta = '" + cadenaFCR[i] + "'";
                        }

                        if (i == cadenaFCR.Length - 1)
                        {
                            WhatNivel += " ) ";
                        }
                    }

                    WhatNivel += " OR (ENCUESTA.Pregunta = 'P04'";

                    for (int i = 1; i < cadenaNPS.Length; i++)
                    {
                        if (i == 1)
                        {
                            WhatNivel += " OR ENCUESTA.Respuesta = '" + cadenaNPS[i] + "'";
                        }
                        else
                        {
                            WhatNivel += " OR ENCUESTA.Respuesta = '" + cadenaNPS[i] + "'";
                        }

                        if (i == cadenaNPS.Length - 1)
                        {
                            WhatNivel += " )) ";
                        }
                    }
                }
                else
                {
                    WhatNivel += " ";
                }



            }
            else if (data.FILTRO_NIVEL == "2")
            {

                WhatNivel += " AND HOLD_TIME > '" + HOLD_INICIAL + "'" +
                             " AND HOLD_TIME < '" + HOLD_FINAL + "'" +
                             " AND CLIENTE.TALK_TIME+CLIENTE.ACW_TIME+CLIENTE.QUEUE_TIME+CLIENTE.CONSULT_TIME > '" + AHI_INICIAL + "'" +
                             " AND CLIENTE.TALK_TIME+CLIENTE.ACW_TIME+CLIENTE.QUEUE_TIME+CLIENTE.CONSULT_TIME < '" + AHI_FINAL + "'" +
                             " AND CLIENTE.TRANSFERIDA = '" + TRASFERIDA + "'";


                WhatAgente = 1;
                generarRandom = 1;
            }
            else if (data.FILTRO_NIVEL == "3")
            {


                if (data.TIPO_LLAMADAS == 1)
                {
                    WhatFechaDuracion += " AND CLIENTE.TALK_TIME+CLIENTE.ACW_TIME+CLIENTE.QUEUE_TIME+CLIENTE.CONSULT_TIME > '" + INICIO_LLAMADA_CORTA + "'  " +
                                            " AND CLIENTE.TALK_TIME+CLIENTE.ACW_TIME+CLIENTE.QUEUE_TIME+CLIENTE.CONSULT_TIME < '" + FIN_LLAMADA_CORTA + "'  ";
                }
                else if (data.TIPO_LLAMADAS == 2)
                {
                    WhatFechaDuracion += " AND CLIENTE.TALK_TIME+CLIENTE.ACW_TIME+CLIENTE.QUEUE_TIME+CLIENTE.CONSULT_TIME > '" + INICIO_LLAMADA_LARGA + "'  " +
                                         " AND CLIENTE.TALK_TIME+CLIENTE.ACW_TIME+CLIENTE.QUEUE_TIME+CLIENTE.CONSULT_TIME < '" + FIN_LLAMADA_LARGA + "'  ";

                }
                WhatNivel += WhatFechaDuracion;
                generarRandom = 1;
            }
            else if (data.FILTRO_NIVEL == "4")
            {
                WhatNivel += " ";
            }

            if (data.FILTRO_FECHA == "1")
            {
                WhatFecha += " AND FECHA_FINAL > '" + FECHA_INICIAL + "'" +
                             " AND FECHA_FINAL < '" + FECHA_FINAL + "'";
            }
            else if (data.FILTRO_FECHA == "2")
            {
                WhatFecha += " AND FECHA_FINAL > '" + fechaInicial + "'" +
                              " AND FECHA_FINAL < '" + fechaFinal + "'";
            }




            int cont = 1;

            List<EVALUAR_AGENTES> oListAgentesEvaluar = new List<EVALUAR_AGENTES>();

            oListAgentesEvaluar = AppConfigureRandom.loadAgentesEvaluar(Convert.ToInt32(AGENTE_EVALUAR), CCMS);

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            if (oListAgentesEvaluar.Count != 0 && WhatAgente == 1)
            {
                foreach (var item in oListAgentesEvaluar)
                {
                    if (WhatAgente == 0)
                    {
                        agenteEvaluar = " ";
                    }
                    else if (WhatAgente == 1)
                    {
                        agenteEvaluar = " AND AGENTE_FINAL = '" + item.LOGIN_AGENTE + "' ";
                    }

                    if (WhatNivel == "1")
                    {
                        consultaSQL += WhatNivel + WhatFecha + WhatFecha + "GROUP BY CLIENTE.UCID, ENCUESTA.ANI, CTE.NUMERO_CONTACTOS HAVING CTE.NUMERO_CONTACTOS > 1 "; ;
                    }
                    else
                    {
                        consultaSQL += "SELECT TOP " + item.NUMERO_MONITOREOS + " CAMPAÑA, EMPLEADOS.Nombre,  UCID, isnull(DURACION ,0) AS DURACION, CONCTACT_ID, isnull(AGENTE_FINAL ,0) AS AGENTE_FINAL, " +
                                                                   "isnull(TALK_TIME ,0) AS TALK_TIME, isnull(HOLD_TIME, 0) AS HOLD_TIME, ORIGEN_COLGADA, TRANSFERIDA, newid() AS RandomOrder " +
                                                                   "from CMS.TBL_ECH_" + custom[1].ToUpper() + " AS CLIENTE " +
                                                                   "INNER JOIN [HC].[TBL_FactEmpleados] AS EMPLEADOS ON CLIENTE.AGENTE_FINAL = EMPLEADOS.Login " +
                                                                   "WHERE EMPLEADOS.Estado = 'Active' AND  HOLD_TIME != 0 AND DURACION > 0 AND SKILL = '" + data.LOB + "'" +
                                                                    WhatFecha + WhatNivel + WhatSQl + agenteEvaluar +
                                                                   " ";

                        if (cont < oListAgentesEvaluar.Count)
                        {
                            consultaSQL += "  UNION  ";
                        }
                        else
                        {
                            consultaSQL += "ORDER BY RandomOrder";
                        }
                        cont++;
                    }
                }

            }
            if (data.FILTRO_NIVEL == "1")
            {
                consultaSQL += WhatNivel + WhatFecha + @"
                                                        GROUP BY 
                                                            CLIENTE.UCID,
                                                            CLIENTE.CAMPAÑA,
	                                                        CLIENTE.DURACION,
	                                                        CLIENTE.CONCTACT_ID,
	                                                        CLIENTE.AGENTE_FINAL,
	                                                        EMPLEADOS.Nombre,
	                                                        CLIENTE.TALK_TIME,
	                                                        CLIENTE.HOLD_TIME,
	                                                        CLIENTE.ORIGEN_COLGADA,
	                                                        CLIENTE.TRANSFERIDA,
                                                            ENCUESTA.ANI,
                                                            CTE.NUMERO_CONTACTOS 
                                                        HAVING 
                                                            CTE.NUMERO_CONTACTOS > 1
                                                        ";
            }
            else if (data.FILTRO_NIVEL == "3" || data.FILTRO_NIVEL == "4")
            {
                consultaSQL += "SELECT TOP " + NUMERO_MONITOREOS + " CAMPAÑA, EMPLEADOS.Nombre,  UCID, isnull(DURACION ,0) AS DURACION, CONCTACT_ID, isnull(AGENTE_FINAL ,0) AS AGENTE_FINAL, " +
                                                                   "isnull(TALK_TIME ,0) AS TALK_TIME, isnull(HOLD_TIME, 0) AS HOLD_TIME, ORIGEN_COLGADA, TRANSFERIDA " +
                                                                   "from CMS.TBL_ECH_" + custom[1].ToUpper() + " AS CLIENTE " +
                                                                   "INNER JOIN [HC].[TBL_FactEmpleados] AS EMPLEADOS ON CLIENTE.AGENTE_FINAL = EMPLEADOS.Login " +
                                                                   "WHERE EMPLEADOS.Estado = 'Active' AND HOLD_TIME != 0 AND DURACION > 0 AND SKILL = '" + data.LOB + "'" +
                                                                    WhatFecha + WhatNivel + WhatSQl + agenteEvaluar +
                                                                   " ORDER BY newid() ";
            }



            SqlCommand oQueryList = new SqlCommand(consultaSQL, Connection.oConnection);
            oQueryList.CommandTimeout = 120;
            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_ECH> oListCalls = new List<TBL_ECH>();

            while (oRead.Read())
            {
                oListCalls.Add(new TBL_ECH
                {
                    CAMPAÑA = Convert.ToString(oRead["CAMPAÑA"]),
                    UCID = Convert.ToString(oRead["UCID"]),
                    DURACION = Convert.ToInt32(oRead["DURACION"]),
                    CONCTACT_ID = Convert.ToInt32(oRead["CONCTACT_ID"]),
                    AGENTE_INICIAL = Convert.ToInt32(oRead["AGENTE_FINAL"]),
                    N_AGENTE_FINAL = Convert.ToString(oRead["Nombre"]),
                    TALK_TIME = Convert.ToInt32(oRead["TALK_TIME"]),
                    HOLD_TIME = Convert.ToInt32(oRead["HOLD_TIME"]),
                    ORIGEN_COLGADA = Convert.ToString(oRead["ORIGEN_COLGADA"]),
                    TRANSFERIDA = Convert.ToString(oRead["TRANSFERIDA"]),
                });
            }
            return oListCalls;

        }

    }
}