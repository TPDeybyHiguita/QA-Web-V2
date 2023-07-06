using Speech_analytics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Speech_analytics.Data
{
    public class LoadDataInSkillClient
    {
        const string query = @" SELECT Dispositionskillkey, Skill, Region, Nombre, Meta, Campaña, Codigo_Campaña, Tipo_Skill
                                  FROM  [CMS].TBL_Homologacion_Skill_09_Noviembre
                                  WHERE Skill = @Skill AND Campaña = @Campaña";

        private HomologacionSkil homologacionSkil;
        private string Skill;
        private string Campaña;

        public LoadDataInSkillClient(string Skill, string Campaña)
        {
            string[] cliente = Campaña.Split('-');
            this.Skill = Skill;
            this.Campaña = cliente[1];
            homologacionSkil = new HomologacionSkil();
            homologacionSkil.Skill = Skill;

            if (cliente[1] == "SURA")
            {
                homologacionSkil.Campaña = "sura";
            }
            else
            {
                homologacionSkil.Campaña = Campaña;
            }
            
        }

        public HomologacionSkil Process()
        {
            Connection.oConnection.Close();
            Connection.oConnection.Open();

            SqlCommand command = new SqlCommand(query, Connection.oConnection);
            command.Parameters.AddWithValue("@Skill", homologacionSkil.Skill);
            command.Parameters.AddWithValue("@Campaña", homologacionSkil.Campaña);

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                homologacionSkil.Dispositionskillkey = reader.GetDouble(0).ToString();
                homologacionSkil.Skill = reader.GetDouble(1).ToString();
                homologacionSkil.Region = reader.GetString(2);
                homologacionSkil.Nombre = reader.GetString(3);
                homologacionSkil.Meta = reader.GetDouble(4).ToString();
                homologacionSkil.Campaña = reader.GetString(5);
                homologacionSkil.Codigo_Campaña = reader.GetString(6);
                homologacionSkil.Tipo_Skill = reader.GetString(7);
            }
            Connection.oConnection.Close();
            return homologacionSkil;
        }


    }
}