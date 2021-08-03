using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RRODCA
{
    /// <summary>
    /// Логика взаимодействия для NewShiftWindow.xaml
    /// </summary>
    public partial class NewShiftWindow : Window
    {
        internal DutyRangers dutyRangers = new();
        DutySubdivisionWindow subdivisionWindow = new();
        Subdivision subdivision = new();





        /// <summary>
        /// промежуточный массив данных из объекта выбора личного состава
        /// </summary>
        public string[,] Rangers { get; private set; }
       
       
        /// <summary>
        /// Объект класса отбора личного состава
        /// </summary>
        ChoiceTeamToday teamToday;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public NewShiftWindow()
        {
            InitializeComponent();
        }
          
        private void buttonSaveNewShift_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
        /// <summary>
        /// по двойному клику скрывается и активирует комбобокс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shift_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            shiftComboBox.Visibility = Visibility.Visible;
            shift.Visibility = Visibility.Hidden;

        }
            /// <summary>
            /// после выбора значения, копирует его в скрытый лейбл, активирует его и скрывает себя
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void ShiftComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dutyRangers.Shift = shiftComboBox.SelectedItem.ToString();
            shiftComboBox.Visibility = Visibility.Hidden;
            shift.Visibility = Visibility.Visible;
            shift.Content = dutyRangers.Shift;
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
            //RangersCount = teamToday.RangersCount;
        }
        
        

        /// <summary>
        /// создаёт объект класса   DutySubdivisionWindow, подписывается на событие кнопки внутри него
        /// </summary>
        private void DutySubdivion()
        {
            
            DutySubdivisionWindow subdivisionWindow = new();
            subdivisionWindow.SubdivisionDone += Subdivion_Done;
            subdivisionWindow.Owner = this;
            this.subdivisionWindow = subdivisionWindow;
            subdivisionWindow.ShowDialog();
        }

        private void Subdivion_Done (object sender, EventArgs e)
        {
            subdivision = subdivisionWindow.SubdivisionReturn();
        }


         private void Manager_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChoiceTeamToday("SELECT [Post] , [FullName] , [SubDivision] FROM Rangers");
            dutyRangers.Manager = Rangers[0, 0] + "  " + Rangers[0, 1] + "  " + Rangers[0, 2];
            manager.Content  = (Rangers[0,1].ToString());


        }


        private void SeniorOfficer_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChoiceTeamToday("SELECT [Post] , [FullName] , [SubDivision] FROM Rangers");
            dutyRangers.SeniorOfficer = Rangers[0,0] + "  " +  Rangers[0, 1] + "  " + Rangers[0, 2];
            seniorOfficer.Content  = (Rangers[0,1].ToString());
        }

        private void OSO_CZ_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DutySubdivion();
            dutyRangers.OSOCZ = subdivision;
            OSO_CZ.Content = "Done";
        }

        private void OSO_CSS_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DutySubdivion();
            dutyRangers.OSOCSS = subdivision;
            OSO_CSS.Content = "Done";

        }

        private void OSO_DLFN_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DutySubdivion();
            dutyRangers.OSODLFN = subdivision;
            OSO_DLFN.Content = "Done";
        }

        private void OSO_AKSU_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DutySubdivion();
            dutyRangers.OSOAKSU = subdivision;
            OSO_AKSU.Content = "Done";

        }

        private void OSO_EKBZ_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DutySubdivion();
            dutyRangers.OSOEKBZ = subdivision;
            OSO_EKBZ.Content = "Done";
        }

        private void OSO_Reserve_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DutySubdivion();
            dutyRangers.OSOReserve = subdivision;
            OSO_Reserve.Content = "Done";

        }


        public event EventHandler ShiftDone;
        private void ButtonSaveNewShift_Click_1(object sender, RoutedEventArgs e)
        {
            if (dutyRangers.Shift != null && dutyRangers.Manager != null && dutyRangers.SeniorOfficer != null && dutyRangers.OSOCZ !=null && dutyRangers.OSOCSS != null && dutyRangers.OSODLFN != null && dutyRangers.OSOAKSU != null && dutyRangers.OSOEKBZ != null && dutyRangers.OSOReserve != null)
            {
                ShiftDone?.Invoke(this, EventArgs.Empty);
                Close();
            }
            else
            {
                if (MessageBox.Show("not all data is filled in!", "attention!", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                     
                }
            }
          

        }
       
    }
}
