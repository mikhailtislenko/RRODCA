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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RRODCA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IConnector Connector = new ConnectionManager ();

        DutyRangers dutyRangers = new();

        NewShiftWindow newShiftWindow = new();

        DutySubdivisionWindow subdivisionWindow = new();

        /// <summary>
        /// Коструктор класса
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitForm();
            

        }

        private void ButtonNewOperatin_Click(object sender, RoutedEventArgs e)
        {
            WorkedWindow workedWindow = new WorkedWindow();
            workedWindow.Owner = this;
            workedWindow.Show();
        }

        private void MenuItemNewShift_Click(object sender, RoutedEventArgs e)
        {
            NewShiftWindow newShiftWindow = new NewShiftWindow();
            newShiftWindow.Owner = this;
            this.newShiftWindow = newShiftWindow;
            newShiftWindow.ShiftDone += NewShiftToday;
            newShiftWindow.Show();
        }

         private void MenuItemNewShiftRangers_Click(object sender, RoutedEventArgs e)
        {

            ChoiceTeamToday choiceTeamToday = new ChoiceTeamToday();
            choiceTeamToday.Owner = this;
            choiceTeamToday.ShowDialog();
        }

        private void MenuItemDutySubdivision_Click(object sender, RoutedEventArgs e)
        {
            DutySubdivisionWindow subdivisionWindow = new();
            subdivisionWindow.Owner = this;
            this.subdivisionWindow = subdivisionWindow;
            subdivisionWindow.Show();
            
        }


        /// <summary>
        /// Проверяет  есль ли данные о сегодняшней смене в базе, ругается если нет.
        /// </summary>
        private void InitForm()  
        {

            if ((Connector.CountInBase ("SELECT FullName FROM RangersToday WHERE  Subdivision <> NULL")) == 0  )
            {
                if (MessageBox.Show("No people in shift!", "Warning!", MessageBoxButton.OK , MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    
                }
                
            }                                                                  
            
        }
          /// <summary>
          /// Метод заполняет смену собранными данными 
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
         private void NewShiftToday(object sender, EventArgs e)
        {
            //   обработчик события
            dutyRangers = newShiftWindow.dutyRangers;
        }





        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            

            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;

            }

        }

        
        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
