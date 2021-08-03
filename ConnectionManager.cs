using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;


namespace RRODCA
{   
/// <summary>
/// Класс предоставляет статические методы для обращения к базе данных
/// </summary>
    class ConnectionManager  : IConnector
    {    /// <summary>
         ///  строка подключения хранится в файле конфигурации
         /// </summary>
         readonly string connectString = ConfigurationManager.AppSettings.Get("connectString"); 

        /// <summary>
        /// 
        ///  Метод осуществляет запрос к базе данных, и возвращает количество затронутых строк в виде INT32 
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <returns></returns>
        public int RequestToBase(string sqlExpression)              
        {
            int tmp = 0;
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            connection.Open();
            tmp =  command.ExecuteNonQuery();
            connection.Close();
            return tmp;
        }

        /// <summary>
        /// 
        ///  Метод осуществляет запрос к базе данных, и возвращает двумерный массив строк из базы данных
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <param name="count">задаёт размерность массива выделяемого под данные</param>
        /// <returns></returns>
        public string[,] ReadFromBase(string sqlExpression, int count)    
        {
            int i = 0;
            string[,] list = new string[count, 3]; 
          
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            { 
                list[i, 0] = dataReader[0].ToString();
                list[i, 1] = dataReader[1].ToString();
                list[i, 2] = dataReader[2].ToString();
                i++;
            }
            dataReader.Close();
            connection.Close();
            return list;
        }
        /// <summary>
        /// Метод осуществляет запрос к базе данных, и возвращает количество объектов соответствующих запросу
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <returns></returns>
          public int CountInBase(string sqlExpression)
        {
            int tmp = 0;
            SqlConnection connection = new SqlConnection(connectString);
            SqlCommand command = new SqlCommand(sqlExpression, connection);
            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read()) tmp++;

            }
            
            dataReader.Close();
            connection.Close();
            return tmp;

        }





    }
}
