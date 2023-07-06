using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class myActividadLlamadas
    {
        public static List<PDF_AL> actividadLlamadas(string IDPDF)
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();


            SqlCommand oQueryList = new SqlCommand(@"   SELECT ID, CUMPLIMIENTO,OBSERVACION, POSITION_PDF,ID_PDF, AGENTE_INICIAL, TALK_TIME, HOLD_TIME, TIMES_HELD, DURACION, ORIGEN_COLGADA, TRANSFERIDA, CAMPAÑA, UCID    
                                                            FROM AppSA.PDF_AL 
                                                            WHERE ID_PDF = @ID_PDF ORDER BY POSITION_PDF ", Connection.oConnectionQaWeb);
            
            oQueryList.Parameters.AddWithValue("@ID_PDF", IDPDF);
            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<PDF_AL> oListData = new List<PDF_AL>();

            while (oRead.Read())
            {
                oListData.Add(new PDF_AL
                {
                    POSITION_PDF = Convert.ToInt16(oRead["POSITION_PDF"]),
                    AGENTE_INICIAL = Convert.ToInt32(oRead["AGENTE_INICIAL"]),
                    TALK_TIME = Convert.ToInt16(oRead["TALK_TIME"]),
                    HOLD_TIME = Convert.ToInt16(oRead["HOLD_TIME"]),
                    TIMES_HELD = Convert.ToInt32(oRead["TIMES_HELD"]),
                    DURACION = Convert.ToInt32(oRead["DURACION"]),
                    ORIGEN_COLGADA = Convert.ToString(oRead["ORIGEN_COLGADA"]),
                    TRANSFERIDA = Convert.ToString(oRead["TRANSFERIDA"]),
                    UCID = Convert.ToString(oRead["UCID"]),
                    CUMPLIMIENTO = Convert.ToString(oRead["CUMPLIMIENTO"]),
                    OBSERVACION = Convert.ToString(oRead["OBSERVACION"]),
                    ID = Convert.ToInt16(oRead["ID"])

                });
            }

            Connection.oConnectionQaWeb.Close();

            const string query = @"SELECT DISTINCT(Login), Nombre FROM HC.TBL_FactEmpleados  WHERE Login = @login";

            foreach (var item in oListData)
            {
                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand command = new SqlCommand(query, Connection.oConnection);
                command.Parameters.AddWithValue("@login", item.AGENTE_INICIAL);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    item.NOMBRE_AGENTE = reader.GetString(1);
                }

                Connection.oConnection.Close();
            }
            return oListData;
        }


        public static bool actualizarEstadoMonitoreo(string REFERENCIA, int ID, string OBSERVACION, string ESTADO )
        {

            try
            {
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                SqlCommand oQueryUpdate = new SqlCommand(   "UPDATE AppSA.PDF_AL " +
                                                            "SET " +
                                                            " [CUMPLIMIENTO] = @CUMPLIMIENTO, " +
                                                            " [OBSERVACION] = @OBSERVACION, " +
                                                            " [FECHA_CUMPLIMIENTO] = GETDATE()" +
                                                            " WHERE ID_PDF = @ID_PDF AND ID = @ID", Connection.oConnectionQaWeb);

                oQueryUpdate.Parameters.AddWithValue("@CUMPLIMIENTO", ESTADO);
                oQueryUpdate.Parameters.AddWithValue("@OBSERVACION", OBSERVACION);
                oQueryUpdate.Parameters.AddWithValue("@ID_PDF", REFERENCIA);
                oQueryUpdate.Parameters.AddWithValue("@ID", ID);

                if (oQueryUpdate.ExecuteNonQuery() != 0)
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
                throw new HttpException(500, "Internal Server Error", ex);
            }

        }

        public static List<PDF_AL_USER> loadDataActividad(string IDPDF)
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();


            SqlCommand oQueryList = new SqlCommand(@"SELECT U.CRETE_DATETIME, U.LOB, U.ID_PDF, D.NOMBRES, D.APELLIDOS, C.CLIENTE 
                                                        FROM AppSA.PDF_AL_USER AS U
                                                        INNER JOIN AppSA.USERDATA AS D ON U.CCMS_EVALUATOR = D.CCMS
                                                        INNER JOIN AppSA.MANAGER_CONFIGURATION AS C ON U.ID_CLIENTE = C.ID_CLIENTE
                                                        WHERE U.ID_PDF = @ID_PDF", Connection.oConnectionQaWeb);

            oQueryList.Parameters.AddWithValue("@ID_PDF", IDPDF);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<PDF_AL_USER> oListData = new List<PDF_AL_USER>();

            while (oRead.Read())
            {
                oListData.Add(new PDF_AL_USER
                {
                     
                    CRETE_DATETIME = Convert.ToString(oRead["CRETE_DATETIME"]),
                    LOB = Convert.ToString(oRead["LOB"]),
                    USERDATA = new USERDATA
                    {
                        NOMBRES = Convert.ToString(oRead["NOMBRES"]),
                        APELLIDOS = Convert.ToString(oRead["APELLIDOS"]),
                    },
                    TBL_Info_Clientes = new TBL_Info_Clientes
                    {
                       Nombre_Cliente  = Convert.ToString(oRead["CLIENTE"]),
                    }

                });
            }
            Connection.oConnectionQaWeb.Close();
            return oListData;
        }

    }
}