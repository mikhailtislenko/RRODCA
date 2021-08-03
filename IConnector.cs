using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RRODCA
{
    interface IConnector
    {
        /// <summary>
        /// 
        ///  Метод осуществляет запрос к базе данных.
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <returns>Количество затронутых запросом строк в int 32 </returns>
        int RequestToBase(string sqlExpression);

        /// <summary>
        /// 
        ///  Перегрузка. Метод осуществляет запрос к базе данных.
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <param name="Parameters">список параметров для записи в базу</param>
        /// <returns>Количество затронутых запросом строк в int 32 </returns>
        int RequestToBase(string sqlExpression, List<SqlParameter> Parameters);


        /// <summary>
        /// 
        ///  Метод осуществляет запрос к базе данных.
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <param name="count">задаёт размерность массива выделяемого под данные</param>
        /// <returns>возвращает двумерный массив строк из базы данных</returns>
        public string[,] ReadFromBase(string sqlExpression, int count);

        /// <summary>
        /// Метод осуществляет запрос к базе данных
        /// </summary>
        /// <param name="sqlExpression">SQL строка запроса к базе</param>
        /// <returns>возвращает количество объектов соответствующих запросу</returns>
        int CountInBase(string sqlExpression);

    }
}
