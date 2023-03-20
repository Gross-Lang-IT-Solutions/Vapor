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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;


namespace Vapor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             firstIndicator.Visibility = Visibility.Hidden;
            thirdIndicator.Visibility = Visibility.Hidden;
        }
        private void ExecuteEnterLevel()
        {
            MessageBox.Show("cddddd");
        }

        public void PwCh()
        {

            firstIndicator.Visibility = Visibility.Hidden;
            secondIndicator.Visibility = Visibility.Visible;
            fourthIndicator.Visibility = Visibility.Hidden;
            thirdIndicator.Visibility = Visibility.Visible;
        }
        public void EmCh()
        {

            firstIndicator.Visibility = Visibility.Visible;
            secondIndicator.Visibility = Visibility.Hidden;
            fourthIndicator.Visibility = Visibility.Visible;
            thirdIndicator.Visibility = Visibility.Hidden;
        }
        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PwCh();
        }

        private void emailInput_GotFocus(object sender, RoutedEventArgs e)
        {
            EmCh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            Start start = new Start();
            start.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Start start = new Start();
            start.Show();
            this.Close();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            Start start = new Start();
            start.Show();
            this.Close();
        }
    }
}
