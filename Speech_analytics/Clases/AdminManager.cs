using Speech_analytics.Data;
using Speech_analytics.Models;
using Speech_analytics.Models.States;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Clases
{
    public class AdminManager
    {
        public static List<USERDATA> newAnalista(string USER_RED)
        {

            try
            {
                LoadDataUserByUserRedInUserData loadDataUserByUserRedInUserData = new LoadDataUserByUserRedInUserData(USER_RED);
                return loadDataUserByUserRedInUserData.Process();
            }
            catch (Exception ex)
            {
                throw new HttpException(500, "Internal Server Error", ex);
            }
        }


        public static List<MANAGER_ANALISTA> listAnalysts(string userRedManager)
        {

            LoadDataManagerAnalistasByUserRedManagerAndEstado loadDataManagerAnalistasByUserRedManagerAndEstado = new LoadDataManagerAnalistasByUserRedManagerAndEstado(userRedManager);
            return loadDataManagerAnalistasByUserRedManagerAndEstado.process();
        }

        public static List<MANAGER_PERMISOS> loadAnalys(string USER_RED_MANAGER, string CCMS_ANALISTA)
        {
            LoadCcmsInDataUserByUserRed loadCcmsInDataUserByUserRed = new LoadCcmsInDataUserByUserRed(USER_RED_MANAGER);
            int CCMS_MANAGER = loadCcmsInDataUserByUserRed.Process();
            LoadDataInManagerPermisosByCcmsManagerAndCcmsAnalista loadDataInManagerPermisosByCcmsManagerAndCcmsAnalista = new LoadDataInManagerPermisosByCcmsManagerAndCcmsAnalista(Convert.ToString(CCMS_MANAGER), CCMS_ANALISTA);

            return loadDataInManagerPermisosByCcmsManagerAndCcmsAnalista.process();
        }

        public static int updateLob(int ID_LOB)
        {

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            const string query = @"UPDATE [AppSA].[MANAGER_PERMISOS]" +
                                                        "SET [ESTADO] = '" + "INACTIVO" + "', [FECHA_ACTUALIZADO] = GETDATE() " +
                                                        "WHERE ID = @ID_LOB";
            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnectionQaWeb);
            oQueryUpdate.Parameters.AddWithValue("@ID_LOB", ID_LOB);

            if (oQueryUpdate.ExecuteNonQuery() != 0)
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

        public static int activeLob(int ID_LOB)
        {
            const string query = @" UPDATE [AppSA].[MANAGER_PERMISOS] 
                                        SET [ESTADO] = 'ACTIVO', [FECHA_ACTUALIZADO] = GETDATE()
                                        WHERE ID = @ID_LOB";

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnectionQaWeb);
            oQueryUpdate.Parameters.AddWithValue("@ID_LOB", ID_LOB);

            if (oQueryUpdate.ExecuteNonQuery() != 0)
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

        public static int updateStatus(int CCMS_ANALISTA, string USER_RED_MANAGER)
        {
            string UserRedAnalista;
            const string query = @" UPDATE AppSA.MANAGER_ANALISTAS
                                        SET ESTADO = 'INACTIVO', FECHA_ACTUALIZADO = GETDATE()
                                        WHERE USER_RED = @USER_RED AND USER_RED_MANAGER = @USER_RED_MANAGER";

            LoadUserRedByCcmsInUserData loadUserRedByCcmsInUserData = new LoadUserRedByCcmsInUserData(CCMS_ANALISTA);
            UserRedAnalista = loadUserRedByCcmsInUserData.Process();

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnectionQaWeb);

            oQueryUpdate.Parameters.AddWithValue("@USER_RED", UserRedAnalista);
            oQueryUpdate.Parameters.AddWithValue("@USER_RED_MANAGER", USER_RED_MANAGER);

            oQueryUpdate.ExecuteNonQuery();

            if (oQueryUpdate.ExecuteNonQuery() != 0)
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

        public static bool updateConfiguracionAnalistasAleatoriedad(MANAGER_CONFcs data)
        {
            string[] custom = data.CLIENTE.Split('-');
            const string query = @" UPDATE [AppSA].[FILTER_RANDOM_CALL]
                                        SET [NUMERO_MONITOREOS] = @NUM_MONITOREOS 
                                        WHERE ID_CLIENTE = @ID_CLIENTE AND LOB = @LOB AND MES_ASIGNADO = @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO";

            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand oQueryUpdate = new SqlCommand(query, Connection.oConnection);

            oQueryUpdate.Parameters.AddWithValue("@NUM_MONITOREOS", data.NUM_MONITOREOS);
            oQueryUpdate.Parameters.AddWithValue("@ID_CLIENTE", custom[0]);
            oQueryUpdate.Parameters.AddWithValue("@LOB", data.LOB);
            oQueryUpdate.Parameters.AddWithValue("@MES_ASIGNADO", data.MES_ASIGNADO);
            oQueryUpdate.Parameters.AddWithValue("@AÑO_ASIGNADO", data.AÑO_ASIGNADO);

            oQueryUpdate.ExecuteNonQuery();

            if (oQueryUpdate.ExecuteNonQuery() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static int numDays(int month, int year)
        {
            int numDays = System.DateTime.DaysInMonth(year, month);
            return numDays;
        }

        public static int analystsAssign(MANAGER_CONFIGURATION managerSetting)
        {
            int numAnalys = 0;
            const string query = @"    SELECT COUNT(USER_ANALISTA) AS NUM_ANALYS 
                                          FROM AppSA.MANAGER_PERMISOS AS MP
                                          INNER JOIN AppSA.MANAGER_ANALISTAS AS MA ON MP.USER_ANALISTA = MA.USER_RED
                                          WHERE LOB = @LOB AND MA.ESTADO = 'ACTIVO' AND CLIENTE = @CLIENTE";

            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();            

            SqlCommand command = new SqlCommand(query, Connection.oConnectionQaWeb);
            command.Parameters.AddWithValue("@LOB", managerSetting.IdLob);
            command.Parameters.AddWithValue("@CLIENTE", managerSetting.Cliente);
            SqlDataReader reader = command.ExecuteReader();


            if (reader.Read())
            {
                numAnalys = reader.GetInt32(0);
            }
            Connection.oConnectionQaWeb.Close();

            return numAnalys;
        }

        public static List<MANAGER_CONFcs> analystsAssign2(MANAGER_CONFcs data)
        {
            const string query = @" SELECT CUOTA_MENSUAL, DIAS_ACTIVIDAD, NUM_MONITOREOS
                                        FROM AppSA.MANAGER_CONF
                                        WHERE CLIENTE = @CLIENTE AND LOB = @LOB AND MES_ASIGNADO = @MES_ASIGNADO AND AÑO_ASIGNADO = @AÑO_ASIGNADO";
            
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryList = new SqlCommand(query, Connection.oConnectionQaWeb);
            oQueryList.Parameters.AddWithValue("@CLIENTE", data.CLIENTE);
            oQueryList.Parameters.AddWithValue("@LOB", data.LOB);
            oQueryList.Parameters.AddWithValue("@MES_ASIGNADO", data.MES_ASIGNADO);
            oQueryList.Parameters.AddWithValue("@AÑO_ASIGNADO", data.AÑO_ASIGNADO);

            SqlDataReader oRead = oQueryList.ExecuteReader();

            List<MANAGER_CONFcs> oListData = new List<MANAGER_CONFcs>();

            while (oRead.Read())
            {
                oListData.Add(new MANAGER_CONFcs
                {
                    CUOTA_MENSUAL = Convert.ToString(oRead["CUOTA_MENSUAL"]),
                    DIAS_ACTIVIDAD = Convert.ToString(oRead["DIAS_ACTIVIDAD"]),
                    NUM_MONITOREOS = Convert.ToString(oRead["NUM_MONITOREOS"])
                });
            }
            Connection.oConnectionQaWeb.Close();
            return oListData;
        }

    }
}
