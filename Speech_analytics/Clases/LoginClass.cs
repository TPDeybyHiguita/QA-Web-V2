using Speech_analytics.Data;
using Speech_analytics.Models;
using System.Data.SqlClient;


namespace Speech_analytics.Clases
{
    public class LoginClass
    {
        const string query = @" SELECT NOMBRES, APELLIDOS, UD.CCMS, UD.USER_RED, ESTADO,LAST_LOGIN, PERMISOS.createUser,PERMISOS.editPermissions,PERMISOS.AddCampaigns,PERMISOS.myTeams, PERMISOS.randomnessResults, PERMISOS.random, PERMISOS.planCalculator, PERMISOS.daysCalculator, PERMISOS.bitacoraSura 
                                    FROM AppSA.USERDATA AS UD
                                        INNER JOIN AppSA.PERMISOS_USERS AS PERMISOS ON UD.USER_RED = PERMISOS.userRed 
	                                    INNER JOIN AppSA.USER_LOGIN AS USERS ON UD.USER_RED = USERS.USER_RED
                                        WHERE UD.USER_RED = @USER AND USERS.ESTADO = 'active'";


        private readonly DirectoryActive directoryActive;

        public LoginClass(DirectoryActive directoryActive)
        {
            this.directoryActive = directoryActive;
        }

        public SqlDataReader Process()
        {
            
            Connection.oConnectionQaWeb.Close();
            Connection.oConnectionQaWeb.Open();

            SqlCommand oQueryValidate = new SqlCommand(query, Connection.oConnectionQaWeb);
            

            oQueryValidate.Parameters.AddWithValue("@USER", directoryActive.user);

            SqlDataReader oRead = oQueryValidate.ExecuteReader();

            if (oRead.Read())
            {
                return oRead;
            }
            else
            {
                return null;
            }
        }
    }
}