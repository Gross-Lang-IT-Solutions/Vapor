using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Path = System.IO.Path;
namespace Vapor
{
    /// <summary>
    /// Interaktionslogik für Start.xaml
    /// </summary>
    public partial class Start : Window
    {
        int x = 1;
        int gameCount = 0;
        int index = 0;
        private Config config;
        Guid guid = Guid.Empty;
        private const string configFilePath = "config.json";

        public Start()
        {
            InitializeComponent();

            if (!Config.TryLoad(configFilePath, out this.config))
                this.config = new Config();

            showAllGames();
        }
        private void showAllGames()
        {
            foreach (var g in config.Games.OrderBy(g => g.Value.Name)) showGames(Convert.ToString(g.Key));

        }
        private void showGames(string key)
        {
            var hexagonPoints = new Point[] {
 new Point(50, 0), new Point(100, 25), new Point(100, 75),
 new Point(50, 100), new Point(0, 75), new Point(0, 25)
 };
            var hexagon = CreateHexagon(hexagonPoints);                 // Compute the position of the new hexagon
            int hexPerRow = Math.Min(config.Games.Count, 5);
            int row = (config.Games.Count - 1) / hexPerRow;
            int col = (config.Games.Count - 1) % hexPerRow;
            double x = col * 100 + (row % 2 == 0 ? 50 : 0);
            double y = row * 87;                 // Set the position of the new hexagon
            Canvas.SetLeft(hexagon, x);
            Canvas.SetTop(hexagon, y);                 // Add the hexagon shape to the UI

            // Create a new button
            var button = new Button();
            button.Content = key;
            button.Width = 110;
            button.Height = 100;


            Canvas.SetLeft(button, x);
            Canvas.SetTop(button, y);

            // Add the button click event handler
            button.Click += (sender, e) => Button_Click(Convert.ToString(button.Content));

            button.Opacity = 0;

            canvas.Children.Add(hexagon);
            canvas.Children.Add(button);
            index++;

        }
        private Polygon CreateHexagon(Point[] points)
        {
            var hexagon = new Polygon();
            hexagon.Points = new PointCollection(points);
            hexagon.Fill = Brushes.Blue;
            hexagon.Stroke = Brushes.Black;
            hexagon.StrokeThickness = 2;
            return hexagon;
        }
        private void addButton_Click_1(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable Files|*.exe";
            openFileDialog.Title = "Select Game Executable";
            if (openFileDialog.ShowDialog() == true)
            {
                var game = new Game(openFileDialog.FileName, Path.GetFullPath(openFileDialog.FileName), DateTime.Now);
                config.AddGame(game);                 // Create a hexagon shape for the new game

                showAllGames();

                config.Save(configFilePath);
            }
        }
        // Define the button click event handler
        private void Button_Click(string index)
        {
            MessageBox.Show(Convert.ToString(index));

            guid = Guid.Parse(index);
        }
        private void gameList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void startButton_Click_1(object sender, RoutedEventArgs e)
        {

            Process.Start(new ProcessStartInfo(config.Games[guid].ExecutablePath));
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            config.RemoveGame(guid);
            config.Save(configFilePath);

            Start start = new Start();

            this.Close();

            start.Show();

        }
    }
}