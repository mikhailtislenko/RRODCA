﻿namespace RRODCA
{
    /// <summary>
    /// Класс хранит данные о дежурной смене одного подразделения
    /// </summary>
    internal class Subdivision
    {
        /// <summary>
        /// Старший смены
        /// </summary>
        public string SeniorRanger { get; set; } 
        /// <summary>
        /// Дежурная автомашина
        /// </summary>
        public string DutyWenicle { get; set; } = "no duty wenicle";


        /// <summary>
        /// Смена, вместе со старшим. Старший на нулевой позиции
        /// </summary>
        public string[,] RangersTeam { get; set; } 
        
        /// <summary>
        /// Техника в распоряжении смены
        /// </summary>
        public string[,] Mashinery { get; set; }
        
        /// <summary>
         /// Специальная техника в распоряжении смены
         /// </summary>
        public string[,] SpecialMashinery { get; set; }
        
        /// <summary>
        /// Плавсредства в распоряжении смены
        /// </summary>
        public string[,] Boats { get; set; }

    }
}
