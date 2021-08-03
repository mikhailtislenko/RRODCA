using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRODCA
{   /// <summary>
/// Вся смена отряда.
/// </summary>
   internal class DutyRangers
    {

        /// <summary>
        /// Конструктора с перегрузками.
        /// Не помню зачем перегрузки, но помню что нужны. 
        /// </summary>
        public DutyRangers()
        {



        }

        /// <summary>
        /// Конструктора с перегрузками.
        /// Не помню зачем перегрузки, но помню что нужны. 
        /// </summary>
        public DutyRangers(DutyRangers Arg)
        {



        }

        /// <summary>
        /// Конструктора с перегрузками.
        /// Не помню зачем перегрузки, но помню что нужны. 
        /// </summary>
        public DutyRangers(Subdivision Arg, string NameSubdivision)
        {



        }

        /// <summary>
        /// Номер смены. От первой до четвертой
        /// </summary>
        public string Shift { get; set; }
         
        /// <summary>
         /// Дежурный диспетчер.
         /// </summary>
        public string Manager { get; set; }
        
        /// <summary>
         /// Ответственный по отряду
         /// </summary>
        public string SeniorOfficer { get; set; }

        /// <summary>
        /// Хромзавод
        /// </summary>
        public Subdivision OSOCZ = new();
       
        /// <summary>
        /// ЦСС
        /// </summary>
        public Subdivision OSOCSS = new();
       
        /// <summary>
        /// Дельфин
        /// </summary>
        public Subdivision OSODLFN = new();
        
        /// <summary>
        /// Аксу
        /// </summary>
        public Subdivision OSOAKSU = new();
        
        /// <summary>
        /// Экибас
        /// </summary>
        public Subdivision OSOEKBZ = new();
       
        /// <summary>
        /// Резерв
        /// </summary>
        public Subdivision OSOReserve = new();



    }
}
