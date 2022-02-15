using System;
using System.Collections.Generic;
using System.Windows;
using System.Data.SqlClient;


namespace RRODCA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IConnector Connector = new ConnectionManager();

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
         private void NewShiftToday(object sender, EventArgs e)
        {
            //   обработчик события
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
        /// <summary>
        ///  Метод сохраняет класс DutyRangers, в котором записана вся дежурная смена отряда, в базу данных
        /// </summary>
        /// <param name="dutyRangers">объект класса DutyRangers</param>

        private void SaveDutyRangersInBase(DutyRangers dutyRangers)
        {
            string StrRequest = "INSERT INTO DutyRangers (Date, Shift, Manager, SeniorOfficer, " +
                "OSOCZ_SeniorRanger, OSOCZ_DutyWenicle, OSOCZ_RangersTeam, OSOCZ_Mashinery, OSOCZ_SpecialMashinery, " +
                " OSOCZ_Boats, OSOCSS_SeniorRanger, OSOCSS_DutyWenicle, OSOCSS_RangersTeam, OSOCSS_Mashinery, OSOCSS_SpecialMashinery, " +
                " OSOCSS_Boats, OSODLFN_SeniorRanger, OSODLFN_DutyWenicle, OSODLFN_RangersTeam, OSODLFN_Mashinery, OSODLFN_SpecialMashinery, " +
                " OSODLFN_Boats, OSOAKSU_SeniorRanger, OSOAKSU_DutyWenicle, OSOAKSU_RangersTeam, OSOAKSU_Mashinery, OSOAKSU_SpecialMashinery, " +
                " OSOAKSU_Boats, OSOEKBZ_SeniorRanger, OSOEKBZ_DutyWenicle, OSOEKBZ_RangersTeam, OSOEKBZ_Mashinery, OSOEKBZ_SpecialMashinery, " +
                " OSOEKBZ_Boats, OSOReserve_SeniorRanger, OSOReserve_DutyWenicle, OSOReserve_RangersTeam, OSOReserve_Mashinery, OSOReserve_SpecialMashinery, " +
                " OSOReserve_Boats ) VALUES (@Date, @Shift, @Manager, @SeniorOfficer, " +
                "@OSOCZ_SeniorRanger, @OSOCZ_DutyWenicle, @OSOCZ_RangersTeam, @OSOCZ_Mashinery, @OSOCZ_SpecialMashinery, " +
                " @OSOCZ_Boats, @OSOCSS_SeniorRanger, @OSOCSS_DutyWenicle, @OSOCSS_RangersTeam, @OSOCSS_Mashinery, @OSOCSS_SpecialMashinery, " +
                " @OSOCSS_Boats, @OSODLFN_SeniorRanger, @OSODLFN_DutyWenicle, @OSODLFN_RangersTeam, @OSODLFN_Mashinery, @OSODLFN_SpecialMashinery, " +
                " @OSODLFN_Boats, @OSOAKSU_SeniorRanger, @OSOAKSU_DutyWenicle, @OSOAKSU_RangersTeam, @OSOAKSU_Mashinery, @OSOAKSU_SpecialMashinery, " +
                " @OSOAKSU_Boats, @OSOEKBZ_SeniorRanger, @OSOEKBZ_DutyWenicle, @OSOEKBZ_RangersTeam, @OSOEKBZ_Mashinery, @OSOEKBZ_SpecialMashinery, " +
                " @OSOEKBZ_Boats, @OSOReserve_SeniorRanger, @OSOReserve_DutyWenicle, @OSOReserve_RangersTeam, @OSOReserve_Mashinery, @OSOReserve_SpecialMashinery, " +
                " @OSOReserve_Boats  )";

            

            List<SqlParameter> Parameters = new List<SqlParameter>() ; // объявляем список параметров, потом его передадим коннектору
           
            SqlParameter Date = new SqlParameter("@Date", DateTime.Now.ToShortDateString());
            SqlParameter Shift = new SqlParameter("@Shift", dutyRangers.Shift);
            SqlParameter Manager = new SqlParameter("@Manager", dutyRangers.Manager);
            SqlParameter SeniorOfficer = new SqlParameter("@SeniorOfficer", dutyRangers.SeniorOfficer);
           
            SqlParameter OSOCZ_SeniorRanger = new SqlParameter("@OSOCZ_SeniorRanger", dutyRangers.OSOCZ.SeniorRanger);
            SqlParameter OSOCZ_DutyWenicle = new SqlParameter("@OSOCZ_DutyWenicle", dutyRangers.OSOCZ.DutyWenicle);
            SqlParameter OSOCZ_RangersTeam = new SqlParameter("@OSOCZ_RangersTeam", dutyRangers.OSOCZ.RangersTeam);
            SqlParameter OSOCZ_Mashinery = new SqlParameter("@OSOCZ_Mashinery", dutyRangers.OSOCZ.Mashinery);
            SqlParameter OSOCZ_SpecialMashinery = new SqlParameter("@OSOCZ_SpecialMashinery", dutyRangers.OSOCZ.SpecialMashinery);
            SqlParameter OSOCZ_Boats = new SqlParameter("@OSOCZ_Boats", dutyRangers.OSOCZ.Boats);

            SqlParameter OSOCSS_SeniorRanger = new SqlParameter("@OSOCSS_SeniorRanger", dutyRangers.OSOCSS.SeniorRanger);
            SqlParameter OSOCSS_DutyWenicle = new SqlParameter("@OSOCSS_DutyWenicle", dutyRangers.OSOCSS.DutyWenicle);
            SqlParameter OSOCSS_RangersTeam = new SqlParameter("@OSOCSS_RangersTeam", dutyRangers.OSOCSS.RangersTeam);
            SqlParameter OSOCSS_Mashinery = new SqlParameter("@OSOCSS_Mashinery", dutyRangers.OSOCSS.Mashinery);
            SqlParameter OSOCSS_SpecialMashinery = new SqlParameter("@OSOCSS_SpecialMashinery", dutyRangers.OSOCSS.SpecialMashinery);
            SqlParameter OSOCSS_Boats = new SqlParameter("@OSOCSS_Boats", dutyRangers.OSOCSS.Boats);

            SqlParameter OSODLFN_SeniorRanger = new SqlParameter("@OSODLFN_SeniorRanger", dutyRangers.OSODLFN.SeniorRanger);
            SqlParameter OSODLFN_DutyWenicle = new SqlParameter("@OSODLFN_DutyWenicle", dutyRangers.OSODLFN.DutyWenicle);
            SqlParameter OSODLFN_RangersTeam = new SqlParameter("@OSODLFN_RangersTeam", dutyRangers.OSODLFN.RangersTeam);
            SqlParameter OSODLFN_Mashinery = new SqlParameter("@OSODLFN_Mashinery", dutyRangers.OSODLFN.Mashinery);
            SqlParameter OSODLFN_SpecialMashinery = new SqlParameter("@OSODLFN_SpecialMashinery", dutyRangers.OSODLFN.SpecialMashinery);
            SqlParameter OSODLFN_Boats = new SqlParameter("@OSODLFN_Boats", dutyRangers.OSODLFN.Boats);

            SqlParameter OSOAKSU_SeniorRanger = new SqlParameter("@OSOAKSU_SeniorRanger", dutyRangers.OSOAKSU.SeniorRanger);
            SqlParameter OSOAKSU_DutyWenicle = new SqlParameter("@OSOAKSU_DutyWenicle", dutyRangers.OSOAKSU.DutyWenicle);
            SqlParameter OSOAKSU_RangersTeam = new SqlParameter("@OSOAKSU_RangersTeam", dutyRangers.OSOAKSU.RangersTeam);
            SqlParameter OSOAKSU_Mashinery = new SqlParameter("@OSOAKSU_Mashinery", dutyRangers.OSOAKSU.Mashinery);
            SqlParameter OSOAKSU_SpecialMashinery = new SqlParameter("@OSOAKSU_SpecialMashinery", dutyRangers.OSOAKSU.SpecialMashinery);
            SqlParameter OSOAKSU_Boats = new SqlParameter("@OSOAKSU_Boats", dutyRangers.OSOAKSU.Boats );

            SqlParameter OSOEKBZ_SeniorRanger = new SqlParameter("@OSOEKBZ_SeniorRanger", dutyRangers.OSOEKBZ.SeniorRanger);
            SqlParameter OSOEKBZ_DutyWenicle = new SqlParameter("@OSOEKBZ_DutyWenicle", dutyRangers.OSOEKBZ.DutyWenicle);
            SqlParameter OSOEKBZ_RangersTeam = new SqlParameter("@OSOEKBZ_RangersTeam", dutyRangers.OSOEKBZ.RangersTeam);
            SqlParameter OSOEKBZ_Mashinery = new SqlParameter("@OSOEKBZ_Mashinery", dutyRangers.OSOEKBZ.Mashinery);
            SqlParameter OSOEKBZ_SpecialMashinery = new SqlParameter("@OSOEKBZ_SpecialMashinery", dutyRangers.OSOEKBZ.SpecialMashinery);
            SqlParameter OSOEKBZ_Boats = new SqlParameter("@OSOEKBZ_Boats", dutyRangers.OSOEKBZ.Boats);

            SqlParameter OSOReserve_SeniorRanger = new SqlParameter("@OSOReserve_SeniorRanger", dutyRangers.OSOReserve.SeniorRanger);
            SqlParameter OSOReserve_DutyWenicle = new SqlParameter("@OSOReserve_DutyWenicle", dutyRangers.OSOReserve.DutyWenicle);
            SqlParameter OSOReserve_RangersTeam = new SqlParameter("@OSOReserve_RangersTeam", dutyRangers.OSOReserve.RangersTeam);
            SqlParameter OSOReserve_Mashinery = new SqlParameter("@OSOReserve_Mashinery", dutyRangers.OSOReserve.Mashinery);
            SqlParameter OSOReserve_SpecialMashinery = new SqlParameter("@OSOReserve_SpecialMashinery", dutyRangers.OSOReserve.SpecialMashinery);
            SqlParameter OSOReserve_Boats = new SqlParameter("@OSOReserve_Boats", dutyRangers.OSOReserve.Boats);

            Parameters.AddRange (new SqlParameter[]{ Date, Shift, Manager, SeniorOfficer, 
            OSOCZ_SeniorRanger, OSOCZ_DutyWenicle, OSOCZ_RangersTeam, OSOCZ_Mashinery, OSOCZ_SpecialMashinery, OSOCZ_Boats, 
            OSOCSS_SeniorRanger, OSOCSS_DutyWenicle, OSOCSS_RangersTeam, OSOCSS_Mashinery, OSOCSS_SpecialMashinery, OSOCSS_Boats,
            OSODLFN_SeniorRanger, OSODLFN_DutyWenicle, OSODLFN_RangersTeam, OSODLFN_Mashinery, OSODLFN_SpecialMashinery, OSODLFN_Boats,
            OSOAKSU_SeniorRanger, OSOAKSU_DutyWenicle, OSOAKSU_RangersTeam, OSOAKSU_Mashinery, OSOAKSU_SpecialMashinery, OSOAKSU_Boats,
            OSOEKBZ_SeniorRanger, OSOEKBZ_DutyWenicle, OSOEKBZ_RangersTeam, OSOEKBZ_Mashinery, OSOEKBZ_SpecialMashinery, OSOEKBZ_Boats,
            OSOReserve_SeniorRanger, OSOReserve_DutyWenicle, OSOReserve_RangersTeam, OSOReserve_Mashinery, OSOReserve_SpecialMashinery, 
            OSOReserve_Boats});  // заполняем список параметров

            Connector.RequestToBase(StrRequest, Parameters);

        }


    }
}
