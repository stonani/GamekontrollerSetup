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

namespace GamekontrollerSetup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            this.KeyUp += new KeyEventHandler(MainWindow_KeyUp);
            W_box.Background = Brushes.White;
            A_box.Background = Brushes.White;
            S_box.Background = Brushes.White;
            D_box.Background = Brushes.White;

        }

        void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {

            W_box.Background = Brushes.White;
            A_box.Background = Brushes.White;
            S_box.Background = Brushes.White;
            D_box.Background = Brushes.White;
        }
        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                W_box.Background= Brushes.Gray;
            }
            if (e.Key == Key.Down)
            {
                S_box.Background = Brushes.Gray;
            }
            if (e.Key == Key.Right)
            {
                D_box.Background = Brushes.Gray;
            }
            if (e.Key == Key.Left)
            {
                A_box.Background = Brushes.Gray;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
