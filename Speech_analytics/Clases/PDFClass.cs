using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Speech_analytics.Clases
{
    public class PDFClass
    {  
        public static List<PDFALEATORIO> dataPDF(string idPDF)
        {

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

                SqlCommand oQueryList = new SqlCommand(@"   SELECT  POSITION_PDF,ID_PDF, AGENTE_INICIAL, TALK_TIME, HOLD_TIME, TIMES_HELD, DURACION, ORIGEN_COLGADA, TRANSFERIDA, CAMPAÑA, UCID
                                                            FROM AppSA.PDF_AL
                                                            WHERE ID_PDF = @ID_PDF ORDER BY POSITION_PDF ", Connection.oConnectionQaWeb);

                oQueryList.Parameters.AddWithValue("@ID_PDF", idPDF);
                SqlDataReader oRead = oQueryList.ExecuteReader();

                List<PDFALEATORIO> oListGenerarPDF = new List<PDFALEATORIO>();

                 while (oRead.Read())
                    {
                        oListGenerarPDF.Add(new PDFALEATORIO
                        {
                            POSITION_PDF = Convert.ToInt16(oRead["POSITION_PDF"]),
                            ID_PDF = Convert.ToString(oRead["ID_PDF"]),
                            AGENTE_INICIAL = Convert.ToInt32(oRead["AGENTE_INICIAL"]),
                            TALK_TIME = Convert.ToInt32(oRead["TALK_TIME"]),
                            HOLD_TIME = Convert.ToInt32(oRead["HOLD_TIME"]),
                            TIMES_HELD = Convert.ToInt32(oRead["TIMES_HELD"]),
                            DURACION = Convert.ToInt32(oRead["DURACION"]),
                            ORIGEN_COLGADA = Convert.ToString(oRead["ORIGEN_COLGADA"]),
                            TRANSFERIDA = Convert.ToString(oRead["TRANSFERIDA"]),
                            CAMPAÑA = Convert.ToString(oRead["CAMPAÑA"]),
                            UCID = Convert.ToString(oRead["UCID"])
   
                        });
                    }

                 oRead.Close();


            const string query = @"SELECT DISTINCT(Login), Nombre FROM HC.TBL_FactEmpleados  WHERE Login = @login";

            foreach (var item in oListGenerarPDF)
            {
                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand command = new SqlCommand(query, Connection.oConnection);
                command.Parameters.AddWithValue("@login", item.AGENTE_INICIAL);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    item.N_AGENTE_FINAL = reader.GetString(1);
                }

                Connection.oConnection.Close();
            }         
            

            SqlCommand oQueryList_Act = new SqlCommand(@"SELECT DISTINCT P.ID_PDF, P.CCMS_EVALUATOR, E.NOMBRES,E.APELLIDOS, P.CRETE_DATETIME, U.CLIENTE 
                                                            FROM AppSA.PDF_AL_USER AS P
                                                            INNER JOIN AppSA.MANAGER_CONFIGURATION AS U ON P.ID_CLIENTE = U.ID_CLIENTE
                                                            INNER JOIN AppSA.USERDATA AS E ON P.CCMS_EVALUATOR = E.CCMS
                                                            WHERE ID_PDF = @ID_PDF", Connection.oConnectionQaWeb);

            oQueryList_Act.Parameters.AddWithValue("@ID_PDF", idPDF);

            SqlDataReader oRead_Act = oQueryList_Act.ExecuteReader();


            while (oRead_Act.Read())
            {
                oListGenerarPDF.Add(new PDFALEATORIO
                {
                    ID_PDF2 = Convert.ToString(oRead_Act["ID_PDF"]),
                    CCMS_EVALUATOR = Convert.ToInt32(oRead_Act["CCMS_EVALUATOR"]),
                    Nombre = Convert.ToString(oRead_Act["NOMBRES"]) +" "+ Convert.ToString(oRead_Act["APELLIDOS"]),
                    Rol = "Empleado",
                    CRETE_DATETIME = Convert.ToString(oRead_Act["CRETE_DATETIME"]),
                    Nombre_Campaña = Convert.ToString(oRead_Act["CLIENTE"]),

                });
            }
            Connection.oConnectionQaWeb.Close();
            return oListGenerarPDF;
        }

        public static List<PDFALEATORIO> dataPDFSURV(string idPDF)
        {

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryList = new SqlCommand("SELECT  Campaña,POSITION_PDF, ID_Encuesta, Split, Fecha_Fin, Login, ANI, UCID, STRPregunta, Respuesta " +
                                              "FROM AppSA.PDF_AL_SURV WHERE ID_PDF = @ID_PDF", Connection.oConnection);
            
            oQueryList.Parameters.AddWithValue("@ID_PDF", idPDF);
            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<PDFALEATORIO> oListGenerarPDF = new List<PDFALEATORIO>();

            while (oRead.Read())
            {
                oListGenerarPDF.Add(new PDFALEATORIO
                {
                    POSITION_PDF = Convert.ToInt16(oRead["POSITION_PDF"]),
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

            oRead.Close();

            SqlCommand oQueryList_Act = new SqlCommand("SELECT distinct  P.ID_PDF, P.CCMS_EVALUATOR, E.Nombre, E.Rol, P.CRETE_DATETIME, U.Nombre_Campaña " +
                                                   "FROM AppSA.PDF_AL_USER_SURV AS P	inner join GENERAL.TBL_Campañas_TP AS U on P.ID_CLIENTE = U.ID_Cliente " +
                                                   "inner join HC.TBL_FactEmpleados as E on P.CCMS_EVALUATOR = E.idccms " +
                                                   "WHERE ID_PDF = @ID_PDF", Connection.oConnection);

            oQueryList_Act.Parameters.AddWithValue("@ID_PDF", idPDF);

            SqlDataReader oRead_Act = oQueryList_Act.ExecuteReader();

            while (oRead_Act.Read())
            {
                oListGenerarPDF.Add(new PDFALEATORIO
                {
                    ID_PDF2 = Convert.ToString(oRead_Act["ID_PDF"]),
                    CCMS_EVALUATOR = Convert.ToInt32(oRead_Act["CCMS_EVALUATOR"]),
                    Nombre = Convert.ToString(oRead_Act["Nombre"]),
                    Rol = Convert.ToString(oRead_Act["Rol"]),
                    CRETE_DATETIME = Convert.ToString(oRead_Act["CRETE_DATETIME"]),
                    Nombre_Campaña = Convert.ToString(oRead_Act["Nombre_Campaña"]),

                });
            }
            return oListGenerarPDF;
        }
    }
}