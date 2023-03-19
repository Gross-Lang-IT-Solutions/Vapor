using System.Windows;
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
using System.IO;

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
        private bool isDragging = false;
        private Point startPoint;

        public Start()
        {
            InitializeComponent();

            if (!Config.TryLoad(configFilePath, out this.config))
                this.config = new Config();

            showAllGames();
        }
        public enum SortOption
        {
            NameAscending,
            NameDescending,
            DateAddedAscending,
            DateAddedDescending
        }

        private SortOption currentSortOption = SortOption.NameAscending;

        private void showAllGames()
        {
            canvas.Children.Clear();

            IEnumerable<KeyValuePair<Guid, Game>> games = config.Games;

            switch (currentSortOption)
            {
                case SortOption.NameAscending:
                    games = games.OrderBy(g => g.Value.Name);
                    break;
                case SortOption.NameDescending:
                    games = games.OrderByDescending(g => g.Value.Name);
                    break;
                case SortOption.DateAddedAscending:
                    games = games.OrderBy(g => g.Value.InstallDateTime);
                    break;
                case SortOption.DateAddedDescending:
                    games = games.OrderByDescending(g => g.Value.InstallDateTime);
                    break;
            }

            showGames(games);
        }


        private void ascendingButton_Click(object sender, RoutedEventArgs e)
        {
            currentSortOption = SortOption.NameAscending;
            showAllGames();
            var button = sender as Button;
        }

        private void descendingButton_Click(object sender, RoutedEventArgs e)
        {
            currentSortOption = SortOption.NameDescending;
            showAllGames();
            
        }
        private void dateAddedAscending_Click(object sender, RoutedEventArgs e)
        {
            currentSortOption = SortOption.DateAddedAscending;
            showAllGames();
            
        }

        private void dateAddedDescending_Click(object sender, RoutedEventArgs e)
        {
            // Definition der Variable "expander"
            Expander expander = new Expander();

            // Initialisierung der Variablen "expander"
            expander.Header = "Expander Header";
            expander.Content = "Expander Content";

            // Verwendung der Variablen "expander"
          

            currentSortOption = SortOption.DateAddedDescending;
            showAllGames();
            expander.IsExpanded = false;

            
        }

        private void showGames(IEnumerable<KeyValuePair<Guid, Game>> games)
        {
            canvas.Children.Clear(); // Remove all existing hexagons and buttons from the canvas

            var hexagonPoints = new Point[] {
        new Point(50, 0), new Point(100, 25), new Point(100, 75),
        new Point(50, 100), new Point(0, 75), new Point(0, 25)
    };
            int hexPerRow = Math.Min(games.Count(), 5);
            int index = 0;
            foreach (var game in games)
            {
                int row = index / hexPerRow;
                int col = index % hexPerRow;
                double x = col * 100 + (row % 2 == 0 ? 50 : 0);
                double y = row * 87;
                bool isChoosed = false;
                if(game.Key == guid)
                {
                    isChoosed = true;
                }
                var hexagon = CreateHexagon(hexagonPoints, game.Value.ExecutablePath, isChoosed); // Load the game icon here

                Canvas.SetLeft(hexagon, x);
                Canvas.SetTop(hexagon, y);
                canvas.Children.Add(hexagon);
                var button = new Button();
                button.Content = game.Key;
                button.Width = 110;
                button.Height = 100;
                Canvas.SetLeft(button, x);
                Canvas.SetTop(button, y);
                button.Click += (sender, e) => Button_Click(Convert.ToString(button.Content));
                button.Opacity = 0;
                canvas.Children.Add(button);

                index++;
            }
        }


        private void setGameIcon(Button button, string gamePath)
        {
            try
            {
                string iconPath = gamePath;
                iconPath = iconPath.Replace("\\", "/");
                iconPath = "file:///" + iconPath;
                iconPath = iconPath + ",0";
                ImageBrush iconBrush = new ImageBrush();
                iconBrush.ImageSource = new BitmapImage(new Uri(iconPath));
                button.Background = iconBrush;
            }
            catch
            {
                // Wenn das Icon nicht gefunden wurde oder nicht gelesen werden konnte, bleibt das Standard-Hexagon-Icon erhalten.
            }
        }

        public class GameInfo
{
    public string AgeRating { get; set; }
    public string Category { get; set; }
    public string Publisher { get; set; }
}



        private Polygon CreateHexagon(Point[] points, string gamePath, bool selected = false)
        {
            var hexagon = new Polygon();
            hexagon.Points = new PointCollection(points);

            if (selected)
            {
                hexagon.Stroke = new SolidColorBrush(Color.FromArgb(0xFF, 0x8D, 0x00, 0xF3));
            }
            else
            {
                hexagon.Stroke = Brushes.Black;
            }



            hexagon.StrokeThickness = 2;

            // Load the icon of the game executable file
            try
            {
                using (var icon = System.Drawing.Icon.ExtractAssociatedIcon(gamePath))
                {
                    if (icon != null)
                    {
                        var bmp = icon.ToBitmap();
                        var stream = new MemoryStream();
                        bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        stream.Seek(0, SeekOrigin.Begin);

                        var brush = new ImageBrush();
                        brush.ImageSource = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        hexagon.Fill = brush;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return hexagon;
        }

        private void addButton_Click_1(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable Files|*.exe";
            openFileDialog.Title = "Select Game Executable";
            if (openFileDialog.ShowDialog() == true)
            {
                var game = new Game(openFileDialog.SafeFileName, Path.GetFullPath(openFileDialog.FileName), DateTime.Now);
                config.AddGame(game);                 // Create a hexagon shape for the new game

                showAllGames();

                config.Save(configFilePath);
            }

        }
        // Define the button click event handler
        private void Button_Click(string index)
        {
            //MessageBox.Show(Convert.ToString(index));

            guid = Guid.Parse(index);
            showAllGames();
        }

        private void startButton_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {

                var psi = new ProcessStartInfo(config.Games[guid].ExecutablePath);
                psi.Verb = "runas"; // add the 'runas' verb to prompt for elevated privileges
                Process.Start(psi);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("This program must be run as an administrator to start the selected game.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("No game selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            config.RemoveGame(guid);
            config.Save(configFilePath);

            showAllGames();

        }
        private void ShowGameDetails(Game game)
        {
            string message = $"Name: {game.Name}\n" +
                             $"Publisher: {game.Publisher}\n" +
                             $"Install Date: {game.InstallDateTime}\n" +
                             $"PlayTime : {game.PlayTime}\n" +
                             $"LastPlayedDateTime : {game.LastPlayedDateTime}\n" +
                             $"Category : {game.Category}\n" +
                             $"AgeRating : {game.AgeRating}\n" +
                             $"Executable Path: {game.ExecutablePath}";
            MessageBox.Show(message, "Game Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void detailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string index = Convert.ToString(guid);
                guid = Guid.Parse(index);
                var game = config.Games[guid];
                ShowGameDetails(game);
            }
            catch { MessageBox.Show("No game selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error); };
        }
        


        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (searchTextBox == null || config == null)
            {
                // handle the null case
                return;
            }
            string searchText = searchTextBox.Text.ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                showAllGames();
            }
            else
            {
                canvas.Children.Clear();

                var hexagonPoints = new Point[] {
            new Point(50, 0), new Point(100, 25), new Point(100, 75),
            new Point(50, 100), new Point(0, 75), new Point(0, 25)
        };

                int hexPerRow = Math.Min(config.Games.Count, 5);
                int index = 0;

                foreach (var game in config.Games.OrderBy(g => g.Value.Name))
                {
                    if (game.Value.Name.ToLower().Contains(searchText))
                    {
                        int row = index / hexPerRow;
                        int col = index % hexPerRow;
                        double x = col * 100 + (row % 2 == 0 ? 50 : 0);
                        double y = row * 87;
                        var hexagon = CreateHexagon(hexagonPoints, game.Value.ExecutablePath); // Load the game icon here
                        Canvas.SetLeft(hexagon, x);
                        Canvas.SetTop(hexagon, y);
                        canvas.Children.Add(hexagon);
                        var button = new Button();
                        button.Content = game.Key;
                        button.Width = 110;
                        button.Height = 100;
                        Canvas.SetLeft(button, x);
                        Canvas.SetTop(button, y);
                        button.Click += (sender, e) => Button_Click(Convert.ToString(button.Content));
                        button.Opacity = 0;
                        canvas.Children.Add(button);

                        index++;
                    }
                }
            }
        }
        private void RemovePlaceholderText(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "Search...")
            {
                searchTextBox.Text = "";
            }
        }

        public void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            // Code, der ausgeführt werden soll, wenn der Expander geöffnet wird
        }
        public void Expander_Collapsed(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            startPoint = e.GetPosition(this);
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                double deltaX = currentPosition.X - startPoint.X;
                double deltaY = currentPosition.Y - startPoint.Y;

                this.Left += deltaX;
                this.Top += deltaY;
            }
        }
    }
}
        
 