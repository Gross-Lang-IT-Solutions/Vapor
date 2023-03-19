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
using static Vapor.Start;

namespace Vapor
{
    /// <summary>
    /// Interaktionslogik für Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        private Game game;

        

        public Details()
        {
            InitializeComponent();

          
        }
        public string GameName { get; set; }
        public int AgeRating { get; set; }
        public string Publisher { get; set; }

       
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            GameName = gameNameTextBox.Text;
            AgeRating = int.Parse(ageRatingComboBox.Text);
            Publisher = publisherTextBox.Text;

            DialogResult = true;
        }

    }
}
