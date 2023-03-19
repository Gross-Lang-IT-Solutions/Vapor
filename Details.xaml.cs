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
using Vapor;
using static Vapor.Start;

namespace Vapor
{
    /// <summary>
    /// Interaktionslogik für Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        private Game game;

        

        public Details(string name, string publisher, DateTime installDate, TimeSpan playTime, DateTime lastPlayed, string category, uint ageRating, string path)
        {
            InitializeComponent();

            lbl.Content = name;
            lbl_Copy.Content = publisher;
            lbl_Copy1.Content = installDate;
            lbl_Copy2.Content = playTime;
            lbl_Copy3.Content = lastPlayed;
            lbl_Copy4.Content = category;
            lbl_Copy6.Content = ageRating;
            lbl_Copy7.Content = path;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}


