using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class AppResultsRandom
    {

        public static List<loadActGenerada> dataUserRamdomCallCustoms(string CCMS, string cliente, string lob, string fechaInicial, string fechaFinal)
        {
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            string[] custom = cliente.Split('-');

            SqlCommand oQueryList = new SqlCommand(@"   SELECT DISTINCT  U.ID_PDF, U.CRETE_DATETIME, C.CLIENTE, P.LOB, C.ID_LOB
                                                            FROM AppSA.PDF_AL AS P 
                                                            INNER JOIN AppSA.PDF_AL_USER AS U ON P.ID_PDF = U.ID_PDF 
                                                            INNER JOIN AppSA.MANAGER_CONFIGURATION AS C ON U.ID_CLIENTE = C.ID_CLIENTE
                                                            INNER JOIN AppSA.USERDATA AS UD ON U.CCMS_EVALUATOR = UD.CCMS
                                                        WHERE UD.USER_RED = @USER_RED AND U.CRETE_DATETIME BETWEEN @fechaInicial AND @fechaFinal AND P.CAMPAÑA =  @CAMPAÑA AND C.ID_LOB = @ID_LOB", Connection.oConnectionQaWeb);

            oQueryList.Parameters.AddWithValue("@USER_RED", CCMS);
            oQueryList.Parameters.AddWithValue("@fechaInicial", fechaInicial+ " 00:00:00");
            oQueryList.Parameters.AddWithValue("@fechaFinal", fechaFinal+ " 59:59:59");
            oQueryList.Parameters.AddWithValue("@CAMPAÑA", custom[1]);
            oQueryList.Parameters.AddWithValue("@ID_LOB", lob);

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

    }
}