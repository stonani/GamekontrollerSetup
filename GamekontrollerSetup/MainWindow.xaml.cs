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

namespace GamekontrollerSetup
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sensorB_Click(object sender, RoutedEventArgs e)
        {
            SensorWindow sensorW = new SensorWindow();
            sensorW.Show();
        }

        private void exitB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
