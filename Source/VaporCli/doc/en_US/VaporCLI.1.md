% VaporCLI(1) 
% Noah van Uytrecht 
% February 2023 

# NAME

vaporCLI - a CLI tool for managing your games

# SYNOPSIS

**vaporCLI**

# DESCRIPTION

vaporCLI starts an interactive shell, where you are able to manage your games including:
- adding games with '**add**'
- removing games with '**remove** _guid_'
- list all games with '**list**'
- get details about games with '**details** _guid_'
- starting games with '**start** _guid_'

# EXAMPLE

## Interactive Mode

To start an interactive mode and list all available games do:

    $ vaporCLI
    > list

To add a new game to your collection do:

    $ vaporCLI
    > add
    Game Title: <Game Title>
    Executable path: </path/to/executable/of/Game>

To start a game from your list do:

    $ vaporCLI
    > list
    c1ce0ca0-8a5d-4bf6-970f-b84ec0d1920b: FooGame 
    > start c1ce0ca0-8a5d-4bf6-970f-b84ec0d1920b

To get details about a game from your list do:

    $ vaporCLI
    > list
    c1ce0ca0-8a5d-4bf6-970f-b84ec0d1920b: FooGame 
    > details c1ce0ca0-8a5d-4bf6-970f-b84ec0d1920b
    Name: 'FooGame'
    Executable path: '/path/to/Executable'
    Install date and time: 11/02/2023 18:32:51
    Is favorite? No
    Play time: 00:00:00
    Last played date and time: 01/01/1970 00:00:00
    Category: Uncategorized
    Publisher: Unknown publisher
    Age rating: 0
