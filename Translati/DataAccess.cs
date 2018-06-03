using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Translati
{
    public class DataAccess
    {
        public string Fetch(string language)
        {
            SqlConnection connection = null;
            SqlDataAdapter dataAdapter = null;
            DataSet dataset = new DataSet();
            string server = ConfigurationManager.AppSettings["server"];
            string database = ConfigurationManager.AppSettings["database"];
            string spname = "Ref.Get_Language_Abbr";
            string username = ConfigurationManager.AppSettings["username"];
            string password = ConfigurationManager.AppSettings["password"];
            string response = string.Empty;
            try
            {
                connection = new SqlConnection("server=" + server + ";DataBase=" + database + ";User Id=" + username + ";Password=" + password + ";Integrated Security=SSPI; Trusted_Connection=False; Encrypt=True; TrustServerCertificate=True;");
                connection.Open();
                dataAdapter = new SqlDataAdapter(spname, connection);
                dataAdapter.SelectCommand.Parameters.Add(new SqlParameter("@Lang", language));                
                dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dataAdapter.Fill(dataset);
            }

            catch (SqlException sExp)
            {
                string errorMsg = sExp.Message.ToString();
                string myString = string.Empty;

                Console.Write(string.Format("Exception:{0} [Get_Response]", sExp.Message));
                throw new ApplicationException(myString);
            }
            catch (ApplicationException aExp)
            {
                Console.Write(string.Format("Excpetion:{0} [Get_Response]", aExp));
                throw aExp;
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Excpetion:{0} [Get_Response]", ex));
                throw ex;
            }
            finally
            {
                if (dataAdapter != null)
                    dataAdapter.Dispose();
                if (connection != null)
                    connection.Close();
            }
            foreach (DataRow dr in dataset.Tables[0].Rows)
            {
                response = dr["Lang_Abbr"].ToString();
            }
            return response;
        }
    }
}