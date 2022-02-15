using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RRODCA
{
    /// <summary>
    /// Логика взаимодействия для DutySubdivisionWindow.xaml
    /// </summary>
    public partial class DutySubdivisionWindow : Window
    {

        /// <summary>
        /// Объект класса отбора личного состава, еще и техники
        /// </summary>
        ChoiceTeamToday teamToday;


        /// <summary>
        /// Дежурные спасатели и дежурная техника одного отделения
        /// </summary>
        Subdivision subdivision = new();


        /// <summary>
        /// промежуточный массив данных из объекта выбора личного состава. и техники
        /// </summary>
        public string[,] Rangers { get; private set; }

        /// <summary>
        /// Количество строк в массиве элементов
        /// </summary>
        public int RangersCount = 0;

        /// <summary>
        /// Конструктор
        /// </summary>
        public DutySubdivisionWindow()
        {
            InitializeComponent();
           
        }







        private void ChoiceTeamToday(string arg)
        {
            ChoiceTeamToday teamToday = new(arg);
            teamToday.ButtonClicked += TeamToDay_ButtonClicked;
            teamToday.Owner = this;
            this.teamToday = teamToday;
            teamToday.ShowDialog();

        }
        private void TeamToDay_ButtonClicked(object sender, EventArgs e)
        {
            Rangers = teamToday.Rangers;
            RangersCount = teamToday.RangersCount;
        }






        /// <summary>
        /// Выбирает старшего смены, заносит его в класс хранения и в лайбу
        /// </summary>
        private void Senior_Ranger_Choice(object sender, MouseButtonEventArgs e)
        {
            ChoiceTeamToday("SELECT [Post] , [FullName] , [SubDivision] FROM Rangers");
            subdivision.SeniorRanger = Rangers[0, 1].ToString();
            Senior_Ranger.Content = Rangers[0, 1].ToString();
        }



        /// <summary>
        /// Выбирает выбирает команду спасателей
        /// </summary>
        private void Rangers_Choice(object sender, MouseButtonEventArgs e)
        {
            ChoiceTeamToday("SELECT [Post] , [FullName] , [SubDivision] FROM Rangers");
            subdivision.RangersTeam = Rangers;

            ListBox a = (ListBox)sender;
            a.Items.Clear();

            for (int i = 0; i < RangersCount; i++)
            {
                a.Items.Add(Rangers[i, 1].ToString());
            }
        }

        /// <summary>
        /// дежурная машина
        /// </summary>
        private void Duty_Wenicle_Choice(object sender, MouseButtonEventArgs e)
        {
            ChoiceTeamToday("SELECT  [Name] , [Model] , [State_number] FROM Mashinery");
            Duty_Wenicle.Content = subdivision.DutyWenicle = " \" " + Rangers[0, 0].ToString() + " \" " + Rangers[0, 1].ToString() + "  " + Rangers[0, 2].ToString();

        }


        /// <summary>
        /// дежурная техника
        /// </summary>
        private void Wenicle_Choice(object sender, MouseButtonEventArgs e)
        {

            ChoiceTeamToday("SELECT  [Name] , [Model] , [State_number] FROM Mashinery");
            subdivision.Mashinery = Rangers;
            ListBox a = (ListBox)sender;
            a.Items.Clear();

            for (int i = 0; i < RangersCount; i++)
            {
                a.Items.Add(" \" " + Rangers[i, 0].ToString() + " \" " + Rangers[i, 1].ToString() + "  " + Rangers[i, 2].ToString());
            }


        }

        private void Special_Wenicle_Choice(object sender, MouseButtonEventArgs e)
        {

            ChoiceTeamToday("SELECT [Name] , [Model] , [State_number]  FROM SpacialMashinery");
            subdivision.SpecialMashinery = Rangers;

            ListBox a = (ListBox)sender;
            a.Items.Clear();

            for (int i = 0; i < RangersCount; i++)
            {
                a.Items.Add(" \" " + Rangers[i, 0].ToString() + " \" " + Rangers[i, 1].ToString() + "  " + Rangers[i, 2].ToString());
            }


        }

        private void Boats_Choice(object sender, MouseButtonEventArgs e)
        {

            ChoiceTeamToday("SELECT [Name] , [Model] , [State_number]   FROM Boats");
            subdivision.Boats = Rangers;

            ListBox a = (ListBox)sender;
            a.Items.Clear();

            for (int i = 0; i < RangersCount; i++)
            {
                a.Items.Add(" \" " + Rangers[i, 0].ToString() + " \" " + Rangers[i, 1].ToString() + "  " + Rangers[i, 2].ToString());
            }


        }

       
        
        public event EventHandler SubdivisionDone;

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ChkNlRng())
            {
                SubdivisionDone?.Invoke(this, EventArgs.Empty);

                Close();
            }
            
        }

        internal Subdivision SubdivisionReturn()
        {
            return this.subdivision;
        }

        /// <summary>
        /// проверка на налл
        /// </summary>
        private bool ChkNlRng()
        {
            if (subdivision.SeniorRanger == null | subdivision.RangersTeam == null)
            {

                MessageBox.Show("No people in shift!", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (subdivision.Boats == null)
            { 
            subdivision.Boats = new string[1, 1];
            subdivision.Boats[0, 0] = "no boats";
            }

            if (subdivision.Mashinery == null)
            {
                subdivision.Mashinery = new string[1, 1];
                subdivision.Mashinery[0, 0] = "no mashinery";
            }
            if (subdivision.SpecialMashinery == null)
            {
                subdivision.SpecialMashinery = new string[1, 1];
                subdivision.SpecialMashinery[0, 0] = "no special Mashinery";
            } 
            return true;
        }
       

    }
}
