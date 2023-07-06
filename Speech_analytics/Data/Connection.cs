using System.Data.SqlClient;
namespace Speech_analytics.Data
{
    public class Connection
    {
        public static SqlConnection oConnection = new SqlConnection("server=TPCCP-DB103.teleperformance.co\\SQL2016STD,5081; database= Calidad; integrated security = true");
        public static SqlConnection oConnectionQaWeb = new SqlConnection("server=TPCCP-DB103.teleperformance.co\\SQL2016STD,5081; database= QA_WEB; integrated security = true");
    }
}