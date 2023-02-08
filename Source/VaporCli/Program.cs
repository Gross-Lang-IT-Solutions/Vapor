using System.Text;
using Vapor;

const string configFilePath = "config.dat";

if (!Config.TryLoad(configFilePath, out var config))
    config = new Config();

Console.WriteLine(
    "Welcome to Vapor CLI!\n" +
    "Copyright (C) 2023 Groß & Lang IT Solutions and GSODev. All rights reserved.");

var running = true;
while (running)
{
    Console.Write("> ");
    var segs = ReadLine().Trim().Split(' ').Select(s => s.Trim()).ToArray();

    switch (segs[0])
    {
        case "quit":
        case "exit":
            running = false;
            break;
        case "add":
            {
                Console.Write("Game title: ");
                var name = ReadLine();
                Console.Write("Executable path: ");
                var path = ReadLine();

                var game = new Game(name, path);
                config.AddGame(game);
            }
            break;
        case "remove":
            {
                if (segs.Length != 2)
                {
                    Console.WriteLine("Usage: 'remove <guid>'");
                    break;
                }
                                
                if (!Guid.TryParse(segs[1], out var guid) || !config.Games.ContainsKey(guid))
                {
                    Console.WriteLine("There is no game with the specified GUID your list of games.");
                    break;
                }
                config.RemoveGame(guid);
            }
            break;
        case "list":
            {
                if (config.Games.Count == 0)
                {
                    Console.WriteLine("There are no games in your list.");
                    break;
                }

                foreach (var g in config.Games)
                    Console.WriteLine($"{g.Key}: '{g.Value.Name}' installed at '{g.Value.ExecutablePath}'");
            }
            break;
        default:
            Console.WriteLine($"Command '{segs[0]}' was not recognized.");
            break;
    }
}

config.Save(configFilePath);

static string ReadLine()
{
    var sb = new StringBuilder();

    var read = true;
    while (read)
    {
        var c = Console.ReadKey(true);

        switch (c.Key)
        {
            case ConsoleKey.Backspace:
                if (sb.Length == 0)
                    break;
                sb.Remove(sb.Length - 1, 1);
                Console.Write("\b \b");
                break;
            case ConsoleKey.Enter:
                Console.Write('\n');
                read = false;
                break;
            default:
                sb.Append(c.KeyChar);
                Console.Write(c.KeyChar);
                break;
        }
    }

    return sb.ToString();
}
