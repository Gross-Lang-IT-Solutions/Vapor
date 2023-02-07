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
            emailInput.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(EmailInput), true);
            passwordInput.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(PasswordInput), true);
            firstIndicator.Visibility = Visibility.Hidden;
            thirdIndicator.Visibility = Visibility.Hidden;
        }
        private void EmailInput(object sender, MouseButtonEventArgs e)
        {

          firstIndicator.Visibility = Visibility.Visible;
            secondIndicator.Visibility = Visibility.Hidden;
            fourthIndicator.Visibility = Visibility.Visible;
            thirdIndicator.Visibility = Visibility.Hidden;

        }

        private void PasswordInput(object sender, MouseButtonEventArgs e)
        {
              firstIndicator.Visibility = Visibility.Hidden;
            secondIndicator.Visibility = Visibility.Visible;
            fourthIndicator.Visibility = Visibility.Hidden;
            thirdIndicator.Visibility = Visibility.Visible;
        }
        private void ExecuteEnterLevel()
        {
            MessageBox.Show("cddddd");
        }

    }
}
