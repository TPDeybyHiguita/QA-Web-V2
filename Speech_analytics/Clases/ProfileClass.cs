using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class ProfileClass
    {

        public static List<USERDATA> dataUser(string user)
        {

            int idccms =   ProfileClass.dataIDCCMS(user);

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryList = new SqlCommand("SELECT distinct F.idccms, U.DEPARTAMENTO,U.EMAIL_ALTERNO, U.CCMSMANAGER, U.NOMBRES, U.APELLIDOS, U.EMAIL, U.TEL, U.IDFOTO, F.Estado, F.Puesto, U.DIRECCION, U.CIUDAD " +
                                                        "FROM HC.TBL_FactEmpleados AS F " +
                                                        "inner join AppSA.USERDATA AS U on F.idccms = U.CCMS " +
                                                        "WHERE CCMS = @CCMS", Connection.oConnection);

            oQueryList.Parameters.AddWithValue("@CCMS", idccms);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<USERDATA> oListUser = new List<USERDATA>();

            while (oRead.Read())
            {
                oListUser.Add(new USERDATA
                {
                    CCMS = Convert.ToInt32(oRead["idccms"]),
                    CCMSMANAGER = Convert.ToInt32(oRead["CCMSMANAGER"]),
                    NOMBRES = Convert.ToString(oRead["NOMBRES"]),
                    APELLIDOS = Convert.ToString(oRead["APELLIDOS"]),
                    EMAIL = Convert.ToString(oRead["EMAIL"]),
                    EMAIL_ALTERNO = Convert.ToString(oRead["EMAIL_ALTERNO"]),
                    Estado = Convert.ToString(oRead["Estado"])

                });
            }

            Connection.oConnection.Close();
            return oListUser;
        }

        public static int dataIDCCMS(string userRed)
        {

            const string query = @"SELECT CCMS FROM AppSA.USERDATA WHERE USER_RED = @userRed";

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryList = new SqlCommand(query, Connection.oConnectionQaWeb);
            oQueryList.Parameters.AddWithValue("@userRed", userRed);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            if (oRead.Read())
            {
                int CCMS = Convert.ToInt32(oRead["CCMS"]);
                oRead.Close();
                Connection.oConnectionQaWeb.Close();
                return CCMS;
            }
            else
            {
                oRead.Close();
                Connection.oConnectionQaWeb.Close();
                return 0;
            }
            
        }

        public static void actualizarDataUser(USERDATA dataUser, string user)
        {
            try
            {
                int idccms = ProfileClass.dataIDCCMS(user);

                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand OQueyUpdate = new SqlCommand("UPDATE [AppSA].[USERDATA]" +
                                                        "SET "+
                                                        " [CCMSMANAGER] = @CCMSMANAGER, " +
                                                        " [NOMBRES] = @NOMBRES, " +
                                                        " [APELLIDOS] = @APELLIDOS, " +
                                                        " [EMAIL] = @EMAIL, " +
                                                        " [EMAIL_ALTERNO] = @EMAIL_ALTERNO" +
                                                        " WHERE CCMS = @CCMS", Connection.oConnection);

                OQueyUpdate.Parameters.AddWithValue("@CCMSMANAGER", dataUser.CCMSMANAGER);
                OQueyUpdate.Parameters.AddWithValue("@NOMBRES", dataUser.NOMBRES);
                OQueyUpdate.Parameters.AddWithValue("@APELLIDOS", dataUser.APELLIDOS);
                OQueyUpdate.Parameters.AddWithValue("@EMAIL", dataUser.EMAIL);
                OQueyUpdate.Parameters.AddWithValue("@EMAIL_ALTERNO", dataUser.EMAIL_ALTERNO);
                OQueyUpdate.Parameters.AddWithValue("@CCMS", idccms);
                
                OQueyUpdate.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        public static int actualizarDataUserPassword(SessionModel password, string user)
        {
            try
            {
                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand OQueyUpdate = new SqlCommand("UPDATE [AppSA].[USERS] " +
                                                            " SET " +
                                                            " [PASSWORD_USER] = @PASSWORD_USER " +
                                                            " WHERE ID_USER = @ID_USER ", Connection.oConnection);

                OQueyUpdate.Parameters.AddWithValue("@PASSWORD_USER", password.UserPassword);
                OQueyUpdate.Parameters.AddWithValue("@ID_USER", user);

                OQueyUpdate.ExecuteNonQuery();

                return 1;
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }



    }
}