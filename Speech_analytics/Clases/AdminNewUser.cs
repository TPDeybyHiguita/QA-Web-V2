using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class AdminNewUser
    {
        public static List<TBL_FactEmpleados> loadDataUser(int CCMS)
        {
            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryList = new SqlCommand(@"   SELECT idcuenta, Nombre, email, idfiscal, idmanager
                                                            FROM [HC].[TBL_FactEmpleados]
                                                            WHERE  idccms = @idccms and Estado = 'Active'", Connection.oConnection);
            
            oQueryList.Parameters.AddWithValue("@idccms", CCMS);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<TBL_FactEmpleados> oListData = new List<TBL_FactEmpleados>();

            while (oRead.Read())
            {
                oListData.Add(new TBL_FactEmpleados
                {
                    idcuenta = Convert.ToString(oRead["idcuenta"]),
                    Nombre = Convert.ToString(oRead["Nombre"]),
                    email = Convert.ToString(oRead["email"]),
                    idfiscal = Convert.ToString(oRead["idfiscal"]),
                    idmanager = Convert.ToInt32(oRead["idmanager"])
                    
                });
            }
            Connection.oConnection.Close();
            return oListData;
        }

        public static int validateExisUser(int CCMS)
        {
            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQuerySelect = new SqlCommand("SELECT CCMS FROM AppSA.USERDATA  WHERE CCMS = @CCMS", Connection.oConnection);
            oQuerySelect.Parameters.AddWithValue("@CCMS", CCMS);
            SqlDataReader oRead = oQuerySelect.ExecuteReader();

            if (!oRead.Read())
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public static int saveDataUser(USERDATA data)
        {
            try
            {
                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand OQueyInsert = new SqlCommand("INSERT INTO [AppSA].[USERDATA]([CCMS],[CCMSMANAGER],[NOMBRES],[APELLIDOS],[EMAIL],[EMAIL_ALTERNO]) " +
                "VALUES (@CCMS, @CCMS_MANAGER, @NOMBRES, @APELLIDOS, @EMAIL, @EMAIL_ALTERNO)", Connection.oConnection);

                OQueyInsert.Parameters.AddWithValue("@CCMS", data.CCMS);
                OQueyInsert.Parameters.AddWithValue("@CCMS_MANAGER", data.CCMS_MANAGER);
                OQueyInsert.Parameters.AddWithValue("@NOMBRES", data.NOMBRES);
                OQueyInsert.Parameters.AddWithValue("@APELLIDOS", data.APELLIDOS);
                OQueyInsert.Parameters.AddWithValue("@EMAIL", data.EMAIL);
                OQueyInsert.Parameters.AddWithValue("@EMAIL_ALTERNO", data.EMAIL_ALTERNO);

                OQueyInsert.ExecuteNonQuery();

                return 1;
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        public static int saveDataUserSession(USERDATA data)
        {
            try
            {
                Connection.oConnection.Close();
                Connection.oConnection.Open();

                SqlCommand OQueyInsert = new SqlCommand("INSERT INTO [AppSA].[USERS] ([ID_USER],[PASSWORD_USER] ,[DOC_USER],[LAST_LOGIN]) " +
                "VALUES (@USER,GETDATE())", Connection.oConnection);

                OQueyInsert.Parameters.AddWithValue("@USER", data.USER);
                OQueyInsert.ExecuteNonQuery();

                return 1;
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }

        public static int mayusculasNum(string cadena)
        {
            try
            {
                char[] letras = cadena.ToArray();
                int numeroDeLetras = letras.Length;
                int cont = 0;

                foreach (char letra in letras)
                {
                    string converMayuscula = Convert.ToString(letra).ToUpper();

                    if (converMayuscula == Convert.ToString(letra))
                    {
                        cont++;
                    }
                }

                if (cont > 0 )
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

    }
}