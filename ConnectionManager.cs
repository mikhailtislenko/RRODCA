using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;


namespace RRODCA
{
    /// <summary>
    /// Класс предоставляет статические методы для обращения к базе данных
    /// </summary>
    class ConnectionManager : IConnector
    {
        /// <summary>
        ///  строка подключения хранится в файле конфигурации
        /// </summary>
        readonly string connectString = ConfigurationManager.AppSettings.Get("connectString");

        /// <summary>
        ///  Метод осуществляет запрос к базе данных 
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <returns>количество затронутых строк тип int32</returns>
        public int RequestToBase(string sqlExpression)
        {
            int tmp = 0;
            SqlConnection connection = new(connectString);
            SqlCommand command = new(sqlExpression, connection);
            connection.Open();
            tmp = command.ExecuteNonQuery();
            connection.Close();
            return tmp;
        }

        /// <summary>
        /// 
        ///  Перегрузка. Метод осуществляет запрос к базе данных. Предназначен для записи в базу.
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <param name="Parameters">список параметров для записи в базу</param>
        /// <returns>Количество затронутых запросом строк в int 32 </returns>
        public int RequestToBase(string sqlExpression, List<SqlParameter> Parameters)
        {
            int tmp = 0;
            SqlConnection connection = new(connectString);
            SqlCommand command = new(sqlExpression, connection);
            
            foreach (SqlParameter Param in Parameters)
            {
                command.Parameters.Add(Param);
            }

            connection.Open();
            tmp = command.ExecuteNonQuery();
            connection.Close();

           


            return tmp;
        }


        /// <summary>
        /// 
        ///  Метод осуществляет запрос к базе данных
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <param name="count">задаёт размерность массива выделяемого под данные</param>
        /// <returns>двумерный массив строк из базы данных</returns>
        public string[,] ReadFromBase(string sqlExpression, int count)
        {
            int i = 0;
            string[,] list = new string[count, 3];

            SqlConnection connection = new(connectString);
            SqlCommand command = new(sqlExpression, connection);
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
            SqlConnection connection = new(connectString);
            SqlCommand command = new(sqlExpression, connection);
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
