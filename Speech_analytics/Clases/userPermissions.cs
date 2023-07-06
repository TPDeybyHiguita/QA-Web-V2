using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class userPermissions
    {
        public static List<USERDATA> loadUser(int CCMS)
        {
            Connection.oConnection.Close();
            Connection.oConnection.Open();


            SqlCommand oQueryList = new SqlCommand("SELECT F.Nombre, F.idcuenta, U.EMAIL, U.EMAIL_ALTERNO, US.LAST_LOGIN   " +
            "FROM AppSA.USERDATA as U " +
            "INNER JOIN HC.TBL_FactEmpleados AS F ON U.CCMS = F.idccms " +
            "INNER JOIN AppSA.PERMISOS_USER AS P ON F.idccms = P.CCMS " +
            "INNER JOIN AppSA.USERS AS US ON F.idcuenta = US.ID_USER " +
            "WHERE  idccms = '" + CCMS + "'", Connection.oConnection);
            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<USERDATA> oListData = new List<USERDATA>();

            while (oRead.Read())
            {
                oListData.Add(new USERDATA
                {
                    NOMBRES = Convert.ToString(oRead["Nombre"]),
                    USER = Convert.ToString(oRead["idcuenta"]),
                    EMAIL = Convert.ToString(oRead["EMAIL"]),
                    EMAIL_ALTERNO = Convert.ToString(oRead["EMAIL_ALTERNO"]),
                    LAST_LOGIN = Convert.ToString(oRead["LAST_LOGIN"])
                });
            }

            return oListData;
        }

        //public static List<PERMISOS_USERcs> loadUserPermissions(int CCMS)
        //{
        //    Connection.oConnection.Close();
        //    Connection.oConnection.Open();

        //    SqlCommand oQueryList = new SqlCommand("SELECT * " +
        //                                            "FROM AppSA.PERMISOS_USER "+
        //                                            "WHERE	CCMS = '"+CCMS+"' ", Connection.oConnection);

        //    SqlDataReader oRead = oQueryList.ExecuteReader();

        //    List<PERMISOS_USERcs> oList = new List<PERMISOS_USERcs>();

        //    while (oRead.Read())
        //    {
        //        oList.Add(new PERMISOS_USERcs
        //        {
        //            ADMINISTRADOR = Convert.ToInt32(oRead["ADMINISTRADOR"]),
        //            CALCULADORAS = Convert.ToInt32(oRead["CALCULADORAS"]),
        //            ALEATORIEDAD = Convert.ToInt32(oRead["ALEATORIEDAD"]),
        //            INFORMES_SPEECH = Convert.ToInt32(oRead["INFORMES_SPEECH"]),
        //            BITACORA_SURA = Convert.ToInt32(oRead["BITACORA_SURA"]),
        //            FECHA_ACTUALIZADO = Convert.ToString(oRead["FECHA_ACTUALIZADO"])

        //        });
        //    }

        //    return oList;
        //}














    }
}