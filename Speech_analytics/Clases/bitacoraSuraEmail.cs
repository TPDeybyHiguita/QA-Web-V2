using Speech_analytics.Data;
using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class bitacoraSuraEmail
    {

        public static List<TBL_Bitacora_Actualizaciones_arlcs> loadListManagerBitacoraRechazadas()
        {
            string estado = "Vencido";
            List<TBL_Bitacora_Actualizaciones_arlcs> listBitacora = new List<TBL_Bitacora_Actualizaciones_arlcs>();
            string query = "SELECT *  FROM SURA.TBL_Bitacora_Actualizaciones_arl WHERE ESTADO = @ESTADO";

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnection);
            oQueryRead.Parameters.AddWithValue("@ESTADO", estado);
            using (SqlDataReader reader = oQueryRead.ExecuteReader())
            {
                while (reader.Read())
                {
                    listBitacora.Add(new TBL_Bitacora_Actualizaciones_arlcs
                    {
                        ID_CASO = Convert.ToString(reader["ID_CASO"]),
                        MOTIVO_1 = Convert.ToString(reader["MOTIVO_1"]),
                        COMENTARIO = Convert.ToString(reader["COMENTARIO"]),
                        ESTADO = Convert.ToString(reader["ESTADO"]),
                        NOMBRE_CLIENTE = Convert.ToString(reader["NOMBRE_CLIENTE"])

                    });
                }
            }

            Connection.oConnection.Close();
            return listBitacora;

        }


        public static int setEmailBitacora(Models.EMAILINF obclsCorreo)
        {
            try
            {

                Connection.oConnection.Close();
                Connection.oConnection.Open();


                using (SqlCommand command = new SqlCommand("AppSA.SendEmailFromSql", Connection.oConnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@recipients", SqlDbType.NVarChar, -1).Value = obclsCorreo.EMAILTO;
                    command.Parameters.Add("@subject", SqlDbType.NVarChar, -1).Value = obclsCorreo.ASUNTO;
                    command.Parameters.Add("@body", SqlDbType.NVarChar, -1).Value = obclsCorreo.MANSAJE;

                    command.ExecuteNonQuery();
                }

                return 1;

            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static string timeSendEmails()
        {
            string timeSend = "";
            string Query = "  SELECT fechaSendEmail FROM AppSA.datetimeSendEmailsWeb WHERE id = '1' ";

            SqlCommand comando = new SqlCommand(Query, Connection.oConnectionQaWeb);
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();
            SqlDataReader reader = comando.ExecuteReader();

            if (reader.Read())
            {
                timeSend = Convert.ToString(reader["fechaSendEmail"]);
            }

            Connection.oConnectionQaWeb.Close();
            return timeSend;

        }


        public static bool timeSendEmailsUpdate(String timeSendEmails)
        {

            try
            {
                Connection.oConnectionQaWeb.Close();
                Connection.oConnectionQaWeb.Open();

                string timeSend = "";
                string query = @" UPDATE [AppSA].[datetimeSendEmailsWeb] SET [fechaSendEmail] = @timeUpdate WHERE id = '1'";


                SqlCommand oQueryRead = new SqlCommand(query, Connection.oConnection);
                oQueryRead.Parameters.AddWithValue("@timeUpdate", timeSendEmails);
                using (SqlDataReader reader = oQueryRead.ExecuteReader())
                {
                    Connection.oConnectionQaWeb.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Connection.oConnectionQaWeb.Close();
                return false;
            }
        }
    }
}