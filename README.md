# Vapor
## The best choice for a perfect game experience
Experience gaming like never before with Vapor, the all-new game launcher! Say goodbye to cluttered game libraries and hello to an organized and sleek platform that makes finding and launching your favorite games a breeze. Whether you're a hardcore gamer or just looking for something to pass the time, Vapor has got you covered. With its smooth navigation and user-friendly interface, gaming has never been more accessible and enjoyable. So why wait? Get your hands on Vapor today and step into the future of gaming!

Vapor Documentation
Vapor is a gaming platform that lets users manage and launch their games easily. It features a custom-designed UI with a hexagonal layout for a unique and immersive experience.

Namespace
The Vapor namespace contains all the classes and components required to run Vapor.

Class: Start
The Start class is the main window of the Vapor application. It contains the logic for displaying the games and sorting them based on user preference.

Properties
x: An integer that is set to 1.
gameCount: An integer that is set to 0.
index: An integer that is set to 0.
config: An instance of the Config class that stores the configuration data for Vapor.
guid: An instance of the Guid class that represents a unique identifier.
configFilePath: A constant string that represents the path to the configuration file.
isDragging: A boolean value that indicates whether an element is being dragged or not.
startPoint: An instance of the Point class that represents the starting point of an element being dragged.
Methods
showAllGames(): Clears the canvas and displays all the games based on the current sort option.
ascendingButton_Click(): Event handler for the "Name Ascending" button click event. Sets the current sort option to name ascending and displays the games accordingly.
descendingButton_Click(): Event handler for the "Name Descending" button click event. Sets the current sort option to name descending and displays the games accordingly.
dateAddedAscending_Click(): Event handler for the "Date Added Ascending" button click event. Sets the current sort option to date added ascending and displays the games accordingly.
dateAddedDescending_Click(): Event handler for the "Date Added Descending" button click event. Sets the current sort option to date added descending and displays the games accordingly.
showGames(): Displays the games on the canvas.
setGameIcon(): Sets the game icon for a button based on the game executable path.
CreateHexagon(): Creates a hexagon polygon with a stroke color based on whether the game is selected or marked as a favorite.
Enumerations
SortOption: Represents the sort options for the games. Includes NameAscending, NameDescending, DateAddedAscending, and DateAddedDescending.
Class: Config
The Config class is responsible for storing and loading the configuration data for Vapor.

Properties
Games: A dictionary that stores the game data, with the key being a unique identifier of the game and the value being an instance of the Game class.
Methods
TryLoad(): Attempts to load the configuration data from a file.
Class: Game
The Game class represents a game in Vapor and contains the game data.

Properties
Name: A string that represents the name of the game.
ExecutablePath: A string that represents the path to the game executable.
InstallDateTime: A DateTime object that represents the date and time when the game was installed.
IsFavorite: A boolean value that indicates whether the game is marked as a favorite.
