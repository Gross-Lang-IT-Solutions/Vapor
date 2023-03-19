# Vapor
## The best choice for a perfect game experience
Experience gaming like never before with Vapor, the all-new game launcher! Say goodbye to cluttered game libraries and hello to an organized and sleek platform that makes finding and launching your favorite games a breeze. Whether you're a hardcore gamer or just looking for something to pass the time, Vapor has got you covered. With its smooth navigation and user-friendly interface, gaming has never been more accessible and enjoyable. So why wait? Get your hands on Vapor today and step into the future of gaming!

# Vapor Start.xaml Documentation
##This is the documentation for the Start.xaml file in the Vapor project. This file contains the logic for displaying and sorting the games in the Vapor game launcher.

# Dependencies
## This file uses the following dependencies from the System.Windows namespace:

* Window
* Canvas
* Point
* Expander
* Button
* Polygon
* SolidColorBrush
* Color
* Brushes
* ImageBrush
* BitmapImage
* Variables
# This file contains the following variables:

* x: an integer initialized to 1
* gameCount: an integer initialized to 0
* index: an integer initialized to 0
* config: a Config object
* guid: a Guid object initialized to Guid.Empty
* configFilePath: a string representing the file path for the config.json file
* isDragging: a boolean initialized to false
* startPoint: a Point object
# Methods
## This file contains the following methods:

# Start()
This is the constructor method for the Start class. It initializes the config variable by attempting to load the config.json file. If the file does not exist or cannot be loaded, a new Config object is created. It then calls the showAllGames method to display all of the games.

# enum SortOption
This is an enumeration representing the sort options available for the games. It contains the following values:

NameAscending
NameDescending
DateAddedAscending
DateAddedDescending

# showAllGames()
This method displays all of the games in the config object on the canvas object. It first clears the canvas object of all existing hexagons and buttons. It then orders the games based on the currentSortOption and calls the showGames method to display them.

# ascendingButton_Click(object sender, RoutedEventArgs e)
This method is called when the "Ascending" button is clicked. It sets the currentSortOption to SortOption.NameAscending and calls the showAllGames method to display the games.

# descendingButton_Click(object sender, RoutedEventArgs e)
This method is called when the "Descending" button is clicked. It sets the currentSortOption to SortOption.NameDescending and calls the showAllGames method to display the games.

# dateAddedAscending_Click(object sender, RoutedEventArgs e)
This method is called when the "Date Added Ascending" button is clicked. It sets the currentSortOption to SortOption.DateAddedAscending and calls the showAllGames method to display the games.

# dateAddedDescending_Click(object sender, RoutedEventArgs e)
This method is called when the "Date Added Descending" button is clicked. It sets the currentSortOption to SortOption.DateAddedDescending, creates an Expander object, and sets its header and content properties. It then calls the showAllGames method to display the games and sets the IsExpanded property of the Expander object to false.

# showGames(IEnumerable<KeyValuePair<Guid, Game>> games)
This method displays the games on the canvas object. It first clears the canvas object of all existing hexagons and buttons. It then creates a hexagon and button for each game and sets their properties. It also sets the OnClick property of the button to call the Button_Click method with the Content property of the button. It then adds the hexagon

# setGameIcon(Button button, string gamePath)
This method takes a Button and a file path to a game executable as arguments. It tries to load the icon associated with the game executable and set it as the background image of the Button. If the icon cannot be loaded or read, the Button will retain the default hexagon icon.

# public class GameInfo
This is a simple class that defines three properties of a game: AgeRating, Category, and Publisher. It is not currently used in the code, but could potentially be used for future features.

# CreateHexagon(Point[] points, string gamePath, bool selected = false, bool favorite = false)
This method takes an array of Points, a file path to a game executable, and two optional bool arguments: selected and favorite. It creates a Polygon object with the specified Points as vertices, and sets the stroke and stroke thickness of the Polygon based on the selected and favorite arguments. It then calls setGameIcon() to set the background image of the Button associated with the Polygon.

# Button_Click(string gameId)
This method takes a string gameId as an argument. It sets the guid field to the Guid parsed from the gameId, and then calls showAllGames() to update the display of games with the selected game as the chosen game.
