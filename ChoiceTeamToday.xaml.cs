using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;


namespace RRODCA
{
    /// <summary>
    /// Класс для выбора спасателей из предложенного списка
    /// </summary>
    public partial class ChoiceTeamToday : Window
    {   /// <summary>
    /// Список спасателей для передачи родителю
    /// </summary>
        public string[,] Rangers { get; set; }



        /// <summary>
        /// Количество записей в массиве объектов для передачи родителю
        /// </summary>
        public int RangersCount = 0;
       
        private string[,] Ans;
        List<int> lst = new List<int>(); 

        IConnector Connector = new ConnectionManager();

        /// <summary>
        /// Коструктор класса
        /// </summary>
        public ChoiceTeamToday()
        {
            InitializeComponent();
        }


        /// <summary>
        ///  перегруженный коструктор
        /// </summary>
        public ChoiceTeamToday(string arg)
        {
            InitializeComponent();
            listbox.Items.Clear();
            listboxTeam.Items.Clear();
            ContentListbox(arg);

        }


        
          /// <summary>
          /// Наполняет листбокс из базы
          /// </summary>
          private void ContentListbox(string arg)
          {
            int countBase = Connector.CountInBase(arg);
            string [,] Answer =  Connector.ReadFromBase(arg,countBase);
            Ans = Answer;
           
            for (int i= 0; i<countBase; i++)
            {
                listbox.Items.Add(i+1 +"  -  "+ Answer[i,1].ToString() );
            }
          }



        /// <summary>
        /// событие оповещения о том, что список спасателей готов и можно забирать
        /// </summary>
        public event EventHandler ButtonClicked;

        /// <summary>
        /// Кнопка "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoneChangeShift_Click(object sender, RoutedEventArgs e)
        {
            if (listboxTeam.Items.Count > 0)                    // если в листбоксе что-то есть во избежание ошибки на обращение к нулл
            {
                int i = 0;
                string[,] rng = new string[lst.Count, 3];      // значит в списке индексов тоже что-то есть, создаем массив под данные
                foreach (var item in lst)                      //перебирает индексы из списка и по ним находим элементы входящего массива,
                                                               //переносим их в промежуточный
                {
                    rng[i, 0] = Ans[item, 0];
                    rng[i, 1] = Ans[item, 1];
                    rng[i, 2] = Ans[item, 2];
                    i++;
                }
                Rangers = rng;                                    // теперь из промежуточного  в исходящий
                RangersCount = lst.Count;                         // это количество строк в массиве, чтобы дальше не мучится считая их
                ButtonClicked?.Invoke(this, EventArgs.Empty);     // событие, сообщаем что данные готовы

                Close();                                           // форма отработала и закрывается
            }
            
            
        }



        private void Listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listbox.SelectedItem != null)
            { 
            listboxTeam.Items.Add(listbox.SelectedItem.ToString()       // обрезается начальная часть строки до символа " - ", 
                       .Substring(listbox.SelectedItem.ToString().IndexOf("-") + 1)  // и вставляется в лисбокс
                                       );
            lst.Add(listbox.SelectedIndex);     // индекс элемента в листбоксе такой же как и во входящем массиве,
                                                // здесь индекс из листбокса добавляется в список,
                                                // по нему потом будут вынуты элементы входящего массива
            }
        }
    }
}
